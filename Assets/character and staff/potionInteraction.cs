using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionInteraction : Interact
{
    public override void InteractWith()
    {

        Debug.Log("was called potion interact");
        resetText();
        InteractedText += "pick up potion";

        //pickup potion

    }
}
