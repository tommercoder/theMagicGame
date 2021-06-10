using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveData
{
    //character saving
    public int s_XP;
    public GameObject s_respawnObject;
    public float s_x;
    public float s_y;
    public float s_z;
    public bool inZone;
    //inventory
    public List<Item> s_inventory = new List<Item>();
    public List<GameObject> s_inventoryGO = new List<GameObject>();
    public List<GameObject> s_allGameObjectInventory = new List<GameObject>();
    public List<GameObject> s_allInteractableObjects = new List<GameObject>();
    public List<potionInteraction> s_allPotionInteractions = new List<potionInteraction>();
    public List<weaponInteract> s_allWeaponInteractions = new List<weaponInteract>();
    public List<GameObject> s_allWeaponInteractionsGO = new List<GameObject>();
    public List<Quaternion> s_allSwordsRotation = new List<Quaternion>();
    public List<Vector3> s_allSwordsPositions = new List<Vector3>();
    public List<Vector3> s_allPotionsPositions = new List<Vector3>();
    public List<bool> s_allPotionsIsUsed = new List<bool>();
    public List<bool> s_currentSwordBool = new List<bool>();
    public List<Quest> s_allQuests = new List<Quest>();

    public Quest s_currentQuest;
    public int s_HP;
    //slots
    [System.Serializable]
    public struct SlotsData
    {
        public Item s_slotItem;
        public string s_id;
       
    }
    public List<SlotsData> s_slots = new List<SlotsData>();
    
    //sword
    public Transform s_sword;
    public GameObject s_currentSwordGO;
    public GameObject s_temp;
    public swordEquipping s_currentSword;
    //enemy saving
    [System.Serializable]
    public struct EnemyData
    {
        public int e_Health;
        public string e_id;
    }
    public List<EnemyData> enemyData = new List<EnemyData>();
    //procedural enemy saving
    [System.Serializable]
    public struct ProceduralEnemyData
    {
        public int e_ProcHealth;
        public string e_ProcId;
    }
    public List<ProceduralEnemyData> proceduralEnemyData = new List<ProceduralEnemyData>();
    //konwersja danych w ".json"
    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }
    //Konwersja z ".json" do typów danych 
    public void LoadFromJson(string json)
    {

        JsonUtility.FromJsonOverwrite(json, this);
    }


}
//Interfejs implementujący metody dla zapisania i wpisania danych.
public interface ISaveable
{
    void PopulateSaveData(SaveData data);
    void LoadFromSaveData(SaveData data);
}
