using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballAbility : AbilityMain
{
    #region Singleton
    public static fireballAbility instance;

    private void Awake()
    {
        instance = this;
    }


    #endregion
    movement movement;
    Animator animator;
    StateControllerTest controller;
    public bool canCast;
    public GameObject projectile;
    public Transform playerTransform;
    public float timer = 5f;
    public GameObject hand;
    private bool one;
    public float speed = 12f;
    public AbilityUI AbilityUI;
    public bool triggered = false;
    GameObject fireballGO;
    private void Start()
    {
        movement = GetComponent<movement>();
        animator = GetComponent<Animator>();
        controller = GetComponent<StateControllerTest>();
        canCast = false;
    }
    public override void Ability()
    {

        //if (canCast)
        //{
           
           
                //projectile.SetActive(true);
                
                fireball();
                canCast = false;
                AbilityUI.ShowCoolDown(cooldownTime);

        // }
        abilityDone = true;
    }
    IEnumerator destruction()
    {
        yield return new WaitForSeconds(2f);
        
    }
    public void fireball()
    {

        Debug.Log("FIREBALL FUNC");
        projectile.transform.position = hand.transform.position;
        
         fireballGO = Instantiate(projectile, projectile.transform.position,Quaternion.identity);
        
        //fireball.transform.position = hand.transform.position;
        
        Rigidbody rb = fireballGO.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        


    }
    private void Update()
    {
        if (triggered)
        {
            Destroy(fireballGO);
            Debug.Log("destroyed fireball and damage done");
            triggered = false;
        }
        else
            Destroy(fireballGO, 2f);
    }
    public void startFireballEvent()
    {
        canCast = true;
    }
    public void castedEvent()
    {
        movement.canMove = true;
        animator.SetInteger("attackAnimation", 4);
        canCast = false;
    }

}