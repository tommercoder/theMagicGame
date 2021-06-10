using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideHandler : MonoBehaviour
{
    public GameObject menuUiHide;
    public GameObject guideUiShow;
    public void openGuide()
    {
        FindObjectOfType<audioManager>().Play("menuClick");
        MainMenuHandler.instance.changeColorTextBback();
        menuUiHide.SetActive(false);
        guideUiShow.SetActive(true);
    }
    public void closeGuide()
    {
        FindObjectOfType<audioManager>().Play("menuClick");
        menuUiHide.SetActive(true);
        guideUiShow.SetActive(false);
        
    }
}
