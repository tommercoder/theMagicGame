using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script is attached to GameManager
/// </summary>
public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

     void Awake()
    {
        if(instance!=null)
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

    public List<GameObject> itemsGameObjects = new List<GameObject>();
    public int size = 16;
    public bool add(Item item)
    {
        for(int i = 0;i < items.Count;i++)
        {
            if(items[i].name==item.name)
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
        if(items.Count >= size)
        {
            Debug.Log("dont have enough space in inventory");
            return false;
        }
        
        items.Add(item);
        
        if(onItemChangedCalled != null)
        onItemChangedCalled.Invoke();
        return true;
    }
    public bool addGOforPotions(GameObject item)
    {
        //check if i can add a potion GO
        if(item.GetComponent<potionInteraction>()!=null)
        for (int i = 0; i < itemsGameObjects.Count; i++)
        {
                //if (itemsGameObjects[i].name == item.name)
                // {
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

        if (itemsGameObjects.Count >= size)
        {
            Debug.Log("dont have enough space for game object to add");
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

}
