using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class respawnScript : MonoBehaviour,ISaveable
{
    #region singleton
    public static respawnScript instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject checkPoint;
    public GameObject player;
    public Text text;
 
    public CanvasGroup canvas;
    public GameObject abilitiesUI;
    public GameObject healthbarUI;
    

    // Update is called once per frame
    void Update()
    {
        
        
       
    }
    public void respawn()
    {
        if (playerHealth.instance.isPlayerDead)
        {
            
            //characterStats.instance.LoadJsonData(characterStats.instance);
            playerHealth.instance.currentHealth = playerHealth.instance.health;
            playerHealth.instance.isPlayerDead = false;
           


            
            while (canvas.alpha != 0)
            {
                
                canvas.alpha -= Time.deltaTime / 5;
                text.text = " ";
                playerHealth.instance.textSTR = " ";
                if (canvas.alpha < 0.3f)
                {
                    //canvas.enabled = false;
                    abilitiesUI.SetActive(true);
                    healthbarUI.SetActive(true);
                 
                }

            }
            player.transform.position = checkPoint.transform.position;
            playerHealth.instance.RagdollActive(false);


            foreach (GameObject b in playerHealth.instance.swordRigidbody)
            {
                if (b.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody rb = b.GetComponent<Rigidbody>();
                    rb.detectCollisions = true;
                }

            }
        }
    }
    public void PopulateSaveData(SaveData sd)
    {
        if(checkPoint!=null)
        sd.s_respawnObject = checkPoint;
    }
    //interface method
    public void LoadFromSaveData(SaveData sd)
    {
        if(sd.s_respawnObject!=null)
        checkPoint = sd.s_respawnObject;
    }

}

