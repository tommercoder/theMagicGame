using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tooltipManager : MonoBehaviour
{
    private static tooltipManager instance;
    public tooltip tooltip;
    private void Awake()
    {
        instance = this;
    }

    public static void showTooltip(string name,string whatDo, Sprite icon,string description)
    {
        instance.tooltip.setInfo(name, whatDo, icon, description);
        instance.tooltip.gameObject.SetActive(true);
    }
    public static void hideTooltip()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
