using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamIdentifier))]
public class WeaponBase : MonoBehaviour
{

    public Trigger trigger;
    public Gimbal gimbal;

    private TeamIdentifier teamIdentity;

    void Start()
    {
        teamIdentity = GetComponent<TeamIdentifier>();
        SelectionManager.OnSelect.AddListener(OnSelectEvent);
    }

    void Update()
    {
        if (gimbal.target != null)
        {
            Debug.DrawLine(gimbal.puppet.position, gimbal.target.transform.position, new Color(255, 0, 0), 0.1f);
        }

        GetTarget();

    }

    void GetTarget()
    {

        bool foundTarget = false;

        foreach (GameObject obj in trigger.objectsList)
        {

            TeamIdentifier objTeamIdentity = obj.GetComponent<TeamIdentifier>();
            if (objTeamIdentity != null)
            {

                if (objTeamIdentity.team != teamIdentity.team)
                {
                    gimbal.target = obj.transform;
                    foundTarget = true;
                    break;
                }
                    
            }

        }

        if (!foundTarget)
        {
            gimbal.target = gimbal.defaultTarget;
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
