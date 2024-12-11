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
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
    }

    public override void MoveLeft()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
    }

    public override void MoveForward() // SCREEN UP
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
    }

    public override void MoveBackward() // SCREEN DOWN
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
    }

    public override void MoveUp() // SCENE UP
    {

        if (transform.position.y < MaximumHeight)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
        }

    }

    public override void MoveDown() // SCENE DOWN
    {

        if (transform.position.y > MinimumHeight)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
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
