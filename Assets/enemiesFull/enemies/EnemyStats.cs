using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour,ISaveable
{
    [Header("ENEMY ID")]
    public string id;


    public int currentHP;
    public int maxHP;
    public Animator animator;
    public Collider[] colliders;
    public Rigidbody[] rigidbodies;
    public Rigidbody rigidbody;
    public CapsuleCollider collider;
    public EnemyPatrol patrolScript;
    public FootIK footScript;
    public GameObject[] swordRigidbody;
    public CharacterController controller;
    public int XPforDeath;
    public GameObject player;
    bool addedXP = false;
    public GameObject log;
    // Start is called before the first frame update
    void Start()
    {
       
      

        RagdollActive(false);
        //setting rigiddbodies collision for swords
        foreach (GameObject b in swordRigidbody){
            if (b.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = b.GetComponent<Rigidbody>();
                rb.detectCollisions = true;
            }
                
            }
        }
    private void Awake()
    {
        currentHP = maxHP;
        patrolScript = GetComponent<EnemyPatrol>();
        animator = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<CapsuleCollider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        controller = GetComponent<CharacterController>();
        footScript = GetComponent<FootIK>();
        collider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();

        //swordRigidbody = GameObject.FindGameObjectsWithTag("interactable object");
        player = GameObject.Find("character");
        log = GameObject.Find("error text");
        log.GetComponent<Text>().text = " ";
    }
    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            Die();
            MarieleQuest.instance.questPointer.SetActive(false);
            if (!addedXP)
            {
                //FindObjectOfType<questPointer>().target = null;
                
                Debug.Log("QUEST POINTER ACTIVE " + MarieleQuest.instance.questPointer.activeSelf);
                Quest quest = MarieleQuest.instance.currentMarieleQuest;
                if (quest.isActive)
                {
                    quest.goal.EnemyKilled();
                    
                    if (quest.goal.isReached())
                    {
                        //add reward to inventory
                        
                        characterStats.instance.XP += quest.XP;
                        quest.complete();
                    }
                }
                characterStats.instance.dead_enemies_ids.Add(id);
                logShow.instance.showText("+" + " " + XPforDeath + "xp");
                player.GetComponent<characterStats>().XP += XPforDeath;
                addedXP = true;
            }
        }
    }
    void Die()
    {
       
        Debug.Log("DIE");
        RagdollActive(true);
        //StartCoroutine(waitDeath());
       Destroy(this.gameObject, 1f);

    }
    IEnumerator waitDeath()
    {
        yield return new WaitForSeconds(4f);
        addedXP = false;
        //log.GetComponent<Text>().text = " ";
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
        //this.enabled = !active;
        patrolScript.enabled = !active;
        //footScript.enabled = !active;
        

        animator.enabled = !active;
        rigidbody.detectCollisions = !active;
        rigidbody.isKinematic = !active;
        collider.enabled = !active;

    }

    public void PopulateSaveData(SaveData sd)
    {
        SaveData.EnemyData enemyData = new SaveData.EnemyData();
        enemyData.e_Health = currentHP;
        enemyData.e_id = id;
        sd.enemyData.Add(enemyData);
    }
    public void LoadFromSaveData(SaveData sd)
    {
        foreach(SaveData.EnemyData enemyData in sd.enemyData)
        {
            if(enemyData.e_id == id)
            {
                currentHP = enemyData.e_Health;
                break;
            }
        }
        if(currentHP <= 0)
        {
            Destroy(this.gameObject);
        }


    }
}
