using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : CameraControllerBase
{

    [SerializeField] private Transform XAxis;
    [SerializeField] private Transform YAxis;

    public float sensitivity = 600;

    private float xRotation;

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
        if (homeTransform == null)
        {
            homePosition = transform.position;
            defaultRotation = transform.rotation;

            xHomeRotation = XAxis.rotation.x;
            yHomeRotation = YAxis.rotation.y;
        }
        else
        {
            homePosition = homeTransform.position;
            defaultRotation = homeTransform.rotation;

            xHomeRotation = XAxis.rotation.x;
            yHomeRotation = YAxis.rotation.y;
        }
    }

    public override void SendCameraToHome()
    {
        XAxis.localPosition = homePosition;
        YAxis.position = homePosition;

        XAxis.localRotation = Quaternion.Euler(new Vector3(xHomeRotation, 0, 0));
        YAxis.rotation = Quaternion.Euler(0, xHomeRotation, 0);

        xRotation = YAxis.rotation.x; // So that rotating with the mouse doesn't snap to pre reset angle

    }

    public override void RotateWithMouse()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

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
