using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


public class dropItem : MonoBehaviour, IDropHandler
{
    public Inventory inventory;
    int index = 0;
    public RectTransform panel;
    public List<potionInteraction> temp = new List<potionInteraction>();
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
               // Vector3 position = Inventory.instance.itemsGameObjects[index].transform.position;
                Inventory.instance.itemsGameObjects[index].transform.rotation = Quaternion.identity;
              //  position.y = swordEquipping.instance.savedPosition.y;
               // Inventory.instance.itemsGameObjects[index].transform.position = position;



                hit.point = new Vector3(hit.point.x,inventory.itemsGameObjects[index].transform.position.y, hit.point.z);//zmieniam hit point zeby otzymac "y" position objektu
                inventory.itemsGameObjects[index].SetActive(true);//robie objekt ponownie aktywnym
                Inventory.instance.itemsGameObjects[index].GetComponent<FloatingItem>().Rotating = true;
                inventory.itemsGameObjects[index].transform.position = hit.point;//wstawiam go na scene
                //saving lists
                temp = GameObject.FindObjectsOfType<potionInteraction>().ToList();

                for (int k = 0; k < temp.Count; k++)
                {
                    if (temp[k].gameObject == Inventory.instance.itemsGameObjects[index])
                    {
                        temp[k].isUsed = false;
                        //Debug.Log("LOG" + temp);
                    }
                }
                //characterStats.instance.allPotionInteractionO[index].isUsed = false;
                // characterStats.instance.allPotionsIsUsed[index] = false;
                ////
                inventory.removeGOitem(inventory.itemsGameObjects[index]);//usuwam z pierwszej listy 
                inventory.removeItem(gameObject.GetComponentInParent<Slot>().item);//usuwam z drugiej listy
                    
                
                

                index = 0;
                
            }
        }
    }
}
