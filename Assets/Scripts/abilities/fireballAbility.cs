using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballAbility : AbilityMain
{
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
        
    }
    IEnumerator destruction()
    {
        yield return new WaitForSeconds(2f);
        
    }
    public void fireball()
    {
        Debug.Log("FIREBALL FUNC");
        projectile.transform.position = hand.transform.position;
        
        GameObject fireball = Instantiate(projectile, projectile.transform.position,Quaternion.identity) as GameObject;
        
        //fireball.transform.position = hand.transform.position;
        
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(fireball,2f);


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
