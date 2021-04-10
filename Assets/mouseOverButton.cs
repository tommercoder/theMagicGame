using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mouseOverButton : MonoBehaviour
{
    public Button a;
    public Button b;
    public Button c;
   public void changeColorTextA()
    {
        a.GetComponentInChildren<Text>().color = Color.blue;
    }
    public void changeColorTextAback()
    {
        a.GetComponentInChildren<Text>().color = Color.white;
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
    public float delayBeforeLoading = 10f;
    public bool LoadingGame = false;
    public string name = "game";
    public float timeElapsed;
    public Camera cameraMenu;
    public Camera cameraGame;
    public GameObject menuCanvas;
    public GameObject mainCanvas;
    private void Start()
    {
        mainCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
    private void Update()
    {
        //timeElapsed += Time.deltaTime;
        //if (timeElapsed > delayBeforeLoading)
        //{
        //    
        //}
        //else
        //    LoadingGame = false;

    }
    public IEnumerator loadingBar()
    {
        //AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        //while (!operation.isDone)
        //{
        //    float progress = Mathf.Clamp01(operation.progress / .9f);

        //    yield return null;
        //}
        //show loading
        yield return null;
        
    }
    public void openGame()
    {
        menuCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        cameraMenu.enabled = false;
        cameraGame.enabled = true;
        //StartCoroutine(loadingBar());

        //LoadingGame = true;
        /// SceneManager.LoadScene("game");
        //LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //characterStats.instance.LoadJsonData(characterStats.instance);
    }
    public void closeGame()
    {
        Application.Quit();
    }
}
