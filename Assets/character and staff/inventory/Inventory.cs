using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script is attached to GameManager
/// </summary>
public class Inventory : MonoBehaviour, ISaveable
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("instance inventory.cs");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCalled;
    public List<Item> items = new List<Item>();
    [Header("SWORDS CANT BE SAME DURING GAME")]
    public List<GameObject> itemsGameObjects = new List<GameObject>();

    public int size = 16;
    public bool add(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)//check
            {

                // if(items[i].currentStack < items[i].maxStack)
                //{

                items[i].currentStack++;
                Debug.Log(items[i].name + "has more than one item");

                return true;
                //}

            }
        }
        ///////////////////////
        if (items.Count >= size)
        {
            Debug.Log("dont have enough space in inventory");
            return false;
        }

        items.Add(item);

        if (onItemChangedCalled != null)
            onItemChangedCalled.Invoke();
        return true;
    }
    public bool addGOforPotions(GameObject item)
    {

        //for (int i = 0; i < item.GetComponent<potionInteraction>().item.currentStack; i++)
        //{
        //    Debug.Log("stackI" + item.GetComponent<potionInteraction>().item.currentStack);
        //}
        //characterStats.instance.allAddedToInventoryGO.Add(item);
        //check if i can add a potion GO
        if (item.GetComponent<potionInteraction>() != null)
        {

            for (int i = 0; i < itemsGameObjects.Count; i++)
            {
                //if (itemsGameObjects[i].name == item.name)
                // {
                //this handle adding only potions

                if (itemsGameObjects[i].GetComponent<potionInteraction>() != null)
                {
                    Debug.Log("game object item :" + itemsGameObjects[i].GetComponent<potionInteraction>().item.name + "\nitem name " + item.GetComponent<potionInteraction>().item.name);

                    if (itemsGameObjects[i].GetComponent<potionInteraction>().item.name == item.GetComponent<potionInteraction>().item.name)
                    {


                        return true;
                    }
                }
                //}
            }
        }

        if (itemsGameObjects.Count >= size)
        {
            logShow.instance.showText("dont have enough space for game object to add");
            return false;
        }

        itemsGameObjects.Add(item);

        if (onItemChangedCalled != null)
            onItemChangedCalled.Invoke();
        return true;
    }
    public void removeGOitem(GameObject item)
    {
        itemsGameObjects.Remove(item);
    }
    public void removeItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCalled != null)
            onItemChangedCalled.Invoke();
        //remove functionality
    }
    public void PopulateSaveData(SaveData sd)
    {
        sd.s_inventory = items;
        sd.s_inventoryGO = itemsGameObjects;
        //foreach(GameObject g in sd.s_inventoryGO)
        //{
        //    Debug.Log("POP INV" + g.name);
        //}

        //sd.s_allGameObjectInventory = characterStats.instance.allAddedToInventoryGO;
    }
    public void LoadFromSaveData(SaveData sd)
    {

        for (int i = 0; i < sd.s_inventory.Count; i++)
        {
            if (sd.s_inventory[i] != null)
            {
                add(sd.s_inventory[i]);

            }
        }
        for (int i = 0; i < sd.s_inventoryGO.Count; i++)
        {
            if (sd.s_inventoryGO[i] != null)
            {
                //  Debug.Log("INVENTORY" + sd.s_inventoryGO[i]);
                addGOforPotions(sd.s_inventoryGO[i]);
                sd.s_inventoryGO[i].SetActive(false);
            }
            //else
            //{
            //    GameObject temp;
            //    for (int t = 0; t < characterStats.instance.allInteractableGameObjects.Count; t++)
            //    {

            //        if (characterStats.instance.allInteractableGameObjects[t].GetComponent<potionInteraction>() != null)
            //        {
            //            if (sd.s_inventory[i] == characterStats.instance.allInteractableGameObjects[t].GetComponent<potionInteraction>().item)

            //            {
            //                addGOforPotions(characterStats.instance.allInteractableGameObjects[t]);
            //                characterStats.instance.allInteractableGameObjects[t].SetActive(false);
            //            }
            //        }
            //        else if (characterStats.instance.allInteractableGameObjects[t].GetComponent<weaponInteract>() != null)
            //        {
            //            // Debug.Log("A" + sd.s_inventory[i]);
            //            // Debug.Log("B" + characterStats.instance.allInteractableGameObjects[t].GetComponent<weaponInteract>().item);
            //            if (sd.s_inventory[i] == characterStats.instance.allInteractableGameObjects[t].GetComponent<weaponInteract>().item)
            //            {
            //                addGOforPotions(characterStats.instance.allInteractableGameObjects[t]);
            //                characterStats.instance.allInteractableGameObjects[t].SetActive(false);
            //            }
            //        }
            //    }

            //}
        }

        /////////////////////////////////////////////////////////

        //characterStats.instance.allAddedToInventoryGO = sd.s_allGameObjectInventory;
        //for(int i = 0;i < sd.s_allGameObjectInventory.Count;i++)
        //{
        //    sd.s_allGameObjectInventory[i].SetActive(false);
        //}
        //resetting object that are not in inventory
        //for (int i = 0; i < resetScriptableObjects.instance.scriptableObjects.Count; i++)
        //    if (!sd.s_inventory.Contains(resetScriptableObjects.instance.scriptableObjects[i]))
        //    {

        //        resetScriptableObjects.instance.scriptableObjects[i].currentStack = 1;
        //    }
    }
}
