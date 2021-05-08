using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroing : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            
            Destroy(gameObject);
        }
        else
            StartCoroutine(destruction());
      
          

    }
    IEnumerator destruction()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
