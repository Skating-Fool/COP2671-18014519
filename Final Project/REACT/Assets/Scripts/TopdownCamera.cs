using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopdownCamera : CameraControllerBase
{

    public float MinimumHeight = 2; 
    public float MaximumHeight = 20;

    public float sensitivity = 600;

    private float xRotation;

    void Start()
    {
        SetHome();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RotateWithMouse()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        transform.Rotate(new Vector3(0, 0, mouseX));

    }

    public override void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public override void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public override void MoveForward() // SCREEN UP
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public override void MoveBackward() // SCREEN DOWN
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    public override void MoveUp() // SCENE UP
    {

        if (transform.position.y < MaximumHeight)
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        
    }

    public override void MoveDown() // SCENE DOWN
    {

        if (transform.position.y > MinimumHeight)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

    }

    public override void RotateRight()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    public override void RotateLeft()
    {
        transform.Rotate(0f, 0f, -(rotationSpeed * Time.deltaTime));
    }

}
