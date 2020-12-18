using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement : MonoBehaviour
{

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
	public bool canMove ;
	attacksController attacks;
	// Use this for initialization
	void Start()
	{
		
		anim = this.GetComponent<Animator>();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController>();
		canMove = true;
		attacks = GetComponent<attacksController>();
	}

	// Update is called once per frame
	void Update()
	{
		
		InputMagnitude();
		isRunningSword = anim.GetBool("isRunningSword");

		if (isRunningSword)
			playerSpeed = 5;
		else if (!isRunningSword)
			playerSpeed = 4;
		else
			playerSpeed = 3;


		bool isDrawedSword = anim.GetBool("isDrawedSword");
		bool sPressed = Input.GetKey("s");
		isGrounded = controller.isGrounded;
		if (isGrounded)
		{
			verticalVel -= 0;
		}
		else
		{
			verticalVel -= 2;
		}
		moveVector = new Vector3(0, verticalVel, 0);
		
		controller.Move(moveVector);

		//slow walking
		if (attacks.enemiesAround && isDrawedSword && InputZ >= 0.0f)
		{
			anim.SetBool("walkAttack", true);
		}
		if (!attacks.enemiesAround && isDrawedSword && InputZ >= 0.0f)
		{
			anim.SetBool("walkAttack", false);

		}
		if (InputX.Equals(0.0f) && InputZ.Equals(0.0f))
		{
			anim.SetBool("walkAttack", false);
			anim.SetBool("walkAttackBack", false);
		}
		if(isDrawedSword && attacks.enemiesAround && InputZ < 0.0f)
        {
			anim.SetBool("walkAttackBack", true);
		}
		/*if(sPressed && isDrawedSword && attacks.enemiesAround)
        {
			anim.SetBool("walkAttackBack", false);

		}*/
		if(InputZ < 0.0f && !attacks.enemiesAround )
        {
			anim.SetBool("walkAttackBack", false);
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
		desiredMoveDirectionBack = -forwardP *  -InputZ + rightP*InputX;// forward * InputZ + right * InputX;
		if(isDrawedSword && MouseattackPressed)
        {
			canMove = false;

        }
		//Debug.Log("can move ?" + canMove);
		if (blockRotationPlayer == false && canMove)
		{

			if (attacks.enemiesAround && attacks.attackState && isDrawedSword)
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
			}
			else
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
				controller.Move(desiredMoveDirection * Time.deltaTime * playerSpeed);

			}
			//canMove = true;
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
