using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class attacksController : MonoBehaviour
{
    public static attacksController instanceA;

    protected static attacksController s_Instance;
    public static attacksController instance { get { return s_Instance; } }
    public Animator animator;
    int isDrawedSwordHash;
    public audioManager am;
    public bool attackState = false;
   
    StateControllerTest controller;
    movement movement;
    public Text errorText;

    bool firstPlayed;
    bool secondPlayed;
    public AbilityMain[] Abilities;
    public bool canClick;
    public bool canClickSec;
    public int noOfClick;
    public int noOfClickSecond;

    public GameObject dialogBox;
    public bool canCast;
    public CharacterController CharacterController;
    [SerializeField] public ParticleSystem dashParticle = default;
    public bool isDrawedSword;
    public swordEquipping fire;
    public swordEquipping air;
    public swordEquipping earth;
    public swordEquipping water;
    public swordEquipping emotions;
    private void Awake()
    {
        Abilities = GetComponents<AbilityMain>();//mozna dodac jeszcze abilities ille chcę
        instanceA = this;
        s_Instance = this;
    }
    
    void Start()
    {
        canCast = false;

        noOfClick = 0;
        noOfClickSecond = 0;
        canClick = true;
        canClickSec = true;
        firstPlayed = false;
        secondPlayed = false;
        
        controller = GetComponent<StateControllerTest>();
        animator = GetComponent<Animator>();
        movement = GetComponent<movement>();
        CharacterController = GetComponent<CharacterController>();

        isDrawedSwordHash = Animator.StringToHash("isDrawedSword");

    }

    IEnumerator waitErrorText()
    {
        yield return new WaitForSeconds(3);
        errorText.gameObject.SetActive(false);
    }

    
    void Update()
    {

        if ((movement.instance.MouseOverInventoryB && inventoryManager.instance.inventoryOpened) || dialogBox.activeSelf
            || pauseMenu.instance.menuIsOpened || pauseMenu.instance.pauseOpened)
            return;
        isDrawedSword = animator.GetBool(isDrawedSwordHash);
    
        bool castPressed = Input.GetKey("c");
        bool ballCastPressed = Input.GetKey("x");
        bool swordSlashAbilityPressed = Input.GetKey("q");


        #region abilities
        if (isDrawedSword)
        {

            
            if(castPressed && Abilities[1].canUse && !animator.GetCurrentAnimatorStateInfo(2).IsName("ballcast") && noOfClick==0 && noOfClickSecond==0
               &&  !animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
                && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
                && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack")
               
              && !animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
                    && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
                    && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
            {
                
                if (playerSword.instance.currentSword == water || playerSword.instance.currentSword == earth || playerSword.instance.currentSword == emotions)
                {
                    Abilities[1].TriggerAbility();
                    if (!Abilities[1].dashStarted)
                    {
                        
                        logShow.instance.showText("you can't use dash here");
                        

                        return;
                    }
                    
                    canClick = false;
                    canClickSec = false;
                    FindObjectOfType<audioManager>().Play("dashSound");
                    dashParticle.Play();
                    controller.timeRemaining = 5;
                    controller.timerIsRunning = true;
                }
                else
                {
                    logShow.instance.showText("dash can be used only with WATER or EARTH sword or EMOTIONS sword");
                }
                
                //random damage and effects here
            }
            
            if (ballCastPressed && !animator.GetCurrentAnimatorStateInfo(2).IsName("ballcast")  && Abilities[0].canUse && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash") && noOfClick== 0 && noOfClickSecond== 0
                &&  !animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
                && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
                && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack")
              && !animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
                    && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
                    && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
            {
                if (playerSword.instance.currentSword == fire || playerSword.instance.currentSword == air || playerSword.instance.currentSword==emotions)
                {
                    if (characterStats.instance.damageFromFireball == 0)
                    {
                        logShow.instance.showText("now your fireball damage is 0(get some XP)");
                    }
                   // movement.canMove = false;
                    animator.SetInteger("attackAnimation", 21);
                    canClick = false;
                    canClickSec = false;
                    Abilities[0].TriggerAbility();
                    am.Play("fireball");
                    canCast = false;
                    controller.timeRemaining = 5;
                    controller.timerIsRunning = true;
                }
                else
                {
                    logShow.instance.showText("fireball can be used only with AIR or FIRE sword or EMOTIONS sword");
                }

            }
            if(swordSlashAbilityPressed && Abilities[2].canUse)
            {
                //because at start we have 0 time of ability
                if (characterStats.instance.timeOfSwordAbility == 0)
                {
                    logShow.instance.showText("get min 100XP to use this ability");
                }
                else
                {
                    Abilities[2].TriggerAbility();
                    controller.timeRemaining = 10;
                    controller.timerIsRunning = true;
                }
                
            }
            
        }
        #endregion
      
        //attacks
        if (Input.GetMouseButtonDown(0) && isDrawedSword && !firstPlayed )  
        {
            ComboStarter();
            if (noOfClick > 6)
            {
                animator.SetInteger("attackAnimation", 4);
                
                noOfClick = 0;

                canClick = true;
            }
            controller.timeRemaining = 5;
            controller.timerIsRunning = true;
           
        }
        if(firstPlayed && Input.GetMouseButtonDown(0) && isDrawedSword)  
        {
            ComboStarterSecond();
            if (noOfClickSecond > 6)
            {
                animator.SetInteger("attackAnimation", 4);
                noOfClickSecond = 0;
             
                canClickSec = true;
              
            }
            controller.timeRemaining = 10;
            controller.timerIsRunning = true;
            
        }
        
        if(firstPlayed && secondPlayed)
        {
            firstPlayed = false;
            secondPlayed = false;
        }
        
        if(animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
        {
            movement.canMove = true;
        }

        #region stopattacksSHIFT
        if (Input.GetKey(KeyCode.LeftShift) &&(animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
                )
            {

                animator.SetInteger("attackAnimation", 4);
              //  movement.canMove = true;
                canClick = true;
                noOfClick = 0; noOfClickSecond = 0;
        }
            else if (Input.GetKey(KeyCode.LeftShift) &&(animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
               )
            {
                canClickSec = true;
                noOfClickSecond = 0; noOfClick = 0;
                animator.SetInteger("attackAnimation", 4);
              //  movement.canMove = true;
                
            }

        
        #endregion


        //#region try
        //if (((animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
        //        || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
        //        || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack")) && noOfClick == 0)
        //        )
        //{
        //    StartCoroutine(waitSec());

        //}
        //else if ((animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
        //    || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
        //    || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing")) && noOfClickSecond==0)

        //{
        //    StartCoroutine(waitSec2());
        //    //  movement.canMove = true;

        //}

        //#endregion
        //if (noOfClick > 3 || noOfClickSecond > 3)
        //{
        //    noOfClick = 0;
        //    noOfClickSecond = 0;
        //}
        if (animator.GetCurrentAnimatorStateInfo(3).IsName("Great Sword Impact"))
        {
            noOfClick = 0;
            noOfClickSecond = 0;
            canClick = false;
            canClickSec = false;
        }
    }
    IEnumerator waitSec()
    {
        yield return new WaitForSeconds(1f);
        animator.SetInteger("attackAnimation", 4);
        //  movement.canMove = true;
        canClick = true;
        noOfClick = 0; //noOfClickSecond = 0;
    }
    IEnumerator waitSec2()
    {
        yield return new WaitForSeconds(1f);
        canClickSec = true;
        noOfClickSecond = 0; //noOfClick = 0;
        animator.SetInteger("attackAnimation", 4);
    }
    public void endImpactEvent()
    {
        
        Debug.Log("impact event called");
    }
    public void startFireballEvent()
    {
        canCast = true;
    }
    public void dashEndedEvent()
    {
        animator.SetInteger("attackAnimation", 4);
        movement.canMove = true;
        canClick = true;
        canClickSec = true;
        dashParticle.Stop();
    }
    public void castedEvent()
    {
        movement.canMove = true;
        animator.SetInteger("attackAnimation", 4);
        canClick = true;
        canClickSec = true;
        canCast = false;
    }
    void ComboStarterSecond()
    {
        
        if (canClickSec && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash") && !animator.GetCurrentAnimatorStateInfo(2).IsName("ballcast"))
        {
            noOfClickSecond++;
           
        }
        if(noOfClickSecond == 1 && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash") && !animator.GetCurrentAnimatorStateInfo(2).IsName("ballcast"))
        {
            animator.SetInteger("attackAnimation", 11);
        }
        
    }
    void ComboStarter()
    {
        if(canClick && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash") && !animator.GetCurrentAnimatorStateInfo(2).IsName("ballcast"))
        {
            noOfClick++;
            
        }
        if(noOfClick == 1 && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash") && !animator.GetCurrentAnimatorStateInfo(2).IsName("ballcast"))
        {
            animator.SetInteger("attackAnimation", 1);
        }
        
    }    
    public void secondCombatCheck()//animation event
    {
        canClickSec = false;
        
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing") && noOfClickSecond == 1 )
        {
            
            animator.SetInteger("attackAnimation", 4);
            canClickSec = true;

            noOfClickSecond = 0; 
            noOfClick = 0;
            movement.canMove = true;
           
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing") && noOfClickSecond >= 2)
        {

            animator.SetInteger("attackAnimation", 12);
            canClickSec = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing") && noOfClickSecond == 2 )
        {

            animator.SetInteger("attackAnimation", 4);
            canClickSec = true;
            noOfClickSecond = 0; noOfClick = 0;
            movement.canMove = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing") && noOfClickSecond >= 3)
        {
            
            animator.SetInteger("attackAnimation", 13);
            canClickSec = true;
            
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
        {
           
            animator.SetInteger("attackAnimation", 4);

            movement.canMove = true; 
            noOfClickSecond = 0; 
            noOfClick = 0;
            canClickSec = true;
            
        }
        else
        {

            animator.SetInteger("attackAnimation", 4);

            movement.canMove = true;
            noOfClickSecond = 0;
            noOfClick = 0;
            canClickSec = true;
        }
        //if(noOfClickSecond > 3 || noOfClick > 3)
        //{
        //    animator.SetInteger("attackAnimation", 4);
        //    noOfClickSecond = 0; 
        //    noOfClick = 0;
        //    canClickSec = true;
        //    movement.canMove = true;
        //}
        //if (Input.GetMouseButtonDown(0) && (noOfClick > 3 || noOfClickSecond > 3) && isDrawedSword && (!animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
        //       && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
        //       && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
        //       )
        //{
        //    Debug.Log("SUUUUUUUUUUUUUUUKA");
        //    animator.SetInteger("attackAnimation", 4);
        //    //  movement.canMove = true;
        //    canClick = true; canClickSec = true;
        //    noOfClick = 0; noOfClickSecond = 0;
        //}
        //else if (Input.GetMouseButtonDown(0) && (noOfClick > 3 || noOfClickSecond > 3) && isDrawedSword && (!animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
        //    && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
        //    && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
        //   )
        //{
        //    Debug.Log("SUUUUUUUUUUUUUUUKA");
        //    canClickSec = true;
        //    canClick = true;
        //    noOfClickSecond = 0; noOfClick = 0;
        //    animator.SetInteger("attackAnimation", 4);
        //    //  movement.canMove = true;

        //}
        secondPlayed = true;
        controller.timeRemaining = 5;
        controller.timerIsRunning = true;
        movement.canMove = true;
        canClickSec = true;
     
    }
    //sound effects
    public void firstAttackSoundEffectEvent()
    {
    
        if(!pauseMenu.instance.menuIsOpened && ! pauseMenu.instance.pauseOpened)
       am.Play("swish1");
    }
    public void secondAttackSoundEffectEvent()
    {
        if (!pauseMenu.instance.menuIsOpened && !pauseMenu.instance.pauseOpened)
        {
            am.Stop("walk");
            am.Stop("swordRun");
            am.Play("swish2");
        }
    }
    public void thirdAttackSoundEffectEvent()
    {
        
        if (!pauseMenu.instance.menuIsOpened && !pauseMenu.instance.pauseOpened)
        {
            am.Stop("walk");
            am.Stop("swordRun");
            am.Play("swish5");
        }
    }
    public void firstAttackSecondThingAudioEvent()
    {
        if (!pauseMenu.instance.menuIsOpened && !pauseMenu.instance.pauseOpened)
        {
            am.Stop("walk");
            am.Stop("swordRun");
            am.Play("swish2");
        }
    }
    public void secondAttackSecondThingAudioEvent()
    {
        if (!pauseMenu.instance.menuIsOpened && !pauseMenu.instance.pauseOpened)
        {
            am.Stop("walk");
            am.Stop("swordRun");
            am.Play("swish3");
        }
    }
    public void thirdAttackSecondThingAudioEvent()
    {
        if (!pauseMenu.instance.menuIsOpened && !pauseMenu.instance.pauseOpened)
        {
            am.Stop("walk");
            am.Stop("swordRun");
            am.Play("swish4");
        }
    }
    public void CombatCheck()
    {
        canClick = false;

        
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack") &&noOfClick == 1 )
        {
            
            animator.SetInteger("attackAnimation", 4);
            canClick = true;
            noOfClick = 0; 
            noOfClickSecond = 0;
            
            movement.canMove = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack") && noOfClick >= 2 )
        {
           
            animator.SetInteger("attackAnimation", 2);
            canClick = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack") && noOfClick == 2)
        {
            
            animator.SetInteger("attackAnimation", 4);
            canClick = true;
            noOfClick = 0; 
            noOfClickSecond = 0;
            movement.canMove = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack") && noOfClick >= 3 )
        {
           
            animator.SetInteger("attackAnimation", 3);
            canClick = true;


            if(noOfClick==0)
            {
                animator.SetInteger("attackAnimation", 4);
                canClick = true;

            }
            //noOfClick = 0;
        }
        else if(animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
        {
           
            animator.SetInteger("attackAnimation", 4);
           
           
            noOfClick = 0; 
            noOfClickSecond = 0;
            canClick = true;
         
            movement.canMove = true;
        }
        else
        {
            animator.SetInteger("attackAnimation", 4);
            noOfClick = 0;
            noOfClickSecond = 0;
            canClick = true;
            canClickSec = true;
        }
        //if(noOfClick > 3 || noOfClickSecond > 3)
        //{
        //    animator.SetInteger("attackAnimation", 4);
        //    noOfClick = 0;
        //    noOfClickSecond = 0;
        //    canClick = true;
        //    canClickSec = true;
        //}
        //if (Input.GetMouseButtonDown(0) && (noOfClick > 3 || noOfClickSecond > 3) && isDrawedSword && (!animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
        //        && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
        //        && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
        //        )
        //{
        //    Debug.Log("SUUUUUUUUUUUUUUUKA");
        //    animator.SetInteger("attackAnimation", 4);
        //    //  movement.canMove = true;
        //    canClick = true; canClickSec = true;
        //    noOfClick = 0; noOfClickSecond = 0;
        //}
        //else if (Input.GetMouseButtonDown(0) && (noOfClick > 3 || noOfClickSecond > 3) && isDrawedSword && (!animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
        //    && !animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
        //    && !animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
        //   )
        //{
        //    animator.SetInteger("attackAnimation", 4);
        //    Debug.Log("SUUUUUUUUUUUUUUUKA");
        //    canClickSec = true;
        //    canClick = true;
        //    noOfClickSecond = 0; noOfClick = 0;
            
        //    //  movement.canMove = true;

        //}
        firstPlayed = true;
        controller.timeRemaining = 5;
        controller.timerIsRunning = true;
        movement.canMove = true;
        canClick = true;
       
    }
    
}

