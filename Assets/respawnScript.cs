using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class respawnScript : MonoBehaviour
{
    public GameObject checkPoint;
    public GameObject player;
    public Text text;
 
    public CanvasGroup canvas;
    public GameObject abilitiesUI;
    public GameObject healthbarUI;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
       
    }
    public void respawn()
    {
        if (playerHealth.instance.isPlayerDead)
        {
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
            
        }
    }
    

}

