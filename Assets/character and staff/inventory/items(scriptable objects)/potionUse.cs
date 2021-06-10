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
                    #region deleting
                    if (this.currentStack > 1)
                    {
                        this.currentStack -= 1;
                    }
                    else
                    {
                        //usuwa wykorzystaną miksturę z ekwipunku w przypadku jeśli został tylko jeden objekt
                        for (int i = 0; i < Inventory.instance.items.Count; i++)
                        {
                            if (Inventory.instance.items[i] == this)
                            {
                                Inventory.instance.removeItem(this);
                                Inventory.instance.removeGOitem(Inventory.instance.itemsGameObjects[i]);
                            }
                        }
                    }
                    //włącza particle system z danym typem
                    potionParticle.instance.turn(type);
                    #endregion
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
                logShow.instance.showText("increased 20hp \n your hp now:"+playerHealth.instance.currentHealth);
                potionParticle.instance.turn(type);
                
            }
        }
        //działa tak samo
        if (type == typeOfItem.damagePotion)
        {

            if (!potionParticle.instance.usingDamagePotionNow)
            {
                //powiększa obrażenia miecza o 20%
                playerSword.instance.currentSword.swordDamage += playerSword.instance.currentSword.swordDamage * 20 / 100;
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
                logShow.instance.showText("increased damage by " + playerSword.instance.currentSword.swordDamage * 20 / 100);
                inventoryManager.instance.damageText.text = "current damage is: " + (playerSword.instance.currentSword.swordDamage+ (playerSword.instance.currentSword.swordDamage * 20 / 100));
                potionParticle.instance.turn(type);
                potionParticle.instance.startTimer(60.0f, type);
            }
            else
            {
                logShow.instance.showText("you can't use damage potion twice,wait until effect is gone");
            }

        }
        if (type == typeOfItem.speedPotion)
        {
            
            if ((movement.instance.playerSpeed == 5 || movement.instance.playerSpeed == 6) && !movement.instance.speedPotionUsingNow)
            {
                waitAfterSpeed();
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
                //speedPotionUsing = true;
                logShow.instance.showText("increased speed by 2");
                movement.instance.speedPotionUsingNow = true;
                potionParticle.instance.turn(type);
                potionParticle.instance.startTimer(120.0f, type);
            }
            else
            {
                logShow.instance.showText("last speed potions is still in use");
            }
   
        }
     
    }
    IEnumerator waitAfterSpeed()
    {
        yield return new WaitForSeconds(5f);
    }
    
    
}
public enum typeOfItem { healthPotion, speedPotion, damagePotion}