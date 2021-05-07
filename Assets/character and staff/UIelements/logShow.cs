using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logShow : MonoBehaviour
{
    public static logShow instance;
    private void Awake()
    {
        instance = this;
    }
    public Text text;
    public Text questBigText;

    public void showQuestText(string text)
    {
        questBigText.text = text;
        StartCoroutine(waitToHide1());
    }
    public void showText(string textSTR)
    {
        text.text = textSTR;
        StartCoroutine(waitToHide());
    }
    IEnumerator waitToHide1()
    {
        yield return new WaitForSeconds(4f);
        questBigText.text = " ";

    }
    IEnumerator waitToHide()
    {
        yield return new WaitForSeconds(2f);
        text.text = " ";

    }
}
