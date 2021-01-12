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
