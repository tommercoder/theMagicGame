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
	public float desiredRotationSpeed;
	public Animator anim;
	public float Speed;
	public float allowPlayerMove;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;
	public float playerSpeed;
	bool isRunningSword;
	public bool canMove;
	public bool MouseOverInventoryB;//setting in event of entering ui
	public bool speedPotionUsingNow = false;
	public GameObject dialogBox;
	public audioManager am;

	void Start()
	{
	
		anim = this.GetComponent<Animator>();
		
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		controller = this.GetComponent<CharacterController>();
		canMove = true;
		
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
		//zmiana szybkości w zależności czy korzysta się gracz z mikstury szybkości
		if (!speedPotionUsingNow)
		{
			if (isRunningSword)
				playerSpeed = 6	;
			else if (!isRunningSword)
				playerSpeed = 5;
			else
				playerSpeed = 5;
		}
		
	}
	
	void PlayerMoveAndRotation()
	{
		//otrzymanie danych z wektorów wejściowych
		InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");
		
		Vector3 forward = cam.transform.forward;
		Vector3 right = cam.transform.right;
		//ograniczanie poruszania i obracania po y
		forward.y = 0f;
		//ograniczanie poruszania i obracania po y
		right.y = 0f;

		//robię tak żę wektory mają długość 1.0 dlatego że Input jest od 0 do 1 i nie ma sensu mieć większe znaczenia
		forward.Normalize();
		right.Normalize();
		//w zależności od tego jaki wektory wejściowy jest != 0 wyznaczam kierunek poruszania
		desiredMoveDirection = forward * InputZ + right * InputX;
		
		if (canMove)
		{
			//funkcja Slerp powoli się obraca transform od aktualnej rotacji do pożądanej z szybkością desiredRotationSpeed
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
			//sprawdzam czy CharacterContoller jest włączony
			if (controller.enabled)
			{
				//poruszam Controller funkcjią Move do żądanej pozycji z szybkościa Time.deltaTime * playerSpeed 
				controller.Move(desiredMoveDirection * Time.deltaTime * playerSpeed);
			}
		}		
	}
	public void runningAudioEvent()//animation event
    {
		if (!anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack")
				  && !anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
						&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
						&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
		{
			//włączam dzwięk poruszania w animacji poruszania się bez miecza
			am.Play("walk");
		}
    }
	public void swordRunningAudioEvent()//animation event
	{
		if (!anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttack")
				&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttack")
				&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttack")
			  && !anim.GetCurrentAnimatorStateInfo(2).IsName("firstAttackSecondThing")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("secondAttackSecondThing")
					&& !anim.GetCurrentAnimatorStateInfo(2).IsName("thirdAttackSecondThing"))
		{
			//włączam dzwięk poruszania w animacji poruszania się z mieczem
			am.Play("swordRun");
		}
    }
	
	void InputMagnitude()
	{
		
		//Input wektory
		InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");
		//Oblicz szybkość wejściową
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;
		//przesuwanie za pomocą animacji i funkcji PlayerMoveAndRotation(); w przypadku jeśli wejśiowe dane > 0
		if (Speed > allowPlayerMove)
		{
			//sprawdzenie czy mogę presuwać się
			if (canMove)
			{	
				//funkcja dla presuwania i obracania
				PlayerMoveAndRotation();	
				//zmieniam animacje w animatorze
				anim.SetBool("isRunning", true);		
			}
		}
	}

}
