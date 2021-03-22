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
    public GameObject[] swordRigidbody;
    public CharacterController controller;
    public int XPforDeath;
    public GameObject player;
    bool addedXP = false;
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

        swordRigidbody = GameObject.FindGameObjectsWithTag("interactable object");
        player = GameObject.Find("character");
    }
    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            Die();
            if(!addedXP)
            {
                player.GetComponent<characterStats>().XP += XPforDeath;
                addedXP = true;
            }
        }
    }
    void Die()
    {
        Debug.Log("DIE");
        RagdollActive(true);
        StartCoroutine(waitDeath());
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
        //this.enabled = !active;
        patrolScript.enabled = !active;
        //footScript.enabled = !active;
        

        animator.enabled = !active;
        rigidbody.detectCollisions = !active;
        rigidbody.isKinematic = !active;
        collider.enabled = !active;

    }
}
