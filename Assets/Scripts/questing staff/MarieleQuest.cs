using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarieleQuest : MonoBehaviour
{
    public static MarieleQuest instance;
    private void Awake()
    {
        instance = this;
    }
    public Quest currentMarieleQuest;

    public GameObject questWindow;
    public Text title;
    public Text description;
    public Text reward;
    public GameObject questPointer;
    List<EnemyPatrol> enemies = new List<EnemyPatrol>();
    List<ProceduralStats> pEnemies = new List<ProceduralStats>();
    List<NPCinteraction> allnpc = new List<NPCinteraction>();
    private void Start()
    {
        enemies = FindObjectsOfType<EnemyPatrol>().ToList();
        pEnemies = FindObjectsOfType<ProceduralStats>().ToList();
        allnpc = FindObjectsOfType<NPCinteraction>().ToList();
      
    }
    float shortestDistance;
    void Update()
    {
        if(currentMarieleQuest!=null && currentMarieleQuest.title!="")
        {
            if (!questPointer.activeSelf/* || questPointer.activeSelf*/)
            {
                enemies = FindObjectsOfType<EnemyPatrol>().ToList();
                pEnemies = FindObjectsOfType<ProceduralStats>().ToList();
                allnpc = FindObjectsOfType<NPCinteraction>().ToList();
                if (currentMarieleQuest.goal.goalType == goalType.killEnemyQuest)
                {
                    // Debug.Log(findNearestEnemy());
                    if (GameObject.FindObjectOfType<questPointer>().target != findNearestEnemy())
                    {
                        GameObject.FindObjectOfType<questPointer>().target = findNearestEnemy();
                        questPointer.SetActive(true);
                    }
                   
                }
                if (currentMarieleQuest.goal.goalType == goalType.killPEnemyQuest)
                {

                    //shortestDistance = Vector3.Distance(transform.position, pEnemies[0].transform.position);
                    //for (int i = 0; i < pEnemies.Count; i++)
                    //{
                    //    float distance = Vector3.Distance(transform.position, pEnemies[i].transform.position);
                    //    if (distance < shortestDistance)
                    //    {
                    //        shortestDistance = distance;

                    //        GameObject.FindObjectOfType<questPointer>().target = pEnemies[i].transform;
                    //    }
                    //}
                    if (GameObject.FindObjectOfType<questPointer>().target != findNearestPEnemy())
                    {
                        GameObject.FindObjectOfType<questPointer>().target = findNearestPEnemy();
                        questPointer.SetActive(true);
                    }
                    
                }
                if (currentMarieleQuest.goal.goalType == goalType.speakQuest)
                {
                    //shortestDistance = Vector3.Distance(transform.position, allnpc[0].transform.position);
                    for (int i = 0; i < allnpc.Count; i++)
                    {
                        if(allnpc[i].dialogue.npcEnumName.ToString()==currentMarieleQuest.npcEnumName.ToString())
                            GameObject.FindObjectOfType<questPointer>().target = allnpc[i].transform;
                        //float distance = Vector3.Distance(transform.position, allnpc[i].transform.position);
                        // if (distance < shortestDistance)
                        // {
                        //    shortestDistance = distance;


                        // }
                    }
                    questPointer.SetActive(true);
                }
               
            }
            
            
        }
        
        Transform findNearestEnemy()
        {

            //shortestDistance = Vector3.Distance(transform.position, enemies[0].transform.position);
            // Debug.Log("SHORTEST DISTANCE " + shortestDistance);
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            for (int i = 0; i < enemies.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (distance < minDist)
                {
                    tMin = enemies[i].transform;
                    minDist = distance;

                    
                   // Debug.Log("ENEMIES QUEST " + enemies[i].transform.name);
                }

            }

            return tMin;
        }

        Transform findNearestPEnemy()
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            for (int i = 0; i < pEnemies.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, pEnemies[i].transform.position);
                if (distance < minDist)
                {
                    tMin = pEnemies[i].transform;
                    minDist = distance;


                    // Debug.Log("ENEMIES QUEST " + enemies[i].transform.name);
                }

            }
            //write as above
            return tMin;
        }
        
        if(Input.GetKeyDown(KeyCode.P))
        {
           // questWindow.SetActive(!questWindow.activeSelf);
            if (questWindow.activeSelf)
            {
                questWindow.SetActive(false);
            }
            else
            {
                questWindow.SetActive(true);
                if (currentMarieleQuest != null && currentMarieleQuest.title != "")
                {
                    title.text = currentMarieleQuest.title;
                    description.text = currentMarieleQuest.description;
                    reward.text = currentMarieleQuest.rewardText;
                }
            }
        }
    }
    public void clearPWindow()
    {
        title.text = " ";
        description.text = " ";
        reward.text = " ";
    }
}
