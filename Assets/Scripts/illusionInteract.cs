using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illusionInteract : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            playerHealth.instance.currentHealth = 0;

        }
    }
}
