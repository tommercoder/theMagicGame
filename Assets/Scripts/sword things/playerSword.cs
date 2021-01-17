using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSword : MonoBehaviour
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
    public Item item = null;
    private void Start()
    {
        //modernSword = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/ModernSword");
        //modernSword.SetActive(false);

        //pearlSword = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/PearlSword");
        //pearlSword.SetActive(false);
        
    }
    private void Update()
    {
       
             temp = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/" + currentSwordGameObject.name);
        
            currentSwordGameObject = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/" + currentSwordGameObject.name);

            currentSwordGameObject.SetActive(true);
            currentSwordGameObject.GetComponent<weaponInteract>().interacting = false;
            currentSwordGameObject.GetComponent<FloatingItem>().Rotating = false;

            sword = currentSwordGameObject.transform;
        
        //if (Input.GetKey("1"))
        //{
        //    swordBeg.SetActive(true);
        //    pearlSword.SetActive(false);
        //    modernSword.SetActive(false);
        //    sword = swordBeg.transform;
        //}
        //if (Input.GetKey("2"))
        //{
        //    swordBeg.SetActive(false);
        //    pearlSword.SetActive(false);
        //    modernSword.SetActive(true);
        //    sword = modernSword.transform;
        //}
        //if(Input.GetKey("3"))
        //{
        //    swordBeg.SetActive(false);
        //    modernSword.SetActive(false);
        //    pearlSword.SetActive(true);
        //    sword = pearlSword.transform;
        //}
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
