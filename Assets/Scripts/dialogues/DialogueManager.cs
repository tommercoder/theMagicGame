using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region singleton
    public static DialogueManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    private Queue<string> sentences;
    private Queue<string> sentences2;
    private string answer;
    private string answer2;
    private string answerBye;
    private string greeting;
    private string goodbye;
    public Text nameText;
    public Text dialogueText;
    public Animator anim;
    public GameObject dialogBOX;

    public GameObject answer1G;
    public GameObject answer2G;
    public GameObject answerByeG;
    public GameObject questButton;
    public bool questBool;
    public NPCinteraction npcWhoSpeaking;
    
    private void Start()
    {
        sentences = new Queue<string>();
        sentences2 = new Queue<string>();

        answer1G.SetActive(false);
        answer2G.SetActive(false);
        answerByeG.SetActive(false);

        


    }

    private void Update()
    {
        if (questBool)
        {
            if (dialogBOX.activeSelf && answer1G.activeSelf == false && answer2G.activeSelf == false)
            {
                Debug.Log("test1" + npcWhoSpeaking.quest.title);
                if (npcWhoSpeaking.quest!=null && npcWhoSpeaking.quest.title!="" )
                {
                   
                        Debug.Log("FORHANDLE != null");
                        Quest quest = MarieleQuest.instance.currentMarieleQuest;
                        if (quest != null)
                            if (quest.isActive && forhandleD.npcEnumName.ToString() == quest.npcEnumName.ToString())
                            {
                                quest.goal.SpokeToAnotherNPC();
                                if (quest.goal.isReached())
                                {
                                    //add reward to inventory
                                    characterStats.instance.XP += quest.XP;
                                    quest.complete();
                                }
                            }
                    



                    Debug.Log("test2" + npcWhoSpeaking.quest.title);
                    questButton.SetActive(true);
                    questBool = false;
                }
            }
        }

    }
    Dialogue forhandleD;
    public void StartDialog(Dialogue dialogue,NPCinteraction temp)
    {
        npcWhoSpeaking = temp;
        forhandleD = dialogue;
        Debug.Log("forhandleD" + forhandleD.npcEnumName);
        dialogBOX.SetActive(true);
        anim.SetBool("dialogOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        if (dialogue.sentences2.Length > 0)
        {
            foreach (string sentence2 in dialogue.sentences2)
            {
                sentences2.Enqueue(sentence2);
            }
        }
        if (dialogue.answer != "")
        {
            answer = dialogue.answer;
            answer1G.SetActive(true);
            answer1G.GetComponentInChildren<Text>().text = answer;
        }
        if (dialogue.answer2 != "")
        {
            answer2 = dialogue.answer2;
            answer2G.SetActive(true);
            answer2G.GetComponentInChildren<Text>().text = answer2;
        }
        else
        {
            answerByeG.transform.position = answer2G.transform.position;
        }


        greeting = dialogue.greeting;
        goodbye = dialogue.goodbye;

        answerBye = dialogue.answerBye;
        answerByeG.SetActive(true);
        answerByeG.GetComponentInChildren<Text>().text = answerBye;

        dialogueText.text = greeting;
        //greeting
    }
    public void DisplayNext()
    {
        //if (sentences.Count == 0 && sentences2.Count == 0 )
        //{

        //    EndDialogue();
        //    return;
        //}

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(type(sentence));

    }
    public void displayNext2()
    {
        string sentence2 = " ";
        if (sentences2.Count > 0)
        {
            sentence2 = sentences2.Dequeue();
        }

        StopAllCoroutines();
        StartCoroutine(type(sentence2));
    }
    public void answer1Click()
    {

        DisplayNext();
        answer1G.SetActive(false);
        
    }
    public void answer2Click()
    {

        displayNext2();
        answer2G.SetActive(false);
       
    }
    public void answerByeClick()
    {
        
        StopAllCoroutines();
        Quest quest = MarieleQuest.instance.currentMarieleQuest;
        //Debug.Log("A" + forhandleD.npcEnumName.ToString());
        // Debug.Log("B" + quest.npcEnumName.ToString());
        if(quest!=null)
        if (quest.isActive && forhandleD.npcEnumName.ToString() == quest.npcEnumName.ToString())
        {
            quest.goal.SpokeToAnotherNPC();
            if (quest.goal.isReached())
            {
                //add reward to inventory
                characterStats.instance.XP += quest.XP;
                quest.complete();
            }
        }
        StartCoroutine(type(goodbye));
        StartCoroutine(waitEnd());
        answerByeG.SetActive(false);
        questUI.SetActive(false);
    }
    IEnumerator type(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    IEnumerator waitEnd()
    {
        questBool = false;
        questButton.SetActive(false);
        if (answer1G.activeSelf)
        answer1G.SetActive(false);
        if(answer2G.activeSelf)
        answer2G.SetActive(false);

        yield return new WaitForSeconds(1f);
        EndDialogue();
    }
    public void EndDialogue()
    {
        anim.SetBool("dialogOpen", false);
        dialogBOX.SetActive(false);
        List<NPCinteraction> temp = new List<NPCinteraction>();
        temp = GameObject.FindObjectsOfType<NPCinteraction>().ToList();
        //for(int i = 0;i < temp.Count;i++)
        //{
        //    if(temp[i].interacting==true)
        //    {
        //        temp[i].dialogHappening = false;
        //    }    
        //}
        
        NPCinteraction.instance.dialogHappening = false;

        //NPCinteraction.instance.dialogEnded = true;

    }

    public GameObject questUI;
    public Text questTitle;
    public Text questDesc;
    public Text questReward;
    Quest forHandle;
    public  void openQuestWindow()
    {
        questUI.SetActive(true);
       
    }
    public void handleQuest(Quest quest,NPCinteraction npc)
    {
        questTitle.text = npc.quest.title;
        questDesc.text = npc.quest.description;
        questReward.text = npc.quest.rewardText;
        forHandle = npc.quest;
    }
    List<NPCinteraction> allNpc;
    public void acceptQuest()
    {
        
        if (string.IsNullOrEmpty(MarieleQuest.instance.currentMarieleQuest.title)/* && MarieleQuest.instance.currentMarieleQuest.isActive == false*/ )
        {
            questUI.SetActive(false);
            questButton.SetActive(false);

            EndDialogue();
            logShow.instance.showText("you got new quest,for details press P");
            forHandle.isActive = true;

            MarieleQuest.instance.currentMarieleQuest = forHandle;
            questBool = false;

            allNpc = FindObjectsOfType<NPCinteraction>().ToList();
            foreach(NPCinteraction n in allNpc)
            {
                if(n.quest == forHandle)
                {
                    n.quest = null;
                    forHandle = null;
                }
            }
           // forHandle = null;

        }
        else
        {
            logShow.instance.showText("You can have one quest at time only");
        }
    }
    public void declineQuest()
    {
        questUI.SetActive(false);
        questButton.SetActive(false);
        logShow.instance.showText("you can accept it later");
        questBool = false;
        EndDialogue();
    }
}
