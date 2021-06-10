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
    //ten parametr służy do odnowienia obrażenia miecza na stare obrażenie w przypadku wyjścia z gry
    public int oldDamage;
   
   
    public override void useFromInventory()
    {
        base.useFromInventory();
        //wstawia się aktualny miecz na "ten" miecz
        playerSword.instance.currentSword = this;//dodaje ten item do scriptu playersword
        //szukamy tego miecza w ekwipunku
        for(int i = 0;i< Inventory.instance.items.Count;i++)
        {//jeśli został znaleziony
            if(Inventory.instance.items[i].Equals(this))
            {

                //wstawia się i mówi że aktualny miecz i jego GameObject jest "ten" miecz
                playerSword.instance.sword = Inventory.instance.itemsGameObjects[i].transform;
                playerSword.instance.currentSwordGameObject = Inventory.instance.itemsGameObjects[i];
                //usuwa się go z ekwipunku
                Inventory.instance.itemsGameObjects.Remove(Inventory.instance.itemsGameObjects[i]);
                Inventory.instance.removeItem(Inventory.instance.items[i]);

               
                //wstawia nowy miecz do tyłu i zmiena mu "rodziców"
                //put new game object instead of old
                playerSword.instance.currentSwordGameObject.GetComponent<weaponInteract>().isCurrentSword = true;
                playerSword.instance.sword.SetParent(playerSword.instance.spine.transform);
                playerSword.instance.temp.transform.SetParent(playerSword.instance.itemsOnScene.transform);
                playerSword.instance.temp.SetActive(false);
                //wstawia miecz który był w rękach w ekwipunek i robi go nieaktywnym
                //put saved temp object to inventory back;
                playerSword.instance.temp.GetComponent<weaponInteract>().isCurrentSword = false;
                Inventory.instance.itemsGameObjects.Add(playerSword.instance.temp);
                Inventory.instance.add(playerSword.instance.temp.GetComponent<weaponInteract>().item);
   
            }
        }
       
        
        
    }
 
}
