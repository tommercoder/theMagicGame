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
  
    public bool attackState = false;
    //bool isFullEnergyBar = false;
    //playerSword playerSwordController;
    //List<int> possibleAttacks = Enumerable.Range(1, 3).ToList();
    StateControllerTest controller;
    movement movement;
    public Text errorText;
   
    bool firstPlayed;
    bool secondPlayed;
    public AbilityMain [] Abilities;
    public bool canClick;
    public bool canClickSec;
   public int noOfClick;
    public int noOfClickSecond;

    public GameObject dialogBox;
    public bool canCast;
    public CharacterController CharacterController;
    [SerializeField] public ParticleSystem dashParticle = default;
    public bool isDrawedSword;
    private void Awake()
    {
        Abilities = GetComponents<AbilityMain>();//mozna dodac jeszcze abilities ille chcę
        instanceA = this;
        s_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canCast = false;
        
        noOfClick = 0;
        noOfClickSecond = 0;
        canClick = true;
        canClickSec = true;
        firstPlayed = false;
        secondPlayed = false;
        //playerSwordController = GetComponent<playerSword>();
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
   
    // Update is called once per frame
    void Update()
    {
       
        if ((movement.instance.MouseOverInventoryB && inventoryManager.instance.inventoryOpened) || dialogBox.activeSelf
            || pauseMenu.instance.menuIsOpened)
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
                Abilities[1].TriggerAbility();
                
                if (!Abilities[1].dashStarted)
                {
                    //errorText.gameObject.SetActive(true);
                    // errorText.text = "you can't use dash here";
                    logShow.instance.showText("you can't use dash here");
                    //StartCoroutine(waitErrorText());
                    
                    return;
                }
                canClick = false;
                canClickSec = false;
                FindObjectOfType<audioManager>().Play("dashSound");
                dashParticle.Play();
                    controller.timeRemaining = 5;
                    controller.timerIsRunning = true;
                
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
               if(characterStats.instance.damageFromFireball==0)
                {
                    logShow.instance.showText("now your fireball damage is 0(get some XP)");
                }
                movement.canMove = false;
                    animator.SetInteger("attackAnimation", 21);
                canClick = false;
                canClickSec = false;
                Abilities[0].TriggerAbility();
                    canCast = false;
                    controller.timeRemaining = 5;
                    controller.timerIsRunning = true;
                

            }
            if(swordSlashAbilityPressed && Abilities[2].canUse)
            {
                //because at start we have 0 time of ability
                if(characterStats.instance.timeOfSwordAbility==0)
                {
                    logShow.instance.showText("get min 100XP to use this ability");
                }
                Abilities[2].TriggerAbility();
                controller.timeRemaining = 10;
                controller.timerIsRunning = true;
                //noOfClick = 0;
                //noOfClickSecond = 0;
            }
            
        }
        #endregion
      
        //attacks
        if (Input.GetMouseButtonDown(0) && isDrawedSword && !firstPlayed )  
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
        
        if(animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
        {
            movement.canMove = true;
        }

        #region stopattacksSHIFT
        if ((animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack"))
                && Input.GetKey(KeyCode.LeftShift))
            {

                animator.SetInteger("attackAnimation", 4);
                movement.canMove = true;
                canClick = true;
                noOfClick = 0; noOfClickSecond = 0;
        }
            else if ((animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
                || animator.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
                && Input.GetKey(KeyCode.LeftShift))
            {
                canClickSec = true;
                noOfClickSecond = 0; noOfClick = 0;
            animator.SetInteger("attackAnimation", 4);
                movement.canMove = true;
                
            }
        #endregion
       // Debug.Log(noOfClick + " " + noOfClickSecond);
        if(animator.GetCurrentAnimatorStateInfo(3).IsName("Great Sword Impact"))
        {
            //noOfClick = 0;
           // noOfClickSecond = 0;
            //canClick = false;
            //canClickSec = false;
        }
    }
    public void endImpactEvent()
    {
        //canClick = true;
        //canClickSec = true;
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
        if(canClickSec && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash"))
        {
            noOfClickSecond++;
        }
        if(noOfClickSecond == 1 && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash"))
        {
            animator.SetInteger("attackAnimation", 11);
        }
    }
    void ComboStarter()
    {
        if(canClick && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash"))
        {
            noOfClick++;
        }
        if(noOfClick == 1 && !animator.GetCurrentAnimatorStateInfo(2).IsName("dash"))
        {
            animator.SetInteger("attackAnimation", 1);
        }
    }    
    public void secondCombatCheck()//animation event
    {
        canClickSec = false;
        
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing") && noOfClickSecond == 1)
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
        else if (animator.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing") && noOfClickSecond == 2)
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
            noOfClickSecond = 0; noOfClick = 0;
            canClickSec = true;
            movement.canMove = true;
        }

        secondPlayed = true;
        controller.timeRemaining = 5;
        controller.timerIsRunning = true;
        movement.canMove = true;
     
    }
    public void firstAttackSoundEffectEvent()
    {
        FindObjectOfType<audioManager>().Play("attack1");
        FindObjectOfType<audioManager>().Play("swish1");
    }
    public void CombatCheck()
    {
        canClick = false;

        
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("firstAttack") && noOfClick == 1)
        {
            
            animator.SetInteger("attackAnimation", 4);
            canClick = true;
            noOfClick = 0; 
            noOfClickSecond = 0;
          
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
            noOfClickSecond = 0;
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
           
           
            noOfClick = 0; 
            noOfClickSecond = 0;
            canClick = true;
         
            movement.canMove = true;
        }
       
        firstPlayed = true;
        controller.timeRemaining = 5;
        controller.timerIsRunning = true;
        movement.canMove = true;
       
    }
    /*public void firstDoneEvent()
    {
        firstPlayed = true;
    }
    public void secondDoneEvent()
    {
        secondPlayed = true;
    }*/
    //public void checkEnergyAttackFunc()
    //{
         
    //    //isEnergyAttackDone = true;
       
    //    movement.canMove = true;
    //    noOfClick = 0;
    //    noOfClickSecond = 0;
    //}
    /*public void checkIfCanMoveEvent()
    {
        movement.canMove = true;
    }*/
    //int randomAttack()
    //{
    //    int number = 0;

        
    //    if (possibleAttacks.Count == 3)
    //    {
    //        int index = Random.Range(0, 3);
    //        number = possibleAttacks[index];
    //        possibleAttacks.RemoveAt(index);
    //    }
    //    else if(possibleAttacks.Count == 2)
    //    {
    //        int index = Random.Range(0, 2);
    //        number = possibleAttacks[index];
    //        possibleAttacks.RemoveAt(index);
    //    }
    //    else if(possibleAttacks.Count == 1)
    //    {
    //        int index = Random.Range(0, 1);
    //        number = possibleAttacks[index];
    //        possibleAttacks.RemoveAt(index);
    //    }
    //    else if (possibleAttacks.Count == 0)
    //    {
    //        possibleAttacks = Enumerable.Range(1, 3).ToList();

    //        int index = Random.Range(0, 3);
    //        number = possibleAttacks[index];
    //        possibleAttacks.RemoveAt(index);
    //    }

       


    //    // int number = Random.Range(1, 4);

    //    return number;
    //}

}

