using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
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
        collider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        instance = this;
        textSTR = text.text;
        text.text = "";
    }

    #endregion
    public CanvasGroup canvas;
    public Text text;
    string textSTR;
    public int health = 100;
    public int currentHealth;
    public healthBarController healthBar;
    public bool isPlayerDead;
    //ragdoll 
    public Collider[] colliders;
    public Rigidbody[] rigidbodies;
    public FootIK legScript;
    public Animator animator;
    public CapsuleCollider collider;
    public Rigidbody rigidbody;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthBar.setMaxHealth(health);
        RagdollActive(false);
        
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
        if(currentHealth > 0)
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
        canvas.alpha += Time.deltaTime / 2;
        if (canvas.alpha > 0.9)
            StartCoroutine(PlayText());
        RagdollActive(true);
        
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
        rigidbody.detectCollisions = !active;
        rigidbody.isKinematic = !active;
        
            collider.enabled = !active;
        



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
}
