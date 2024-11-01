using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimbal : MonoBehaviour
{

    public bool TestMode = false;

    public Transform yawTransform;
    public Transform pitchTransform;
    public Transform pointer;

    public float yawSpeed = 0.0001f;
    public float pitchSpeed = 0.0001f;

    public Transform target;

    [SerializeField] private float yaw;
    [SerializeField] private float pitch;

    public float Yaw
    {
        get { return yaw; }
        set
        {
            yaw = value;
            Vector3 oldRot = yawTransform.rotation.eulerAngles;
            //yawTransform.localRotation = Quaternion.Euler(oldRot.x, yaw, oldRot.z);
        }
    }
    public float Pitch
    {
        get { return pitch; }
        set
        {
            pitch = value;
            Vector3 oldRot = yawTransform.rotation.eulerAngles;
            //pitchTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }



    void Start()
    {
        
    }
    private float timeCount = 0.0f;
    // Update is called once per frame
    void Update()
    {
        
        if (TestMode)
        {

            //Yaw = Mathf.Sin(Time.timeSinceLevelLoad) * 180.0f;
            //Pitch = Mathf.Cos(Time.timeSinceLevelLoad) * 180.0f;

            pointer.LookAt(target);
            Yaw = pointer.rotation.y * 180;
            Pitch = pointer.rotation.x * 180;

            yawTransform.rotation = 
                Quaternion.Lerp(yawTransform.rotation, Quaternion.Euler(0, Yaw, 0), Time.deltaTime * yawSpeed);
            
            pitchTransform.localRotation = 
                Quaternion.Lerp(pitchTransform.rotation, Quaternion.Euler(Pitch, 0, 0), Time.deltaTime * pitchSpeed);

            timeCount = timeCount + Time.deltaTime;
        }

    }
}
