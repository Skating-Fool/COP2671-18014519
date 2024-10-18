using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{

    public float moveSpeed = 15f;
    public float rotationSpeed = 60f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
