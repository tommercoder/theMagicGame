using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Linq;

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

    List<NPCinteraction> allnpc = new List<NPCinteraction>();
    public Quest quest;
    private void Start()
    {
        allnpc = GameObject.FindObjectsOfType<NPCinteraction>().ToList();
        player = GameObject.Find("character");
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

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
            for (int i = 0; i < allnpc.Count; i++)
            {
                if (allnpc[i].interacting)
                {
                    if (!allnpc[i].dialogHappening)
                    {
                        Debug.Log("allnpc[i]" + allnpc[i]);
                        camera.GetComponent<CinemachineBrain>().enabled = false;
                        inventoryManager.instance.hidePanel();
                        resetText();

                        //dialogue

                        FindObjectOfType<DialogueManager>().StartDialog(dialogue,allnpc[i]);
                    }
                }
            }

            if (quest != null && characterStats.instance.loadCompleted && quest.title != "")
            {
               // Debug.Log("QUEST NOT EQUAL TO NULL");
                FindObjectOfType<DialogueManager>().questBool = true;
                logShow.instance.showText("before ending of dialog you will be able to get the quest");
            }
            // dialogHappening = true;
            textName.GetComponent<TextMesh>().text = dialogue.name;

            for (int i = 0; i < allnpc.Count; i++)
            {
                if (allnpc[i].name == name && allnpc[i].interacting)
                {
                    allnpc[i].dialogHappening = true;
                }
            }


        }
        for (int i = 0; i < allnpc.Count; i++)
        {
            if (allnpc[i].quest != null)
            {
                if (FindObjectOfType<DialogueManager>().questUI.activeSelf)
                {
                    FindObjectOfType<DialogueManager>().handleQuest(quest);

                }
            }
        }
        //if i already got quest from this npc then i need to clear it
        for (int i = 0; i < allnpc.Count; i++)
        {
            if (allnpc[i].quest != null)
            {

                if (allnpc[i].quest.isActive)
                {
                    allnpc[i].quest = null;
                }
            }
        }
        //for movement enabling camera brain
        if (!interacting && !DialogueManager.instance.dialogBOX.activeSelf)
        {
            for (int i = 0; i < allnpc.Count; i++)
            {
                allnpc[i].dialogHappening = false;
            }


        }

        //floating name and text
        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            if (!dialogHappening)
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
            textName.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position);
            textName.transform.Rotate(0, 180, 0);
        }
    }


    //questing







}
