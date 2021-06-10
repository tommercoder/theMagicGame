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
    public void Start()
    {
        damage = item.swordDamage;
        if (transform.parent.name == "character")
        {
            animator = GetComponentInParent<Animator>();
        }
        else
        {
            animator = GameObject.Find("character").GetComponent<Animator>();
        }
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
            pickUp();
        }
    }
    void pickUp()
    {
        //dodaje do ekwipunku
        bool added = Inventory.instance.add(item);
        
        if (added)
        {
            //dodaje GameObject do ekwipunku
            Inventory.instance.itemsGameObjects.Add(gameObject);
            //chowa panel interakcji
            manager.hidePanel();
            interacting = false;

            if (gameObject.GetComponent<FloatingItem>() != null)
            {
                gameObject.GetComponent<FloatingItem>().Rotating = true;
            }
            //wyłącza objekt na scenie
            gameObject.SetActive(false);
        
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
                    //w przypadku interakcji miecza z wrogiem
                    if (other.gameObject.CompareTag("ENEMY"))
                    {
                        isColliding = true;
                        if (other.gameObject.GetComponent<ProceduralStats>() != null)
                        {
                            //odejmuje zdrownie wroga
                            other.gameObject.GetComponent<ProceduralStats>().currentHealth -= item.swordDamage;
                            //odsuwa go w kierunku miecza po uderzeniu
                            other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.root.forward * 4), 0.2f);//moving enemy back after hit
                        }
                        else if (other.gameObject.GetComponent<EnemyStats>() != null)
                        {
                            //odejmuje zdrownie wroga
                            other.gameObject.GetComponent<EnemyStats>().currentHP -= item.swordDamage;
                            //logic
                            otherController = other.GetComponent<CharacterController>();
                            otherAnimator = other.GetComponent<Animator>();
                            //zmienia animacje 
                            otherAnimator.SetBool("isWalkingEnemy", false);
                            otherAnimator.SetBool("isRunningEnemy", false);
                            int temp = Random.Range(0, 2);
                            //w przypadku losowych liczb włącza animacje otrzymania uderzenia
                            if (temp == 1)
                            {
                                otherAnimator.SetTrigger("hitEnemy");
                            }
                             if(temp == 0)
                            {
                                otherAnimator.SetTrigger("hitEnemy2");
                            }
                            EnemyPatrol.instance.canMove = false;
                            otherController.enabled = false;
                            if (temp == 1)
                            {
                                //odsuwa go w kierunku miecza po uderzeniu
                                other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.root.forward * 4), 0.2f);
                            }
                           //czeka i włącza wroga z powrotem
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
