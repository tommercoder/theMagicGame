using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New potion", menuName = "Inventory/potion")]
public class potionUse : Item
{
    #region Singletom
    public static potionUse instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public typeOfItem type;
    public bool speedPotionUsing;
    //public ParticleSystem healthParticle;
    //public ParticleSystem speedParticle;
    //public ParticleSystem damageParticle;
    public override void useFromInventory()
    {
        base.useFromInventory();
      
        if (type == typeOfItem.healthPotion)
        {

            
            if (playerHealth.instance.currentHealth < playerHealth.instance.health)
            {
                //if(hp) od 81 do 100 than make it 100
                if(playerHealth.instance.currentHealth > 80 )
                {
                    playerHealth.instance.currentHealth = 100;
                    return;
                }
             //delete from inventory if only one remain
             //else
             //change current stack number
                playerHealth.instance.currentHealth += 20;
                if (this.currentStack > 1)
                {
                    this.currentStack -= 1;
                }
                else
                {
                    for (int i = 0; i < Inventory.instance.items.Count; i++)
                    {
                        if (Inventory.instance.items[i] == this)
                        {
                            Inventory.instance.removeItem(this);
                            Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);
                        }
                    }
                }
                potionParticle.instance.turn(type);
            }
           
            Debug.Log("using healthPotion" + name);
        }
        if (type == typeOfItem.damagePotion)
        {

            //if(//damage is in some range then use and delete it from inventory;)
            //{

            //if (this.currentStack > 1)
            //{
            //    this.currentStack -= 1;
            //}
            //else
            //{
            //    for (int i = 0; i < Inventory.instance.items.Count; i++)
            //    {
            //        if (Inventory.instance.items[i] == this)
            //        {
            //            Inventory.instance.removeItem(this);
            //            Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);
            //        }
            //    }
            //}
            //}
            //move it to if above
            potionParticle.instance.turn(type);
            potionParticle.instance.startTimer(60.0f, type);
            //set the damage plus for player for given amount of time;
            Debug.Log("using damagePotion" + name);
        }
        if (type == typeOfItem.speedPotion)
        {
            
            if (movement.instance.playerSpeed == 4)
            {
                movement.instance.playerSpeed += 2;
                if (this.currentStack > 1)
                {
                    this.currentStack -= 1;
                }
                else
                {
                    for (int i = 0; i < Inventory.instance.items.Count; i++)
                    {
                        if (Inventory.instance.items[i] == this)
                        {
                            Inventory.instance.removeItem(this);
                            Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);
                        }
                    }
                }
                //this bool is for movement script to stop changing speed
                speedPotionUsing = true;
                potionParticle.instance.turn(type);
                potionParticle.instance.startTimer(120.0f, type);
            }
            
            
            Debug.Log("using speedPotion" + name);
        }
     
    }

    

}
public enum typeOfItem { healthPotion, speedPotion, damagePotion}//etc