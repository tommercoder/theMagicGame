using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this script is in UI/ui manager/health bar
/// </summary>
public class healthBarController : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void setHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        
    }
   
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);

    }
}
