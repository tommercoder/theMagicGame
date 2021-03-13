using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    #region 
    public static characterStats instance;
    private void Awake()
    {
        instance = this;
    }
       #endregion
    public int damageFromFireball;
    public float timeOfSwordAbility;
    public int lvl;

    public int XP = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(XP%100==0)
        {

            lvl = XP / 100;
            damageFromFireball = lvl * 3;
            timeOfSwordAbility = lvl * 3;
        }
    }
}
