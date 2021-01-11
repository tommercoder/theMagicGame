using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// just for ovverite interaction
/// </summary>
public class Interact : MonoBehaviour
{
    public string InteractedText ;
    
    public virtual void InteractWith()
    {
       
        Debug.Log("interacting with " + transform.name);
    }
    public void resetText()
    {
        InteractedText = "";
    }
}
