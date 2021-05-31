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
    //sword(transform miecza),swordEquiped(pusty GameObject z pozycją która zostaje przypisana wziętemu mieczu,swordUnequiped(to samo dla schowanego miecza)
    public Transform sword, swordEquiped, swordUnequiped;
    //zmienna która odpowiada za "class Item",czyli stworzony "ScriptableObjects" z właściwościami(icon,damage,właściwości...)
    public swordEquipping currentSword;
    //dwa objekty który trzymają GameObject modeli miecza i temp który jest wykorzystywany przy zmienianiu miecza na inny
    public GameObject currentSwordGameObject,temp;
    public bool isEquipedSword = false;
    public Transform spine;
    public Transform itemsOnScene;

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

        
        temp = currentSwordGameObject;
        currentSwordGameObject.SetActive(true);
        currentSwordGameObject.GetComponent<weaponInteract>().interacting = false;
        if (currentSwordGameObject.GetComponent<FloatingItem>() != null)
        {
            currentSwordGameObject.GetComponent<FloatingItem>().Rotating = false;
        }
        sword = currentSwordGameObject.transform;
          
       
        if (isEquipedSword)
        {
            //wstawia się pozycja i rotacja wziętego miecza
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
