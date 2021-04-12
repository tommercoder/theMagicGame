using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StateControllerTest : MonoBehaviour
{
    #region Singleton

    public static StateControllerTest instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    Animator animator;
    playerSword playerSwordController;

    public float timeRemaining = 5;
    public bool timerIsRunning = false;
    public float SwordRunning;
    public float SwordIdle;
    public float attackStates = 0.0f;
    public float acceleration = 0.1f;
    public float decceleration = 0.5f;
    int isRunningHash, isDrawedSwordHash, isIdleSwordHash, isRunningSwordHash, isSneathedHash;
    public bool enemiesAround = false;
    public GameObject dialogBox;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesAround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesAround = false;
        }
    }
    attacksController attacks;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerSwordController = GetComponent<playerSword>();
        timerIsRunning = true;

        attacks = GetComponent<attacksController>();

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
        bool takeSwordPressed = Input.GetKey(KeyCode.Mouse2);

        //stopping player when inventory is opened and mouse is in invenotry tab
        if ((movement.instance.MouseOverInventoryB && inventoryManager.instance.inventoryOpened) ||dialogBox.activeSelf || pauseMenu.instance.menuIsOpened)
        {
            if (isRunning)
            {
                animator.SetBool(isRunningHash, false);
            }
            else if (isDrawedSword && isRunningSword)
            {
                animator.SetBool(isRunningSwordHash, false);
                animator.SetBool(isRunningHash, false);
            }
            return;
        }



        ///trail setting
        if (isDrawedSword)
        {
            Transform sword = playerSwordController.sword.transform;
            if (sword.gameObject.transform.childCount > 0)
                sword.GetChild(0).gameObject.SetActive(false);
            //sword.GetChild(1).gameObject.SetActive(true);
            //Debug.Log("sword = " + sword.gameObject.name);

        }
        else
        {
            Transform sword = playerSwordController.sword.transform;
            if (sword.gameObject.transform.childCount > 0)
            {
                sword.GetChild(0).gameObject.SetActive(true);
                sword.GetChild(1).gameObject.SetActive(false);
            }
            movement movement = GetComponent<movement>();
            movement.canMove = true;

        }
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
        //hide the sword



        if (runningPressed && !isDrawedSword && takeSwordPressed && !animator.GetCurrentAnimatorStateInfo(1).IsName("sneathSword"))
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

        if (runningPressed && isDrawedSword && !takeSwordPressed /*&& !attacks.enemiesAround*/)
        {
            animator.SetBool(isIdleSwordHash, false);
            animator.SetBool(isRunningSwordHash, true);
        }
        //if is standard idle and taking sword then idle with sword
        if (!isRunningSword && !isRunning && !isDrawedSword && takeSwordPressed && !animator.GetCurrentAnimatorStateInfo(1).IsName("sneathSword"))
        {
            animator.SetBool(isDrawedSwordHash, true);
            animator.SetBool(isIdleSwordHash, true);
            animator.SetBool(isSneathedHash, false);
            timeRemaining = 5;
            timerIsRunning = true;
            playerSwordController.swordEquipMethod();
        }
        //if walking back and pressed sword draw then run back with sword
        if (walkingBackPressed && !isDrawedSword && takeSwordPressed && !animator.GetCurrentAnimatorStateInfo(1).IsName("sneathSword"))
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
        if (isDrawedSword && walkingBackPressed && !takeSwordPressed /*&& !attacks.enemiesAround*/)
        {
            animator.SetBool(isRunningSwordHash, true);
        }
        //for D pressed
        if (diPressing && !isDrawedSword && takeSwordPressed && !animator.GetCurrentAnimatorStateInfo(1).IsName("sneathSword"))
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
        if (isDrawedSword && diPressing && !takeSwordPressed /*&& !attacks.enemiesAround*/)
        {
            animator.SetBool(isRunningSwordHash, true);
        }
        //for A pressed
        if (aPressed && !isDrawedSword && takeSwordPressed && !animator.GetCurrentAnimatorStateInfo(1).IsName("sneathSword"))
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
        if (isDrawedSword && aPressed && !takeSwordPressed /*&& !attacks.enemiesAround*/)
        {
            animator.SetBool(isRunningSwordHash, true);
        }
        //when we are running with sword and there are no enemies around
        if (isDrawedSword)
        {


            if (timerIsRunning && !enemiesAround)
            {
                if (timeRemaining > 0 && isDrawedSword)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    if (isDrawedSword && isRunningSword)
                    {
                        animator.SetBool("isSneathed", true);//idzie animacja sneath sword
                        animator.SetBool(isRunningHash, true);
                        animator.SetBool("isDrawedSword", false);
                        animator.SetBool(isRunningSwordHash, false);
                        playerSwordController.swordUnequipMethod();//wstawiam sword na back
                    }
                    if (isDrawedSword && isIdleSword)
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
