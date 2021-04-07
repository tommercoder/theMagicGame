using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarieleQuest : MonoBehaviour
{
    public static MarieleQuest instance;
    private void Awake()
    {
        instance = this;
    }
    public Quest currentMarieleQuest;

    public GameObject questWindow;
    public Text title;
    public Text description;
    public Text reward;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
           // questWindow.SetActive(!questWindow.activeSelf);
            if (questWindow.activeSelf)
            {
                questWindow.SetActive(false);
            }
            else
            {
                questWindow.SetActive(true);
                title.text = currentMarieleQuest.title;
                description.text = currentMarieleQuest.description;
                reward.text = currentMarieleQuest.rewardText;
            }
        }
    }
    public void clearPWindow()
    {
        title.text = " ";
        description.text = " ";
        reward.text = " ";
    }
}
