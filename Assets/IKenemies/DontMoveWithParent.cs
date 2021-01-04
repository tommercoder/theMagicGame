using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class DontMoveWithParent : MonoBehaviour
{
    public Vector3 savedPosition;

    [Tooltip("When DontMoveWithParent is on, Ctrl+Z doesn't work for movement changes on this GameObject.")]
    public bool dontMoveWithParent = true;
    bool lastDontMoveWithParent = true;

    Vector3 parentLastPos;

    private void Update()
    {
        
        if (transform.hasChanged  && savedPosition != transform.position)
        {
            
            savedPosition = transform.position;
            transform.hasChanged = false;
        }

        if (!lastDontMoveWithParent && dontMoveWithParent)
            savedPosition = transform.position;

        lastDontMoveWithParent = dontMoveWithParent;
    }

    private void LateUpdate()
    {
        if (dontMoveWithParent)
        {
            if (savedPosition == Vector3.zero)
            {
                savedPosition = transform.position;
            }

            if (transform.parent.hasChanged)
            {
                transform.position = savedPosition;
                transform.parent.hasChanged = false;
            }
        }
    }
}