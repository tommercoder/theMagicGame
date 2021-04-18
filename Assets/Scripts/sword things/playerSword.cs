using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSword : MonoBehaviour,ISaveable
{
    #region Singleton
    public static playerSword instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("instance playerSword.cs");
            return;
        }

        instance = this;
    }

    #endregion

    public Transform sword, swordEquiped, swordUnequiped;
    public swordEquipping currentSword;
    
    public GameObject currentSwordGameObject,temp;
    public bool isEquipedSword = false;
    public Transform spine;
    public Transform itemsOnScene;
    //public Item item = null;

    public void PopulateSaveData(SaveData sd)
    {
        sd.s_sword = sword;
        sd.s_currentSword = currentSword;
        sd.s_currentSwordGO = currentSwordGameObject;
        sd.s_temp = temp;
        



    }
    public void LoadFromSaveData(SaveData sd)
    {

        
        temp.transform.SetParent(itemsOnScene.transform);
        temp.SetActive(false);
        //Debug.Log(sd.s_sword);
        //Debug.Log(sd.s_currentSword);
        //Debug.Log(sd.s_currentSwordGO);
        //Debug.Log(sd.s_temp);
        sword = sd.s_sword;
        temp = sd.s_temp;
        currentSwordGameObject = sd.s_currentSwordGO;
        
        currentSword = sd.s_currentSword;
        sword.SetParent(spine.transform);



    }
 
    private void Start()
    {

        currentSword = currentSwordGameObject.GetComponent<weaponInteract>().item;
        currentSwordGameObject.GetComponent<weaponInteract>().isCurrentSword = true;
        
    }
    private void Update()
    {

        //temp = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/" + currentSwordGameObject.name);
            temp = currentSwordGameObject;
            currentSwordGameObject.SetActive(true);
            currentSwordGameObject.GetComponent<weaponInteract>().interacting = false;
            currentSwordGameObject.GetComponent<FloatingItem>().Rotating = false;

            sword = currentSwordGameObject.transform;
          
       
        if (isEquipedSword)
        {
            sword.position = swordEquiped.position;
            sword.rotation = swordEquiped.rotation;
        }
        else 
        {
            sword.position = swordUnequiped.position;
            sword.rotation = swordUnequiped.rotation;
        }
    }
    //setting sword to hand animation event
    public void swordEquipMethod()
    {
        isEquipedSword = true;
    }
    public void swordUnequipMethod()
    {
        isEquipedSword = false;
    }
    
}
