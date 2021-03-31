using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour, ISaveable
{
    public void PopulateSaveData(SaveData sd)
    {
        SaveData.SlotsData slotsData = new SaveData.SlotsData();
        slotsData.s_slotItem = item;
        slotsData.s_id = id;

        //foreach (Sprite s in characterStats.instance.s_icons)
        //{
        //    if (id == slotsData.s_id)
        //    {
               
        //        slotsData.s_icon = s;
        //    }

        //}
        sd.s_slots.Add(slotsData);
    }
    public void LoadFromSaveData(SaveData sd)
    {
        foreach (SaveData.SlotsData slotsData in sd.s_slots)
        {
            if (slotsData.s_id == id)
            {
                if (slotsData.s_slotItem != null)
                {

                    add(slotsData.s_slotItem);
                    //countText.text = slotsData.s_slotItem.currentStack.ToString();
                    //item = slotsData.s_slotItem;
                    //icon.sprite = slotsData.s_icon;
                   
                    //characterStats.instance.s_icons.Add(slotsData.s_icon);
                    ////item.icon = slotsData.s_icon;
                    //icon.enabled = true;
                    //dropButton.interactable = true;
                   // countPanel.SetActive(true);
                   // countText.enabled = true;

                    //inventoryManager.instance.updateUI();
                    break;
                }

            }
        }
    }

        
    



    
    [Header("SLOT ID")]
    public string id;
    public Item item;



    //public List<Item> itemsInSlot = new List<Item>();
    public delegate void onSlotChangedCallback();
    public onSlotChangedCallback onSlotChangedCalled;
    public GameObject countPanel;
    public Text countText;
    public Image icon;
    public Button dropButton;
    public Sprite savedIcon;
    public Transform character;
    public void add(Item item)
     {

        this.item = item;
        //itemsInSlot.Add(item);

        //characterStats.instance.s_icons.Add(item.icon);
        if (icon.sprite != item.icon)
        {
            icon.sprite = item.icon;
        }
        
        icon.enabled = true;
        dropButton.interactable = true;
        countPanel.SetActive(true);
        countText.enabled = true;
        if (onSlotChangedCalled != null)
            onSlotChangedCalled.Invoke();


    }
    private void Update()
    {
        if (countPanel.activeInHierarchy)
            countText.text = item.currentStack.ToString();
    }
    public void clearSlot()
    {
        // Debug.Log("clear slot wass called");
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        dropButton.interactable = false;
        countPanel.SetActive(false);
        countText.enabled = false;
        countText.text = " ";
        if (onSlotChangedCalled != null)
            onSlotChangedCalled.Invoke();
    }
    public void useItem()
    {
        if (item != null)
        {

            item.useFromInventory();
            if (onSlotChangedCalled != null)
                onSlotChangedCalled.Invoke();
        }
    }
    public void DropButton()//set to onclick
    {


        for (int i = 0; i < Inventory.instance.items.Count; i++)
        {
            if (Inventory.instance.items[i] == item)
            {

                Vector3 dropPosition = character.position + Vector3.up * 3;//new Vector3(character.position.x, character.position.y+Vector3.up/*Inventory.instance.itemsGameObjects[i].transform.position.y*/, character.position.z);
                ////Vector3 position = Inventory.instance.itemsGameObjects[i].transform.position;
                Inventory.instance.itemsGameObjects[i].transform.rotation = Quaternion.identity;
                //////position.y = swordEquipping.instance.savedPosition.y;
                ////Inventory.instance.itemsGameObjects[i].transform.position = position;



                Inventory.instance.itemsGameObjects[i].transform.position = dropPosition + (-(transform.forward * 2));
                Inventory.instance.itemsGameObjects[i].SetActive(true);
                Inventory.instance.itemsGameObjects[i].GetComponent<FloatingItem>().Rotating = true;
                //characterStats.instance.allAddedToInventoryGO.Remove(Inventory.instance.itemsGameObjects[i]);


                //characterStats.instance.allAddedToInventoryGO.RemoveAll(gb => gb==Inventory.instance.itemsGameObjects[i]);

                //for (int g = 0; g < characterStats.instance.allAddedToInventoryGO.Count; g++)
                //{

                //    if (characterStats.instance.allAddedToInventoryGO[g].GetComponent<potionInteraction>() != null)
                //    {

                //        Debug.Log(characterStats.instance.allAddedToInventoryGO[g].GetComponent<potionInteraction>().item + " " + Inventory.instance.itemsGameObjects[i].GetComponent<potionInteraction>().item);
                //        if (characterStats.instance.allAddedToInventoryGO[g].GetComponent<potionInteraction>().item.Equals(Inventory.instance.itemsGameObjects[i].GetComponent<potionInteraction>().item))
                //        {
                //            characterStats.instance.allAddedToInventoryGO.Remove(characterStats.instance.allAddedToInventoryGO[g]);
                //            continue;
                //        }
                //    }
                //}



                //foreach (GameObject g in characterStats.instance.allAddedToInventoryGO)
                //{ 
                //    if(g.GetComponent<potionInteraction>()!=null)
                //    if (g.GetComponent<potionInteraction>().item == Inventory.instance.itemsGameObjects[i].GetComponent<potionInteraction>().item)
                //        characterStats.instance.allAddedToInventoryGO.Remove(g);
                //}

                characterStats.instance.allPotionInteractionO[i].isUsed = false;
                characterStats.instance.allPotionsIsUsed[i] = false;
                //for(int a = 0;a < characterStats.instance.allPotionInteractionO.Count;a++)
                //{
                //    if(characterStats.instance.allPotionInteractionO[i].GetComponent<potionInteraction>()!=null)
                //    {
                //        if(characterStats.instance.allPotionInteractionO[i]== Inventory.instance.itemsGameObjects[i])
                //        {
                //            characterStats.instance.allPotionInteractionO[i].isUsed = false;
                //        }
                //    }
                //}
                Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);
                
                
            }
        }
        Inventory.instance.removeItem(item);
        
    }

}
