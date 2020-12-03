using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControllerTest : MonoBehaviour
{
    Animator animator;
    playerSword playerSwordController;
 
    public float timeRemaining = 5;
    public bool timerIsRunning = false;
    public float SwordRunning;
    public float SwordIdle;
    public float attackStates = 0.0f;
    public float acceleration = 0.1f;
    public float decceleration = 0.5f;
    int isRunningHash, isDrawedSwordHash, isIdleSwordHash, isRunningSwordHash,  isSneathedHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerSwordController = GetComponent<playerSword>();
        timerIsRunning = true;
        
       
        isDrawedSwordHash = Animator.StringToHash("isDrawedSword");
        isIdleSwordHash = Animator.StringToHash("isIdleSword");
        isRunningSwordHash = Animator.StringToHash("isRunningSword");
        isRunningHash = Animator.StringToHash("isRunning");
        isSneathedHash = Animator.StringToHash("isSneathed");

  
    }

    // Update is called once per frame
    void Update()
    {


        bool isRunning = animator.GetBool(isRunningHash);
       
        bool isIdleSword = animator.GetBool(isIdleSwordHash);
        bool isDrawedSword = animator.GetBool(isDrawedSwordHash);
        bool isRunningSword = animator.GetBool(isRunningSwordHash);
     
        bool isSneathedSword = animator.GetBool(isSneathedHash);

        bool runningPressed = Input.GetKey("w");
        bool walkingBackPressed = Input.GetKey("s");
        bool diPressing = Input.GetKey("d");
        bool aPressed = Input.GetKey("a");
        bool takeSwordPressed = Input.GetKey(KeyCode.Mouse0);
        Debug.Log("RUNNING" + isRunningSword);
        Debug.Log("Idle" + isIdleSword);
        if (!runningPressed && !walkingBackPressed && !diPressing && !aPressed)
        {
            if (isDrawedSword)
            {
                animator.SetBool(isIdleSwordHash, true);
                animator.SetBool(isRunningHash, false);
            }
            else
            {
                animator.SetBool(isRunningHash, false);
                animator.SetBool(isIdleSwordHash, false);
            }
        }
      



        if (runningPressed  && !isDrawedSword && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
             animator.SetBool(isRunningSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining = 5;
            timerIsRunning = true;
            playerSwordController.swordEquipMethod();
            
        }
        //if doesnt run and already have sword go to idle with sword
        if (!runningPressed && isDrawedSword && !takeSwordPressed)
        {
            animator.SetBool(isRunningSwordHash, false);
            animator.SetBool(isIdleSwordHash, true);
        }
       
        if (runningPressed && isDrawedSword && !takeSwordPressed)
        {
            animator.SetBool(isIdleSwordHash, false);
            animator.SetBool(isRunningSwordHash, true);
        }
        //if is standard idle and taking sword then idle with sword
        if (!isRunningSword && !isRunning && !isDrawedSword && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isIdleSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining = 5;
            timerIsRunning = true;
            playerSwordController.swordEquipMethod();
        }
        //if walking back and pressed sword draw then run back with sword
        if (walkingBackPressed && !isDrawedSword && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isRunningSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining = 5;
            timerIsRunning = true;
            playerSwordController.swordEquipMethod();
        }
        //if we are not already running back then go to idle sword
        if (!walkingBackPressed && isDrawedSword && !takeSwordPressed)
        {
            animator.SetBool(isIdleSwordHash, true);
        }
        //if idle sword and pressed back run back with sword
        if (isDrawedSword && walkingBackPressed && !takeSwordPressed)
        {
            animator.SetBool(isRunningSwordHash, true);
        }
        //for D pressed
        if (diPressing && !isDrawedSword && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isRunningSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining = 5;
            timerIsRunning = true;
            playerSwordController.swordEquipMethod();
        }
        if (!diPressing && isDrawedSword && !takeSwordPressed)
        {
            animator.SetBool(isIdleSwordHash, true);
        }
        if (isDrawedSword && diPressing && !takeSwordPressed)
        {
            animator.SetBool(isRunningSwordHash, true);
        }
        //for A pressed
        if (aPressed && !isDrawedSword && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isRunningSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining = 5;
            timerIsRunning = true;
            playerSwordController.swordEquipMethod();
        }
        if (!aPressed && isDrawedSword && !takeSwordPressed)
        {
            animator.SetBool(isIdleSwordHash, true);
        }
        if (isDrawedSword && aPressed && !takeSwordPressed)
        {
            animator.SetBool(isRunningSwordHash, true);
        }
        //when we are running with sword and there are no enemies around
        if (isDrawedSword)
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0 && isDrawedSword)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    if (isDrawedSword && isRunningSword)//add enemies detection and check if there are no enemies when we are going to sneathe
                    {
                        animator.SetBool("isSneathed", true);
                        animator.SetBool(isRunningHash, true);
                        animator.SetBool("isDrawedSword", false);
                        animator.SetBool(isRunningSwordHash, false);
                        playerSwordController.swordUnequipMethod();
                    }
                    if (isDrawedSword && isIdleSword)//add enemies detection and check if there are no enemies when we are going to sneathe
                    {
                        animator.SetBool(isSneathedHash, true);
                        animator.SetBool(isRunningHash, false);
                        animator.SetBool(isDrawedSwordHash, false);
                        animator.SetBool(isIdleSwordHash, false);
                        playerSwordController.swordUnequipMethod();
                      

                    }
                    /*if (isDrawedSword && isRunningSwordBack)
                    {

                        animator.SetBool(isSneathedHash, true);
                        animator.SetBool(isRunningBackSwordHash, false);
                        animator.SetBool(isWalkingBackHash, true);
                        playerSwordController.swordUnequipMethod();

                    }*/

                    
                    timeRemaining = 0;
                    timerIsRunning = false;

                }
            }
        }

    }

}
