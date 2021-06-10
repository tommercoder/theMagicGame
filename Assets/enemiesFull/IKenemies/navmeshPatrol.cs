using System.Collections;
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
    public void Start()
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
    //Obraca broń w stronę bohatera.
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
        //Robot z dwiema nogami tylko obraca do celi w miejscu, a z jedną nogą także idzie za bohaterem(ta część jest funkcja "MoveForward" w "ProceduralLeg.cs").
        attackingPlayer = true;

        //Cały czas obraca broń.
        RotateGun();

        //Wyliczba wektor między bohaterem a robotem.
        Vector3 relativePos = player.transform.position - transform.position;
        //Ogranicza obracanie po osi y.
        relativePos.y = 0;
        //Wylicza rotacje.
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime/3f);
        //Jeśli czas jest > niż nextFire znaczy że czas przeszedł do momentu "Czas + fireRate".
        if (Time.time > nextFire)
        {
            //Tworzy instancje kuli.
            GameObject bullet = Instantiate(prefab, projectilePoint.position, Quaternion.identity) as GameObject;
            //Wylicza kierunek kuli.
            projectileDirection = (player.transform.position + Vector3.up*2 - projectilePoint.transform.position).normalized * projectileSpeed;
            //Dodaje do komponentu rigidbody szybkość w wyliczonym kierunku.
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(projectileDirection.x, projectileDirection.y, projectileDirection.z);
            FindObjectOfType<audioManager>().Play("proceduralShooting");
            //Znowu dodaje i wylicza następny czas strzelania.
            nextFire = Time.time + fireRate;
        }

            
    }


    public void Update()
    {
        if (pauseMenu.instance.menuIsOpened || playerHealth.instance.currentHealth <= 0)
            return;
       
        
            //Jeśli dystans jest < 20 i ten robot nie jest npc,a jest wrogiem.
            if (Vector3.Distance(transform.position, player.transform.position) < 20 &&  type != proceduralType.npcProcedural)
            { 
   
                    attackingPlayer = true;
            }
            else
            {
                //Patrolowanie między punktami,wylicza pozycje i rotacje i stosuje ich.
                Vector3 relativePos = (points[current].transform.position - transform.position).normalized;
               
                rotation = Quaternion.LookRotation((points[current].position - transform.position).normalized);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime / 4f);

                rotated = true;

                attackingPlayer = false;
            }

        
        if (!attackingPlayer)
        {
            //Jeśli jest na już obrócony w stronę następnego punktu to zaczyna poruszać w stronę tego punktu funkcją "MoveTowards()".
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
                //Wstawia punkt na następny.
                current = (current + 1) % points.Length;
               
            }
        }
        else
        {
            AttackPlayer();
        }
       

    }
}
