using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Entity
{

    public float Speed
    {
        get
        {
            if (trackTrain != null)
            {
                return trackTrain.speed;
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (trackTrain != null)
            {
                trackTrain.speed = value;
            }
        }

    }

    public Gradient healthGradient = new Gradient();
    public Color healthColor;
    public Trigger trigger;
    public TrackTrain trackTrain;

    public bool constantDamage = false;
    public float damage = 5.0f;
    public float randomRandomRange = 1.0f;
    public float critChance = 0.2f;
    public float critAmount = 20.0f;

    public float cooldown = 1.0f;
    public bool canFire = true;

    public bool friendlyFire = false;
    public bool ignoreTeam = false;

    private bool foundTarget;

    public GameObject target;

    private Coroutine fire;

    private Renderer meshRenderer;

    void Start()
    {
        //trackTrain = GetComponent<TrackTrain>();
        SelectionManager.OnSelect.AddListener(OnSelectEvent);
        meshRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        healthColor = healthGradient.Evaluate(health / maxHealth);

        meshRenderer.material.SetColor("_Color", healthColor);

        GetTarget();
        if (foundTarget && canFire)
        {
            fire = StartCoroutine(nameof(Fire));
        }

    }

    void GetTarget()
    {

        foundTarget = false;

        foreach (GameObject obj in trigger.objectsList)
        {

            if (obj != null)
            {

                Entity entity = obj.GetComponent<Entity>();
                if (entity != null && obj != gameObject)
                {

                    if (entity.team != team)
                    {
                        target = obj;
                        foundTarget = true;
                        break;
                    }
                    else if (ignoreTeam)
                    {
                        target = obj;
                        foundTarget = true;
                        break;
                    }

                }

            }

        }

        if (!foundTarget)
        {
            //target = null;
        }

    }

    public float DamageAmount
    {
        get
        {
            float amount = 0;
            if (constantDamage)
            {
                return damage;
            }
            else
            {

                amount = damage * Random.Range(damage / 2, damage * 1.5f);

                // Is Crit?
                if (Random.value <= critChance)
                {
                    amount += critAmount;
                }

                amount = Mathf.Ceil(amount * 100) / 100;
                Debug.Log($"Damage: {amount}");
                return amount;

            }
        }
    }

    private IEnumerator Fire()
    {

        canFire = false;

        RaycastHit hit;
        Vector3 fireDirection = target.transform.position - transform.position;

        //Debug.DrawRay(transform.position, fireDirection, Color.blue,cooldown);

        if (Physics.Raycast(transform.position, fireDirection, out hit))
        {
            
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();

            if (entity != null)
            {
                Debug.Log("Hit");
                if (entity.team != team)
                {
                    Debug.DrawRay(
                        transform.position,
                        fireDirection,
                        Color.red,
                        cooldown);
                    entity.Damage(DamageAmount);
                }
                else if (ignoreTeam)
                {
                    if (friendlyFire)
                    {
                        Debug.DrawRay(
                            transform.position,
                            fireDirection,
                            Color.green,
                            cooldown);
                        entity.Damage(DamageAmount);
                    }
                }
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        yield return new WaitForSeconds(cooldown);
        canFire = true;

    }

    void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {
            Debug.Log("Click Event Not Implemented Yet");
        }
    }

}
