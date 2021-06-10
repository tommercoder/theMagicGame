using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tooltipManager : MonoBehaviour
{
    public static tooltipManager instance;
    public tooltip tooltip;
    private void Awake()
    {
        instance = this;
    }

    public  void showTooltip(string name,string whatDo, Sprite icon,string description)
    {
       tooltip.setInfo(name, whatDo, icon, description);
       tooltip.gameObject.SetActive(true);
    }
    public  void hideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }
}
