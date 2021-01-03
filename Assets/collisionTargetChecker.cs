using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionTargetChecker : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        this.transform.parent.position = other.transform.position + (this.transform.parent.position - this.transform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
