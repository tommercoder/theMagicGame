using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class enemySwordInteract : MonoBehaviour
{
    public CharacterController otherController;
    public Animator otherAnimator;
    public Animator animator;
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }
    public int damage;
    public void OnTriggerEnter(Collider other)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("swordCast") || animator.GetCurrentAnimatorStateInfo(0).IsName("fastSwordSlash")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("bottomSwordSlash"))
        {
            if (other.CompareTag("Player"))
            {

                Debug.Log("Hitting player");
                otherController = other.GetComponent<CharacterController>();
                otherAnimator = other.GetComponent<Animator>();
                if (attacksController.instance.isDrawedSword)
                {
                    
                    otherAnimator.SetTrigger("hitByEnemy");
                    attacksController.instance.noOfClick = 0;
                    attacksController.instance.noOfClickSecond = 0;

                }

                otherController.enabled = false;
                other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.root.forward * 4), 0.2f);
                playerHealth.instance.currentHealth -= damage;

                StartCoroutine(turnOffCharacter());
            }
        }
    }

    IEnumerator turnOffCharacter()
    {
        yield return new WaitForSeconds(1f);
        otherController.enabled = true;
    }
}
