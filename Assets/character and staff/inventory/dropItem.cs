using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class dropItem : MonoBehaviour, IDropHandler
{
    public Inventory inventory;
    int index = 0;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform panel = transform as RectTransform;
        for(int i = 0;i < inventory.items.Count;i++)
        {
            if(inventory.items[i]== gameObject.GetComponentInChildren<Slot>().item)
            {
                index = i;
                break;
            }
        }
        if(!RectTransformUtility.RectangleContainsScreenPoint(panel,Input.mousePosition))
        {
            Debug.Log("DROP ITEM");
            inventory.removeItem(gameObject.GetComponentInChildren<Slot>().item);
            
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                hit.point = new Vector3(hit.point.x,inventory.itemsGameObjects[index].transform.position.y, hit.point.z);
                inventory.itemsGameObjects[index].SetActive(true);
                inventory.itemsGameObjects[index].transform.position = hit.point;
                inventory.removeGOitem(inventory.itemsGameObjects[index]);

                
               //gameObject.GetComponentInChildren<Slot>().item.SetActive(true);
               // gameObject.GetComponentInChildren<Slot>().item.transform.position = hit.point;

               index = 0;
            }
        }
    }
}
