using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSword : MonoBehaviour
{
    public  Transform sword, swordEquiped, swordUnequiped;
    public bool isEquipedSword = false;
    private void Update()
    {
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
    public void swordEquipMethod()
    {
        isEquipedSword = true;
    }
    public void swordUnequipMethod()
    {
        isEquipedSword = false;
    }

}
