using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerFireBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            fireballAbility.instance.triggered = true;

            //make other.getComponent<HP>()-=damage;
        }
    }
}
