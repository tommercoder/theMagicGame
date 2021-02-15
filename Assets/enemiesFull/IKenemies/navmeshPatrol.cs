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
    Quaternion rotation;
    private void Start()
    {
        player = GameObject.Find("character");
        current = 0;
        rotation = transform.rotation;
    }
    
    void AttackPlayer()
    {
        attackingPlayer = true;
       

        Vector3 relativePos = player.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime/3f);

      
    }
    private void Update()
    {

       
        if (Vector3.Distance(transform.position,player.transform.position) < 20)
        {
            attackingPlayer = true;
        }
        else
        {
            //transform.LookAt(points[current].position);
             rotation = Quaternion.LookRotation(points[current].position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime / 3f);
            rotated = true;
            Debug.Log("rotated" + rotated);
            attackingPlayer = false;
        }
        float dot = Vector3.Dot(transform.right, (points[current].position - transform.position).normalized);
        
        
        if (!attackingPlayer)
        {
            if (Vector3.Distance(transform.position, points[current].position) > 0.7)
            {
                if (rotated)
                {
                    transform.position = Vector3.MoveTowards(transform.position, points[current].position, Time.deltaTime * speed);
                    rotated = false;
                }
                
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
       

    }
}
