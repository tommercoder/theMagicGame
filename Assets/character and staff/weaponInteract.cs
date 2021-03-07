using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class weaponInteract : Interact
{
    
    public inventoryManager manager;
    public swordEquipping item;
    public Animator animator;
    public bool isColliding;
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
    private void OnTriggerEnter(Collider other)
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


                    other.gameObject.transform.DOMove(other.gameObject.transform.position + (-other.gameObject.transform.forward * 4), 0.2f);//moving enemy back after hit
                    
                    
                }
            }
            StartCoroutine(Reset());
        }
        
    }
   
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.3f);
        isColliding = false;
    }


}
