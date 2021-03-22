using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralStats : MonoBehaviour
{
    public GameObject[] stepTargets;
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
            if (!addedXP)
            {
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
        StartCoroutine(waitDeath());
        for (int i = 0;i < collider.Length; i++) {
            collider[i].isTrigger = false;
            collider[i].enabled = true;
        }
        
        //Destroy(this.gameObject, 4f);
    }
    IEnumerator waitDeath()
    {
        yield return new WaitForSeconds(4f);
        addedXP = false;
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
}
