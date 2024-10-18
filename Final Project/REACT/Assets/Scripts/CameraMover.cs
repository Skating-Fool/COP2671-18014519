using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
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

    public virtual void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public virtual void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public virtual void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public virtual void MoveBackward()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    public virtual void MoveUp()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public virtual void MoveDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    public virtual void RotateRight()
    {
        transform.Rotate(0f,  rotationSpeed * Time.deltaTime, 0f);
    }

    public virtual void RotateLeft()
    {
        transform.Rotate(0f, -(rotationSpeed * Time.deltaTime), 0f);
    }

    public virtual void RotateWithMouse()
    {

    }

}
