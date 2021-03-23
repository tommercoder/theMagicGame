using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// just for ovverite interaction
/// </summary>
public class Interact : MonoBehaviour
{
    #region Singleton
    public static Interact instance;

    private void Awake()
    {
        if(instance!=null)
        {
            //Debug.LogWarning("instance error interact.cs");
            return;
        }

        instance = this;
    }

    #endregion
    public string InteractedText ;
    public bool interacting = false;
    
    
    public virtual void InteractWith()
    {
       
        //Debug.Log("interacting with " + transform.name);

    }
    public void resetText()
    {
        InteractedText = "";
    }

   
}
