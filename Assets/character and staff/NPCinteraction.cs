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
    
    public bool dialogHappening;
    public GameObject textName;
    private GameObject player;

    public Dialogue dialogue;

    private void Start()
    {
        player = GameObject.Find("character");
        camera = Camera.main;
        
        dialogHappening = false;

    }
    public override void InteractWith()
    {

        
        resetText();
        InteractedText += "speak with " + dialogue.name;
        interacting = true;
        

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && (interacting || DialogueManager.instance.dialogBOX.activeSelf)) //&& !dialogEnded)
        {
            if (!dialogHappening)
            {
                camera.GetComponent<CinemachineBrain>().enabled = false;
                inventoryManager.instance.hidePanel();
                resetText();

                //dialogue
                FindObjectOfType<DialogueManager>().StartDialog(dialogue);
            }

            
            dialogHappening = true;
            textName.GetComponent<TextMesh>().text = dialogue.name;
        }

        //for movement enabling camera brain
        if (!interacting && !DialogueManager.instance.dialogBOX.activeSelf)
        {
            dialogHappening = false;
            
        }
       

        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            if(!dialogHappening)
            textName.GetComponent<TextMesh>().text = dialogue.uiText;
            else
                textName.GetComponent<TextMesh>().text = dialogue.name;
        }
        else
        {
            textName.GetComponent<TextMesh>().text = dialogue.name;
        }

        if (textName != null)
        {
            textName.transform.LookAt(Camera.main.transform.position);
            textName.transform.Rotate(0, 180, 0);
        }
    }


}
