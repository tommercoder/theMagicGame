using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScript : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    protected Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (controller != null)
        //{
        //    //direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //    //direction = transform.TransformDirection(direction);
        //    //direction *= 2.5f;

        //    //controller.SimpleMove(direction);

        //    // Handle jump
        //    if (controller.isGrounded && Input.GetKey(KeyCode.Space))
        //    {
        //        GetComponent<movement>().enabled = false;
        //        moveDirection.y = 20f;
        //        Debug.Log("move" + moveDirection);
        //    }

        //    // Apply gravity
        //    moveDirection += Physics.gravity * Time.deltaTime;
        //    controller.Move(moveDirection * Time.deltaTime);
        //    if(moveDirection.y < 15)
        //        GetComponent<movement>().enabled = true;
        //    //transform.Translate(moveDirection);

        //}
    }
}
