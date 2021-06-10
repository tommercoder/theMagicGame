using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProceduralStats : MonoBehaviour,ISaveable
{
    [Header("PROCEDURAL ID")]
    public string id;

    public Rigidbody rigidbody;
    public CapsuleCollider[] collider;
    public bool isDead;
    //public int damage;
    public int health = 10;
    public int currentHealth;
    public Animator animator;
    public Collider[] colliders;
    public Rigidbody[] rigidbodies;

    public ProceduralLeg legScript;
    public MainProceduralController mainScript;
    public navmeshPatrol patrolScript;
    public GameObject player;
    public int XPforDeath;
    bool addedXP = false;
    
    public void Awake()
    {
        currentHealth = health;
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponents<CapsuleCollider>();
        if (GetComponent<ProceduralLeg>() != null)
        {
            legScript = GetComponent<ProceduralLeg>();
        }
        if (GetComponentInChildren<MainProceduralController>() != null)
        {
            mainScript = GetComponentInChildren<MainProceduralController>();
        }

        patrolScript = GetComponent<navmeshPatrol>();
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        player = GameObject.Find("character");
    }
   public void Start()
    {
        //Wyłącza ragdoll.
        RagdollActive(false);
        
    }
    public void Update()
    {
        //Jeśli zdrowie jest < 0.
        if (currentHealth<=0)
        {
            Die();
            //Jeśli jest zadanie i wkaźnik jest na tym wrogu wskażnik się wyłącza.
            if (GameObject.FindObjectOfType<questPointer>().target != null && transform == GameObject.FindObjectOfType<questPointer>().target)
            {
                MarieleQuest.instance.questPointer.SetActive(false);
            }
            //Dodaje doświadczenie.
            if (!addedXP)
            {
                Quest quest = MarieleQuest.instance.currentMarieleQuest;
                //Sprawdza czy zadanie istnieje i czy zostało wykonane.
                if (quest!=null && quest.isActive)
                {
                    quest.goal.ProceduralEnemyKilled();
                    if (quest.goal.isReached())
                    {
                        //add reward to inventory
                        characterStats.instance.XP += quest.XP;
                        quest.complete();
                        
                    }
                }
                //Dodaje do listy zabitych wrogów dla zapisywania informacji.
                characterStats.instance.all_procedural_ids.Add(id);
                logShow.instance.showText("+" + " " + XPforDeath + "xp");
                player.GetComponent<characterStats>().XP += XPforDeath;
                addedXP = true;
                
            }
        }
    }
    
    void Die()
    {
        isDead = true;
        //Włącza ragdoll.
        RagdollActive(true);
        //Wyłącza trigger dla kolizji z ziemią.
        for (int i = 0;i < collider.Length; i++) {
            collider[i].isTrigger = false;
            collider[i].enabled = true;
        }
        //Usuwa objekt.
        Destroy(this.gameObject, 1f);
    }
 
    public void RagdollActive(bool active)
    {
        //Włącza/wyłącza kolizje.
        foreach (var collider in colliders)
        {
            collider.enabled = active;
        }
        //Włącza/wyłącza rigidbody.
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        
        this.enabled = !active;
        patrolScript.enabled = !active;
        if (GetComponent<ProceduralLeg>() != null)
        {
            legScript.enabled = !active;
        }
        if (GetComponentInChildren<MainProceduralController>() != null)
        {
            mainScript.enabled = !active;
        }

        animator.enabled = !active;
        rigidbody.detectCollisions = !active;
        rigidbody.isKinematic = !active;
        for (int i = 0; i < collider.Length; i++)
        {
            collider[i].enabled = !active;
        }
       
    }

    public void PopulateSaveData(SaveData sd)
    {
        SaveData.ProceduralEnemyData enemyData = new SaveData.ProceduralEnemyData();
        enemyData.e_ProcHealth = currentHealth;
        enemyData.e_ProcId = id;
        sd.proceduralEnemyData.Add(enemyData);
    }
    public void LoadFromSaveData(SaveData sd)
    {
        foreach (SaveData.ProceduralEnemyData enemyData in sd.proceduralEnemyData)
        {
            if (enemyData.e_ProcId == id)
            {
                currentHealth = enemyData.e_ProcHealth;
                break;
            }
        }
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }


    }
}
