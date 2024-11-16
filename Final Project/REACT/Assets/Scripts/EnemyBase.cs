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
    public float randomDamageRange = 1.0f;
    public float critChance = 0.2f;
    public float critAmount = 20.0f;

    public float cooldown = 1.0f;
    private bool canFire = true;

    public bool penetrate = false;
    public int maxPenetrateCount = 1;

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
            target = null;
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
                //Debug.Log($"Damage: {amount}");
                return amount;

            }
        }
    }

    private IEnumerator Fire()
    {

        canFire = false;

        //Entity ent = target.GetComponent<Entity>();
        //if (ent != null) ent.Damage(DamageAmount);
        
        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
        Vector3 targetPoint;
        if (targetRigidbody != null)
        {
            targetPoint = target.transform.position + targetRigidbody.centerOfMass;
        }
        else
        {
            targetPoint = target.transform.position;
        }

        Vector3 fireDirection = targetPoint - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, fireDirection);

        int hitcount = 0;
        string debugMessage = "Hits: ";
        foreach(RaycastHit hit in hits)
        {

            if (hit.collider.isTrigger)
            {
                continue;
            }

            GameObject objectHit = hit.collider.gameObject;
            
            Debug.DrawRay(transform.position, fireDirection, Color.white, cooldown * 2);

            Entity entity = objectHit.GetComponent<Entity>();

            debugMessage += $"{objectHit.name}, ";

            if (entity != null)
            {

                if (entity.team != team)
                {

                    Debug.DrawRay(
                        transform.position,
                        fireDirection,
                        Color.red,
                        cooldown * 2);
                    entity.Damage(DamageAmount);

                    if (penetrate && entity != null)
                    {
                        hitcount++;
                    }

                }
                else if (ignoreTeam)
                {

                    if (friendlyFire)
                    {

                        Debug.DrawRay(
                            transform.position,
                            fireDirection,
                            Color.green,
                            cooldown * 2);
                        entity.Damage(DamageAmount);

                        if (penetrate && entity != null)
                        {
                            hitcount++;
                        }

                    }

                }

                if (hitcount >= maxPenetrateCount && penetrate)
                {
                    break;
                }

            }

        }
        Debug.Log(debugMessage);
        
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
