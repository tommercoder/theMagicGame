using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using DG.Tweening;
public class EnemyPatrol : MonoBehaviour
{

    #region Singleton
    public static EnemyPatrol instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public Transform[] points;
    public GameObject player;
    public int current;
    Quaternion rotation;
    public bool rotated;
    public bool attackingPlayer;
    public float speed;
    public CharacterController controller;
    public Animator animator;
    public bool WaitOnPoint;
    public NavMeshAgent agent;
    float nextFire;
    public float fireRate;
    public bool nearPlayer;
    public bool canMove;
    public audioManager am;
    private void Start()
    {
        nearPlayer = false;
        am = FindObjectOfType<audioManager>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("character");
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        current = 0;
        rotation = transform.rotation;
        WaitOnPoint = false;

        fireRate = 2f;
        nextFire = Time.time;
        canMove = true;
        
    }

    void chasePlayer()
    {
        if (pauseMenu.instance.menuIsOpened)
            return;
        Debug.Log("chasePlayer called");
        //attackingPlayer = true;
        if (canMove)
        {
            if (!animator.GetBool("isRunningEnemy"))
            {
                animator.SetBool("isRunningEnemy", true);
            }
            Vector3 relativePos = (player.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime);

            Vector3 moveVector = player.transform.position - transform.position;
            if(controller.enabled)
            controller.Move(moveVector * Time.deltaTime);
        }
        //Debug.Log(moveVector * Time.deltaTime);






        //if (Time.time > nextFire)
        //{


        //    nextFire = Time.time + fireRate;
        //}


    }
   
    private void Update()
    {
        if (pauseMenu.instance.menuIsOpened)
            return;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("distance " + distance);
        
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            attackingPlayer = true;
        }
        else
        {
            Vector3 relativePos = (points[current].transform.position- transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime);
            //rotation = Quaternion.LookRotation(points[current].position - transform.position);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
            rotated = true;
            
            attackingPlayer = false;
            
        }



        if (!attackingPlayer)
        {
            if (Vector3.Distance(transform.position, points[current].position) > 0.7)
            {
                if (rotated && !WaitOnPoint)
                {
                    if (pauseMenu.instance.menuIsOpened)
                        return;
                    ////transform.position = Vector3.MoveTowards(transform.position, points[current].position, Time.deltaTime * speed);
                    ///
                    Vector3 moveVector = points[current].position - transform.position;
                    controller.Move(moveVector * Time.deltaTime * speed); 
                   
                    //Debug.Log("speed " + moveVector * Time.deltaTime * speed); 
                    if (!animator.GetBool("isWalkingEnemy"))
                    {
                        animator.SetBool("isWalkingEnemy", true);
                    }

                    
                    rotated = false;
                }

            }
            else
            {
                current = (current + 1) % points.Length;
                // if(current > 0)
                StartCoroutine(waitOnPoint());
            }
        }
        else
        {
            if (distance < 20f && distance > 4f 
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("bottomSwordSlash") && !animator.GetCurrentAnimatorStateInfo(0).IsName("fastSwordSlash")
                &&!animator.GetCurrentAnimatorStateInfo(0).IsName("swordCast"))
            {
                chasePlayer();
            }
             if(distance < 4f)
            {
                startAttack();
            }
        }
    }
    void startAttack()
    {
        if (pauseMenu.instance.menuIsOpened)
            return;
        Debug.Log("start attack called");
        animator.SetBool("isRunningEnemy", false);
        animator.SetBool("isWalkingEnemy", false);
        Vector3 relativePos = (player.transform.position - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime*2);

        
        animator.SetInteger("enemyAttackInteger", pickAttack());
        //if (!animator.GetCurrentAnimatorStateInfo(1).IsName("bottomSwordSlash") && !animator.GetCurrentAnimatorStateInfo(1).IsName("swordCast")
        //    && !animator.GetCurrentAnimatorStateInfo(1).IsName("fastSwordSlash"))
        //{
        //    animator.SetInteger("enemyAttackInteger", 4);
        //}

        


    }
  
    int pickAttack()
    {
        int attackNumber = 0;

        System.Random r = new System.Random();
        attackNumber = r.Next(0, 3);
        
        return attackNumber;
    }
    IEnumerator waitOnPoint()
    {
        WaitOnPoint = true;
        animator.SetBool("isWalkingEnemy", false);
        yield return new WaitForSeconds(5f);
        WaitOnPoint = false;
    }
    public void enemyFitstAttackEvent()
    {
        am.Play("swish5");
        //FindObjectOfType<audioManager>().Play("enemyGr1");

    }
    public void secondAttackEvent()
    {
        am.Play("swish2");
    }
    public void thirdEnemyAttackEvent()
    {
        am.Play("swish3");
        //FindObjectOfType<audioManager>().Play("enemyGr2");
    }
    public void enemyRunSoundEvent()
    {
        am.Play("swordRun");
    }
}
