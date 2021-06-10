using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//all abilities are called and used in "attackController.cs"
//klasa abstrakcyjna 
public abstract class AbilityMain : MonoBehaviour
{
    #region Singleton
    public static AbilityMain instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    //public class MyFloatEvent : UnityEvent<float> { }
    //public MyFloatEvent OnAbilityUse = new MyFloatEvent();
    [Header("Ability Info")]
    public string title;
    public Sprite icon;
    public float cooldownTime = 1;
    public bool canUse = true;
    public bool dashStarted;
    public bool abilityDone;
    //metoda która uruchamia zdolność,włącza czas jej odnowienia
    public void TriggerAbility()
    {
        if (canUse)
        {
            Ability();
            if (abilityDone)
            {
                StartCooldown();
            }
        }  
    }
    //logika zdolności
    public abstract void Ability();
    //czekam podaną ilość czasu na odnowienie zdolności
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
