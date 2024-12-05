using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class WeaponBase : Entity
{

    public UnityEvent OnFire;

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

    public float powerDraw = 1.0f;
    public float powerDrawDelay = 0.5f;
    public bool drawPower = true;
    public float power = 100.0f;
    public float powerCapacity = 100.0f;

    private bool canDrawPower = true;

    private Coroutine getPower;

    public override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        OnFire ??= new UnityEvent();
    }

    void Update()
    {
        
        if (isAlive)
        {

            if (gimbal.target != null)
            {
                //Debug.DrawLine(gimbal.puppet.position, gimbal.target.transform.position, new Color(255, 0, 0, 0.1f), 0.1f);
            }

            if (canDrawPower)
            {
                getPower = StartCoroutine(nameof(GetPower));
            }

            GetTarget();
            if (foundTarget && canFire)
            {
                fire = StartCoroutine(nameof(Fire));
            }

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

    public virtual bool DrainPower(float? amount = null)
    {

        float _amount;
        if (amount == null)
        {
            _amount = powerDraw;
        }
        else
        {
            _amount = amount.Value;
        }

        if (drawPower)
        {

            if (power >= powerDraw)
            {
                power -= _amount;
                // Limit to 2 decimal places
                power = Mathf.Ceil(power * 100) / 100;
            }
            else
            {
                return false;
            }

            if (power <= 0.0f)
            {
                power = 0.0f;
            }

        }

        return true;

    }

    private IEnumerator GetPower()
    {
        canDrawPower = false;
        if (resourceManager != null)
        {
            if (resourceManager.power >= powerDraw && power <= powerCapacity - powerDraw)
            {
                if (resourceManager.power < powerDraw)
                {
                    float amount = resourceManager.power;
                    resourceManager.power -= amount;
                    power += amount;
                }
                else
                {
                    power += powerDraw;
                    resourceManager.power -= powerDraw;
                }
            }
        }

        yield return new WaitForSeconds(powerDrawDelay);
        canDrawPower = true;
    }

    private IEnumerator Fire()
    {
        canFire = false;

        if (drawPower)
        {
            if (DrainPower() == false)
            {
                canFire = true;
                yield break;
            }
        }

        OnFire.Invoke();

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

    public override void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {

            if ( mouseClickNum == 1)
            {
                Debug.Log($"[{name}] Click Event Not Implemented Yet");
            }
            
        }
    }

}
