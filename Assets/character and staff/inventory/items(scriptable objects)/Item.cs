using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
   new public string name = "";
   
    public Sprite icon = null;
    public int maxStack = 1;
    public int currentStack = 1;

    public string description;
    public string option;
    //public bool isDefaultItem = false;
    
    public virtual void useFromInventory()//set to onClick
    {
        
    }
    public virtual Item getType()
    {
        return this;
    }
    public void remove()
    {

    }
}
