using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class tooltip : MonoBehaviour
{
    public Text nameUI;
    public TextMeshProUGUI whatDoUI;
    public Image itemIconUI;
    public TextMeshProUGUI descriptionUI;
    //public LayoutElement layoutElement;

    public int charLimit;
    private void Update()
    {
        int whatDoLenth = whatDoUI.text.Length;
        int descLength = descriptionUI.text.Length;

        //layoutElement.enabled = (whatDoLenth > charLimit || descLength > charLimit) ? true : false;
    }
    public void setInfo(string name,string whatDo, Sprite icon,string desription)
    {
        nameUI.text = name;
        whatDoUI.text = whatDo;
        itemIconUI.sprite = icon;
        descriptionUI.text = desription;
    }
}
