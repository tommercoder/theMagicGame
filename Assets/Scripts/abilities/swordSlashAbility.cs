using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSlashAbility : AbilityMain
{
    public AbilityUI AbilityUI;
    public float waitToTurnOffSlash;
 
    void Start()
    {
    
    }
    public override void Ability()
    {
        //włacza się drugi trail na mieczu
        playerSword.instance.sword.GetChild(1).gameObject.SetActive(true);
        AbilityUI.ShowCoolDown(cooldownTime);
        abilityDone = true;
        //zmienia obrażenie miecza
        playerSword.instance.currentSword.swordDamage += playerSword.instance.currentSword.swordDamage * 20 / 100;
        //czeka na wyłączenie effektu
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
