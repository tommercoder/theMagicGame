using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class ikController : MonoBehaviour
{
    /*[Header("OBJECTS")]
    public Transform target = null;
    
    public Transform baksBot = null;

    public DontMoveWithParent dontMoveWithParentClass;
    public LayerMask mask;
    [Header("STEP SETTINGS")]
    public bool Moving;
    public Transform stepTarget;
    public List<Transform> stepTargets = new List<Transform>(2);
    public float wantStepAtDistance = 0.45f;
    public float timeToMakeStep = 0.125f;
    public bool grounded;

    [SerializeField, Range(0, 1)] float stepOvershootFraction = 0.8f;
    public float standardBodyOffset;
    public Transform offsetObject;
    
    private Vector3 bodyPosOffset;
    private Vector3 defaultBodyPosOffset;
    private List<Transform> footIKTargets = new List<Transform>(2);
    
    public TwoBoneIKConstraint twoBoneIKConstraint;
    private void Start()
    {
        footIKTargets.Add(target);
        stepTargets.Add(stepTarget);

        
    }



    void Update()
    {
        //check for ik target(not step through objects)
        stepTargetIk();

        float distance = Vector3.Distance(footIKTargets[0].position, stepTargets[0].position);
        if (distance > wantStepAtDistance)
        {
            if (GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal,0))
            {
                // Get rotation facing in the home forward direction but aligned with the normal plane
                Quaternion endRot = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(stepTargets[0].forward, endNormal),
                    endNormal
                );


                StartCoroutine(
                    MoveToPointCoroutine(
                        endPos,
                        //stepTarget.rotation,
                        endRot,
                        timeToMakeStep
                    )
                );
                StartCoroutine(
                    MoveToPoint(endPos, endRot, timeToMakeStep,0));

                //StartCoroutine(
                //    MoveToPointSecondLeg(endPos, endRot, timeToMakeStep));

            }
        }
        else
            dontMoveWithParentClass.dontMoveWithParent = true;
    }
    void offsetCheck()
    {
        Vector3 feetPos = Vector3.zero;
        feetPos += target.position;
        Vector3 averagePos = feetPos / 2;

    }
    
    void GetBodyOffset() // called at the beginning to get both the positional and the rotational offset between the body and the feet.
    {
        Vector3 cummulativeStableFeetPos = Vector3.zero;


        for (int i = 0; i < footIKTargets.Count; i++)
        {
            cummulativeStableFeetPos += footIKTargets[i].localPosition;
        }


        Vector3 averageStableFeetPos = cummulativeStableFeetPos / (footIKTargets.Count);

        defaultBodyPosOffset = baksBot.position - averageStableFeetPos;

        bodyPosOffset = defaultBodyPosOffset;

        
        
    }


    private void BodyControl() // Simply updates the position and rotation of the body based on the new feet positions
    {
        //Store all foot target positions before moving their parent, which is this transform
        Vector3[] tempTargetPositions = new Vector3[footIKTargets.Count];

        //Calculate the average stable feet position
        Vector3 cummulativeStableFeetPos = Vector3.zero;

        // Calculating the averages
        for (int i = 0; i < footIKTargets.Count; i++)
        {
            tempTargetPositions[i] = footIKTargets[i].localPosition;

            cummulativeStableFeetPos += footIKTargets[i].localPosition;
        }

        Vector3 averageStableFeetPos = cummulativeStableFeetPos / (footIKTargets.Count);
        Debug.Log("c" + cummulativeStableFeetPos);
        //The position of the body is the average stable feet positions + the body offset
        baksBot.position = bodyPosOffset + averageStableFeetPos;
        Debug.Log("pos posle" + averageStableFeetPos);
        //Restore foot target positions to cancel out parent's movement
        for (int i = 0; i < footIKTargets.Count; i++)
        {
            footIKTargets[i].localPosition = tempTargetPositions[i];
        }
        Debug.Log("body controll end");
        //RotationControl(); // updates the rotation of the body
    }

    bool GetGroundedEndPosition(out Vector3 position, out Vector3 normal,int index)
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
    void stepTargetIk()
    {

        float distance = 100f;
        Ray ray = new Ray(stepTarget.position, -stepTarget.up);
        //works
        RaycastHit hit;

        // Debug.DrawRay(stepTarget.position, Vector3.down, Color.red);
        if (Physics.Raycast(stepTarget.position, Vector3.down, out hit, distance, mask))
        {

            Vector3 targetLocation = hit.point;
            var slopeRotation = Quaternion.FromToRotation(stepTarget.up, hit.normal);

            stepTarget.rotation = Quaternion.Slerp(stepTarget.rotation, slopeRotation * stepTarget.rotation, 10 * Time.deltaTime);

            targetLocation += new Vector3(0, stepTarget.localScale.y / 2, 0);

            stepTarget.position = targetLocation;
        }

    }

    IEnumerator MoveToPoint(Vector3 endPoint,Quaternion endRot,float moveTime,int index)
    {
        Moving = true;
        Vector3 startPoint = footIKTargets[index].position;//target.position;
        Quaternion startRot = footIKTargets[index].rotation;

        endPoint += footIKTargets[index].up * 0.2f;


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
            //target.position = tip.position;

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveTime);
        //BodyControl();
        Moving = false;
    }
    IEnumerator MoveToPointSecondLeg(Vector3 endPoint, Quaternion endRot, float moveTime)
    {
        Moving = true;
        Vector3 startPoint = footIKTargets[1].position;//target.position;
        Quaternion startRot = footIKTargets[1].rotation;

        endPoint += footIKTargets[1].up * 0.2f;


        Vector3 centerPoint = (startPoint + endPoint) / 2;

        centerPoint += footIKTargets[1].up * Vector3.Distance(startPoint, endPoint) / 2f;


        float timeElapsed = 0;

        // Here we use a do-while loop so normalized time goes past 1.0 on the last iteration,
        // placing us at the end position before exiting.
        do
        {
            timeElapsed += Time.deltaTime;


            float normalizedTime = timeElapsed / moveTime;


            normalizedTime = Easing.EaseInOutCubic(normalizedTime);

            footIKTargets[1].position =
                Vector3.Lerp(
                    Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                    Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                    normalizedTime
                );

            footIKTargets[1].rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);
            //target.position = tip.position;

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveTime);
        //BodyControl();
        Moving = false;
    }
    IEnumerator MoveToPointCoroutine(Vector3 endPoint, Quaternion endRot, float moveTime)
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
    }
    public Transform tip
    {
        get
        {
            if (twoBoneIKConstraint) return twoBoneIKConstraint.data.tip;
            else
            {
                Debug.LogWarning("Tip transform is not yet ready.");
                return null;
            }
        }
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