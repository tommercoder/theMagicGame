using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler instance;
    private void Awake()
    {
        instance = this;
    }
    public Button a;
    public Button b;
    public Button c;
    public Button d;
   public audioManager am;
   public void changeColorTextA()
    {
        a.GetComponentInChildren<Text>().color = Color.blue;
    }
    public void changeColorTextAback()
    {
        a.GetComponentInChildren<Text>().color = Color.white;
    }
    public void changeColorTextD()
    {
        d.GetComponentInChildren<Text>().color = Color.blue;
    }
    public void changeColorTextDback()
    {
        d.GetComponentInChildren<Text>().color = Color.white;
    }
    public void changeColorTextB()
    {
        b.GetComponentInChildren<Text>().color = Color.blue;
    }
    public void changeColorTextBback()
    {
        b.GetComponentInChildren<Text>().color = Color.white;
    }
    public void changeColorTextC()
    {
        c.GetComponentInChildren<Text>().color = Color.blue;
    }
    public void changeColorTextCback()
    {
        c.GetComponentInChildren<Text>().color = Color.white;
    }
   
    public bool LoadingGame = false;
    public bool startedNewGame = false;
   
    public Camera cameraMenu;
    public Camera cameraGame;
    public GameObject menuCanvas;
    public GameObject mainCanvas;

    public GameObject DisableMenuCompletely;
    private void Start()
    {
        mainCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
   
    public IEnumerator loadingBar()
    {
        
        //show loading
        yield return new WaitForSeconds(1f);
        changeColorTextAback();
        menuCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        cameraMenu.enabled = false;
        cameraGame.enabled = true;
        pauseMenu.instance.menuIsOpened = false;
        am.Play("main theme");
        DisableMenuCompletely.SetActive(false);

    }
    public void openGame()
    {
        am.Play("menuClick");
        am.Stop("main menu");
        StartCoroutine(loadingBar());
        
        
    }
    public void closeGame()
    {
        am.Play("menuClick");
        Application.Quit();
    }
    
}
