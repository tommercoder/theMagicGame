using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestGoal 
{
    public goalType goalType;
    
    public int requiredAmount;
    public int currentAmount;
    //jeśli cel zadania jest wykonana
    public bool isReached()
    {
        questPointer.instance.target = null;
        MarieleQuest.instance.questPointer.SetActive(false);
        return (currentAmount >= requiredAmount);
    }
    //jeśli celą jest zabić wroga to powiększa liczba nieżyjących wrogów
    public void EnemyKilled()
    {
        if (goalType == goalType.killEnemyQuest)
        {
           currentAmount++;
        }
    }
    //jeśli celą jest zabić wroga proceduralnego to powiększa liczba nieżyjących wrogów proceduralnych
    public void ProceduralEnemyKilled()
    {
        if (goalType == goalType.killPEnemyQuest)
            currentAmount++;
    }
    //jeśli celą jest porozmawiać z innym npc
    public void SpokeToAnotherNPC()
    {
        if(goalType==goalType.speakQuest)
        {
            questPointer.instance.target = null;
            MarieleQuest.instance.questPointer.SetActive(false);
            currentAmount++;
        }
    }
    
}

public enum goalType {None, killEnemyQuest,killPEnemyQuest , speakQuest};

