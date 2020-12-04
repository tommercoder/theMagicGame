using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacksController : MonoBehaviour
{
    public Animator animator;
    int isDrawedSwordHash;
    int attackPressedFirstHash;
    int attackPressedSecondHash;
    int longAttackPressedHash;
    

    StateControllerTest controller;
  
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateControllerTest>();
        animator = GetComponent<Animator>();
        
        isDrawedSwordHash = Animator.StringToHash("isDrawedSword");
        attackPressedFirstHash = Animator.StringToHash("attackPressedFirst");
        longAttackPressedHash = Animator.StringToHash("LongAttackPressed");
        attackPressedSecondHash = Animator.StringToHash("attackPressedSecond");
    }

    // Update is called once per frame
    void Update()
    {
        bool isDrawedSword = animator.GetBool(isDrawedSwordHash);
        bool attackPressedFirst = animator.GetBool(attackPressedFirstHash);
        bool attackPressedSecond = animator.GetBool(attackPressedSecondHash);
        bool longAttackPressed = animator.GetBool(longAttackPressedHash);

        bool MouseattackPressed = Input.GetMouseButtonDown(0);
        bool firstAttackDone = false;
        bool secondAttackDone = false;
        // Debug.Log("TIME" + controller.timeRemaining);
       if(MouseattackPressed && isDrawedSword)
        {
            //animator.SetBool(attackPressedFirstHash, true);
            animator.SetTrigger("attackPressedFirstTrigger");
            
            controller.timeRemaining = 5;
            controller.timerIsRunning = true;
        }
     

    }

}
