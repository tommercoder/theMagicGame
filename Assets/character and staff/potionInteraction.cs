using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionInteraction : Interact
{
  
    [Header("ITEM ID")]
    public string id;
    public inventoryManager manager;
    public Item item;
    public override void InteractWith()
    { 
        resetText();
        InteractedText += "pick up potion";
        interacting = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interacting)
        {
            pickUp();
        }
    }
    void pickUp()
    {

      
        bool added = Inventory.instance.add(item);
        bool addedGO = Inventory.instance.addGOforPotions(gameObject);
        if (added && addedGO)
        {

            manager.hidePanel();
            interacting = false;

            if (gameObject.GetComponent<FloatingItem>() != null)
            {
                gameObject.GetComponent<FloatingItem>().Rotating = true;
            }
            gameObject.SetActive(false);
            isUsed = true;
        }
        else
        {
            Debug.Log("not added" + item.name);
        }
    }
}
