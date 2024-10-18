using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthorgraphicCamera : CameraControllerBase
{

    public float MinimumHeight = 2;
    public float MaximumHeight = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public override void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public override void MoveForward()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public override void MoveBackward()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }


    public override void RotateRight()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public override void RotateLeft()
    {
        transform.Rotate(0f, -(rotationSpeed * Time.deltaTime), 0f);
    }

}
