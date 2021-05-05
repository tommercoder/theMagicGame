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
    [Space(70)]
    public string[] sentences2;
    [Space(70)]

    public string greeting;
    public string goodbye;

    [Header("CHARACTER DIALOGUE")]
    public string answer;
    public string answer2;
    public string answerBye;//character bye

    public string uiText;

    public npcName npcEnumName;
    public enum npcName { simpleNPC,waterGirl,airGirl,earthGirl,fireGirl,emotionalSpirit,lifeSpirit,thoughtSpirit,sensibilitySpirit}
}
