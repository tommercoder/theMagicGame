using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    #region Singleton
    public static ItemDrag instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("instance itemGrag.cs");
            return;
        }

        instance = this;
    }

    #endregion
    public Item item = null;
    public void OnDrag(PointerEventData eventData)
    {
      
        item = transform.GetComponentInParent<Slot>().item;
        //item.name = transform.GetComponentInParent<Slot>().item.name;
        transform.position = Input.mousePosition;
       
        //Debug.Log("dragging " + item);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        //item = null;
    }
}
