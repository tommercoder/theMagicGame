using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    #region Singleton
    public static playerHealth instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("instance playerHealth.cs");
            return;
        }

        instance = this;
    }

    #endregion
    public int health = 100;
    public int currentHealth;
    public healthBarController healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthBar.setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBar.setHealth(currentHealth);
        //testing
        if(currentHealth <= 0)
        {
            Die();
        }
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    //test
        //    takeDamage(20);
            
        //}
    }
    void takeDamage(int damage)
    {
        //undersand how much damage i've got from specific enemy which kicked me and than reduce from health and health bar

        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

        if(currentHealth == 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("player is dead");
    }
}
