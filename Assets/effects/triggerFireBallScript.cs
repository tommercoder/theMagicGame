using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class triggerFireBallScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ENEMY") && characterStats.instance.damageFromFireball>0)
        {
            if (other.GetComponent<ProceduralStats>() != null)
            {
                fireballAbility.instance.triggered = true;

                other.gameObject.GetComponent<ProceduralStats>().currentHealth -= characterStats.instance.damageFromFireball;


                other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.forward * 2), 0.2f);
                Debug.Log("fireball hitttin " + other.name);
            }
            else if(other.GetComponent<EnemyStats>()!=null)
            {
                fireballAbility.instance.triggered = true;
                other.gameObject.GetComponent<EnemyStats>().currentHP -= characterStats.instance.damageFromFireball;
                other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.forward * 4), 0.2f);
                Debug.Log("triggerFireBallScript");

            }
        }

    }
}
