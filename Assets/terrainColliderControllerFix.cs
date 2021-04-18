using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainColliderControllerFix : MonoBehaviour
{
    public GameObject cinemachine;


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain"))
        {
            Debug.Log("TERRAIN COLLISION");
            GetComponent<CinemachineTerrainHugger>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            GetComponent<CinemachineTerrainHugger>().enabled = false;
        }
    }
}
