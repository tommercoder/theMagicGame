using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCinteraction : Interact
{
    public override void InteractWith()
    {

        Debug.Log("was called npc interact");
        resetText();
        InteractedText += "speak with " + transform.name;

        //start dialog

    }
}
