﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum proceduralType { twoLegsBig, OneLegSmall, twoLegsSmall,npcProcedural};
public class navmeshPatrol : MonoBehaviour
{
    public proceduralType type;
    public Transform[] points;
    int current;
    public float speed;
    public bool rotated = false;
    public bool attackingPlayer = false;
    public GameObject player;
    Quaternion rotation;
    public Transform projectilePoint;
    public GameObject prefab;
    float nextFire;
    public float fireRate;
    Vector3 projectileDirection;
    public float projectileSpeed;
    public Transform gun;
    private void Start()
    {
       
        this.enabled = true;
        projectileSpeed = 60f;
        fireRate = 2f;
        nextFire = Time.time;   
        player = GameObject.Find("character");
        if (type == proceduralType.twoLegsBig)
        {
            prefab = Resources.Load("projectile") as GameObject;
        }
        else if (type == proceduralType.twoLegsSmall)
        {
            prefab = Resources.Load("projectileTwoLegsSmall") as GameObject;
        }
        else if (type == proceduralType.OneLegSmall)
        {
            prefab = Resources.Load("projectileOneLegSmall") as GameObject;
        }
        else
        {
            prefab = null;
        }
         
        current = 0;
        rotation = transform.rotation;
    }
   IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
    }
    void RotateGun()
    {
        if (gun != null)
        {
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            gun.rotation = Quaternion.Lerp(gun.rotation, toRotation, Time.deltaTime);
        }
    }
    void AttackPlayer()
    {
        //chasing player part is in procedural controller script
        attackingPlayer = true;
        RotateGun();

        Vector3 relativePos = player.transform.position - transform.position;
        relativePos.y = 0;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime/3f);
        if (Time.time > nextFire)
        {
            GameObject bullet = Instantiate(prefab, projectilePoint.position, Quaternion.identity) as GameObject;
            projectileDirection = (player.transform.position + Vector3.up*2 - projectilePoint.transform.position).normalized * projectileSpeed;
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(projectileDirection.x, projectileDirection.y, projectileDirection.z);
            FindObjectOfType<audioManager>().Play("proceduralShooting");
            
            nextFire = Time.time + fireRate;
        }

            
    }


    private void Update()
    {
        if (pauseMenu.instance.menuIsOpened || playerHealth.instance.currentHealth <= 0)
            return;
       
        
            
            if (Vector3.Distance(transform.position, player.transform.position) < 20 &&  type != proceduralType.npcProcedural)
            { 
            //this type is for npc robots 
                
                    attackingPlayer = true;
            

            }
            else
            {
                Vector3 relativePos = (points[current].transform.position - transform.position).normalized;
               
                rotation = Quaternion.LookRotation((points[current].position - transform.position).normalized);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime / 4f);

                rotated = true;

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
