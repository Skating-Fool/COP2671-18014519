using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gimbal : MonoBehaviour
{

    // Doesn't like being rotated
    // Keep upright

    public bool instantSnap = false;
    
    public bool TestMode = false;
    public Vector3 TestModeLimits = new Vector3(10, 10, 10);

    public Transform yawTransform;
    public Transform pitchTransform;

    public Transform puppet;
    public Transform pointer;

    public float yawSpeed = 8.0f;
    public float pitchSpeed = 8.0f;

    public Transform target;

    private float yaw;
    private float pitch;

    public float Yaw
    {
        get { return yaw; }
        set
        {
            yaw = value;
            Vector3 oldRot = yawTransform.rotation.eulerAngles;
            yawTransform.localRotation = Quaternion.Euler(0, yaw, 0);
        }
    }
    public float Pitch
    {
        get { return pitch; }
        set
        {
            pitch = value;
            Vector3 oldRot = yawTransform.rotation.eulerAngles;
            pitchTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
        if (TestMode)
        {
            float x = MathF.Sin(Time.time) * TestModeLimits.x;
            float y = MathF.Sin(Time.time * 3.1425f) * TestModeLimits.y;
            float z = MathF.Cos(Time.time) * TestModeLimits.z;
            target.position = transform.position + new Vector3(x, y, z);
        }


        pointer.LookAt(target);

        if (!instantSnap)
        {
            //find the vector pointing from our position to the target
            Vector3 direction = (target.position - puppet.position).normalized;

            //create the rotation we need to be in to look at the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);


            //puppet.rotation = Quaternion.Slerp(puppet.rotation, lookRotation, Time.deltaTime * yawSpeed);
            float newYaw = Mathf.LerpAngle
            (
                puppet.rotation.eulerAngles.y,
                lookRotation.eulerAngles.y,
                Time.deltaTime * yawSpeed
            );

            float newPitch = Mathf.LerpAngle
            (
                puppet.rotation.eulerAngles.x,
                lookRotation.eulerAngles.x,
                Time.deltaTime * pitchSpeed
            );

            puppet.rotation = Quaternion.Euler(newPitch, newYaw, 0);
            Yaw = puppet.rotation.eulerAngles.y;
            Pitch = puppet.rotation.eulerAngles.x;
        }
        else
        {
            puppet.rotation = pointer.rotation;
            Yaw = puppet.rotation.eulerAngles.y;
            Pitch = puppet.rotation.eulerAngles.x;
        }

    }
}
