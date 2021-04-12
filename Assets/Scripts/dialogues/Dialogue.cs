using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue 
{
    public string name;
    [Header("NPC DIALOGUE")]
    [Header("THERE MUST BE AT LEAST ONE SENTENCE")]
    [TextArea(3,10)]
    public string[] sentences;
    [TextArea(3, 10)]
    public string[] sentences2;
    public string greeting;
    public string goodbye;//npc bye

    [Header("CHARACTER DIALOGUE")]
    public string answer;
    public string answer2;
    public string answerBye;//character bye

    public string uiText;

    public npcName npcEnumName;
    public enum npcName { magicElf,Keeper }
}
