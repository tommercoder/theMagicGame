using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroing : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            //TODO: damage
            Destroy(gameObject);
        }
        else
            StartCoroutine(destruction());
            //Destroy(gameObject, 2f);
        
        
        //Instantiate(explosion, transform.position, transform.rotation);
          

    }
    IEnumerator destruction()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
