using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator anim;
    public GameObject dialogBOX;
    private void Start()
    {
        sentences = new Queue<string>();
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
        DisplayNext();
    }
    public void DisplayNext()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(type(sentence));
    }
    IEnumerator type(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        anim.SetBool("dialogOpen", false);
        NPCinteraction.instance.dialogHappening = false;
        //NPCinteraction.instance.dialogEnded = true;

    }
}
