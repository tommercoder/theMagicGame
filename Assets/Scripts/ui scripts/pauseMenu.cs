using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class pauseMenu : MonoBehaviour
{
    public static pauseMenu instance;
    private void Awake()
    {
        instance = this;

    }
    public bool pauseOpened;
    public GameObject ui;
    public Camera cameraMenu;
    public Camera cameraGame;
    public GameObject menuCanvas; 
    public GameObject mainCanvas;
    public GameObject pauseCanvas;
    public bool menuIsOpened;
    public CanvasGroup canvas;
    public GameObject dialogBox;

    public GameObject enableMenuCompletely;
    public void Start()
    {
        cameraGame.enabled = false;
        cameraMenu.enabled = true;
        menuIsOpened = true;
    }

    public void Update()
    {
        if(menuIsOpened)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>().enabled = false;
        }
        else if(!menuIsOpened && !inventoryManager.instance.inventoryOpened && !dialogBox.activeSelf)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>().enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !menuIsOpened)
        {
            if(pauseOpened)
            {
                close();
            }
            else
            {
                open();
            }
        }

      
    }
    public void open()
    {
        ui.SetActive(true);
        Time.timeScale = 0f;
        pauseOpened = true;
    }
    public void close()
    {
        FindObjectOfType<audioManager>().Play("menuClick");
        ui.SetActive(false);
        Time.timeScale = 1f;
        pauseOpened = false;
    }
    public void loadMenu()
    {
        FindObjectOfType<audioManager>().Play("menuClick");
        Time.timeScale = 1f;
        menuIsOpened = true;
        pauseOpened = false;
        
        mainCanvas.SetActive(false);
        menuCanvas.SetActive(true);

        enableMenuCompletely.SetActive(true);

        cameraMenu.enabled = true;

        if (inventoryManager.instance.inventoryOpened)
        {
            inventoryManager.instance.openInventory();
        }
        if(pauseCanvas.activeSelf)
        {
            pauseCanvas.SetActive(false);
        }
       
    }


}
