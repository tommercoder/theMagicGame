using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class DontMoveWithParent : MonoBehaviour
{
    public Vector3 savedPosition;

    
    public bool dontMoveWithParent = true;
    bool lastDontMoveWithParent = true;

    public void Update()
    {
        
        if (transform.hasChanged  && savedPosition != transform.position)
        {
            //zapisuje pozycje objektu
            savedPosition = transform.position;
            transform.hasChanged = false;
        }

        if (!lastDontMoveWithParent && dontMoveWithParent)
            savedPosition = transform.position;

        lastDontMoveWithParent = dontMoveWithParent;
    }

    public void LateUpdate()
    {
        if (dontMoveWithParent)
        {
            if (savedPosition == Vector3.zero)
            {
                savedPosition = transform.position;
            }

            if (transform.parent.hasChanged)
            {
                //wstawia tą pozycje z powrotem w objekt
                transform.position = savedPosition;
                transform.parent.hasChanged = false;
            }
        }
    }
}