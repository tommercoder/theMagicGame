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
   
    public GameObject dialogBox;
    
    
    bool isRunning;
    bool isIdleSword;
    bool isDrawedSword;
    bool isRunningSword;
    bool runningPressed;
    bool walkingBackPressed;
    bool diPressing;
    bool aPressed;
    bool takeSwordPressed;
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


    

    void Update()
    {
        if(pauseMenu.instance.pauseOpened)
            return;
        //ten "region" odpowiada za sprawdzanie wcisknięcia przycisków
        #region ButtonsEvents
        takeSwordPressed = Input.GetKey(KeyCode.Mouse2);
        isRunning = animator.GetBool(isRunningHash);
        //"Hash" zmienne znaczą że już jest znana animacja informację o której chcę otrzymać,czyli ona jest zdefiniowana na początku i nie muszę ciągle szukać jej w animatorze
        isIdleSword = animator.GetBool(isIdleSwordHash);
        isDrawedSword = animator.GetBool(isDrawedSwordHash);
        isRunningSword = animator.GetBool(isRunningSwordHash);

        runningPressed = Input.GetKey("w");
        walkingBackPressed = Input.GetKey("s");
        diPressing = Input.GetKey("d");
        aPressed = Input.GetKey("a");
        #endregion
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



        
        //jeśli miecz jest wzięty "trail" zostaje wyłączony
        if (isDrawedSword)
        {
            
            Transform sword = playerSwordController.sword.transform;
            //sprawdzanie czy miecz ma "trail" jako GameObject
            if (sword.gameObject.transform.childCount > 0)
            {
                sword.GetChild(0).gameObject.SetActive(false);
            }
        }
        //w innym przypadku kiedy miecz jest z tyłu włączam się ten "trail" i wyłaczam drugi "trail" który jest wykorzystywany w zdolnościach gracza
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
        //jeśli żaden przycisk nie jest wciśnięty to sprawdzam czy miecz jest wzięty i wracam do animacji "idle" z mieczem,albo w innym przypadku bez niego
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
     


        //sprawdzenia różnych możliwości(w tym przypadku czy poruszamy się i wciskamy "wzięcie miecza"
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

        if (runningPressed && isDrawedSword && !takeSwordPressed)
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
        if (isDrawedSword && walkingBackPressed && !takeSwordPressed)
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
        if (isDrawedSword && diPressing && !takeSwordPressed)
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
        if (isDrawedSword && aPressed && !takeSwordPressed)
        {
            animator.SetBool(isRunningSwordHash, true);
        }

        //jeśli miecz jest w rękach
        if (isDrawedSword)
        {

            //timer służy do chowania miecza automatycznie
            if (timerIsRunning)
            {
                //timerIsRunning=true tylko wtedy kiedy miecz jest w rękach
                if (timeRemaining > 0 && isDrawedSword)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    //w przypadku kiedy czas minął chowamy miecz,ale przed tym sprawdzamy warunki
                    if (isDrawedSword && isRunningSword)
                    {
                        animator.SetBool("isSneathed", true);//uruchamia się animacja sneath sword
                        animator.SetBool(isRunningHash, true);
                        animator.SetBool("isDrawedSword", false);
                        animator.SetBool(isRunningSwordHash, false);
                        //ta funckaj zmienia pozycje miecza pod czas aniamcji chowanie miecza i wstawia go do tylu
                        playerSwordController.swordUnequipMethod();//wstawiam miecz do tylu
                    }
                    if (isDrawedSword && isIdleSword)
                    {
                        animator.SetBool(isSneathedHash, true);
                        animator.SetBool(isRunningHash, false);
                        animator.SetBool(isDrawedSwordHash, false);
                        animator.SetBool(isIdleSwordHash, false);
                        playerSwordController.swordUnequipMethod();


                    }
                  
                    timeRemaining = 0;
                    timerIsRunning = false;

                }
            }
        }

    }

}
