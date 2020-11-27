using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isRunningHash,isWalkingBackHash,isDrawedSwordHash,isIdleSwordHash,isRunningSwordHash,isRunningBackSwordHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingBackHash = Animator.StringToHash("isWalkingBack");
        isDrawedSwordHash = Animator.StringToHash("isDrawedSword");
        isIdleSwordHash = Animator.StringToHash("isIdleSword");
        isRunningSwordHash = Animator.StringToHash("isRunningSword");
        isRunningBackSwordHash = Animator.StringToHash("isRunningSwordBack");
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
        }
        //if walking back and pressed sword draw then run back with sword
        if(isWalkingBack && takeSwordPressed)
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isRunningBackSwordHash, true);
        }
        //if idle sword and pressed back run back with sword
        if(!isRunning && isDrawedSword && !takeSwordPressed && !isWalkingBack && walkingBackPressed)
        {
            animator.SetBool(isRunningBackSwordHash, true);
        }

    }
}
