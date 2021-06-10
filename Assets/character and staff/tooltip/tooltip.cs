using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tooltip : MonoBehaviour
{
    public Text nameUI;
    public TextMeshProUGUI whatDoUI;
    public Image itemIconUI;
    public TextMeshProUGUI descriptionUI;
    public int charLimit;

    public void setInfo(string name,string whatDo, Sprite icon,string desription)
    {
        nameUI.text = name;
        whatDoUI.text = whatDo;
        itemIconUI.sprite = icon;
        descriptionUI.text = desription;
    }
}
