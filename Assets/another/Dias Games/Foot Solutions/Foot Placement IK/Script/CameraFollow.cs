using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform rig;
    [SerializeField] private float turnSpeed = 1.5f;

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.Rotate(0, x * turnSpeed, 0);
        rig.Rotate(-y * turnSpeed, 0, 0);

        transform.position = target.position;
    }
}
