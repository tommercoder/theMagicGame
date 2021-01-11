using UnityEngine;
using System.Collections;
/* Written by Elmar Hanlhofer  http://www.plop.at  2015 06 10*/

[ExecuteInEditMode]
public class AlignInEditor : MonoBehaviour 
{
	public bool align = false;
	public bool showLineToSurface = false;


	void Update () 
	{
		if (align)
		{
			RaycastHit hit;
			Ray ray = new Ray (transform.position, Vector3.down);
			if (Physics.Raycast(ray, out hit))
			{
				transform.position = hit.point;
				Debug.Log (transform.name + " aligned.");
			}
			else
			{
				Debug.Log ("No surface found for " + transform.name);
			}
			align = false;

		}

		if (showLineToSurface)
		{
			RaycastHit hit;
			Ray ray = new Ray (transform.position, Vector3.down);
			if (Physics.Raycast(ray, out hit))
			{
				Debug.DrawLine (transform.position, hit.point);
			}
		}
	}
}
