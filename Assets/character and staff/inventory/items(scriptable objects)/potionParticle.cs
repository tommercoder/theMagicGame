using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//turn on particles and turn off and on timers for potions
public class potionParticle : MonoBehaviour
{
    public static potionParticle instance;
    public ParticleSystem healthParticle;
    public ParticleSystem speedParticle;
    public ParticleSystem damageParticle;
    public bool usingDamagePotionNow;
    private void Awake()
    {
        instance = this;
    }

    
    public void turn(typeOfItem type)
    {
        
        StartCoroutine(turnOnParticle(type));
    }
    //włącza particle system na przydzieloną ilośc czasu
    public IEnumerator turnOnParticle(typeOfItem type)
    {
        if(type == typeOfItem.damagePotion)
        {
            damageParticle.Play();
            yield return new WaitForSeconds(2);
            damageParticle.Stop();
        }
        if (type == typeOfItem.healthPotion)
        {
        healthParticle.Play();
            yield return new WaitForSeconds(2);
            healthParticle.Stop();
        }
        if (type == typeOfItem.speedPotion)
        {
            speedParticle.Play();
            yield return new WaitForSeconds(2);
            speedParticle.Stop();
        }
    }

    public void startTimer(float time,typeOfItem type)
    {
        StartCoroutine(timeForPotion(time, type));
    }
    public IEnumerator timeForPotion(float time,typeOfItem type)
    {
        if (type == typeOfItem.damagePotion)
        {
            usingDamagePotionNow = true;
            Debug.Log("weapon damage now " + weaponInteract.instance.damage);
            yield return new WaitForSeconds(time);
            usingDamagePotionNow = false;
            foreach(swordEquipping s in allSwordReset.instance.swords)
            {
                if(s.oldDamage!=s.swordDamage)
                {
                    s.swordDamage = s.oldDamage;
                }
            }
            inventoryManager.instance.damageText.text = "current damage is: " + playerSword.instance.currentSword.swordDamage;
        }
        if (type == typeOfItem.speedPotion)
        {
            yield return new WaitForSeconds(time);
            movement.instance.playerSpeed -= 2;
            movement.instance.speedPotionUsingNow = false;
        }
    }
  
}
