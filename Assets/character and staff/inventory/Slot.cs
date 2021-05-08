using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour, ISaveable
{
    public void PopulateSaveData(SaveData sd)
    {
        SaveData.SlotsData slotsData = new SaveData.SlotsData();
        slotsData.s_slotItem = item;
        slotsData.s_id = id;
     
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
                    
                    break;
                }

            }
        }
    }

        
    



    
    [Header("SLOT ID")]
    public string id;
    public Item item;



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
       
        if (icon.sprite != item.icon)
        {
            icon.sprite = item.icon;
        }
        
        icon.enabled = true;
        dropButton.interactable = true;
        if(!(item is swordEquipping))
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
    public List<potionInteraction> temp = new List<potionInteraction>();
    public void DropButton()//set to onclick
    {


        for (int i = 0; i < Inventory.instance.items.Count; i++)
        {
            if (Inventory.instance.items[i] == item)
            {

                Vector3 dropPosition = character.position + Vector3.up * 3;
               
                Inventory.instance.itemsGameObjects[i].transform.rotation = Quaternion.identity;
               


                Inventory.instance.itemsGameObjects[i].transform.position = dropPosition + (-(transform.forward * 2));
                Inventory.instance.itemsGameObjects[i].SetActive(true);
                Inventory.instance.itemsGameObjects[i].GetComponent<FloatingItem>().Rotating = true;
                
                
                 temp = GameObject.FindObjectsOfType<potionInteraction>().ToList();

                for (int k = 0;k < temp.Count;k++ )
                {
                    if(temp[k].gameObject == Inventory.instance.itemsGameObjects[i])
                    {
                        temp[k].isUsed = false;
                        //Debug.Log("LOG" + temp);
                    }
                }
                
                Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);
                
                
            }
        }
        Inventory.instance.removeItem(item);
        
    }

}
