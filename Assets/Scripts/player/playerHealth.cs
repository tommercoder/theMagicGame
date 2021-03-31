﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour,ISaveable
{
    #region Singleton
    public static playerHealth instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("instance playerHealth.cs");
            return;
        }
        colliders = GetComponentsInChildren<CapsuleCollider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        legScript = GetComponent<FootIK>();
        animator = GetComponent<Animator>();
        //collider = GetComponent<CapsuleCollider>();
        //rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        instance = this;
        textSTR = text.text;
        text.text = "";
        swordRigidbody = GameObject.FindGameObjectsWithTag("interactable object");//GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/" + playerSword.instance.currentSwordGameObject.name).GetComponent<Rigidbody>();
    }

    #endregion
    public CanvasGroup canvas;
    public Text text;
    public string textSTR;
    public int health = 100;
    public int currentHealth;
    public healthBarController healthBar;
    public bool isPlayerDead;
    //ragdoll 
    public Collider[] colliders;
    public Rigidbody[] rigidbodies;
    public FootIK legScript;
    public Animator animator;
   // public CapsuleCollider collider;
   // public Rigidbody rigidbody;
    public CharacterController controller;

    public GameObject abilitiesUI;
    public GameObject healthbarUI;
    public GameObject[] swordRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthBar.setMaxHealth(health);
        RagdollActive(false);


        //swordRigidbody.detectCollisions = true;
        foreach (GameObject b in swordRigidbody)
        {
            if (b.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = b.GetComponent<Rigidbody>();
                rb.detectCollisions = true;
            }

        }

    }
   
    // Update is called once per frame
    void Update()
    {
        
        healthBar.setHealth(currentHealth);
        //testing
        if(currentHealth <= 0)
        {
            Die();
        }
        else if(currentHealth > 0)
        {
            isPlayerDead = false;
        }
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    //test
        //    takeDamage(20);
            
        //}
    }
   
    public void Die()
    {
        Debug.Log("player is dead");
        isPlayerDead = true;
        
       // canvas.enabled = true;
        abilitiesUI.SetActive(false);
        healthbarUI.SetActive(false);
        canvas.alpha += Time.deltaTime / 2;
        RagdollActive(true);
        if (canvas.alpha ==1 && isPlayerDead)
            StartCoroutine(PlayText());
        else {
            StopCoroutine(PlayText());
                
            text.text = " ";
        }
       
        
    }

    public void RagdollActive(bool active)
    {

        controller.enabled = !active;

        //children
        foreach (var collider in colliders)
            collider.enabled = active;
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        //root
        
         legScript.enabled = !active;
        

        animator.enabled = !active;
        //rigidbody.detectCollisions = !active;
       
        



    }
    IEnumerator PlayText()
    {
        text.text = " ";
        foreach (char c in textSTR.ToCharArray())
        {
            text.text += c;
            yield return null;
            //yield return new WaitForSeconds(0.25f);


        }
    }

    public void PopulateSaveData(SaveData sd)
    {
        sd.s_HP = currentHealth;
    }
    public void LoadFromSaveData(SaveData sd)
    {
        currentHealth = sd.s_HP;
    }
}
