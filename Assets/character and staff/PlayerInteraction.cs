using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// this script is attached to player
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction instance;
    
    public inventoryManager panelShow;

    public respawnScript respawnScript;
    private void Awake()
    {
        respawnScript = GameObject.Find("MainElements").GetComponent<respawnScript>();
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        //dlatego zeby nie wyswietlac pick up panel kiedy mam sword u siebie wziety
        if(other.GetComponent<weaponInteract>()!= null)
        {
            if (playerSword.instance.currentSwordGameObject == other.GetComponent<weaponInteract>().gameObject)
                return;
        }
        //zeby nie wyswitliac w juz ustalonym checkpoint tekstu interakcji
        else if(other.gameObject==respawnScript.checkPoint)
        {
            Debug.Log(other.gameObject + " " + respawnScript.checkPoint);
            return;
        }
         if(other.CompareTag("interactable object") && other.GetComponent<Interact>()!=null )
        {
            //Debug.Log("interacting with" + other.name);
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
            {
                other.GetComponent<FloatingItem>().Rotating = true;
            }

            if (other.GetComponent<Interact>() != null)
            {
                other.GetComponent<Interact>().interacting = false;
            }
        }
    }

  
}
