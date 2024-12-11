using UnityEngine;

public class CameraControllerBase : MonoBehaviour
{

    [SerializeField] protected private GameObject cameraObject;
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    protected Vector3 homePosition;
    protected Quaternion defaultRotation;

    public virtual void SendCameraToHome()
    {
        transform.SetPositionAndRotation(homePosition, defaultRotation);
    }

    public virtual void SetHome()
    {

        homePosition = transform.position;
        defaultRotation = transform.rotation;

    }

    public virtual void MoveRight() { }
    public virtual void MoveLeft() { }
    public virtual void MoveForward() { }
    public virtual void MoveBackward() { }
    public virtual void MoveUp() { }
    public virtual void MoveDown() { }
    public virtual void RotateRight() { }
    public virtual void RotateLeft() { }
    public virtual void RotateWithMouse() { }

}
