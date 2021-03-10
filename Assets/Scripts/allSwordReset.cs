using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allSwordReset : MonoBehaviour
{
    public swordEquipping[] swords;
    private void OnApplicationQuit()
    {
        for(int i = 0;i < swords.Length;i++)
        if (potionParticle.instance.usingDamagePotionNow && swords[i].swordDamage > swords[i].oldDamage)
        {
                swords[i].swordDamage = swords[i].oldDamage;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
