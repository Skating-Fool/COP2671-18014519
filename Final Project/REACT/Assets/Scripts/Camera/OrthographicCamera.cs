using UnityEngine;

public class OthorgraphicCamera : CameraControllerBase
{

    public float MinimumHeight = 2;
    public float MaximumHeight = 20;

    private void Start()
    {
        SetHome();
    }

    public override void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
    }

    public override void MoveLeft()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
    }

    public override void MoveForward()
    {
        transform.Translate(moveSpeed * Time.deltaTime * new Vector3(0, 0, 1));
    }

    public override void MoveBackward()
    {
        transform.Translate(moveSpeed * Time.deltaTime * new Vector3(0, 0, -1));
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
