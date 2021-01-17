using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
public class attacksController : MonoBehaviour
{
    protected static attacksController s_Instance;
    public static attacksController instance { get { return s_Instance; } }
    public Animator animator;
    int isDrawedSwordHash;
    int attackPressedFirstHash;
    int attackPressedSecondHash;
    int superAttackPressedHash;
    bool isEnergyAttackDone = false;
    public bool attackState = false;
    bool isFullEnergyBar = false;
    playerSword playerSwordController;
    List<int> possibleAttacks = Enumerable.Range(1, 3).ToList();
    StateControllerTest controller;
    movement movement;

    public bool enemiesAround = false;
    bool firstPlayed;
    bool secondPlayed;
    public AbilityMain [] Abilities;
    bool canClick;
    bool canClickSec;
    int noOfClick;
    int noOfClickSecond;
    //string[] trigger = { "attackPressedFirstTrigger", "attackPressedSecondTrigger", "superAttackTrigger" };
    Transform sword;
    int swordEnergy;
    public bool canCast;
    public CharacterController CharacterController;
    [SerializeField] private ParticleSystem dashParticle = default;
    private void Awake()
    {
        Abilities = GetComponents<AbilityMain>();
        s_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canCast = false;
        swordEnergy = 0;
        noOfClick = 0;
        noOfClickSecond = 0;
        canClick = true;
        canClickSec = true;
        firstPlayed = false;
        secondPlayed = false;
        playerSwordController = GetComponent<playerSword>();
        controller = GetComponent<StateControllerTest>();
        animator = GetComponent<Animator>();
         movement = GetComponent<movement>();
        CharacterController = GetComponent<CharacterController>();
        sword = playerSwordController.sword.transform;
        isDrawedSwordHash = Animator.StringToHash("isDrawedSword");
        attackPressedFirstHash = Animator.StringToHash("attackPressedFirstTrigger");
        superAttackPressedHash = Animator.StringToHash("superAttackTrigger");
        attackPressedSecondHash = Animator.StringToHash("attackPressedSecondTrigger");
    }
    void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "enemy")
            {
                enemiesAround = true;
            }
            

        }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            enemiesAround = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        /*if (EventSystem.current.IsPointerOverGameObject())
            return;*/
        bool isDrawedSword = animator.GetBool(isDrawedSwordHash);
        bool attackPressedFirst = animator.GetBool(attackPressedFirstHash);
        bool attackPressedSecond = animator.GetBool(attackPressedSecondHash);
        bool superAttackPressed = animator.GetBool(superAttackPressedHash);

        bool MouseattackPressed = Input.GetMouseButtonDown(0);
        bool wPressed = Input.GetKey("w");
        bool sPressed = Input.GetKey("s");
        bool diPressing = Input.GetKey("d");
        bool aPressed = Input.GetKey("a");
        bool castPressed = Input.GetKey("c");
        bool ballCastPressed = Input.GetKey("x");
        bool swordSlashAbilityPressed = Input.GetKey("q");
        //add energy check here
        //isFullEnergyBar = true;

        //Debug.Log("state" + isEnergyAttackDone);
        ////////////////
       
        
        if (isDrawedSword)
        {

            if(castPressed && Abilities[1].canUse)
            {



                //CharacterController.Move(transform.position + (transform.forward * 1));
                dashParticle.Play();
                Abilities[1].TriggerAbility();
                //movement.canMove = false;

                //animator.SetInteger("attackAnimation", 20);
                controller.timeRemaining = 5;
                controller.timerIsRunning = true;
                //random damage and effects here
            }

           
                if (ballCastPressed && !animator.GetCurrentAnimatorStateInfo(2).IsName("ballcast") && Abilities[0].canUse)
                {
                    movement.canMove = false;
                    animator.SetInteger("attackAnimation", 21);
               // if (canCast)
               // {
                    Abilities[0].TriggerAbility();
                    canCast = false;
               // }
               // }
                    controller.timeRemaining = 5;
                    controller.timerIsRunning = true;
                    //yield return new WaitForSeconds(5);

            }
            if(swordSlashAbilityPressed && Abilities[2].canUse)
            {
                Abilities[2].TriggerAbility();

                controller.timeRemaining = 10;
                controller.timerIsRunning = true;
            }
            /*{
                movement.canMove = false;
                animator.SetInteger("attackAnimation", 21);
                controller.timeRemaining = 5;
                controller.timerIsRunning = true;

            }*/

            /*if (enemiesAround && wPressed )
            {
                // animator.SetBool("walkAttack", true);
                Set(0);
            }*/
            /*else if (enemiesAround && !wPressed)
            {
                animator.SetBool("walkAttack", false);
                animator.SetBool("isRunning", false);
            }
            else if (!enemiesAround && wPressed)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("walkAttack", false);
            }*/
            if (isDrawedSword && enemiesAround)
                attackState = true;
            if (isDrawedSword && enemiesAround && Input.GetKey(KeyCode.LeftShift) && (wPressed || sPressed) )
            {
                animator.SetBool("walkAttack", false);
                animator.SetBool("walkAttackBack", false);
                
                animator.SetBool("isRunningSword", true);
                attackState = false;
            }

           /* if (controller.timerIsRunning)
            {
                swordEnergy += 5;
            }
            if (swordEnergy == 30)
            {
                isFullEnergyBar = true;
            }
            //isFullEnergyBar = true;
            if (isFullEnergyBar)
            {
                sword.GetChild(1).gameObject.SetActive(true);
                isFullEnergyBar = false;
                swordEnergy -= 20;
            }
            if (isEnergyAttackDone)
            {
                sword.GetChild(1).gameObject.SetActive(false);
                isEnergyAttackDone = false;
            }*/
        }

        
        //attacks
        if(Input.GetMouseButtonDown(0) && isDrawedSword && !firstPlayed)
        {
            //animator.SetBool("isRunningSword", false);
            //animator.SetBool("isRunning", false);
            
            ComboStarter();
            controller.timeRemaining = 5;
            controller.timerIsRunning = true;
            //sword.GetChild(1).gameObject.SetActive(true);
        }
        if(firstPlayed && Input.GetMouseButtonDown(0) && isDrawedSword)
        {
            //animator.SetBool("isRunningSword", false);
            //animator.SetBool("isRunning", false);
            
            ComboStarterSecond();
            controller.timeRemaining = 10;
            controller.timerIsRunning = true;
            ///sword.GetChild(1).gameObject.SetActive(true);
        }
       
        if(firstPlayed && secondPlayed)
        {
            firstPlayed = false;
            secondPlayed = false;
            
        }
       

        /*if (MouseattackPressed && isDrawedSword)
        {

            int randAt = randomAttack();

           
            if (randAt == 1 )
            {
                //animator.SetTrigger("attackPressedFirstTrigger");
            if (isFullEnergyBar)
            {
                sword.GetChild(1).gameObject.SetActive(true);
                animator.SetTrigger("attackPressedFirstTrigger");
                isFullEnergyBar = false;
            }
            else
                animator.SetTrigger("attackPressedFirstTrigger");
        }
            if (randAt == 2 )
            {

            if (isFullEnergyBar)
            {

                sword.GetChild(1).gameObject.SetActive(true);
                animator.SetTrigger("attackPressedSecondTrigger");
                isFullEnergyBar = false;
            }
            else
                animator.SetTrigger("attackPressedSecondTrigger");
        }
        if (randAt == 3)
        {


            if (isFullEnergyBar)
            {
                sword.GetChild(1).gameObject.SetActive(true);
                animator.SetTrigger("superAttackTrigger");
                isFullEnergyBar = false;
            }
            else
                animator.SetTrigger("superAttackTrigger");
        }




            controller.timeRemaining = 5;
            controller.timerIsRunning = true;
        }
        else if(!MouseattackPressed && isDrawedSword)
        {
            
                animator.SetBool("attackPressedFirstTrigger",false);
                animator.SetBool("attackPressedSecondTrigger",false);
                animator.SetBool("superAttackTrigger",false);
          
         
        }*/
        // Debug.Log("energy " + swordEnergy);


        /*if (isEnergyAttackDone)
        {
            sword.GetChild(1).gameObject.SetActive(false);
            isEnergyAttackDone = false;
        }*/
        //check if any animation stoped


        //Debug.Log("can move " + movement.canMove);

            if ((animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
                && Input.GetKey(KeyCode.LeftShift))
            {

                animator.SetInteger("attackAnimation", 4);
                movement.canMove = true;
                canClick = true;
                noOfClick = 0;
            }
            else if ((animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
                && Input.GetKey(KeyCode.LeftShift))
            {
                canClickSec = true;
                noOfClickSecond = 0;
                animator.SetInteger("attackAnimation", 4);
                movement.canMove = true;
                
            }
        //Debug.Log(noOfClickSecond);
    }
    public void startFireballEvent()
    {
        canCast = true;
    }
    public void dashEndedEvent()
    {
        animator.SetInteger("attackAnimation", 4);
        movement.canMove = true;
        dashParticle.Stop();
    }
    public void castedEvent()
    {
        movement.canMove = true;
        animator.SetInteger("attackAnimation", 4);
        canCast = false;
    }
    void ComboStarterSecond()
    {
        if(canClickSec)
        {
            noOfClickSecond++;
        }
        if(noOfClickSecond == 1)
        {
            animator.SetInteger("attackAnimation", 11);
        }
    }
    void ComboStarter()
    {
        if(canClick)
        {
            noOfClick++;
        }
        if(noOfClick == 1)
        {
            animator.SetInteger("attackAnimation", 1);
        }
    }    
    public void secondCombatCheck()
    {
        canClickSec = false;
        
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing") && noOfClickSecond == 1)
        {

            animator.SetInteger("attackAnimation", 4);
            canClickSec = true;
            noOfClickSecond = 0;
            movement.canMove = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing") && noOfClickSecond >= 2)
        {

            animator.SetInteger("attackAnimation", 12);
            canClickSec = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing") && noOfClickSecond == 2)
        {

            animator.SetInteger("attackAnimation", 4);
            canClickSec = true;
            noOfClickSecond = 0;
            movement.canMove = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing") && noOfClickSecond >= 3)
        {
            Debug.Log("ENTERED");
            animator.SetInteger("attackAnimation", 13);
            canClickSec = true;
            //noOfClickSecond = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
        {

            animator.SetInteger("attackAnimation", 4);
            noOfClickSecond = 0;
            canClickSec = true;
            movement.canMove = true;
        }
        else
        {
            animator.SetInteger("attackAnimation", 4);
            noOfClickSecond = 0;
            canClickSec = true;
            movement.canMove = true;
        }
       
        secondPlayed = true;
        controller.timeRemaining = 5;
        controller.timerIsRunning = true;
       
     
    }
    public void CombatCheck()
    {
        canClick = false;

        
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack") && noOfClick == 1)
        {
           
            animator.SetInteger("attackAnimation", 4);
            canClick = true;
            noOfClick = 0;
            movement.canMove = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack") && noOfClick >= 2)
        {
            
            animator.SetInteger("attackAnimation", 2);
            canClick = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack") && noOfClick == 2)
        {
            
            animator.SetInteger("attackAnimation", 4);
            canClick = true;
            noOfClick = 0;
            movement.canMove = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack") && noOfClick >= 3)
        {
           
            animator.SetInteger("attackAnimation", 3);
            canClick = true;
            //noOfClick = 0;
        }
        else if(animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
        {
           
            animator.SetInteger("attackAnimation", 4);
           
            canClick = true;
            noOfClick = 0;
            movement.canMove = true;
        }
       
        firstPlayed = true;
        controller.timeRemaining = 5;
        controller.timerIsRunning = true;
       
       
    }
    /*public void firstDoneEvent()
    {
        firstPlayed = true;
    }
    public void secondDoneEvent()
    {
        secondPlayed = true;
    }*/
    public void checkEnergyAttackFunc()
    {
         
        isEnergyAttackDone = true;
       
        movement.canMove = true;
        noOfClick = 0;
        noOfClickSecond = 0;
    }
    /*public void checkIfCanMoveEvent()
    {
        movement.canMove = true;
    }*/
    int randomAttack()
    {
        int number = 0;

        
        if (possibleAttacks.Count == 3)
        {
            int index = Random.Range(0, 3);
            number = possibleAttacks[index];
            possibleAttacks.RemoveAt(index);
        }
        else if(possibleAttacks.Count == 2)
        {
            int index = Random.Range(0, 2);
            number = possibleAttacks[index];
            possibleAttacks.RemoveAt(index);
        }
        else if(possibleAttacks.Count == 1)
        {
            int index = Random.Range(0, 1);
            number = possibleAttacks[index];
            possibleAttacks.RemoveAt(index);
        }
        else if (possibleAttacks.Count == 0)
        {
            possibleAttacks = Enumerable.Range(1, 3).ToList();

            int index = Random.Range(0, 3);
            number = possibleAttacks[index];
            possibleAttacks.RemoveAt(index);
        }

       


        // int number = Random.Range(1, 4);

        return number;
    }

}

