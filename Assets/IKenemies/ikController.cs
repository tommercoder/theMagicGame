using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ikController : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Transform baksBot = null;
    public float MoveSpeed = 3.5f;
    [SerializeField] Rigidbody targetRb;
    [SerializeField] Transform rightLegTarget;
    GameObject targetDontMove;
    // Is the leg moving?
    public bool Moving;
    public DontMoveWithParent dontMoveWithParentScript;
    private void Start()
    {
        
    }
    private void Update()
    {
        
        //targetRb.constraints = RigidbodyConstraints.FreezeAll;
        if (Input.GetKey(KeyCode.Space))
        {
            dontMoveWithParentScript.dontMoveWithParent = false;
        }
        //target.position = Vector3.MoveTowards(target.position, rightLegTarget.position, Time.deltaTime);

       
    }
   
    /*[SerializeField] Transform homeTransform;
    // Stay within this distance of home
    [SerializeField] float wantStepAtDistance;
    // How long a step takes to complete
    [SerializeField] float moveDuration;

    // Is the leg moving?
    public bool Moving;




    void Start()
    {

    }
    
    void Update()
    {
        // If we are already moving, don't start another move
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, homeTransform.position);

        // If we are too far off in position or rotation
        if (distFromHome > wantStepAtDistance)
        {
            // Start the step coroutine
            StartCoroutine(MoveToHome());
        }



    }
    // Coroutines must return an IEnumerator
    IEnumerator MoveToHome()
    {
        // Indicate we're moving (used later)
        Moving = true;

        // Store the initial conditions
        Quaternion startRot = transform.rotation;
        Vector3 startPoint = transform.position;

        Quaternion endRot = homeTransform.rotation;
        Vector3 endPoint = homeTransform.position;

        // Time since step started
        float timeElapsed = 0;

        // Here we use a do-while loop so the normalized time goes past 1.0 on the last iteration,
        // placing us at the end position before ending.
        do
        {
            // Add time since last frame to the time elapsed
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / moveDuration;

            // Interpolate position and rotation
            transform.position = Vector3.Lerp(startPoint, endPoint, normalizedTime);
            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveDuration);

        // Done moving
        Moving = false;
    }*/
}
