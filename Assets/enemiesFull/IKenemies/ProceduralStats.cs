using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralStats : MonoBehaviour
{

    public Rigidbody rigidbody;
    public CapsuleCollider collider;
    public bool isDead;
    //public int damage;
    public int health = 10;
    public int currentHealth;
    public Animator animator;
    public Collider[] colliders;
    public Rigidbody[] rigidbodies;

    public ProceduralLeg legScript;
    public navmeshPatrol patrolScript;
    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = health;
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        legScript = GetComponent<ProceduralLeg>();
        patrolScript = GetComponent<navmeshPatrol>();

        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }
    void Start()
    {
        RagdollActive(false);
        
    }
    private void Update()
    {
        if(currentHealth<=0)
        {
            Die();
        }
    }
    
    void Die()
    {
        isDead = true;
        //ragdoll physics (using character joints and rigidbodies)
        RagdollActive(true);
        collider.isTrigger = false;
        collider.enabled = true;
        
        Destroy(this.gameObject, 4f);
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
        legScript.enabled = !active;

        animator.enabled = !active;
        rigidbody.detectCollisions = !active;
        rigidbody.isKinematic = !active;
        collider.enabled = !active;
       
    }
}
