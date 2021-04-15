using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class characterStats : MonoBehaviour, ISaveable
{
    #region 
    public static characterStats instance;
    private void Awake()
    {
        //Debug.Log("Awake");
        instance = this;



        
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(Camera.main);
        //GameObject[] arr = GameObject.FindGameObjectsWithTag("interactable object");
        //foreach (GameObject g in arr)
        //{
        //    DontDestroyOnLoad(g);
        //}
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(this);

    }
    #endregion

    [Header("THIS SCRIPT SAVE&LOAD ALL THE DATA")]
    public int damageFromFireball;
    public float timeOfSwordAbility;
    public int lvl;

    public int XP = 100;
    public bool loadCompleted;
    public bool saveCompleted;



    [Header("LIST OF ALL ENEMIES")]
    public List<EnemyStats> all_enemies = new List<EnemyStats>();
    public List<string> dead_enemies_ids = new List<string>();
    public List<ProceduralStats> all_procedural_enemies = new List<ProceduralStats>();
    public List<string> all_procedural_ids = new List<string>();

    public Slot[] slotsToSave;
    //public List<Sprite> s_icons = new List<Sprite>();
    public List<GameObject> allAddedToInventoryGO = new List<GameObject>();
    public List<GameObject> allInteractableGameObjects = new List<GameObject>();

    public List<potionInteraction> allPotionInteractionO = new List<potionInteraction>();
    public List<weaponInteract> all_swords = new List<weaponInteract>();
    public List<GameObject> all_swordsGO = new List<GameObject>();
    //public List<string> allPotionsOids = new List<string>();
    public List<bool> allPotionsIsUsed = new List<bool>();
    public List<bool> allSwordsBool = new List<bool>();
    //list of items positions and rotation
    public List<Quaternion> swordRotation = new List<Quaternion>();
    public List<Vector3> allItemsPositions = new List<Vector3>();

    //quests
    public List<NPCinteraction> allNPCS = new List<NPCinteraction>();
    public List<Quest> allNpcQuests = new List<Quest>();
    
    void Start()
    {
        //Debug.Log("Start");

       // Debug.Log(Application.persistentDataPath);
        XP = 50;
        //setting all
        all_enemies = FindObjectsOfType<EnemyStats>().ToList();
        all_procedural_enemies = FindObjectsOfType<ProceduralStats>().ToList();
        slotsToSave = inventoryManager.instance.itemsParent.GetComponentsInChildren<Slot>();
        //allItemsPositions = GameObject.FindGameObjectsWithTag("interactable object").ToList();
        allInteractableGameObjects = GameObject.FindGameObjectsWithTag("interactable object").ToList();
       

        allPotionInteractionO = GameObject.FindObjectsOfType<potionInteraction>().ToList();
        all_swords = GameObject.FindObjectsOfType<weaponInteract>().ToList();
        //quest
        allNPCS = FindObjectsOfType<NPCinteraction>().ToList();
        for (int i = 0; i < allNPCS.Count; i++)
        {
            allNpcQuests.Add(allNPCS[i].quest);
        }
        //for (int i = 0; i < allNPCS.Count; i++)
        //{
        //    allNpcQuests[i] = allNPCS[i].quest;
        //}
        foreach (weaponInteract w in all_swords)
        {
            all_swordsGO.Add(w.gameObject);
        }
        for (int i = 0; i < allPotionInteractionO.Count; i++)
        {
            allPotionsIsUsed.Add(false);
        }
        //setting ids
        for (int i = 0; i < all_enemies.Count; i++)
        {
            all_enemies[i].id = i.ToString();
        }
        for (int i = 0; i < slotsToSave.Length; i++)
        {
            slotsToSave[i].id = i.ToString();
        }
        for (int i = 0; i < allPotionInteractionO.Count; i++)
        {
            allPotionInteractionO[i].id = i.ToString();
        }
        for (int i = 0; i < all_procedural_enemies.Count; i++)
        {
            all_procedural_enemies[i].id = i.ToString();
        }
        //loading
        



        lvl = XP / 100;
        damageFromFireball = lvl * 3;
        timeOfSwordAbility = lvl * 3;
       // StartCoroutine(waitLoad());
        LoadJsonData(this);

    }
    IEnumerator waitLoad()
    {
        yield return new WaitForEndOfFrame();
       // Debug.Log("LOAD STARTED");
        LoadJsonData(this);
    }
    private void OnLevelWasLoaded(int level)
    {
      //  Debug.Log("OnLevelWasLoaded");

    }

    void Update()
    {
        if (XP % 100 == 0)
        {

            lvl = XP / 100;
            damageFromFireball = lvl * 3;
            timeOfSwordAbility = lvl * 3;
        }

        //if (pauseMenu.instance.pauseOpened)
        //{
        //    SaveJsonData(this);
        //}

    }
    private void OnApplicationQuit()
    {
        SaveJsonData(this);
       
    }
    
    //interface method
    public void PopulateSaveData(SaveData sd)
    {
        //character
        sd.s_XP = XP;

        respawnScript.instance.PopulateSaveData(sd);
        sd.s_x = gameObject.transform.position.x;
        sd.s_y = gameObject.transform.position.y;
        sd.s_z = gameObject.transform.position.z;

        playerHealth.instance.PopulateSaveData(sd);
        //sword
        playerSword.instance.PopulateSaveData(sd);
        //inventory
        Inventory.instance.PopulateSaveData(sd);

        foreach (Slot slot in slotsToSave)
        {
            slot.PopulateSaveData(sd);
           // Debug.Log("SLOT " + sd.s_slots);

        }
        //enemies
        foreach (EnemyStats enemy in all_enemies)
        {
            enemy.PopulateSaveData(sd);
        }
        //procedural enemies
        foreach (ProceduralStats enemy in all_procedural_enemies)
        {
            enemy.PopulateSaveData(sd);
        }

        //sd.s_allItemsPositions = allItemsPositions;
        //sd.s_allPotionInteractions = GameObject.FindObjectsOfType<potionInteraction>().ToList();
        if (allPotionInteractionO.Count > 0)
        {
           // Debug.Log("ALL POTIONS COUNT " + allPotionInteractionO[0]);
            sd.s_allPotionInteractions = allPotionInteractionO;
        }
        
        sd.s_allWeaponInteractions = all_swords;
        sd.s_allWeaponInteractionsGO = all_swordsGO;

        for (int i = 0; i < all_swordsGO.Count; i++)
        {
            sd.s_allSwordsRotation.Add(all_swordsGO[i].transform.rotation);
        }
        for (int i = 0; i < all_swordsGO.Count; i++)
        {
            //because some objects can be destroyed thats why im initializing list again
            //allInteractableGameObjects = GameObject.FindGameObjectsWithTag("interactable object").ToList();
            // sd.s_allItemsPositions.Add(allInteractableGameObjects[i].transform.position);
            sd.s_allSwordsPositions.Add(all_swordsGO[i].transform.position);

        }
        for (int i = 0; i < allPotionInteractionO.Count; i++)
        {
            sd.s_allPotionsPositions.Add(allPotionInteractionO[i].transform.position);
        }
        ///going through list and check if there is used item,then saving its bool to another list at same position
        for (int i = 0; i < allPotionInteractionO.Count; i++)
        {
            if (allPotionInteractionO[i].isUsed)
            {
                allPotionsIsUsed[i] = true;
            }
        }
        sd.s_allPotionsIsUsed = allPotionsIsUsed;
        //quest part
        for (int i = 0; i < allNPCS.Count; i++)
        {
            if (allNPCS[i].quest == null)
            {
                allNpcQuests[i] = null;
            }
        }
        sd.s_allQuests = allNpcQuests;
        if(MarieleQuest.instance.currentMarieleQuest!=null)
        {
            sd.s_currentQuest = MarieleQuest.instance.currentMarieleQuest;
        }
       
    }
    //interface method
    public void LoadFromSaveData(SaveData sd)
    {
        if (SceneManager.GetActiveScene().name == "game")
        {
            //character
            XP = sd.s_XP;
            if (sd.s_HP > 0)
            {
                playerHealth.instance.LoadFromSaveData(sd);
            }
            else if (sd.s_HP == 0)
            {
                playerHealth.instance.currentHealth = 100;
            }
            if (sd.s_respawnObject != null)
            {
                respawnScript.instance.LoadFromSaveData(sd);
            }
            if (sd.s_x != 0 && sd.s_y != 0 && sd.s_z != 0)
            {
                gameObject.transform.position = new Vector3(sd.s_x, sd.s_y, sd.s_z);
            }
            //sword
            if (sd.s_sword != null && sd.s_temp != null && sd.s_currentSword != null && sd.s_currentSwordGO != null)
            {
                playerSword.instance.LoadFromSaveData(sd);

            }
            else
            {
                playerSword.instance.currentSwordGameObject = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/" + playerSword.instance.currentSwordGameObject.name);
                playerSword.instance.temp = playerSword.instance.currentSwordGameObject;
            }


            //potions
            if (sd.s_allPotionsIsUsed.Count > 0)
            {
                allPotionsIsUsed = sd.s_allPotionsIsUsed;
            }
            //quest
            if (sd.s_allQuests.Count > 0)
            {
               // Debug.Log("SD ALL QUESTS" + sd.s_allQuests);
                allNpcQuests = sd.s_allQuests;
            }
            if (sd.s_allQuests.Count > 0)
            {
                for (int i = 0; i < allNPCS.Count; i++)
                {
                    allNPCS[i].quest = allNpcQuests[i];
                }
            }
            if (sd.s_currentQuest != null)
            {
                MarieleQuest.instance.currentMarieleQuest = sd.s_currentQuest;
            }
          

            if (sd.s_allPotionInteractions.Count > 0)//second pattern in if is changed(FOR REMEMBER)
            {
               // Debug.Log("SD ALL POTIONS > 0");
                if (sd.s_allPotionInteractions[0] != null)
                {
                    allPotionInteractionO = sd.s_allPotionInteractions;
                }
                else
                {
                    allPotionInteractionO = GameObject.FindObjectsOfType<potionInteraction>().ToList();
                }
                for (int b = 0; b < allPotionInteractionO.Count; b++)
                {
                    allPotionInteractionO[b].isUsed = allPotionsIsUsed[b];
                }
                for (int i = 0; i < allPotionInteractionO.Count; i++)
                {

                    if (allPotionInteractionO[i].isUsed)
                    {
                        allPotionInteractionO[i].gameObject.SetActive(false);
                    }
                }
            }


            Inventory.instance.LoadFromSaveData(sd);
            foreach (Slot slot in slotsToSave)
            {
                slot.LoadFromSaveData(sd);

            }




            if (sd.s_allWeaponInteractions.Count > 0)
            {

                //all_swords = sd.s_allWeaponInteractions;
               // all_swordsGO = sd.s_allWeaponInteractionsGO;
                for (int i = 0; i < all_swordsGO.Count; i++)
                {
                    if (!Inventory.instance.itemsGameObjects.Contains(all_swordsGO[i])
                    && all_swordsGO[i] != playerSword.instance.currentSwordGameObject)
                    {
                        all_swordsGO[i].SetActive(true);

                        all_swordsGO[i].transform.rotation = sd.s_allSwordsRotation[i];


                    }
                }
                for (int j = 0; j < sd.s_allSwordsPositions.Count; j++)
                {
                    //Debug.Log(sd.s_allItemsPositions[j] + " " + allInteractableGameObjects[j].name);
                    all_swordsGO[j].transform.position = sd.s_allSwordsPositions[j];
                    
                }
                for(int a = 0;a< sd.s_allPotionsPositions.Count;a++)
                {
                    allPotionInteractionO[a].transform.position = sd.s_allPotionsPositions[a];
                }
            }

            
            //enemies
            foreach (EnemyStats enemy in all_enemies)
            {
                enemy.LoadFromSaveData(sd);
            }
            foreach (string id in dead_enemies_ids)
            {
                SaveData.EnemyData enemyData = new SaveData.EnemyData();
                enemyData.e_Health = 0;
                enemyData.e_id = id;
                sd.enemyData.Add(enemyData);
            }
            //procedural enemies
            foreach (ProceduralStats enemy in all_procedural_enemies)
            {
                enemy.LoadFromSaveData(sd);
            }
            foreach (string id in all_procedural_ids)
            {
                SaveData.ProceduralEnemyData enemyData = new SaveData.ProceduralEnemyData();
                enemyData.e_ProcHealth = 0;
                enemyData.e_ProcId = id;
                sd.proceduralEnemyData.Add(enemyData);
            }
        }
    }
    public  void SaveJsonData(characterStats cs)
    {
        SaveData sd = new SaveData();
        cs.PopulateSaveData(sd);

        if (FileManager.WriteToFile("SaveData.dat", sd.toJson()))
        {
            Debug.Log("succesful save");
            
        }
       
    }
    public void LoadJsonData(characterStats cs)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();

            sd.LoadFromJson(json);

            cs.LoadFromSaveData(sd);
            Debug.Log("Load complete");
            loadCompleted = true;
        }
    }
}
