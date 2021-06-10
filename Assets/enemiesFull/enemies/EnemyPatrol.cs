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
    //wszystkie punkty patrolowania
    public Transform[] points;
    public GameObject player;
    public int current;
   
    public bool rotated;
    public bool attackingPlayer;
    public float speed;
    public CharacterController controller;
    public Animator animator;
    public bool WaitOnPoint;

    float distance;
    public bool nearPlayer;
    public bool canMove;

    //audio manager
    public audioManager am;
    public void Start()
    {
        nearPlayer = false;
        am = FindObjectOfType<audioManager>();
        player = GameObject.Find("character");
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        current = 0;
     
        WaitOnPoint = false;
        canMove = true;
        
    }
    //metoda dla doganiania bohater wrogami
    void chasePlayer()
    {
        if (pauseMenu.instance.menuIsOpened || playerHealth.instance.currentHealth <=0)
            return;

        //jeśli może poruszać
        if (canMove)
        {
            //włącza się animacja biegu
            if (!animator.GetBool("isRunningEnemy"))
            {
                animator.SetBool("isRunningEnemy", true);
            }
            //wylicza się wektor między bohaterem a wrogiem
            Vector3 relativePos = (player.transform.position - transform.position).normalized;
            //wylicza się rotacja do wroga
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z));
            //zastosowuję się rotacja
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime);
            //wektor poruszania  
            Vector3 moveVector = player.transform.position - transform.position;
            if (controller.enabled)
            {
                //przesuwa wroga do bohatera
                controller.Move(moveVector * Time.deltaTime);
            }
        }
      
    }
    
    public void Update()
    {
        if (pauseMenu.instance.menuIsOpened || playerHealth.instance.currentHealth <= 0)
            return;
        //dystans pomiędzy wrogiem a bohaterem
         distance = Vector3.Distance(transform.position, player.transform.position);
        //jeśli dystans < 20
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {//atakuj bohatera
            attackingPlayer = true;
        }
        else
        {
            //jeśli dystans > 20 to wylicza się wektor między wrogiem a następnym punktem patrolowania
                Vector3 relativePos = (points[current].transform.position - transform.position).normalized;
            //także rotacja 
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime);
                
                rotated = true;
           
            attackingPlayer = false;
            
        }
        //jeśli nie atakuje bohatera
         if (!attackingPlayer)
            {
            //w przypadku gdy wróg jeszcze nie jest na punkcie patrolowania
                if (Vector3.Distance(transform.position, points[current].position) > 0.7)
                {
                
                    if (rotated && !WaitOnPoint)
                    {
                        if (pauseMenu.instance.menuIsOpened)
                            return;
                        //wylicza sie wektor między punktem a wrogiem        
                        Vector3 moveVector = points[current].position - transform.position;
                    //przesuwa się wroga
                        controller.Move(moveVector * Time.deltaTime * speed);
                    //włącza animacje chodzenia
                        if (!animator.GetBool("isWalkingEnemy"))
                        {
                            animator.SetBool("isWalkingEnemy", true);
                        }
                        rotated = false;
                    }

                }
                else
                {
                //gdy wrog jest na punkcie to punkt zmienia się na następny
                    current = (current + 1) % points.Length;
                   //czeka na punkcie
                    StartCoroutine(waitOnPoint());
                }
          
        }
        else
        {
            //jeśli dystanc < 20 i > 4 zaczyna się funkcjs chasePlayer()
            if (distance < 20f && distance > 4f 
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("bottomSwordSlash") && !animator.GetCurrentAnimatorStateInfo(0).IsName("fastSwordSlash")
                &&!animator.GetCurrentAnimatorStateInfo(0).IsName("swordCast"))
            {
                chasePlayer();
            }
            //gdy już jest blisko zaczyna się atakować
             if(distance < 4f)
            {
                startAttack();
            }
        }
    }
    void startAttack()
    {
        //nie atakuje kiedy menu jest otwarte albo bohater ma 0 zdrowia
        if (pauseMenu.instance.menuIsOpened || playerHealth.instance.currentHealth <= 0)
            return;
        //wyłącza animacje poruszania
        animator.SetBool("isRunningEnemy", false);
        animator.SetBool("isWalkingEnemy", false);
        

        //wektor między bohaterem a wrogiem
        Vector3 relativePos = (player.transform.position - transform.position).normalized;
        //rotacja do bohatera
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z));
        //zastosowanie rotacji
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime*2);

        //włączenie animacji ataki
        animator.SetInteger("enemyAttackInteger", pickAttack());
        

    }
    //generowanie losowej liczby
    int pickAttack()
    {
        int attackNumber = 0;

        System.Random r = new System.Random();
        attackNumber = r.Next(0, 3);
        
        return attackNumber;
    }
    //czeka w miejscu na punkcie
    IEnumerator waitOnPoint()
    {
        WaitOnPoint = true;

        animator.SetBool("isWalkingEnemy", false);
        yield return new WaitForSeconds(4f);
        WaitOnPoint = false;
    }
    //dżwięki
    public void enemyFitstAttackEvent()
    {
        am.Play("swish5");
    }
    public void secondAttackEvent()
    {
        am.Play("swish2");
    }
    public void thirdEnemyAttackEvent()
    {
        am.Play("swish3");
     
    }
    public void enemyRunSoundEvent()
    {
        am.Play("swordRun");
    }
}
