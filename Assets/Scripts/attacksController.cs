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
    playerSword playerSwordController;
    List<int> possibleAttacks = Enumerable.Range(1, 3).ToList();
    StateControllerTest controller;
  
    // Start is called before the first frame update
    void Start()
    {
        playerSwordController = GetComponent<playerSword>();
        controller = GetComponent<StateControllerTest>();
        animator = GetComponent<Animator>();
        
        isDrawedSwordHash = Animator.StringToHash("isDrawedSword");
        attackPressedFirstHash = Animator.StringToHash("attackPressedFirstTrigger");
        superAttackPressedHash = Animator.StringToHash("superAttackTrigger");
        attackPressedSecondHash = Animator.StringToHash("attackPressedSecondTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        Transform sword = playerSwordController.sword.transform;
        bool isDrawedSword = animator.GetBool(isDrawedSwordHash);
        bool attackPressedFirst = animator.GetBool(attackPressedFirstHash);
        bool attackPressedSecond = animator.GetBool(attackPressedSecondHash);
        bool superAttackPressed = animator.GetBool(superAttackPressedHash);

        bool MouseattackPressed = Input.GetMouseButtonDown(0);


        //add energy check here
        if (MouseattackPressed && isDrawedSword)
        {
            
            int randAt = randomAttack();
            Debug.Log("random attack number" + randAt);
            
            if (randAt==1)
            animator.SetTrigger("attackPressedFirstTrigger");
             if(randAt==2)
                animator.SetTrigger("attackPressedSecondTrigger");
           if (randAt == 3)
            {
                
                
                sword.GetChild(1).gameObject.SetActive(true);
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

        }
        
        if (isEnergyAttackDone)
        {
            sword.GetChild(1).gameObject.SetActive(false);
            isEnergyAttackDone = false;
        }

    }
    public void checkEnergyAttackFunc()
    {
        isEnergyAttackDone = true;
    }
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

        Debug.Log("en" + number);


        // int number = Random.Range(1, 4);

        return number;
    }

}
