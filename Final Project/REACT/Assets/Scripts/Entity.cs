using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string team;
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public float power = 100.0f;
    public float maxPower = 100.0f;

    public void Damage(float amount)
    {
        if (health > 0.0f)
        {
            health -= amount;
        }

        if (health <= 0.0f)
        {
            health = 0.0f;
            Destroy(gameObject);
        }
        
        // Limit to 2 decimal places
        health = Mathf.Ceil(health * 100) / 100;

    }

    public void DrainPower(float amount)
    {
        if (power > 0.0f)
        {
            power -= amount;
        }

        if (power <= 0.0f)
        {
            power = 0.0f;
        }

        // Limit to 2 decimal places
        power = Mathf.Ceil(power * 100) / 100;

    }

}
