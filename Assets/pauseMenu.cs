using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void Start()
    {
        cameraGame.enabled = false;
        cameraMenu.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
        ui.SetActive(false);
        Time.timeScale = 1f;
        pauseOpened = false;
    }
    public void loadMenu()
    {
        Time.timeScale = 1f;
        pauseOpened = false;
        //SceneManager.UnloadSceneAsync("game");'

        //#if UNITY_EDITOR
        //        UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //        Application.Quit();
        //#endif
        mainCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        cameraMenu.enabled = true;

        if(pauseCanvas.activeSelf)
        {
            pauseCanvas.SetActive(false);
        }
        //SceneManager.LoadScene("main menu");
        //characterStats.instance.SaveJsonData(characterStats.instance);
    }


}
