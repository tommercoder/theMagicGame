using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6.0F;//Character Movement Speed.
	public float jumpSpeed = 8.0F;//Character jump Speed.
	public float gravity = 20.0F;//Gravity(We have to apply gravity manualy in character controller component).
	private Vector3 moveDirection = Vector3.zero;//Character Movement Direction.
	
	void Update () {
		MoveCharacter ();
	}
	
	//Method For Character Movement
	void MoveCharacter()
	{
		CharacterController controller = GetComponent<CharacterController>();// Get Character Controller Component
		if (controller.isGrounded)//Check Whether The Character is In Air or not
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // Get Input From User
			moveDirection = transform.TransformDirection(moveDirection);//Specify The Character Movement Direction in world space.
			moveDirection *= speed;//Add Movement Speed To Character.
			if (Input.GetButton("Jump"))//Check For JUMP Button.
				moveDirection.y = jumpSpeed;//Make Character JUMP.
		}
		moveDirection.y -= gravity * Time.deltaTime; //Move Character Down Because Of Gravity That We have assign before.
		controller.Move(moveDirection * Time.deltaTime);//Move The Character in specific Direction.
	}

}
