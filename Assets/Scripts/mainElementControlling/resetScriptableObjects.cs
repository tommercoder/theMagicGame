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
    // Start is called before the first frame update
    void Start()
    {
        
        //for(int i = 0;i< scriptableObjects.Count;i++)
        //{
        //    
        //}
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
