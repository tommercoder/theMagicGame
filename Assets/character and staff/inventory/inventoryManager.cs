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
    Inventory inventory;
    public Transform itemsParent;
    Slot[] slots;
    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCalled += updateUI;

        slots = itemsParent.GetComponentsInChildren<Slot>();

    }
    void Update()
    {
        //opening inventory
        if (Input.GetKeyDown(KeyCode.I)) {
            openInventory();
        }

        
    }

    void updateUI()
    {
        Debug.Log("Updating UI");
        for(int i = 0; i < slots.Length;i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].add(inventory.items[i]);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
    }
    public void openInventory()
    {
        inventoryUi.SetActive(!inventoryUi.activeSelf);
        inventoryOpened = true;
    }
    //showing pickup panel
    public void showPanel(string text)
    {

        textPanel.gameObject.SetActive(true);
        textPanel.text = text;
    }
    //closing pickup panel
    public void hidePanel()
    {
        
        textPanel.gameObject.SetActive(false);
       
    }
}
