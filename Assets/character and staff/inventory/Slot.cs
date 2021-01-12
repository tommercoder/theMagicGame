using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public void add(Item item)
    {
        this.item = item;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void clearSlot()
    {
        Debug.Log("clear slot wass called");
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
