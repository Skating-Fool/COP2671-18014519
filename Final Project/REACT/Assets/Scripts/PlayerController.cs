using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public CameraController CameraController;

    public Vector2 OldCursorPos = new Vector2();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Move Camera
        if (Input.GetKey("w"))
        {
            CameraController.MoveForward();
        }

        if (Input.GetKey("a"))
        {
            CameraController.MoveLeft();
        }

        if (Input.GetKey("s"))
        {
            CameraController.MoveBackward();
        }

        if (Input.GetKey("d"))
        {
            CameraController.MoveRight();
        }

        if (Input.GetKey("left shift"))
        {
            CameraController.MoveDown();
        }

        if (Input.GetKey("space"))
        {
            CameraController.MoveUp();
        }

        // Rotate Camera
        if (Input.GetKey("q"))
        {
            CameraController.RotateLeft();
        }

        if (Input.GetKey("e"))
        {
            CameraController.RotateRight();
        }

        if (Input.GetKey("mouse 1"))
        {

            if (Cursor.visible)
            {
                // Store cursor position so that the cursor can be returned there when made visable again
                OldCursorPos = Mouse.current.position.value;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            CameraController.RotateWithMouse();
        }
        else if (!Cursor.visible) // Make visable, unlock, and return to old postion
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Mouse.current.WarpCursorPosition(OldCursorPos);
        }
        
        // Camera Switching
        if (Input.GetKeyDown("1"))
        {
            CameraController.SwitchTo(cam.FreeCam);
        }
        else if(Input.GetKeyDown("2"))
        {
            CameraController.SwitchTo(cam.TopCam);
        }
        else if (Input.GetKeyDown("3"))
        {
            CameraController.SwitchTo(cam.OrthographicCam);
        }

    }

}
