using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void ToggleWindow(GameObject window)
    {
        window.SetActive(!window.activeSelf);
        if (gameObject.activeSelf)
        {
            //Time.timeScale = 0.0f;
        }
        else
        {
            //Time.timeScale = 1.0f;
        }
    }

    public void BackToMainMenu()
    {

        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);

    }

}
