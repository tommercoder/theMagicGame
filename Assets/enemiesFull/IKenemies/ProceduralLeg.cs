using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class ProceduralLeg : MonoBehaviour
{
    //with this script also using dontmovewithparent.cs
    [Header("OBJECTS")]
    
    public Transform targetLeft = null;

    public GameObject player;
    
    public LayerMask mask;
    [Header("STEP SETTINGS")]
    public bool Moving;
    public Transform stepTargetLeft;
   
    public List<Transform> stepTargets = new List<Transform>(1);
    public float wantStepAtDistance = 0.45f;
    public float timeToMakeStep = 0.125f;
    
    public bool waited = false;
    [SerializeField, Range(0, 1)] float stepOvershootFraction = 0.8f;
    
    
    public DontMoveWithParent dontMoveWithParentLeft;
    public Transform target;
    private List<Transform> footIKTargets = new List<Transform>(1);
    
    //enable when collide
    private void OnCollisionEnter(Collision collision)
    {
        //when collide they are visible
        stepTargetLeft.gameObject.GetComponent<Renderer>().enabled = true;
        
    }
    //hide in game steptargets
    private void Awake()
    {
        //for not to see target spheres on game menu
        stepTargetLeft.gameObject.GetComponent<Renderer>().enabled = false;
        
    }
    private void Start()
    {
        if (GetComponent<navmeshPatrol>() != null)
        {
            GetComponent<navmeshPatrol>().enabled = false;
            GetComponent<navmeshPatrol>().enabled = true;
        }
        player = GameObject.Find("character");
        //GetComponent<navmeshPatrol>().enabled = false;
        //GetComponent<navmeshPatrol>().enabled = true;
        //adding targets and targets to lists;
        footIKTargets.Add(targetLeft);
        

        stepTargets.Add(stepTargetLeft);
        

      
        
        
    }

    //time to make step
    IEnumerator wait()
    {
        //czekam krok
        waited = false;
        yield return new WaitForSeconds(timeToMakeStep);
        waited = true;
    }
    //test moving
    void moveForward()
    {
       if(GetComponent<navmeshPatrol>().type != proceduralType.npcProcedural)
        if (Vector3.Distance(player.transform.position, transform.position) > 4f && Vector3.Distance(player.transform.position, transform.position) < 25f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 2f);
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime);
        }

    }

    void Update()
    {
        if (pauseMenu.instance.menuIsOpened)
            return;
        //function to check ground for targets
        stepTargetIk(0);
        
        //distance between target and step target
       
        float distance = Vector3.Distance(footIKTargets[0].position, stepTargets[0].position);
        //make step with right foot
       
        //make step with left foot
        if (distance > wantStepAtDistance )
        {
            
            if (GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal, 0))
            {
                if (endPos.y >= transform.position.y)
                {
                    transform.Translate(Vector3.up* 3* Time.deltaTime);
                }
                else if (endPos.y <= transform.position.y)
                {
                    transform.Translate(Vector3.down * 3 * Time.deltaTime);
                }
                Quaternion endRot = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(stepTargets[0].forward, endNormal),
                    endNormal
                );

                StartCoroutine(
                    MoveToPoint(endPos, endRot, timeToMakeStep, 0));
                StartCoroutine(wait());
            }
        }
       
        moveForward();

    }

    
    //where i can stand after step
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
    //check when moving target for ground
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
            //move upper steptarget to groud
            var slopeRotation = Quaternion.FromToRotation(stepTargets[index].up, hit.normal);
            //rotate steptarget to ground
            stepTargets[index].rotation = Quaternion.Slerp(stepTargets[index].rotation, slopeRotation * stepTargets[index].rotation, 10 * Time.deltaTime);
            //change position to ground
            targetLocation += new Vector3(0, stepTargets[index].localScale.y / 2, 0);

            stepTargets[index].position = targetLocation;
        }
   
    } 
    //change position of target to position of step target slowly and move leg up
    IEnumerator MoveToPoint(Vector3 endPoint, Quaternion endRot, float moveTime, int index)
    {
        Moving = true;
        if(index == 0)
            dontMoveWithParentLeft.dontMoveWithParent = false;
       

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
            

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveTime);
        //BodyControl();
        Moving = false;
        if (index == 0)
        {
            dontMoveWithParentLeft.savedPosition = endPoint;//stepTargets[0].position;
            dontMoveWithParentLeft.dontMoveWithParent = true;

        }
        
    }
   
    
    
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