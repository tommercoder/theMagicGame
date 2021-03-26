﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointInteract : Interact
{
    public respawnScript respawnScript;
    public inventoryManager manager;
    private void Awake()
    {
        respawnScript = GameObject.Find("MainElements").GetComponent<respawnScript>();
       
    }
    public override void InteractWith()
    {
        if (this.gameObject != respawnScript.checkPoint)
        {

            resetText();
            InteractedText += "Set new spawn point";
            interacting = true;
        }
    }

    private void Update()
    {
        //Debug.Log(this.gameObject + " " + respawnScript.checkPoint);
        if(Input.GetKey(KeyCode.E) && interacting)
        {
            setCheckPoint();
        }
    }
    void setCheckPoint()
    {
       
       respawnScript.checkPoint = this.gameObject;


        var col = this.gameObject.GetComponentInChildren<ParticleSystem>().colorOverLifetime;
        //col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        col.color = grad;
        manager.hidePanel();
        resetText();
        interacting = false;
    }
}