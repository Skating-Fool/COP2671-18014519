using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FreeCamera : CameraControllerBase
{

    [SerializeField] private Transform XAxis;
    [SerializeField] private Transform YAxis;

    public float sensitivity = 600;

    private float xRotation;

    protected Vector3 XAxisHomePosition;
    protected Quaternion XAxisDefaultRot;

    protected float xHomeRotation;
    protected float yHomeRotation;

    void Start()
    {
        SetHome();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetHome()
    {

        XAxisHomePosition = XAxis.position;
        homePosition = YAxis.position;

        XAxisDefaultRot = XAxis.rotation;
        defaultRotation = YAxis.rotation;

        //xRotation = XAxis.rotation.eulerAngles.x; // For Mouse Rotation, so it doesn't snap to 0

    }

    public override void SendCameraToHome()
    {

        YAxis.SetPositionAndRotation(homePosition, defaultRotation);
        XAxis.SetPositionAndRotation(XAxisHomePosition, XAxisDefaultRot);

        //xRotation = XAxis.rotation.eulerAngles.x; // So that rotating with the mouse doesn't snap to pre reset angle

    }

    public override void RotateWithMouse()
    {

        // Quaternion can only store 0 -> 360
        // So anything above or below wraps around

        xRotation = XAxis.localRotation.eulerAngles.x;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;

        if (xRotation > 90 && xRotation < 135)
        {
            xRotation = 90;
        }

        if (xRotation < 270 && xRotation > 135)
        {
            xRotation = 270;
        }

        //xRotation = Mathf.Clamp(xRotation - 90, -90.0f, 90.0f);

        XAxis.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0)); 
        YAxis.Rotate(new Vector3(0, mouseX, 0));

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
