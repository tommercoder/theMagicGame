using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Item item;
    //public List<Item> itemsInSlot = new List<Item>();
    public delegate void onSlotChangedCallback();
    public onSlotChangedCallback onSlotChangedCalled;
    public GameObject countPanel;
    public Text countText;
    public Image icon;
    public Button dropButton;

    public Transform character;
    public void add(Item item)
    {
        
        this.item = item;
        //itemsInSlot.Add(item);
        icon.sprite = item.icon;
        icon.enabled = true;
        dropButton.interactable = true;
        countPanel.SetActive(true);
        countText.enabled = true;
        if (onSlotChangedCalled != null)
            onSlotChangedCalled.Invoke();


    }
    private void Update()
    {
        if(countPanel.activeInHierarchy)
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
        if(item!=null)
        {
            item.useFromInventory();
            if (onSlotChangedCalled != null)
                onSlotChangedCalled.Invoke();
        }
    }
    public void DropButton()//set to onclick
    {
       
        
        for(int i = 0;i < Inventory.instance.items.Count;i++)
        {
            if(Inventory.instance.items[i]==item)
            {
                Vector3 dropPosition = new Vector3(character.position.x, Inventory.instance.itemsGameObjects[i].transform.position.y, character.position.z);
                //Vector3 position = Inventory.instance.itemsGameObjects[i].transform.position;
                Inventory.instance.itemsGameObjects[i].transform.rotation = Quaternion.identity;
                //position.y = swordEquipping.instance.savedPosition.y;
                //Inventory.instance.itemsGameObjects[i].transform.position = position;


                
                Inventory.instance.itemsGameObjects[i].transform.position = dropPosition+(-(transform.forward*2));
                Inventory.instance.itemsGameObjects[i].SetActive(true);
                Inventory.instance.itemsGameObjects[i].GetComponent<FloatingItem>().Rotating = true;
                Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);
            }
        }
        Inventory.instance.removeItem(item);

    }

}
