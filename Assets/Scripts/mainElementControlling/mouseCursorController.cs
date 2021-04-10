using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public GameObject menuUI;
    // Update is called once per frame
    void Update()
    {
        if(inventoryManager.instance.inventoryOpened 
            || NPCinteraction.instance.dialogHappening 
            || playerHealth.instance.currentHealth <= 0 || menuUI.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
