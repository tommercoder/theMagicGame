using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSword : MonoBehaviour
{
    public  Transform sword, swordEquiped, swordUnequiped;
    
    public GameObject pearlSword, modernSword, swordBeg;
    public bool isEquipedSword = false;
    
    private void Start()
    {
        modernSword = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/ModernSword");
        modernSword.SetActive(false);
        
        pearlSword = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/PearlSword");
        pearlSword.SetActive(false);

        swordBeg = GameObject.Find("character/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/swordBeg");
        swordBeg.SetActive(true);
        
    }
    private void Update()
    {
        if (Input.GetKey("1"))
        {
            swordBeg.SetActive(true);
            pearlSword.SetActive(false);
            modernSword.SetActive(false);
            sword = swordBeg.transform;
        }
        if (Input.GetKey("2"))
        {
            swordBeg.SetActive(false);
            pearlSword.SetActive(false);
            modernSword.SetActive(true);
            sword = modernSword.transform;
        }
        if(Input.GetKey("3"))
        {
            swordBeg.SetActive(false);
            modernSword.SetActive(false);
            pearlSword.SetActive(true);
            sword = pearlSword.transform;
        }
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
    //setting sword to hand
    public void swordEquipMethod()
    {
        isEquipedSword = true;
    }
    public void swordUnequipMethod()
    {
        isEquipedSword = false;
    }
    
}
