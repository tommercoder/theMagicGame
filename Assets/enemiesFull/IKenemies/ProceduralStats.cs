using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralStats : MonoBehaviour
{
    public bool isDead;
    //public int damage;
    public int health = 10;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }
    private void Update()
    {
        if(currentHealth<=0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
    }
}
