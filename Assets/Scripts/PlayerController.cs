using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D playerRigidBody;
    public Collider2D footCollider;
    public float moveSpeed;
    public float jumpForce;
    bool island = true;
    GameController gameController;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {

        playerRigidBody.AddForce(Vector2.right * playerInput.move * moveSpeed, ForceMode2D.Force);
        if (playerInput.move == 0)
        {
            GetComponent<Animator>().SetBool("IsMove", false);
        }
        else if (playerInput.move < 0)
        {
            GetComponent<Animator>().SetBool("IsMove", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (playerInput.move > 0)
        {
            GetComponent<Animator>().SetBool("IsMove", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void Jump()
    {
        if (playerInput.jump && island)
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            island = false;
            GetComponent<Animator>().SetBool("OnAir", !island);

        }

        if (playerRigidBody.velocity.y > 0)
        {
            GetComponent<Animator>().SetBool("IsUp", true);
            footCollider.enabled = false;
        }
        else if (playerRigidBody.velocity.y < 0)
        {
            GetComponent<Animator>().SetBool("IsUp", false);
            footCollider.enabled = true;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        island = true;
        GetComponent<Animator>().SetBool("OnAir", !island);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        island = true;
        GetComponent<Animator>().SetTrigger("Land");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        island = false;
        GetComponent<Animator>().SetBool("OnAir", !island);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Gem")
        {
            gameController.GetScore();
            GameObject.Destroy(collision.gameObject);
        }
    }
}
