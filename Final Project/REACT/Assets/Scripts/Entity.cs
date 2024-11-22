using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public string team;
    public bool isAlive = true;
    public bool destroyOnDeath = true;
    public float health = 100.0f;
    public float maxHealth = 100.0f;

    public UnityEvent<Entity> OnDeath;

    public ResourceManager resourceManager;

    private void Awake()
    {
        OnDeath ??= new UnityEvent<Entity>();
    }

    public virtual void Start()
    {

        SelectionManager.OnSelect.AddListener(OnSelectEvent);

        if (resourceManager == null)
        {
            resourceManager = FindObjectOfType<ResourceManager>();
        }

    }

    public virtual void Damage(float amount)
    {
        if (health > 0.0f)
        {
            health -= amount;
        }

        if (health <= 0.0f)
        {
            health = 0.0f;
            if (isAlive)
            {
                isAlive = false;
                OnDeath.Invoke(this);
                if (destroyOnDeath)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        // Limit to 2 decimal places
        health = Mathf.Ceil(health * 100) / 100;

    }

    public virtual void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {
            Debug.Log("Click Event Not Implemented Yet");
        }
    }

}
