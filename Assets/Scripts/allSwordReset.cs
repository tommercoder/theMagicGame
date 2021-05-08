using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allSwordReset : MonoBehaviour
{
    public static allSwordReset instance;
    private void Awake()
    {
        instance = this;
    }
    public swordEquipping[] swords;
    private void OnApplicationQuit()
    {
        for(int i = 0;i < swords.Length;i++)
        if (swords[i].swordDamage > swords[i].oldDamage)
        {
                swords[i].swordDamage = swords[i].oldDamage;
        }
    }
 
}
