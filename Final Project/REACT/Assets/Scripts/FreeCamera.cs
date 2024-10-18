using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : CameraBase
{

    [SerializeField] private Transform XAxis;
    [SerializeField] private Transform YAxis;

    public float sensitivity = 600;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        XAxis.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));
        YAxis.Rotate(new Vector3(0, mouseX, 0));
        //transform.Rotate(rotationSpeed * Time.deltaTime * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
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
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public override void MoveBackward()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    public override void MoveUp()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public override void MoveDown()
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
