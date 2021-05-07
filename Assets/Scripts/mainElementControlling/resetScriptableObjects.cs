using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetScriptableObjects : MonoBehaviour
{
    #region singleton
    public static resetScriptableObjects instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public List<Item> scriptableObjects = new List<Item>();
    
    void Start()
    {
        
        
    }
   
  
    void Update()
    {
        
    }
}
