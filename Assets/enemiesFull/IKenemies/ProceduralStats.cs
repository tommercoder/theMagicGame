using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProceduralStats : MonoBehaviour,ISaveable
{
    [Header("PROCEDURAL ID")]
    public string id;


    //public GameObject[] stepTargets;
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
   // public GameObject log;
    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = health;
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponents<CapsuleCollider>();
        if(GetComponent<ProceduralLeg>()!=null)
        legScript = GetComponent<ProceduralLeg>();
        if (GetComponentInChildren<MainProceduralController>() != null)
            mainScript = GetComponentInChildren<MainProceduralController>();

        patrolScript = GetComponent<navmeshPatrol>();


        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        player = GameObject.Find("character");
       // log = GameObject.Find("error text");
       ///log.GetComponent<Text>().text = " ";
    }
    void Start()
    {
        RagdollActive(false);
        
    }
    private void Update()
    {
        //for (int i = 0; i < stepTargets.Length; i++)
        //{
        //    stepTargets[i].GetComponent<SphereCollider>().enabled = true;
        //    stepTargets[i].GetComponent<MeshRenderer>().enabled = true;
        //}
        if (currentHealth<=0)
        {
            Die();
            if (GameObject.FindObjectOfType<questPointer>().target != null && transform == GameObject.FindObjectOfType<questPointer>().target)
            {
                MarieleQuest.instance.questPointer.SetActive(false);
            }
            if (!addedXP)
            {
                Quest quest = MarieleQuest.instance.currentMarieleQuest;
                if (quest!=null && quest.isActive)
                {
                    quest.goal.ProceduralEnemyKilled();
                    if (quest.goal.isReached())
                    {
                        //add reward to inventory
                        characterStats.instance.XP += quest.XP;
                        //logShow.instance.showQuestText("quest " + MarieleQuest.instance.currentMarieleQuest.title + " is completed");
                        quest.complete();
                        
                    }
                }
                // log.gameObject.SetActive(true);
                // log.GetComponent<Text>().text = "+" + " " + XPforDeath + "xp";
                characterStats.instance.all_procedural_ids.Add(id);
                logShow.instance.showText("+" + " " + XPforDeath + "xp");
                player.GetComponent<characterStats>().XP += XPforDeath;
                addedXP = true;
                Debug.Log("added xp");
            }
        }
    }
    
    void Die()
    {
        isDead = true;
        
        //ragdoll physics (using character joints and rigidbodies)
        RagdollActive(true);
        //StartCoroutine(waitDeath());
        for (int i = 0;i < collider.Length; i++) {
            collider[i].isTrigger = false;
            collider[i].enabled = true;
        }
        
        Destroy(this.gameObject, 1f);
    }
    IEnumerator waitDeath()
    {
        yield return new WaitForSeconds(4f);
        addedXP = false;
       // log.GetComponent<Text>().text = " ";
       // log.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
    public void RagdollActive(bool active)
    {
        //children
        foreach (var collider in colliders)
            collider.enabled = active;
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        //root
        this.enabled = !active;
        patrolScript.enabled = !active;
        if(GetComponent<ProceduralLeg>()!=null)
        legScript.enabled = !active;
        if(GetComponentInChildren<MainProceduralController>()!=null)
        mainScript.enabled = !active;

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
