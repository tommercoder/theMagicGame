using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class movement : MonoBehaviour
{
	#region Singleton
	public static movement instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("instance movement.cs");
			return;
		}

		instance = this;
	}

	#endregion
	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public Vector3 desiredMoveDirectionBack;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;
	private float verticalVel;
	private Vector3 moveVector;
	public float playerSpeed;
	bool isRunningSword;

	public bool canMove;
	attacksController attacks;
	public bool MouseOverInventoryB;//setting in event of entering ui
	public bool speedPotionUsingNow = false;
	
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public GameObject dialogBox;
	public audioManager am;
	
	public Rigidbody rb;

	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator>();
		
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		controller = this.GetComponent<CharacterController>();
		canMove = true;
		attacks = GetComponent<attacksController>();
	}
	//EVENT FOR MOUSE ENTERING INVENTORY UI
	public void MouseOverInventory()
    {
		
		cam.GetComponent<CinemachineBrain>().enabled = false;
		MouseOverInventoryB = true;
		
    }
	//EVENT FOR MOUSE EXIT INVENTORY UI
	public void MouseOutInventory()
    {
		
		cam.GetComponent<CinemachineBrain>().enabled = true;
		MouseOverInventoryB = false;

	}
	
	
	
	void Update()
	{
		
		if ((MouseOverInventoryB && inventoryManager.instance.inventoryOpened) || dialogBox.activeSelf
			|| pauseMenu.instance.menuIsOpened)
			return;
		if(!inventoryManager.instance.inventoryOpened && !dialogBox.activeSelf)
        {
			MouseOutInventory();
        }
		
		InputMagnitude();
		isRunningSword = anim.GetBool("isRunningSword");

		if (!speedPotionUsingNow)
		{
			if (isRunningSword)
				playerSpeed = 6	;
			else if (!isRunningSword)
				playerSpeed = 5;
			else
				playerSpeed = 5;
		}
		

		bool isDrawedSword = anim.GetBool("isDrawedSword");
		bool sPressed = Input.GetKey("s");
		isGrounded = controller.isGrounded;
		//gravity applying below
		if (isGrounded)
		{
			desiredMoveDirection += Physics.gravity * Time.deltaTime;
			
		}
		else
		{
			desiredMoveDirection += Physics.gravity * Time.deltaTime;
			
		}
		
		

	}
	
	void PlayerMoveAndRotation()
	{
		InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");
		bool MouseattackPressed = Input.GetMouseButtonDown(0);
		bool isDrawedSword = anim.GetBool("isDrawedSword");
		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;
		var forwardP = transform.forward;
		var rightP = transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize();
		right.Normalize();
		
		desiredMoveDirection = forward * InputZ + right * InputX;
		desiredMoveDirectionBack = -forwardP *  -InputZ + rightP*InputX;
		
		if (isDrawedSword && MouseattackPressed)
        {
			//canMove = false;

        }
		
		if (blockRotationPlayer == false && canMove)
		{
			
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
			if(controller.enabled)
			controller.Move(desiredMoveDirection * Time.deltaTime * playerSpeed);
			
			
		}
					
		
	}
	public void runningAudioEvent()//animation event
    {
	if( !anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
				&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
				&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack")
			  && !anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
		am.Play("walk");
    }
	public void swordRunningAudioEvent()//animation event
	{
		if (!anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
				&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
				&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack")
			  && !anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
			am.Play("swordRun");
    }
	
	void InputMagnitude()
	{
		bool isDrawedSword = anim.GetBool("isDrawedSword");
		//Calculate Input Vectors
		InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");

		anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
		anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);
		
		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;
		
		//Physically move player
		if (Speed > allowPlayerRotation)
		{
			if (canMove )
			{
				
				PlayerMoveAndRotation();
				
				anim.SetBool("isRunning", true);
				
			}
		}
		else if (Speed < allowPlayerRotation)
		{

		}
	}

}
