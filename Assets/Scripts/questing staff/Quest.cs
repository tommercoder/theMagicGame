﻿
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


    //maybe item reward
    public swordEquipping rewardSword;
    public GameObject swordGameObjectReward;
    public potionUse rewardPotion;
    public GameObject potionGameObjectReward;

    public QuestGoal goal;
    public npcName npcEnumName;
    public void complete()
    {
        logShow.instance.showQuestText("quest " + title + " is completed");
        if (Inventory.instance.items.Contains(attacksController.instance.water)
          && Inventory.instance.items.Contains(attacksController.instance.air) &&
          Inventory.instance.items.Contains(attacksController.instance.earth)
          && Inventory.instance.items.Contains(attacksController.instance.fire))
        {
            characterStats.instance.gameEnded = true;
        }
        isActive = false;

        MarieleQuest.instance.currentMarieleQuest.title = ""; 
        MarieleQuest.instance.currentMarieleQuest = null;
        
        
        logShow.instance.showText("+" + XP);
        
        //also clear quest in P window
        MarieleQuest.instance.clearPWindow();
        //save it to completed quests
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

    }
}
public enum npcName { simpleNPC, waterGirl, airGirl, earthGirl, fireGirl, emotionalSpirit, lifeSpirit, thoughtSpirit, sensibilitySpirit }