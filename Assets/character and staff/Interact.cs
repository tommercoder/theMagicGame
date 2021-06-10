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
    //tekst pokazujący się przy interakcjach
    public string InteractedText ;
    //czy objekt jest obecnie w interakcji
    public bool interacting = false;
    //czy objekt jest obecnie wykorzystywany
    public bool isUsed = false;
    
    //metoda wirtualna dla interakcji z objektami
    public virtual void InteractWith()
    {
    }
    public void resetText()
    {
        InteractedText = "";
    }

   
}
