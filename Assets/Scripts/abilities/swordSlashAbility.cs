using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSlashAbility : AbilityMain
{
    public AbilityUI AbilityUI;
    public playerSword swordController;
    public Transform sword;
    public int waitToTurnOffSlash = 5;
    // Start is called before the first frame update
    void Start()
    {
        swordController = GetComponent<playerSword>();
        //sword = swordController.sword.transform;
    }
    public override void Ability()
    {
      
        playerSword.instance.sword.GetChild(1).gameObject.SetActive(true);
        AbilityUI.ShowCoolDown(cooldownTime);
        abilityDone = true;
        //set damage of current sword higher
        StartCoroutine(waitOff());
    }

   IEnumerator waitOff()
    {
        yield return new WaitForSeconds(waitToTurnOffSlash);
        playerSword.instance.sword.GetChild(1).gameObject.SetActive(false);
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
