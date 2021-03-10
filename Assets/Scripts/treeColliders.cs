using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeColliders : MonoBehaviour
{
    public Terrain terrain;
    // Start is called before the first frame update
    void Start()
    {
       terrain.GetComponent<treeColliders>().enabled = false;
       terrain.GetComponent<treeColliders>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
