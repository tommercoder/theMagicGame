using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	RaycastHit hit;//For Detect Sureface/Base.
	Vector3 surfaceNormal;//The normal of the surface the ray hit.
	Vector3 forwardRelativeToSurfaceNormal;//For Look Rotation
	
	// Update is called once per frame
	void Update () {
		CharacterFaceRelativeToSurface ();
	}
	
	//Method For Correct Character Rotation According to Surface.
	private void CharacterFaceRelativeToSurface()
	{
		//For Detect The Base/Surface.
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10)) 
		{
			surfaceNormal = hit.normal; // Assign the normal of the surface to surfaceNormal
			forwardRelativeToSurfaceNormal = Vector3.Cross(transform.right, surfaceNormal); 
			Quaternion targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, surfaceNormal); //check For target Rotation.
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2); //Rotate Character accordingly.
		}
	}
}