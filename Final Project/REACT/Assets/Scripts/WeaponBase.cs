using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.LowLevel;
using System.Linq;

public class WeaponBase : Entity
{

    public Trigger detectionTrigger;
    public Gimbal gimbal;
    public Transform firePoint;

    public bool constantDamage = false;
    public float damage = 5.0f;
    public float randomDamageRange = 1.0f;
    public float critChance = 0.2f;
    public float critAmount = 20.0f;

    public float cooldown = 1.0f;
    public bool canFire = true;

    public bool penetrate = false;
    public int maxPenetrateCount = 1;

    public bool friendlyFire = false;
    public bool ignoreTeam = false;

    private bool foundTarget;

    private Coroutine fire;

    void Start()
    {

        SelectionManager.OnSelect.AddListener(OnSelectEvent);

    }

    void Update()
    {
        if (gimbal.target != null)
        {
            //Debug.DrawLine(gimbal.puppet.position, gimbal.target.transform.position, new Color(255, 0, 0, 0.1f), 0.1f);
        }

        GetTarget();
        if (foundTarget && canFire)
        {
            fire = StartCoroutine(nameof(Fire));
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

        Vector3 fireDirection = firePoint.TransformDirection(Vector3.forward);
        List<RaycastHit> hits = Physics.RaycastAll(firePoint.position, fireDirection).ToList();

        hits.Sort(SortByDistance);

        int hitcount = 0;

        Color randomColor = Random.ColorHSV(0, 1, 1, 1);

        foreach (RaycastHit hit in hits)
        {

            if (hit.collider.isTrigger)
            {
                continue;
            }

            Debug.DrawLine(hit.point, hit.point + (hit.normal / 2), randomColor, 1f);

            GameObject objectHit = hit.collider.gameObject;

            Entity entity = objectHit.GetComponent<Entity>();

            if (entity != null)
            {

                if (entity.team != team)
                {

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

        yield return new WaitForSeconds(cooldown);
        canFire = true;

        /*
        canFire = false;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit))
        {

            Entity entity = hit.collider.gameObject.GetComponent<Entity>();

            if (entity != null)
            {
                if (entity.team != team)
                {
                    Debug.DrawRay(
                        firePoint.position, 
                        firePoint.TransformDirection(Vector3.forward) * hit.distance, 
                        Color.red);
                    entity.Damage(DamageAmount);
                }
                else if (ignoreTeam)
                {
                    if (friendlyFire)
                    {
                        Debug.DrawRay(
                            firePoint.position,
                            firePoint.TransformDirection(Vector3.forward) * hit.distance,
                            Color.green);
                        entity.Damage(DamageAmount);
                    }
                }
            }

        }
        else
        {
            Debug.DrawRay(firePoint.position, firePoint.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
        //Debug.DrawRay(firePoint.position, firePoint.eulerAngles, new Color(255, 255, 0), 0.1f);
        //Physics.Raycast(firePoint.position, firePoint.eulerAngles);

        
        yield return new WaitForSeconds(cooldown);
        canFire = true;
        //yield return null;
        */
    }

    private int SortByDistance(RaycastHit obj1, RaycastHit obj2)
    {
        if (obj2.collider.gameObject == null)
        {
            return 1;
        }
        else if (obj1.collider.gameObject == null)
        {
            return -1;
        }
        else
        {
            float distanceA = Vector3.Distance(transform.position, obj2.point);
            float distanceB = Vector3.Distance(transform.position, obj1.point);
            if (distanceA > distanceB)
            {
                return -1;
            }
            else if (distanceA < distanceB)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {
            Debug.Log("Click Event Not Implemented Yet");
        }
    }

}
