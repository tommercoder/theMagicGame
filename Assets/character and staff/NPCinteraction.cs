using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class NPCinteraction : Interact
{

    public static NPCinteraction instance;
    private void Awake()
    {
        instance = this;
    }
    public Camera camera;
     bool dialogEnded;
    public bool dialogHappening;

 




   
    public Dialogue dialogue;

    private void Start()
    {
        
        camera = Camera.main;
        dialogEnded = false;
        dialogHappening = false;
    }
    public override void InteractWith()
    {

        ///Debug.Log("was called npc interact");
        resetText();
        InteractedText += "speak with " + dialogue.name;
        interacting = true;
        //start dialog

    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.E) && interacting) //&& !dialogEnded)
        {
            if (!dialogHappening)
            {
                camera.GetComponent<CinemachineBrain>().enabled = false;
                inventoryManager.instance.hidePanel();
                resetText();

                //dialogue
                FindObjectOfType<DialogueManager>().StartDialog(dialogue);
            }
            
            //dialogEnded = false;
            dialogHappening = true;
        }
        //for movement enabling camera brain
        if(!interacting)
        {
            dialogHappening = false;
           // dialogEnded = false;
        }
        //if(!dialogHappening && interacting)
        //{
        //    InteractWith();
        //    PlayerInteraction.panelShow.showPanel(InteractedText);
            
        //}
        
    }

   
}
