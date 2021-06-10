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
  
    public bool canCast;
    public GameObject projectile;
    public Transform playerTransform;
    public float timer = 5f;
    public GameObject hand;
  
    public float speed = 12f;
    public AbilityUI AbilityUI;
    public bool triggered = false;
    GameObject fireballGO;
    private void Start()
    {
        movement = GetComponent<movement>();
        animator = GetComponent<Animator>();
        canCast = false;
    }
    //nadpisanie metody 
    public override void Ability()
    { 
     
        fireball();
        canCast = false;
        AbilityUI.ShowCoolDown(cooldownTime);  
        abilityDone = true;
    }
    
    public void fireball()
    {
        //wstawia się pozycję prefabu do ręki bohatera
        projectile.transform.position = hand.transform.position;
        //tworzy instancję prefabu z daną rotacją i pozycją 
        fireballGO = Instantiate(projectile, projectile.transform.position, Quaternion.identity);
        Rigidbody rb = fireballGO.GetComponent<Rigidbody>();
        //porusza rigidbody prefabu w przód z szybkością speed
        rb.velocity = transform.forward * speed;
    }
    private void Update()
    {
        //jeśli funkcja ontriggerenter na prefabie zwróciła true to ten objekt zostaje usunięty
        if (triggered)
        {
            Destroy(fireballGO);

            triggered = false;
        }
        //w przeciwnym wypadku objekt usuwa się po 3 sekundach
        else
        {

            Destroy(fireballGO, 3f);
        }
    }
    public void startFireballEvent()//animation event na początku animacji 
    {
        canCast = true;
    }
    //animation event na końcu animacji dla wyłączenia animacji
    public void castedEvent()
    {
        movement.canMove = true;
        animator.SetInteger("attackAnimation", 4);
        canCast = false;
    }

}