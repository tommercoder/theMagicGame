using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zones : MonoBehaviour
{
    public static zones instance;
    private void Awake()
    {
        instance = this;
    }
    public bool isColliding;
    private void OnTriggerEnter(Collider other)
    {
        if (isColliding)
            return;
        
        if (other.CompareTag("air zone forest"))
        {
            isColliding = true;
            logShow.instance.showQuestText("YOU ARE IN AIR FOREST");
        }
        else if (other.CompareTag("earthZone"))
        {
            isColliding = true;
            logShow.instance.showQuestText("YOU ARE IN EARTH ZONE");
        }
       else if (other.CompareTag("lakeOfHealth"))
        {
            isColliding = true;
            logShow.instance.showQuestText("YOU ARE IN HEALTH LAKE ZONE");
        }
       else if (other.CompareTag("fireZone"))
        {
            isColliding = true;
            logShow.instance.showQuestText("YOU ARE IN FIRE ZONE");
        }
      else  if (other.CompareTag("waterZone"))
        {
            isColliding = true;
            logShow.instance.showQuestText("YOU ARE IN WATER ZONE");
        }
        else
            logShow.instance.showQuestText("");


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("air zone forest"))
        {
            isColliding = false;
        }
        if (other.CompareTag("earthZone"))
        {
            isColliding = false;
        }
        if (other.CompareTag("lakeOfHealth"))
        {
            isColliding = false;
        }
        if (other.CompareTag("fireZone"))
        {
            isColliding = false;
        }
        if (other.CompareTag("waterZone"))
        {
            isColliding = false;
        }
    }

}
