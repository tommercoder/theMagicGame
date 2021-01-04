using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class MainProceduralController : MonoBehaviour
{
    [Header("OBJECTS")]
    public Transform targetRight = null;
    public Transform targetLeft = null;
    public Transform baksBot = null;

    
    public LayerMask mask;
    [Header("STEP SETTINGS")]
    public bool Moving;
    public Transform stepTargetLeft;
    public Transform stepTargetRight;
    public List<Transform> stepTargets = new List<Transform>(2);
    public float wantStepAtDistance = 0.45f;
    public float timeToMakeStep = 0.125f;
    public bool rightStepped;
    public bool leftStepped;
    public bool waited = false;
    [SerializeField, Range(0, 1)] float stepOvershootFraction = 0.8f;
    public float standardBodyOffset;
    public Transform offsetObject;
    public DontMoveWithParent dontMoveWithParentRight;
    public DontMoveWithParent dontMoveWithParentLeft;
    public Transform target;
    private List<Transform> footIKTargets = new List<Transform>(2);
    Vector3 savedPositionRight;
    Vector3 savedPositionLeft;

    public TwoBoneIKConstraint twoBoneIKConstraint;
    private void Start()
    {
        footIKTargets.Add(targetLeft);
        footIKTargets.Add(targetRight);

        stepTargets.Add(stepTargetLeft);
        stepTargets.Add(stepTargetRight);
        rightStepped = false;
        leftStepped = false;

        savedPositionLeft = footIKTargets[0].position;
        savedPositionRight = footIKTargets[1].position;
    }


    IEnumerator wait()
    {
        waited = false;
        yield return new WaitForSeconds(timeToMakeStep);
        waited = true;
    }
    void moveForward()
    {
        baksBot.Translate(Vector3.right * Time.deltaTime*2f);
        //baksBot.localPosition = Vector3.MoveTowards(baksBot.position, offsetObject.position, Time.deltaTime*2f);
    }
   
    void Update()
    {
        
        
        
        stepTargetIk(0);
        stepTargetIk(1);
        
        bool rightGrounded = Physics.Raycast(footIKTargets[0].position, Vector3.down,mask);
        bool leftGrounded = Physics.Raycast(footIKTargets[1].position, Vector3.down,mask);

        float distanceRight = Vector3.Distance(footIKTargets[0].position, stepTargets[0].position);
        float distanceLeft = Vector3.Distance(footIKTargets[1].position, stepTargets[1].position);
        if (distanceRight > wantStepAtDistance)
        {
            dontMoveWithParentRight.dontMoveWithParent = false;
            if (GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal, 0))
            {
                
                Quaternion endRot = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(stepTargets[0].forward, endNormal),
                    endNormal
                );
                StartCoroutine(
                    MoveToPoint(endPos, endRot, timeToMakeStep, 0));
                
                StartCoroutine(wait());
                
                
            }
        }
        else
            dontMoveWithParentRight.dontMoveWithParent = true;

        if (distanceLeft > wantStepAtDistance && waited)
        {
            dontMoveWithParentLeft.dontMoveWithParent = false;
            if (GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal, 1))
            {

                Quaternion endRot = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(stepTargets[1].forward, endNormal),
                    endNormal
                );

                StartCoroutine(
                    MoveToPoint(endPos, endRot, timeToMakeStep, 1));
                StartCoroutine(wait());
            }
        }
        else
            dontMoveWithParentLeft.dontMoveWithParent = true;


        moveForward();


    }

    private void LateUpdate()
    {
        if(!Moving)
        {
            
               
            
               
        }
        if(Moving)
        {
           
               
               
        }
    }

    bool GetGroundedEndPosition(out Vector3 position, out Vector3 normal, int index)
    {
        Vector3 towardHome = (stepTargets[index].position - footIKTargets[index].position).normalized;


        float overshootDistance = wantStepAtDistance * stepOvershootFraction;
        Vector3 overshootVector = towardHome * overshootDistance;


        Vector3 raycastOrigin = stepTargets[index].position + overshootVector + stepTargets[index].up * 2f;

        if (Physics.Raycast(
            raycastOrigin,
            -stepTargets[index].up,
            out RaycastHit hit,
            Mathf.Infinity,
            mask
        ))
        {
            position = hit.point;
            normal = hit.normal;
            return true;
        }
        position = Vector3.zero;
        normal = Vector3.zero;
        return false;
    }
    void stepTargetIk(int index)
    {

        float distance = 100f;
        Ray ray = new Ray(stepTargets[index].position, -stepTargets[index].up);
        //works
        RaycastHit hit;

        // Debug.DrawRay(stepTarget.position, Vector3.down, Color.red);
        if (Physics.Raycast(stepTargets[index].position, Vector3.down, out hit, distance, mask))
        {

            Vector3 targetLocation = hit.point;
            var slopeRotation = Quaternion.FromToRotation(stepTargets[index].up, hit.normal);

            stepTargets[index].rotation = Quaternion.Slerp(stepTargets[index].rotation, slopeRotation * stepTargets[index].rotation, 10 * Time.deltaTime);

            targetLocation += new Vector3(0, stepTargets[index].localScale.y / 2, 0);

            stepTargets[index].position = targetLocation;
        }
   
    } 

    IEnumerator MoveToPoint(Vector3 endPoint, Quaternion endRot, float moveTime, int index)
    {
        Moving = true;
        
        
        Vector3 startPoint = footIKTargets[index].position;//target.position;
        Quaternion startRot = footIKTargets[index].rotation;

        endPoint += footIKTargets[index].up * 0.2f;
        Debug.Log("END POINT" + endPoint);

        Vector3 centerPoint = (startPoint + endPoint) / 2;

        centerPoint += footIKTargets[index].up * Vector3.Distance(startPoint, endPoint) / 2f;


        float timeElapsed = 0;

        // Here we use a do-while loop so normalized time goes past 1.0 on the last iteration,
        // placing us at the end position before exiting.
        do
        {
            timeElapsed += Time.deltaTime;


            float normalizedTime = timeElapsed / moveTime;


            normalizedTime = Easing.EaseInOutCubic(normalizedTime);

            footIKTargets[index].position =
                Vector3.Lerp(
                    Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                    Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                    normalizedTime
                );

            footIKTargets[0].rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);
            

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveTime);
        //BodyControl();
        Moving = false;
       
    }
   
    /*IEnumerator MoveToPointCoroutine(Vector3 endPoint, Quaternion endRot, float moveTime)
    {
        Moving = true;
        Vector3 startPoint = target.position;
        Quaternion startRot = target.rotation;

        endPoint += target.up * 0.2f;


        Vector3 centerPoint = (startPoint + endPoint) / 2;

        centerPoint += target.up * Vector3.Distance(startPoint, endPoint) / 2f;


        float timeElapsed = 0;

        // Here we use a do-while loop so normalized time goes past 1.0 on the last iteration,
        // placing us at the end position before exiting.
        do
        {
            timeElapsed += Time.deltaTime;


            float normalizedTime = timeElapsed / moveTime;


            normalizedTime = Easing.EaseInOutCubic(normalizedTime);

            target.position =
                Vector3.Lerp(
                    Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                    Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                    normalizedTime
                );

            target.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);
            //target.position = tip.position;

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveTime);
        //BodyControl();
        Moving = false;
    }*/
    
    /*void Update()
    {
        //works
        RaycastHit hit;
        
        if (Physics.Raycast(stepTarget.position, Vector3.down, out hit, distance,mask))
        {
            
            Vector3 targetLocation = hit.point;
            var slopeRotation = Quaternion.FromToRotation(stepTarget.up, hit.normal);
            stepTarget.rotation = Quaternion.Slerp(stepTarget.rotation, slopeRotation * stepTarget.rotation, 10 * Time.deltaTime);
            targetLocation += new Vector3(0, stepTarget.localScale.y / 4, 0);
            
            stepTarget.position = targetLocation;
        }
    }*/
}