using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   
    public inventoryManager panelShow;
    public bool interacted = false;
   
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag=="interactable object" && other.GetComponent<Interact>()!=null)
        {
            other.GetComponent<Interact>().InteractWith();
            panelShow.showPanel("Press E to " + other.GetComponent<Interact>().InteractedText);
            interacted = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="interactable object")
        { 
            panelShow.hidePanel();
            interacted = false;
        }
    }
}
