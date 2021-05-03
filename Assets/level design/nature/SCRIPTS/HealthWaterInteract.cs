using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthWaterInteract : Interact
{
    bool restoring = false;
    public override void InteractWith()
    {
        resetText();
        InteractedText += "restore health";
        interacting = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(!restoring)
        {
            StopAllCoroutines();
        }
        if(Input.GetKeyDown(KeyCode.E) && interacting)
        {
            if (!restoring)
            {
                
                StartCoroutine(restoreHealth());
            }
        }
    }

    IEnumerator restoreHealth()
    {
        if (playerHealth.instance.currentHealth < 100)
        {
            restoring = true;
            while (true)
            { 
                if (playerHealth.instance.currentHealth < 100)
                { 
                    playerHealth.instance.currentHealth += 1; 
                    yield return new WaitForSeconds(1);
                }
                else
                { //
                    logShow.instance.showText("you are now fresh");

                    restoring = false;
                    yield return null;
                }
            }
           

            
        }
    }
    
}
