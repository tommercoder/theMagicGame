using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshPatrol : MonoBehaviour
{
    public Transform[] points;
    int current;
    public float speed;
    public bool rotated = false;
    public bool attackingPlayer = false;
    public GameObject player;
    private void Start()
    {
        player = GameObject.Find("character");
        current = 0;
    }
    void lookAt()
    {
        //// Store the other object's position in a temporary variable
        

    }
    void AttackPlayer()
    {
        attackingPlayer = true;
        //Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);

        Vector3 relativePos = player.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, Time.deltaTime* 1);
        // Debug.Log("player attacked");
    }
    private void Update()
    {

       // Debug.Log("distance to player is " + Vector3.Distance(transform.position, player.transform.position));
        if(Vector3.Distance(transform.position,player.transform.position) < 20)
        {
            attackingPlayer = true;
        }
        else
        {
            var rotation = Quaternion.LookRotation(points[current].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
            attackingPlayer = false;
        }
        float dot = Vector3.Dot(transform.right, (points[current].position - transform.position).normalized);
        
        //if (dot > 0.7f)
        //{
        //if (Vector3.Distance(transform.position, points[current].position) <1)
        //{
        //    Vector3 targetDirection = points[current].position - transform.position;

        //    // The step size is equal to speed times frame time.
        //    float singleStep = speed * Time.deltaTime;

        //    // Rotate the forward vector towards the target direction by one step
        //    Vector3 newDirection = Vector3.RotateTowards(transform.right, targetDirection, singleStep, 0.0f);

        //    // Draw a ray pointing at our target in
        //    Debug.DrawRay(transform.position, newDirection, Color.yellow);

        //    // Calculate a rotation a step closer to the target and applies rotation to this object
        //    var rotation = Quaternion.LookRotation(points[current].position - transform.position);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);
        //    rotated = true;
        //}
        //if(rotated  || first_move)
        if (!attackingPlayer)
        {
            if (Vector3.Distance(transform.position, points[current].position) > 0.7)
            {

                transform.position = Vector3.MoveTowards(transform.position, points[current].position, Time.deltaTime * speed);
                ////transform.rotation = Vector3.RotateTowards(transform.position,points[current].position,Time.deltaTime*speed,0.0f);
            }
            else
            {
                current = (current + 1) % points.Length;
            }
        }
        else
        {
            AttackPlayer();
        }
        //}
        //else
        //{
        //    
        //}

    }
}
