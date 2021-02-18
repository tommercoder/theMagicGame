using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProjectile : MonoBehaviour
{
    public float timeToDestroy;
    public int damage;
   
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            playerHealth.instance.currentHealth -= damage;
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
