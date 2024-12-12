using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private CameraManager cameraController;
    [SerializeField] private PauseMenu pauseMenu;

    private Vector2 OldCursorPos = new();

    void Start()
    {
        Assert.IsNotNull(cameraController, "cameraController Is Null");
        Assert.IsNotNull(pauseMenu, "Pause Menu Is Null");
    }

    // Update is called once per frame
    void Update()
    {

        // Open Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.ToggleWindow(pauseMenu.gameObject);
        }

        // Move Camera
        if (Input.GetKey(KeyCode.W))
        {
            cameraController.MoveForward();
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraController.MoveLeft();
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraController.MoveBackward();
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraController.MoveRight();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraController.MoveDown();
        }

        if (Input.GetKey(KeyCode.Space))
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
        if (Input.GetKey(KeyCode.Q))
        {
            cameraController.RotateLeft();
        }

        if (Input.GetKey(KeyCode.E))
        {
            cameraController.RotateRight();
        }

        if (Input.GetKey(KeyCode.Mouse1))
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

        if (Input.GetKey(KeyCode.Mouse2) && Input.GetKey(KeyCode.LeftShift))
        {

        }

        // Camera Switching
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraController.SwitchTo(Cam.FreeCam);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraController.SwitchTo(Cam.TopCam);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraController.SwitchTo(Cam.OrthographicCam);
        }

        // Reset Camera Postion
        if (Input.GetKeyDown(KeyCode.R))
        {
            cameraController.SendCameraToHome();
        }

    }

}
