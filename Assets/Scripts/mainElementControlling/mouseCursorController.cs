using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursorController : MonoBehaviour
{
  
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public GameObject menuUI;
    public GameObject dialogBox;
  
    void Update()
    {
        
        if(inventoryManager.instance.inventoryOpened 
            || dialogBox.activeSelf
            || playerHealth.instance.currentHealth <= 0 || menuUI.activeSelf || pauseMenu.instance.pauseOpened)
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
