using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class enemySwordInteract : MonoBehaviour
{
    public CharacterController otherController;
    public Animator otherAnimator;
    public Animator animator;
    private void Awake()
    {
        
    }
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    public int damage;
      public void OnTriggerEnter(Collider other)
     {
        // Debug.Log("Hitting player");
         if (animator.GetCurrentAnimatorStateInfo(0).IsName("swordCast") || animator.GetCurrentAnimatorStateInfo(0).IsName("fastSwordSlash")
             || animator.GetCurrentAnimatorStateInfo(0).IsName("bottomSwordSlash"))
         {

             if (other.CompareTag("Player"))
             {


                 otherController = other.GetComponent<CharacterController>();
                 otherAnimator = other.GetComponent<Animator>();
                 if (attacksController.instance.isDrawedSword)
                {
                    //movement.instance.canMove = true;
                    //otherAnimator.SetInteger("attackAnimation", 4);

                    attacksController.instance.noOfClick = 0;
                    attacksController.instance.noOfClickSecond = 0;
                    attacksController.instance.canClick = true;
                    attacksController.instance.canClickSec = true;

                    //other.GetComponent<attacksController>().noOfClick = 0;
                    //other.GetComponent<attacksController>().noOfClickSecond = 0;
                    //other.GetComponent<attacksController>().canClick = false;
                    //other.GetComponent<attacksController>().canClickSec = false;
                    //herAnimator.SetInteger("attackAnimation",4);
                    if (Random.Range(0, 2) == 1)
                    {
                        //otherAnimator.SetTrigger("hitByEnemy");
                        //attacksController.instance.noOfClick = 0;
                        //attacksController.instance.noOfClickSecond = 0;
                        //attacksController.instance.canClick = true;
                        //attacksController.instance.canClickSec = true;



                    }

                 }

                 //otherController.enabled = false;
                // if (Random.Range(0, 2) == 1)
                // {
                    // other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.root.forward * 4), 0.2f);
                 //}
                 playerHealth.instance.currentHealth -= damage;

                // StartCoroutine(turnOffCharacter());
             }
         }
     }

  

    IEnumerator turnOffCharacter()
    {
        yield return new WaitForSeconds(1f);
        otherController.enabled = true;
        
    }
}
