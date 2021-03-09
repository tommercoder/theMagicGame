using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetScriptableObjects : MonoBehaviour
{
    public List<Item> scriptableObjects = new List<Item>();
    // Start is called before the first frame update
    void Awake()
    {
        for(int i =0;i< scriptableObjects.Count;i++)
        {
            scriptableObjects[i].currentStack = 1;
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
