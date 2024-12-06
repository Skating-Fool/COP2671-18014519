using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public CameraManager cameraController;
    public PauseMenu pauseMenu;

    private Vector2 OldCursorPos = new Vector2();

    void Start()
    {
        Assert.IsNotNull(cameraController, "cameraController Is Null");
        Assert.IsNotNull(pauseMenu, "Pause Menu Is Null");
    }

    // Update is called once per frame
    void Update()
    {

        // Open Pause Menu
        if (Input.GetKeyDown("escape"))
        {
            pauseMenu.ToggleWindow(pauseMenu.gameObject);
            
        }

        // Move Camera
        if (Input.GetKey("w"))
        {
            cameraController.MoveForward();
        }

        if (Input.GetKey("a"))
        {
            cameraController.MoveLeft();
        }

        if (Input.GetKey("s"))
        {
            cameraController.MoveBackward();
        }

        if (Input.GetKey("d"))
        {
            cameraController.MoveRight();
        }

        if (Input.GetKey("left shift"))
        {
            cameraController.MoveDown();
        }

        if (Input.GetKey("space"))
        {
            cameraController.MoveUp();
        }
        
        /*
        if (Input.mouseScrollDelta.y > 0.1f) // Replace With Mouse Wheel
        {
            CameraController.MoveDown();
        }

        if (Input.mouseScrollDelta.y < -0.1f) // Replace With Mouse Wheel
        {
            CameraController.MoveUp();
        }
        */

        // Rotate Camera
        if (Input.GetKey("q"))
        {
            cameraController.RotateLeft();
        }

        if (Input.GetKey("e"))
        {
            cameraController.RotateRight();
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
            
            cameraController.RotateWithMouse();
        }
        else if (!Cursor.visible) // Make visable, unlock, and return to old postion
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Mouse.current.WarpCursorPosition(OldCursorPos);
        }

        if (Input.GetKey("mouse 2") && Input.GetKey("left shift"))
        {
            
        }
        
        // Camera Switching
        if (Input.GetKeyDown("1"))
        {
            cameraController.SwitchTo(Cam.FreeCam);
        }
        else if(Input.GetKeyDown("2"))
        {
            cameraController.SwitchTo(Cam.TopCam);
        }
        else if (Input.GetKeyDown("3"))
        {
            cameraController.SwitchTo(Cam.OrthographicCam);
        }

        // Reset Camera Postion
        if (Input.GetKeyDown("r"))
        {
            cameraController.SendCameraToHome();
        }

    }

}
