using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class weaponInteract : Interact
{
    #region singleton
    public static weaponInteract instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public inventoryManager manager;
    public swordEquipping item;
    public Animator animator;
    public int damage;
    public bool isColliding;
    public bool isCurrentSword;
    public CharacterController otherController;
    public Animator otherAnimator;
    private void Start()
    {
        damage = item.swordDamage;
        if (transform.parent.name == "character")
            animator = GetComponentInParent<Animator>();
        else
            animator = GameObject.Find("character").GetComponent<Animator>();
    }
    public override void InteractWith()
    {
       
       
        resetText();
        InteractedText += "pick up weapon";
        interacting = true;
   
       
        
    }

    private void Update()
    {
         //pickup weapon
        if (Input.GetKeyDown(KeyCode.E) && interacting)
        {
            //Debug.Log("picking up " + item.name) ;


            pickUp();
            
        }
    }
    void pickUp()
    {


        //add to inventory
        bool added = Inventory.instance.add(item);
        
        if (added)
        {
            Debug.Log("added weapon");
            
            Inventory.instance.itemsGameObjects.Add(gameObject);
            //characterStats.instance.allAddedToInventoryGO.Add(gameObject);
            manager.hidePanel();
            interacting = false;

            if (gameObject.GetComponent<FloatingItem>() != null)
            {
                gameObject.GetComponent<FloatingItem>().Rotating = true;
            }
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    //damage 
    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponent<FloatingItem>() != null)
        {
            if (!gameObject.GetComponent<FloatingItem>().Rotating)
            {

                if (isColliding)
                    return;


                if ((animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
                        || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
                        || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack") ||
                        animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
                        || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
                        || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing")))
                {

                    if (other.gameObject.CompareTag("ENEMY"))
                    {
                        isColliding = true;

                        if (other.gameObject.GetComponent<ProceduralStats>() != null)
                        {

                            other.gameObject.GetComponent<ProceduralStats>().currentHealth -= item.swordDamage;//this.gameObject.GetComponent<weaponInteract>().item.swordDamage;
                                                                                                               //Debug.Log("interact weapon with " + other.name);
                            //if (Random.Range(0, 2) == 1)
                           // {
                                other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.root.forward * 4), 0.2f);//moving enemy back after hit
                           // }                                                                             //Debug.Log(transform.root.name);

                        }
                        else if (other.gameObject.GetComponent<EnemyStats>() != null)
                        {
                            // Debug.Log("hittinhg" + other.name);
                            //damage
                            other.gameObject.GetComponent<EnemyStats>().currentHP -= item.swordDamage;
                            //logic
                            // Debug.Log("interact weapon with " + other.name);
                            otherController = other.GetComponent<CharacterController>();
                            otherAnimator = other.GetComponent<Animator>();
                            otherAnimator.SetBool("isWalkingEnemy", false);
                            otherAnimator.SetBool("isRunningEnemy", false);
                            int temp = Random.Range(0, 2);
                            if (temp == 1)
                            {
                                otherAnimator.SetTrigger("hitEnemy");
                            }
                             if(temp == 0)
                            {
                                otherAnimator.SetTrigger("hitEnemy2");
                            }
                            Debug.Log("TEMP + " + temp);
                            //otherAnimator.SetInteger("enemyAttackInteger", 4);
                            EnemyPatrol.instance.canMove = false;
                            otherController.enabled = false;
                            if (temp == 1)
                            {
                                other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.root.forward * 4), 0.2f);
                            }
                            //otherAnimator.SetTrigger("hitEnemy");
                            StartCoroutine(waitForSec());
                        }
                    }
                    StartCoroutine(Reset());
                }
            }
        }

    }
    IEnumerator waitForSec()
    {
        EnemyPatrol.instance.canMove = true;
        yield return new WaitForSeconds(1f);
        if(otherController !=null)
        otherController.enabled = true;
        
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.3f);
        isColliding = false;
    }


}
