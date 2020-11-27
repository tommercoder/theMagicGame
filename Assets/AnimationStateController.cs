using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    public float timeRemaining = 1;
    public bool timerIsRunning = false;
    int isRunningHash,isWalkingBackHash,isDrawedSwordHash,isIdleSwordHash,isRunningSwordHash,isRunningBackSwordHash, isSneathedHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timerIsRunning = true;
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingBackHash = Animator.StringToHash("isWalkingBack");
        isDrawedSwordHash = Animator.StringToHash("isDrawedSword");
        isIdleSwordHash = Animator.StringToHash("isIdleSword");
        isRunningSwordHash = Animator.StringToHash("isRunningSword");
        isRunningBackSwordHash = Animator.StringToHash("isRunningSwordBack");
        isSneathedHash = Animator.StringToHash("isSneathed");
    }

    // Update is called once per frame
    void Update()
    {


        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkingBack = animator.GetBool(isWalkingBackHash);
        bool isIdleSword = animator.GetBool(isIdleSwordHash);
        bool isDrawedSword = animator.GetBool(isDrawedSwordHash);
        bool isRunningSword = animator.GetBool(isRunningSwordHash);
        bool isRunningSwordBack = animator.GetBool(isRunningBackSwordHash);
        bool isSneathedSword = animator.GetBool(isSneathedHash);

        bool runningPressed = Input.GetKey("w");
        bool walkingBackPressed = Input.GetKey("s");
        bool takeSwordPressed = Input.GetKey(KeyCode.Mouse0);

        if(runningPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        if(!runningPressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
        //walk back
        if(!isWalkingBack && walkingBackPressed)
        {
            animator.SetBool(isWalkingBackHash, true);
        }
        if(isWalkingBack && !walkingBackPressed)
        {
            animator.SetBool(isWalkingBackHash, false);
        }
        //take sword and run with sword after running
        if (runningPressed && !isDrawedSword && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isRunningSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining = 5;
            timerIsRunning = true;

        }
        //if doesnt run and already have sword go to idle with sword
        if(!runningPressed && isDrawedSword && !takeSwordPressed)
        {
            //animator.SetBool(isRunningHash, false);
            animator.SetBool(isRunningSwordHash, false);
            animator.SetBool(isIdleSwordHash, true);
        }
        //if idle with sword and pressed w - RUN with sword
        if(runningPressed && isDrawedSword && !takeSwordPressed && isIdleSword)
        {
            animator.SetBool(isRunningSwordHash, true);
        }
        //if is standard idle and taking sword then idle with sword
        if(!isRunningSword && !isRunning && !isDrawedSword && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isIdleSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining =5;
            timerIsRunning = true;
        }
        //if walking back and pressed sword draw then run back with sword
        if(walkingBackPressed && !isDrawedSword && takeSwordPressed)
        {
            
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isRunningBackSwordHash, true);
        }
        //if we are not already running back then go to idle sword
        if(!walkingBackPressed && isDrawedSword && !takeSwordPressed)
        {
            animator.SetBool(isRunningBackSwordHash, false);
            animator.SetBool(isIdleSwordHash, true);
        }
        //if idle sword and pressed back run back with sword
        if(isDrawedSword && walkingBackPressed && !takeSwordPressed)
        {
            animator.SetBool(isRunningBackSwordHash, true);
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
                      

                    }
                    if (isDrawedSword && isIdleSword)//add enemies detection and check if there are no enemies when we are going to sneathe
                    {
                        animator.SetBool(isSneathedHash, true);
                        animator.SetBool(isRunningHash, false);
                        animator.SetBool(isDrawedSwordHash, false);
                       

                        Debug.Log("Sneathed" + isSneathedSword);
                        Debug.Log("Drawed" + isDrawedSword);
                    }
                    
                    if(isSneathedSword)
                    {
                        animator.SetBool(isSneathedHash, false);
                    }
                    timeRemaining = 0;
                    timerIsRunning = false;

                }
            }
        }

    }
}
