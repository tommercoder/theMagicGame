using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionInteraction : Interact
{
    private void Awake()
    {

        //GameObject[] objs = GameObject.FindGameObjectsWithTag("interactable object");

        //if (objs.Length > 1)
        //{
        //    Destroy(this.gameObject);
        //}

        //DontDestroyOnLoad(this.gameObject);
        //DontDestroyOnLoad(gameObject);
    }
    [Header("ITEM ID")]
    public string id;
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
            
            //Inventory.instance.itemsGameObjects.Add(gameObject);

            manager.hidePanel();
            interacting = false;

            if (gameObject.GetComponent<FloatingItem>() != null)
            {
                gameObject.GetComponent<FloatingItem>().Rotating = true;
            }
            gameObject.SetActive(false);
            isUsed = true;
            //for(int i =0;i < characterStats.instance.allPotionsIsUsed.Count;i++)
            //{
            //    if(characterStats.instance.allPotionsIsUsed[i]==false)
            //    {
            //        if (gameObject == characterStats.instance.allPotionInteractionO[i])
            //        {
            //            characterStats.instance.allPotionsIsUsed[i] = true;
            //            break;
            //        }
            //    }
            //}
            
            //Destroy(gameObject);
        }
        else
            Debug.Log("not added" + item.name);
    }
}
