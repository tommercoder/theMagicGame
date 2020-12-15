using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class attacksController : MonoBehaviour
{
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
    
    
    bool canClick;
    int noOfClick;
    int noOfClickSecond;
    string[] trigger = { "attackPressedFirstTrigger", "attackPressedSecondTrigger", "superAttackTrigger" };
    Transform sword;
    int swordEnergy;
    // Start is called before the first frame update
    void Start()
    {
        swordEnergy = 0;
        noOfClick = 0;
        noOfClickSecond = 0;
        canClick = true;
        firstPlayed = false;
        secondPlayed = false;
       
        playerSwordController = GetComponent<playerSword>();
        controller = GetComponent<StateControllerTest>();
        animator = GetComponent<Animator>();
         movement = GetComponent<movement>();
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
        
        bool isDrawedSword = animator.GetBool(isDrawedSwordHash);
        bool attackPressedFirst = animator.GetBool(attackPressedFirstHash);
        bool attackPressedSecond = animator.GetBool(attackPressedSecondHash);
        bool superAttackPressed = animator.GetBool(superAttackPressedHash);

        bool MouseattackPressed = Input.GetMouseButtonDown(0);
        bool wPressed = Input.GetKey("w");
        bool sPressed = Input.GetKey("s");
        bool diPressing = Input.GetKey("d");
        bool aPressed = Input.GetKey("a");
        //add energy check here
        //isFullEnergyBar = true;

        //Debug.Log("state" + isEnergyAttackDone);
        ////////////////

        if (isDrawedSword)
        {
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
            ComboStarter();
            controller.timeRemaining = 5;
            controller.timerIsRunning = true;
            
        }
        if(firstPlayed && Input.GetMouseButtonDown(0) && isDrawedSword)
        {
            ComboStarterSecond();
            controller.timeRemaining = 10;
            controller.timerIsRunning = true;
           
        }
        if(firstPlayed && secondPlayed)
        {
            firstPlayed = false;
            secondPlayed = false;
        }
        Debug.Log("FIRST PLAYER " + firstPlayed);
       
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
        Debug.Log("energy " + swordEnergy);


        /*if (isEnergyAttackDone)
        {
            sword.GetChild(1).gameObject.SetActive(false);
            isEnergyAttackDone = false;
        }*/
       
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

                animator.SetInteger("attackAnimation", 4);
                movement.canMove = true;
                canClick = true;
                noOfClickSecond = 0;
            }
        
    }
    void ComboStarterSecond()
    {
        if(canClick)
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
        canClick = true;
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing") && noOfClickSecond == 1)
        {

            animator.SetInteger("attackAnimation", 4);
            canClick = true;
            noOfClickSecond = 0;
            movement.canMove = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing") && noOfClickSecond >= 2)
        {

            animator.SetInteger("attackAnimation", 12);
            canClick = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing") && noOfClickSecond == 2)
        {

            animator.SetInteger("attackAnimation", 4);
            canClick = true;
            noOfClickSecond = 0;
            movement.canMove = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing") && noOfClickSecond >= 3)
        {
            animator.SetInteger("attackAnimation", 13);
            canClick = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
        {

            animator.SetInteger("attackAnimation", 4);
            noOfClickSecond = 0;
            canClick = true;
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
        }
        else if(animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
        {
            
            animator.SetInteger("attackAnimation", 4);
            noOfClick = 0;
            canClick = true;
            movement.canMove = true;
        }
        firstPlayed = true;
        controller.timeRemaining = 5;
        controller.timerIsRunning = true;

       
    }
    public void firstDoneEvent()
    {
        firstPlayed = true;
    }
    public void secondDoneEvent()
    {
        secondPlayed = true;
    }
    public void checkEnergyAttackFunc()
    {
        isEnergyAttackDone = true;
       
        movement.canMove = true;
       
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

