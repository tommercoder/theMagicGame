using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
/// <summary>
/// this is script is attached to UI/ui manager
/// </summary>
public class inventoryManager : MonoBehaviour//,ISaveable
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
    public Slot[] slots;
    public void Start()
    {
        inventory = Inventory.instance;
        slots = itemsParent.GetComponentsInChildren<Slot>();
        //updating ui
        inventory.onItemChangedCalled += updateUI;  
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !pauseMenu.instance.pauseOpened && !pauseMenu.instance.menuIsOpened) {
            openInventory();
        }
    }
    public void updateUI()
    {
        
            Debug.Log("Updating UI");
            damageText.text = "";
            damageText.text = "current damage is: " + playerSword.instance.currentSword.swordDamage;
       
        
        for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
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
        damageText.text = "";
        damageText.text = "current damage is: " + playerSword.instance.currentSword.swordDamage;
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
