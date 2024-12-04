using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Cam
{
    FreeCam,
    TopCam,
    OrthographicCam
}

public class CameraManager : MonoBehaviour
{
    
    public GameObject freeCamera;
    public GameObject topdownCamera;
    public GameObject orthographicCamera;

    private GameObject[] cameras = new GameObject[3];
    private Camera[] cameraComponents = new Camera[3];
    private CameraControllerBase[] currentCameraMovers = new CameraControllerBase[3];

    private Camera currentCameraComponent;
    private CameraControllerBase currentCameraController;
    [SerializeField] private Cam currentCamera = Cam.FreeCam;

    void Start()
    {

        cameras[0] = freeCamera;
        cameras[1] = topdownCamera;
        cameras[2] = orthographicCamera;

        int index = 0;
        foreach (var cam in cameras)
        {
            cameraComponents[index] = cam.GetComponent<Camera>();
            currentCameraMovers[index] = cam.GetComponent<CameraControllerBase>();
        }

        SwitchTo(currentCamera);

    }

    void Update()
    {

    }

    public void SwitchTo(Cam camera)
    {

        foreach (var cam in cameras)
        {
            cam.SetActive(false);
        }

        cameras[(int) camera].SetActive(true);
        currentCamera = camera;
        currentCameraComponent = cameraComponents[(int) camera];
        currentCameraController = cameras[(int) camera].GetComponent<CameraControllerBase>();

    }

    public void MoveRight()
    {
        currentCameraController.MoveRight();
    }

    public void MoveLeft()
    {
        currentCameraController.MoveLeft();
    }

    public void MoveForward()
    {
        currentCameraController.MoveForward();
    }

    public void MoveBackward()
    {
        currentCameraController.MoveBackward();
    }

    public void MoveUp()
    {
        currentCameraController.MoveUp();
    }

    public void MoveDown()
    {
        currentCameraController.MoveDown();
    }

    public void RotateRight()
    {
        currentCameraController.RotateRight();
    }

    public void RotateLeft()
    {
        currentCameraController.RotateLeft();
    }

    public void RotateWithMouse()
    {
        currentCameraController.RotateWithMouse();
    }

    public void SendCameraToHome()
    {
        currentCameraController.SendCameraToHome();
    }

}
