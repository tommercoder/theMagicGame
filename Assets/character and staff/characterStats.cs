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
    // Start is called before the first frame update
    void Start()
    {
        lvl = 1;
        damageFromFireball = lvl * 7;
        timeOfSwordAbility = lvl * 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
