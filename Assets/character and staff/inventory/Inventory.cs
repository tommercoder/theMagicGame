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

    //wykorzystuje się delegate dla powiadomienia że coś się zmieniło w ekwipunku
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCalled;
    //lista przedmiotów
    public List<Item> items = new List<Item>();
    [Header("SWORDS CANT BE SAME DURING GAME")]
    //lista objektów predmiotów
    public List<GameObject> itemsGameObjects = new List<GameObject>();

    public int size = 16;
    //dodaje sie do ekwipunku
    public bool add(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)
            {
                items[i].currentStack++;
                return true;
            }
        }
        if (items.Count >= size)
        {
            Debug.Log("do not enough space in inventory");
            return false;
        }

        items.Add(item);

        if (onItemChangedCalled != null)
            onItemChangedCalled.Invoke();
        return true;
    }
    //dodaje GameObject do ekwipunku
    public bool addGOforPotions(GameObject item)
    { 
        if (item.GetComponent<potionInteraction>() != null)
        {
            for (int i = 0; i < itemsGameObjects.Count; i++)
            {

                if (itemsGameObjects[i].GetComponent<potionInteraction>() != null)
                {
                    if (itemsGameObjects[i].GetComponent<potionInteraction>().item.name == item.GetComponent<potionInteraction>().item.name)
                    {
                        return true;
                    }
                }
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
    //usuwa GameObject
    public void removeGOitem(GameObject item)
    {
        itemsGameObjects.Remove(item);
    }
    //usuwa Item
    public void removeItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCalled != null)
            onItemChangedCalled.Invoke();
    }
    public void PopulateSaveData(SaveData sd)
    {
        sd.s_inventory = items;
        sd.s_inventoryGO = itemsGameObjects;
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
                addGOforPotions(sd.s_inventoryGO[i]);
                sd.s_inventoryGO[i].SetActive(false);
            }
        }

    }
}
