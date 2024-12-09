using System;
using UnityEngine;

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

    public float yawSpeed = 5.0f;
    public float pitchSpeed = 8.0f;

    public Transform target;
    public Transform defaultTarget;

    private float yaw;
    private float pitch;

    public float Yaw
    {
        get { return yaw; }
        set
        {
            yaw = value;
            yawTransform.rotation = Quaternion.Euler(0, yaw, 0);
        }

    }

    public float Pitch
    {
        get { return pitch; }
        set
        {
            pitch = value;
            pitchTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }

    }

    void Start()
    {

    }

    void Update()
    {

        // If no target, use default target
        if (defaultTarget != null && target == null)
        {
            target = defaultTarget;
        }
        else
        {
            //Debug.LogWarning("No default target Set");
        }

        if (target != null)
        {

            if (TestMode) // Spin in circle occilating up and down by limit variable
            {
                float x = MathF.Sin(Time.time) * TestModeLimits.x;
                float y = MathF.Sin(Time.time * 3.1425f) * TestModeLimits.y;
                float z = MathF.Cos(Time.time) * TestModeLimits.z;
                target.position = transform.position + new Vector3(x, y, z);
            }

            pointer.LookAt(target);

            if (!instantSnap)
            {

                Vector3 direction = (target.position - puppet.position).normalized;

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
                Pitch = puppet.localRotation.eulerAngles.x;
            }
            else
            {
                puppet.rotation = pointer.rotation;
                Yaw = puppet.rotation.eulerAngles.y;
                Pitch = puppet.localRotation.eulerAngles.x;
            }

        }

    }

}
