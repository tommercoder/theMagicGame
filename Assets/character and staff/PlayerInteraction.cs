﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// this script is attached to player
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
   
    public inventoryManager panelShow;
   
    
    private void OnTriggerEnter(Collider other)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if(other.tag=="interactable object" && other.GetComponent<Interact>()!=null)
        {
            other.GetComponent<Interact>().InteractWith();
            panelShow.showPanel("Press E to " + other.GetComponent<Interact>().InteractedText);
            
            //stopping rotating object
            if(other.GetComponent<FloatingItem>()!=null)
            other.GetComponent<FloatingItem>().Rotating = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="interactable object")
        { 
            panelShow.hidePanel();
            
            if (other.GetComponent<FloatingItem>() != null)
                other.GetComponent<FloatingItem>().Rotating = true;
        }
    }

  
}
