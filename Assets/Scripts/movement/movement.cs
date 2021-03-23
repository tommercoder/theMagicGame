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
	public bool MouseOverInventoryB;
	public bool speedPotionUsingNow = false;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 playerVelocity;
	private float jumpHeight = 10.0f;
	private float gravityValue = -9.81f;
	public Rigidbody rb;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator>();
		cam = Camera.main;
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
	public void StopJumpEvent()
    {
		//desiredMoveDirection.y = 3f;
		//Debug.Log("move" + desiredMoveDirection);
		
		//controller.Move(desiredMoveDirection);

		//anim.ResetTrigger("isJumpingTrigger");
		
	}
	public void StartJumpEvent()
    {
		
		
		
    }
	// Update is called once per frame
	void Update()
	{
		
		if (MouseOverInventoryB && inventoryManager.instance.inventoryOpened)
			return;
		if(!inventoryManager.instance.inventoryOpened)
        {
			MouseOutInventory();
        }
		//stop camera when inventory is open;
		//if (EventSystem.current.IsPointerOverGameObject())
		//{
		//	Debug.Log("entered");
		//	cam.GetComponent<CinemachineBrain>().enabled = false;
		//	return;
		//}
		//else
		//      {
		//	cam.GetComponent<CinemachineBrain>().enabled = true;
		//}

		//if (controller.isGrounded && Input.GetKey(KeyCode.Space))
		//{
		//	anim.SetTrigger("isJumpingTrigger");
		//	controller.enabled = false;
		//	GetComponent<CapsuleCollider>().enabled = true;

		//	rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

		//	Debug.Log("fLying away");

		//}

		
		



		InputMagnitude();
		isRunningSword = anim.GetBool("isRunningSword");

		if (!speedPotionUsingNow)
		{
			if (isRunningSword)
				playerSpeed = 5;
			else if (!isRunningSword)
				playerSpeed = 4;
			else
				playerSpeed = 3;
		}
		

		bool isDrawedSword = anim.GetBool("isDrawedSword");
		bool sPressed = Input.GetKey("s");
		isGrounded = controller.isGrounded;
		//gravity applying below
		if (isGrounded)
		{
			desiredMoveDirection += Physics.gravity * Time.deltaTime;
			//verticalVel -= 0;
		}
		else
		{
			desiredMoveDirection += Physics.gravity * Time.deltaTime;
			//verticalVel -= 2;
		}
		
		//moveVector = new Vector3(0, verticalVel, 0);
		
		
		//controller.Move(moveVector);
		//jumping part
		//if (controller.isGrounded && Input.GetKey(KeyCode.Space))
  //      {
		//	//GetComponent<movement>().enabled = false;
		//	if (!anim.GetBool("isRunning"))
		//	{
		//		anim.SetTrigger("isJumpingTrigger");
		//	}
		//	else if(anim.GetBool("isRunning"))
  //          {
		//		anim.SetBool("isRunning", false);
		//		anim.SetTrigger("isJumpingTrigger");
		//	}
			
			
		//}
	

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
		//if (Input.GetKey(KeyCode.Space) && isGrounded)
		//{
		//	desiredMoveDirection.y += jumpHeight * Time.deltaTime;
		//	Debug.Log("desired =" + desiredMoveDirection.y);
		//}
		//moveDirection.y -= gravity * Time.deltaTime;
		desiredMoveDirection = forward * InputZ + right * InputX;
		desiredMoveDirectionBack = -forwardP *  -InputZ + rightP*InputX;// forward * InputZ + right * InputX;
		
		if (isDrawedSword && MouseattackPressed)
        {
			canMove = false;

        }
		//Debug.Log("can move ?" + canMove);
		if (blockRotationPlayer == false && canMove)
		{
			
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
			if(controller.enabled)
			controller.Move(desiredMoveDirection * Time.deltaTime * playerSpeed);
			/*if (attacks.enemiesAround && attacks.attackState && isDrawedSword)
			{
				if (InputZ > 0.0f)
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
					controller.Move(desiredMoveDirection * Time.deltaTime * playerSpeed);
					
				}
				else
                {
					//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirectionBack), desiredRotationSpeed);
					controller.Move(desiredMoveDirectionBack * Time.deltaTime * playerSpeed);
				}
			}*/
			//else
			//{


			//}

		}
					
		
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

			//anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
			if (canMove )
			{
				
				PlayerMoveAndRotation();
				//if(!attacks.enemiesAround)
				anim.SetBool("isRunning", true);
				
			}
		}
		else if (Speed < allowPlayerRotation)
		{
		
			
			//anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
			

		}
	}

















    /*public CharacterController controller;
    public Transform cam;
	
    public float speed = 6f;
    public float turnTime = 0.1f;
    float turnVelocity;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);


            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }*/
}
