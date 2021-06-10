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
            playerHealth.instance.minusXP = false;
            //wraca do standardowej animacji
            attacksController.instance.animator.SetInteger("attackAnimation", 4);
            movement.instance.canMove = true;
            attacksController.instance.canClick = true;
            attacksController.instance.canClickSec = true;
            attacksController.instance.noOfClick = 0;
            attacksController.instance.noOfClickSecond = 0;
            //wstawia health = 100
            playerHealth.instance.currentHealth = playerHealth.instance.health;
            playerHealth.instance.isPlayerDead = false;
           //odnawia alpha ui elementu
            while (canvas.alpha != 0)
            {      
                canvas.alpha -= Time.deltaTime / 5;
                text.text = " ";
                playerHealth.instance.textSTR = " ";
                if (canvas.alpha < 0.3f)
                { 
                    abilitiesUI.SetActive(true);
                    healthbarUI.SetActive(true);      
                }
            }
            player.transform.position = checkPoint.transform.position;
            //wyłącza Ragdoll
            playerHealth.instance.RagdollActive(false);
            playerSword.instance.currentSwordGameObject.GetComponent<Rigidbody>().detectCollisions = true;
           
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

