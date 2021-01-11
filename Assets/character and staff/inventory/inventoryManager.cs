using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this is script is attached to UI/ui manager
/// </summary>
public class inventoryManager : MonoBehaviour
{
    public bool inventoryOpened = false;
    public GameObject inventoryUi;
    public Text textPanel;
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {

            inventoryUi.SetActive(!inventoryUi.activeSelf);
            inventoryOpened = true;
        }
    }

    public void showPanel(string text)
    {

        textPanel.gameObject.SetActive(true);
        textPanel.text = text;
    }
    public void hidePanel()
    {
        
        textPanel.gameObject.SetActive(false);
       
    }
}
