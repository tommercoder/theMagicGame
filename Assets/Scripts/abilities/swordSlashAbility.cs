using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSlashAbility : AbilityMain
{
    public AbilityUI AbilityUI;
    public playerSword swordController;
    public Transform sword;
    public float waitToTurnOffSlash;
 
    void Start()
    {
        
        swordController = GetComponent<playerSword>();
        
    }
    public override void Ability()
    {
      
        playerSword.instance.sword.GetChild(1).gameObject.SetActive(true);
        AbilityUI.ShowCoolDown(cooldownTime);
        abilityDone = true;
        
        playerSword.instance.currentSword.swordDamage += playerSword.instance.currentSword.swordDamage * 20 / 100;
        StartCoroutine(waitOff());
    }

   IEnumerator waitOff()
    {
        yield return new WaitForSeconds(waitToTurnOffSlash);
        playerSword.instance.sword.GetChild(1).gameObject.SetActive(false);
        playerSword.instance.currentSword.swordDamage -= playerSword.instance.currentSword.swordDamage * 20 / 100;
    }

    void Update()
    {
        waitToTurnOffSlash = characterStats.instance.timeOfSwordAbility;
    }
}
