
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Quest 
{
    public bool isActive;
    public string title;
    public string description;
    public int XP;
    public string rewardText;
    //możliwe nagrody
    public swordEquipping rewardSword;
    public GameObject swordGameObjectReward;
    public potionUse rewardPotion;
    public GameObject potionGameObjectReward;

    public QuestGoal goal;
    public npcName npcEnumName;
    //funkcja wywołana po zakonczeniu zadania
    public void complete()
    {
        logShow.instance.showQuestText("quest " + title + " is completed");
        
        isActive = false;
        //resetowanie aktualnego zadania
        MarieleQuest.instance.hasQuest = false;
        MarieleQuest.instance.currentMarieleQuest.title = ""; 
        MarieleQuest.instance.currentMarieleQuest = null;
        
        
        logShow.instance.showText("+" + XP);
        
        //also clear quest in P window
        MarieleQuest.instance.clearPWindow();
        //dodanie nagrody do ekwipunku
        
        if (rewardSword != null && swordGameObjectReward != null)
        {
            Inventory.instance.add(rewardSword);
            Inventory.instance.itemsGameObjects.Add(swordGameObjectReward);
            swordGameObjectReward.SetActive(false);

        }
        else if(rewardPotion!=null && potionGameObjectReward!=null)
        {
            Inventory.instance.add(rewardPotion);
            Inventory.instance.addGOforPotions(potionGameObjectReward);
            potionGameObjectReward.SetActive(false);
        }

        //w jakimś z przypadków mając wszystkie 5 mieczy gra jest skończona
        if (playerSword.instance.currentSword == attacksController.instance.air && Inventory.instance.items.Contains(attacksController.instance.water) &&
         Inventory.instance.items.Contains(attacksController.instance.earth)
         && Inventory.instance.items.Contains(attacksController.instance.fire))
        {
            characterStats.instance.gameEnded = true;
        }
        else if (playerSword.instance.currentSword == attacksController.instance.fire && Inventory.instance.items.Contains(attacksController.instance.water) &&
       Inventory.instance.items.Contains(attacksController.instance.earth)
       && Inventory.instance.items.Contains(attacksController.instance.air))
        {
            characterStats.instance.gameEnded = true;
        }
        else if (playerSword.instance.currentSword == attacksController.instance.water && Inventory.instance.items.Contains(attacksController.instance.air) &&
       Inventory.instance.items.Contains(attacksController.instance.earth)
       && Inventory.instance.items.Contains(attacksController.instance.fire))
        {
            characterStats.instance.gameEnded = true;
        }
        else if (playerSword.instance.currentSword == attacksController.instance.earth && Inventory.instance.items.Contains(attacksController.instance.water) &&
       Inventory.instance.items.Contains(attacksController.instance.air)
       && Inventory.instance.items.Contains(attacksController.instance.fire))
        {
            characterStats.instance.gameEnded = true;
        }
        else if (Inventory.instance.items.Contains(attacksController.instance.earth) && Inventory.instance.items.Contains(attacksController.instance.water) &&
       Inventory.instance.items.Contains(attacksController.instance.air)
       && Inventory.instance.items.Contains(attacksController.instance.fire))
            {
                characterStats.instance.gameEnded = true;
            }
        }
    
}
//imię npc
public enum npcName { simpleNPC, waterGirl, airGirl, earthGirl, fireGirl, emotionalSpirit, lifeSpirit, thoughtSpirit, sensibilitySpirit }