using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zones : MonoBehaviour
{
    bool isColliding;
    private void OnTriggerEnter(Collider other)
    {
        if (isColliding)
            return;
        isColliding = true;
        if (other.CompareTag("air zone forest"))
        {
            logShow.instance.showQuestText("YOU ENTERED AIR FOREST");
        }
       if (other.CompareTag("earthZone"))
        {
            logShow.instance.showQuestText("YOU ENTERED EARTH ZONE");
        }
      if (other.CompareTag("lakeOfHealth"))
        {
            logShow.instance.showQuestText("YOU ENTERED HEALTH LAKE ZONE");
        }
         if (other.CompareTag("fireZone"))
        {
            logShow.instance.showQuestText("YOU ENTERED FIRE ZONE");
        }
        if (other.CompareTag("waterZone"))
        {
            logShow.instance.showQuestText("YOU ENTERED WATER ZONE");
        }
      
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
