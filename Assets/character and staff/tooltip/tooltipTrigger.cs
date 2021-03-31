using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string name;
    public string whatDo;
    public Sprite icon;
    public string description;

    private void Update()
    {
        // if (gameObject.GetComponent<Slot>().onSlotChangedCalled!=null)
        //{
        //otrzymuje dane z slotu na ktorym jest myszka
        if (gameObject.GetComponent<Slot>().item != null)
        {
            name = gameObject.GetComponent<Slot>().item.name;
            whatDo = gameObject.GetComponent<Slot>().item.option;
            //maybe item has no description
            if (gameObject.GetComponent<Slot>().item.description != null)
            {
                description = gameObject.GetComponent<Slot>().item.description;
            }
            icon = gameObject.GetComponent<Slot>().item.icon;
            //Debug.Log("item.icon " + gameObject.GetComponent<Slot>().item.icon);
            //}
        }
        
        if (!inventoryManager.instance.inventoryOpened)
            tooltipManager.hideTooltip();
    }
    //what we pointed with mouse
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if(gameObject.GetComponent<Slot>().item!=null )//check if there is item in slot
        tooltipManager.showTooltip(name,whatDo,icon,description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipManager.hideTooltip();
    }
}
