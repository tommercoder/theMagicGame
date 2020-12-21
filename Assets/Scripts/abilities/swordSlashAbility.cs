﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSlashAbility : AbilityMain
{
    public AbilityUI AbilityUI;
    public playerSword swordController;
    Transform sword;
    // Start is called before the first frame update
    void Start()
    {
        swordController = GetComponent<playerSword>();
        sword = swordController.sword.transform;
    }
    public override void Ability()
    {
        sword.GetChild(1).gameObject.SetActive(true);
        AbilityUI.ShowCoolDown(cooldownTime);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}