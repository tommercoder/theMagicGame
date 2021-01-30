using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//all abilities are called and used in "attackController.cs"
public abstract class AbilityMain : MonoBehaviour
{
    #region Singleton
    public static AbilityMain instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public class MyFloatEvent : UnityEvent<float> { }
    public MyFloatEvent OnAbilityUse = new MyFloatEvent();
    [Header("Ability Info")]
    public string title;
    public Sprite icon;
    public float cooldownTime = 1;
    public bool canUse = true;
    public bool abilityDone;
    public void TriggerAbility()
    {
        if (canUse)
        {
            Ability();
            if (abilityDone)
            {
                OnAbilityUse.Invoke(cooldownTime);
                StartCooldown();
            }
        }  
    }
    public abstract void Ability();
    public void StartCooldown()
    {
        StartCoroutine(Cooldown());
        IEnumerator Cooldown()
        {
            canUse = false;
            yield return new WaitForSeconds(cooldownTime);
            canUse = true;
        }
    }
}
