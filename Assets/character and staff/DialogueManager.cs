using System.Collections;
using System.Collections.Generic;
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
    

    private void Start()
    {
        sentences = new Queue<string>();
        sentences2 = new Queue<string>();

        answer1G.SetActive(false);
        answer2G.SetActive(false);
        answerByeG.SetActive(false);


     

    }


    public void StartDialog(Dialogue dialogue)
    {
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
        StartCoroutine(type(goodbye));
        StartCoroutine(waitEnd());
        answerByeG.SetActive(false);
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
        yield return new WaitForSeconds(1f);
        EndDialogue();
    }
    public void EndDialogue()
    {
        anim.SetBool("dialogOpen", false);
        NPCinteraction.instance.dialogHappening = false;
       
        //NPCinteraction.instance.dialogEnded = true;

    }
}
