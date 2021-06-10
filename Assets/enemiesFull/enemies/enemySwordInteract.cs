using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class enemySwordInteract : MonoBehaviour
{
    public CharacterController otherController;
    public Animator otherAnimator;
    public Animator animator;

    public void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    public int damage;
    public void OnTriggerEnter(Collider other)
     {
         //jeśli gra jakaś animacja ataki
         if (animator.GetCurrentAnimatorStateInfo(0).IsName("swordCast") || animator.GetCurrentAnimatorStateInfo(0).IsName("fastSwordSlash")
             || animator.GetCurrentAnimatorStateInfo(0).IsName("bottomSwordSlash"))
         {
            //jeśli jest trigger z bohaterem
             if (other.CompareTag("Player"))
             {

                 otherController = other.GetComponent<CharacterController>();
                 otherAnimator = other.GetComponent<Animator>();
                 if (attacksController.instance.isDrawedSword)
                 { 
                    attacksController.instance.noOfClick = 0;
                    attacksController.instance.noOfClickSecond = 0;
                    attacksController.instance.canClick = true;
                    attacksController.instance.canClickSec = true;
                 }
                 //zmniejsza się zdrowie == obrażeniu
                 playerHealth.instance.currentHealth -= damage;
             }
         }
     }


    IEnumerator turnOffCharacter()
    {
        yield return new WaitForSeconds(1f);
        otherController.enabled = true;
        
    }
}
