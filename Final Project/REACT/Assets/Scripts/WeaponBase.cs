using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : Entity
{

    public Trigger detectionTrigger;
    public Gimbal gimbal;
    public Transform firePoint;

    public bool constantDamage = false;
    public float damage = 5.0f;
    public float randomRandomRange = 1.0f;
    public float critChance = 0.2f;
    public float critAmount = 20.0f;

    public bool friendlyFire = false;
    public bool ignoreTeam = false;

    private bool foundTarget;

    void Start()
    {
        SelectionManager.OnSelect.AddListener(OnSelectEvent);
    }

    void Update()
    {
        if (gimbal.target != null)
        {
            Debug.DrawLine(gimbal.puppet.position, gimbal.target.transform.position, new Color(255, 0, 0, 0.1f), 0.1f);
        }

        GetTarget();
        if (foundTarget)
        {
            Fire();
        }

    }

    void GetTarget()
    {

        foundTarget = false;

        foreach (GameObject obj in detectionTrigger.objectsList)
        {

            if (obj != null)
            {
                Entity entity = obj.GetComponent<Entity>();
                if (entity != null && obj != gameObject)
                {

                    if (entity.team != team)
                    {
                        gimbal.target = obj.transform;
                        foundTarget = true;
                        break;
                    }
                    else if (ignoreTeam)
                    {
                        gimbal.target = obj.transform;
                        foundTarget = true;
                        break;
                    }

                }
            }

        }

        if (!foundTarget)
        {
            gimbal.target = gimbal.defaultTarget;
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
                    Debug.Log($"Crit: {amount}");
                }

                return amount;

            }
        }
    }

    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit))
        {

            Entity entity = hit.collider.gameObject.GetComponent<Entity>();

            if (entity != null)
            {
                if (entity.team != team || ignoreTeam)
                {
                    Debug.DrawRay(
                        firePoint.position, 
                        firePoint.TransformDirection(Vector3.forward) * hit.distance, 
                        new Color(255, 100, 0));
                    entity.Damage(DamageAmount);
                }
            }

        }
        else
        {
            Debug.DrawRay(firePoint.position, firePoint.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
        //Debug.DrawRay(firePoint.position, firePoint.eulerAngles, new Color(255, 255, 0), 0.1f);
        //Physics.Raycast(firePoint.position, firePoint.eulerAngles);
    }

    void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {
            Debug.Log("Click Event Not Implemented Yet");
        }
    }

}
