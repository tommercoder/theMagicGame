using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New equipment", menuName = "Inventory/swordEquipping")]
public class swordEquipping : Item
{
    #region Singleton
    public static swordEquipping instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("instance swordEquipping.cs");
            return;
        }

        instance = this;
    }

    #endregion
    public int swordDamage;
    public int oldDamage;
    public Vector3 savedPosition;
    
    public override void useFromInventory()
    {
        base.useFromInventory();
        
        
       Debug.Log("clicked on sword" + name);
        playerSword.instance.currentSword = this;//dodaje ten item do scriptu playersword
        for(int i = 0;i< Inventory.instance.items.Count;i++)
        {
            if(Inventory.instance.items[i].Equals(this))
            {

                savedPosition = Inventory.instance.itemsGameObjects[i].transform.position;
                Debug.Log(savedPosition);
                playerSword.instance.sword = Inventory.instance.itemsGameObjects[i].transform;
                
                playerSword.instance.currentSwordGameObject = Inventory.instance.itemsGameObjects[i];
                

                Inventory.instance.itemsGameObjects.Remove(Inventory.instance.itemsGameObjects[i]);
                Inventory.instance.removeItem(Inventory.instance.items[i]);

                //Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);    

                //put new game object instead of old
                playerSword.instance.currentSwordGameObject.GetComponent<weaponInteract>().isCurrentSword = true;
                playerSword.instance.sword.SetParent(playerSword.instance.spine.transform);
                playerSword.instance.temp.transform.SetParent(playerSword.instance.itemsOnScene.transform);
                playerSword.instance.temp.SetActive(false);

                //put saved temp object to inventory back;
                playerSword.instance.temp.GetComponent<weaponInteract>().isCurrentSword = false;
                Inventory.instance.itemsGameObjects.Add(playerSword.instance.temp);
                Inventory.instance.add(playerSword.instance.temp.GetComponent<weaponInteract>().item);
                

                //setting back values everytime
                //playerSword.instance.temp= GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/" + Inventory.instance.itemsGameObjects[i].name);
                //playerSword.instance.currentSwordGameObject= GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/" + Inventory.instance.itemsGameObjects[i].name);
            }
        }
        //equip sword
        //show it in back of character
        //remove from inventory
        
        
    }
    //create swap function
}
