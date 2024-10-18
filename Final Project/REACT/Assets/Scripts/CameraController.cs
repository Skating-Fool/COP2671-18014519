using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cam
{
    FreeCam,
    TopCam,
    OrthographicCam
}

public class CameraController : MonoBehaviour
{
    
    public GameObject freeCamera;
    public GameObject topdownCamera;
    public GameObject orthographicCamera;

    private GameObject[] cameras = new GameObject[3];
    private Camera[] cameraComponents = new Camera[3];
    private CameraMover[] currentCameraMovers = new CameraMover[3];

    private Camera currentCameraComponent;
    private CameraMover currentCameraMover;
    private cam currentCamera = cam.FreeCam;

    void Start()
    {

        cameras[0] = freeCamera;
        cameras[1] = topdownCamera;
        cameras[2] = orthographicCamera;

        int index = 0;
        foreach (var cam in cameras)
        {
            cameraComponents[index] = cam.GetComponent<Camera>();
            currentCameraMovers[index] = cam.GetComponent<CameraMover>();
        }

        SwitchTo(cam.FreeCam);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTo(cam camera)
    {

        foreach (var cam in cameras)
        {
            cam.SetActive(false);
        }

        cameras[(int) camera].SetActive(true);
        currentCamera = camera;
        currentCameraComponent = cameraComponents[(int) camera];
        currentCameraMover = cameras[(int) camera].GetComponent<CameraMover>();

    }

    public void MoveRight()
    {
        currentCameraMover.MoveRight();
    }

    public void MoveLeft()
    {
        currentCameraMover.MoveLeft();
    }

    public void MoveForward()
    {
        currentCameraMover.MoveForward();
    }

    public void MoveBackward()
    {
        currentCameraMover.MoveBackward();
    }

    public void MoveUp()
    {
        currentCameraMover.MoveUp();
    }

    public void MoveDown()
    {
        currentCameraMover.MoveDown();
    }

    public void RotateRight()
    {
        currentCameraMover.RotateRight();
    }

    public void RotateLeft()
    {
        currentCameraMover.RotateLeft();
    }

    public void RotateWithMouse()
    {
        currentCameraMover.RotateWithMouse();
    }

}
