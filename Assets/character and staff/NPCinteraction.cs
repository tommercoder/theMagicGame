using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    public Quest quest;
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

            if(quest!=null && characterStats.instance.loadCompleted && quest.title!="")
            {
                Debug.Log("QUEST NOT EQUAL TO NULL");
                FindObjectOfType<DialogueManager>().questBool = true;
                logShow.instance.showText("before ending of dialog you will be able to get quest");
            }
            dialogHappening = true;
            textName.GetComponent<TextMesh>().text = dialogue.name;

            


        }
        if (quest != null)
        {
            if (FindObjectOfType<DialogueManager>().questUI.activeSelf)
            {
                FindObjectOfType<DialogueManager>().handleQuest(quest);
                
            }
        }
        //if i already got quest from this npc then i need to clear it
        if (quest != null)
        {
            if (quest.isActive)
            {
                quest = null;
            }
        }
        //for movement enabling camera brain
        if (!interacting && !DialogueManager.instance.dialogBOX.activeSelf)
        {
            dialogHappening = false;
            
        }
       
        //floating name and text
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


    //questing
    
    
    

  


}
