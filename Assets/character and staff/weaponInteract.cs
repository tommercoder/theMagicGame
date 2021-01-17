using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponInteract : Interact
{
    public inventoryManager manager;
    public Item item;
    public override void InteractWith()
    {
       
       
        resetText();
        InteractedText += "pick up weapon";
        interacting = true;
   
        //pickup weapon
        
    }

    private void Update()
    {
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
}
