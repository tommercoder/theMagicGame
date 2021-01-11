using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponInteract : Interact
{
    public override void InteractWith()
    {
       
        Debug.Log("was called weapon interact");
        resetText();
        InteractedText += "pick up weapon";

   
        
    }
}
