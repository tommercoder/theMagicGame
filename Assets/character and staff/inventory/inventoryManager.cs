using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this is script is attached to UI/ui manager
/// </summary>
public class inventoryManager : MonoBehaviour
{
    #region Singleton
    public static inventoryManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public bool inventoryOpened = false;
    public GameObject inventoryUi;
    public Text textPanel;
    public Text damageText;
    Inventory inventory;
    public Transform itemsParent;
    public tooltipManager tooltipManager;
    Slot[] slots;
    private void Start()
    {
        inventory = Inventory.instance;
        //updating ui
        inventory.onItemChangedCalled += updateUI;

        slots = itemsParent.GetComponentsInChildren<Slot>();

    }
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
           
        //}
        ////opening inventory
        //if (inventoryOpened)
        //{
        //    if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        openInventory();
                
        //    }
        //}
        //else
        if (Input.GetKeyDown(KeyCode.I)) {
            openInventory();
        }
       
        
    }

    void updateUI()
    {
        //Debug.Log("Updating UI");
        damageText.text = "";
        damageText.text = "current damage is: " + weaponInteract.instance.damage;
        for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].add(inventory.items[i]);
                    ///
                    
                    
                    //slots[i].countText.text = slots[i].itemsInSlot.Count.ToString();
                }
                else
                {
                    slots[i].clearSlot();
                }
            }
        }
        
    public void openInventory()
    {
        damageText.text = "";
        damageText.text = "current damage is: " + weaponInteract.instance.damage;
        inventoryUi.SetActive(!inventoryUi.activeSelf);
        if (inventoryOpened)
        {
                
            inventoryOpened = false;
            tooltipManager.hideTooltip();
            
            
        }
        else if (!inventoryOpened)
        {
            inventoryOpened = true;

        }
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
