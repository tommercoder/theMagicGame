﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    public GameObject projectile;
    public Animator animator;
    public Transform playerTransform;
    public float timer = 5f;
    public GameObject hand;
    private bool one;
    public float speed = 12f;
    void Start()
    {
        animator = GetComponent<Animator>();
        one = false;
      
   
    }


    void Update()
    {
        if (one)
        {
            if (Input.GetKey(KeyCode.X) && animator.GetBool("isDrawedSword"))
            {
                
                fireball();
                one = false;
            }
        }
        

    }
    public void fireball()
    {
       
        projectile.transform.position = hand.transform.position;
        GameObject fireball = Instantiate(projectile, projectile.transform.position,Quaternion.identity) as GameObject;
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        
    
    }
    public void startFireballEvent()
    {
        one = true;
    }
    public void castedEvent()
    {
        one = false;
    }
   
}
