using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    public Animator animator;
    public Collider[] colliders;
    public Rigidbody[] rigidbodies;
    public Rigidbody rigidbody;
    public CapsuleCollider collider;
    public EnemyPatrol patrolScript;
    public FootIK footScript;

    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
       


        RagdollActive(false);
    }
    private void Awake()
    {
        currentHP = maxHP;
        patrolScript = GetComponent<EnemyPatrol>();
        animator = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<CapsuleCollider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        collider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        RagdollActive(true);
        Destroy(this.gameObject, 3f);

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
}
