using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illusionInteract : MonoBehaviour
{
    //public GameObject moveBackPos;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //GameObject.FindGameObjectWithTag("Player").transform.position = moveBackPos.transform.position;
            playerHealth.instance.currentHealth = 0;

        }
    }
}
