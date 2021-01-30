using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inventoryManager.instance.inventoryOpened)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }
}
