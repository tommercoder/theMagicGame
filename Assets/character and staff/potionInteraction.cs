using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionInteraction : Interact
{
    public inventoryManager manager;
    public Item item;
    public override void InteractWith()
    {

        
        resetText();
        InteractedText += "pick up potion";
        interacting = true;
        //pickup potion

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interacting)
        {
            //Debug.Log("picking up " + item.name);


            pickUp();

        }
    }
    void pickUp()
    {


        //add to inventory
        bool added = Inventory.instance.add(item);
        bool addedGO = Inventory.instance.addGOforPotions(gameObject);
        if (added && addedGO)
        {
            Debug.Log("added & addedGO");
           
            //Inventory.instance.itemsGameObjects.Add(gameObject);

            manager.hidePanel();
            interacting = false;

            if (gameObject.GetComponent<FloatingItem>() != null)
            {
                gameObject.GetComponent<FloatingItem>().Rotating = true;
            }
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        else
            Debug.Log("not added");
    }
}
