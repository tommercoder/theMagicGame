using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class dropItem : MonoBehaviour, IDropHandler
{
    public Inventory inventory;
    int index = 0;
    public RectTransform panel;
    public void OnDrop(PointerEventData eventData)
    {
       // RectTransform panel = transform as RectTransform;
        //biore item ktory wzialem myszka recznie
        Item dropItem = gameObject.GetComponent<ItemDrag>().item;
        Debug.Log("DROP ITEM" + gameObject.GetComponent<ItemDrag>().item);
        if (!RectTransformUtility.RectangleContainsScreenPoint(panel,Input.mousePosition))
        {


            Debug.Log("entered");
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 20))
            {
               
                for (int i = 0; i < inventory.items.Count; i++)
                {
                   
                    if (inventory.items[i] == dropItem)
                    {
                        
                        index = i;
                        
                        break;
                    }
                }
                
                hit.point = new Vector3(hit.point.x,inventory.itemsGameObjects[index].transform.position.y, hit.point.z);//zmieniam hit point zeby otzymac "y" position objektu
                inventory.itemsGameObjects[index].SetActive(true);//robie objekt ponownie aktywnym
                inventory.itemsGameObjects[index].transform.position = hit.point;//wstawiam go na scene
                inventory.removeGOitem(inventory.itemsGameObjects[index]);//usuwam z pierwszej listy 
                inventory.removeItem(gameObject.GetComponentInParent<Slot>().item);//usuwam z drugiej listy

                //gameObject.GetComponentInChildren<Slot>().item.SetActive(true);
                // gameObject.GetComponentInChildren<Slot>().item.transform.position = hit.point;

                index = 0;
                //Debug.Log("dropping item with index " + index + "name " + inventory.itemsGameObjects[index].name);
            }
        }
    }
}
