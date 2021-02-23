using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponInteract : Interact
{
    
    public inventoryManager manager;
    public swordEquipping item;
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
    private void OnTriggerEnter(Collider other)
    {
        if (attacksController.instance.isDrawedSword && Input.GetMouseButtonDown(0))//today this
        {
            if (other.gameObject.CompareTag("ENEMY"))
            {
                Debug.Log("KICKED PROCEDURAL");
                if (other.gameObject.GetComponent<ProceduralStats>() != null)
                {

                    other.gameObject.GetComponent<ProceduralStats>().currentHealth -= item.swordDamage;//this.gameObject.GetComponent<weaponInteract>().item.swordDamage;

                }
            }
        }
    }


}
