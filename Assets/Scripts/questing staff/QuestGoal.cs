﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestGoal 
{
    public goalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if (goalType == goalType.killEnemyQuest)
            currentAmount++;
    }
    public void ProceduralEnemyKilled()
    {
        if (goalType == goalType.killPEnemyQuest)
            currentAmount++;
    }
    public void SpokeToAnotherNPC()
    {
        if(goalType==goalType.speakQuest)
        {
            currentAmount++;
        }
    }
    public void gotSomeStaffToComplete()
    {
        if(goalType==goalType.gatheringQuest)
        {
            currentAmount++;
        }
    }
}

public enum goalType { killEnemyQuest,killPEnemyQuest, gatheringQuest , speakQuest};