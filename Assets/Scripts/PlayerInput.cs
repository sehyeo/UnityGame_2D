using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName;
    public string jumpName;
    public float move { get; private set; }
    public bool jump { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        move = Input.GetAxis(moveAxisName);
        jump = Input.GetButton(jumpName);
    }
}
