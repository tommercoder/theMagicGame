using System.Collections;
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

    public CharacterController controller;

    public GameObject abilitiesUI;
    public GameObject healthbarUI;
    public GameObject[] swordRigidbody;

    public bool minusXP = false;
  
    void Start()
    {
       //spoczątku wstawia się maksymalna liczba zdrowia
        healthBar.setMaxHealth(health);
        //wyłącza się ragdoll
        RagdollActive(false);

        swordRigidbody = GameObject.FindGameObjectsWithTag("interactable object");
        foreach (GameObject b in swordRigidbody)
        {
            if (b.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = b.GetComponent<Rigidbody>();
                rb.detectCollisions = true;
            }

        }

    }
    
    void Update()
    {
        
        healthBar.setHealth(currentHealth);
        //w przypadku gdy zdrowie < 0 bohater umiera
        if(currentHealth <= 0)
        {
            Die();
            playerSword.instance.currentSwordGameObject.GetComponent<Rigidbody>().detectCollisions = true;
            //odejmuje się doświadczenie
            if (!minusXP)
            {
                if (characterStats.instance.XP >= 30)
                {
                    characterStats.instance.XP -= 30;
                    logShow.instance.showText("-30xp");

                    

                }
                minusXP = true;
            }
        }
        else if(currentHealth > 0)
        {
            isPlayerDead = false;
        }
       
    }
   
    public void Die()
    {
       
        isPlayerDead = true;
        

        abilitiesUI.SetActive(false);
        healthbarUI.SetActive(false);
        //pokazuje "dead menu"
        canvas.alpha += Time.deltaTime / 2;
        
        RagdollActive(true);
        if (canvas.alpha ==1 && isPlayerDead)
            StartCoroutine(PlayText());
        else {
            StopCoroutine(PlayText());
                
            text.text = " ";
        }
       
        
    }
    //włącza/wyłącza ragdoll  w zależności od parametru
    public void RagdollActive(bool active)
    {

        controller.enabled = !active;
        foreach (var collider in colliders)
            collider.enabled = active;
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

         legScript.enabled = !active;
         animator.enabled = !active;
  
    }
        IEnumerator PlayText()
        {
            text.text = " ";
            foreach (char c in textSTR.ToCharArray())
            {
                text.text += c;
                yield return null;   
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
