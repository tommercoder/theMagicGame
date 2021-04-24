using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class mouseOverButton : MonoBehaviour
{
    public static mouseOverButton instance;
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
    //public float delayBeforeLoading = 10f;
    public bool LoadingGame = false;
    public bool startedNewGame = false;
    //public string name = "game";
   // public float timeElapsed;
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
        yield return new WaitForSeconds(1f);
        changeColorTextAback();
        menuCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        cameraMenu.enabled = false;
        cameraGame.enabled = true;
        pauseMenu.instance.menuIsOpened = false;

    }
    public void openGame()
    {
        am.Play("menuClick");
        
        StartCoroutine(loadingBar());

        //LoadingGame = true;
        /// SceneManager.LoadScene("game");
        //LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //characterStats.instance.LoadJsonData(characterStats.instance);
    }
    public void closeGame()
    {
        am.Play("menuClick");
        Application.Quit();
    }
    //public void newGame()
    //{
    //    var fullPath = Path.Combine(Application.persistentDataPath, "SaveData.dat");
    //    var fullPath2= Path.Combine(Application.persistentDataPath, "newGame.dat");
    //    if (File.Exists(fullPath))
    //    if(File.ReadAllText(fullPath)!="")
    //    if (FileManager.ClearSaveData("SaveData.dat"))
    //    {
    //                File.WriteAllText(fullPath, File.ReadAllText(fullPath2));
    //                Debug.Log(File.ReadAllText(fullPath2));
    //                //if (FileManager.LoadFromFile("newGame.dat", out var json))
    //                //{
    //                //    SaveData sd = new SaveData();

    //                //    sd.LoadFromJson(json);

    //                //    characterStats.instance.LoadFromSaveData(sd);
    //                //    Debug.Log("Load complete");
    //                //    characterStats.instance.loadCompleted = true;
    //                //}
    //                //Application.Quit();
    //                //startedNewGame = true;
                    
    //    }

    //}
}
