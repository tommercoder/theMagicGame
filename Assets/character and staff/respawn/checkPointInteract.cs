using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointInteract : Interact
{
    public respawnScript respawnScript;
    public inventoryManager manager;
    private void Awake()
    {
        respawnScript = GameObject.Find("MainElementsHandler").GetComponent<respawnScript>();
       
    }
    public override void InteractWith()
    {
        //w przypadku jeśli ten objekt nie jest równy objektowi aktualnemu
        if (this.gameObject != respawnScript.checkPoint)
        {
            resetText();
            InteractedText += "Set new spawn point";
            interacting = true;
        }
        
    }

    public void Update()
    {
      
        if(Input.GetKey(KeyCode.E) && interacting)
        {
            setCheckPoint();
        }
    }
    void setCheckPoint()
    {
       //wstawia nowy checkpoint
       respawnScript.checkPoint = this.gameObject;
        var col = this.gameObject.GetComponentInChildren<ParticleSystem>().colorOverLifetime;
        Gradient grad = new Gradient();
        //wstawia kolor ParticleSystem na inny kolor
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        col.color = grad;
        manager.hidePanel();
        resetText();
        interacting = false;
    }
}
