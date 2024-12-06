using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void ToggleWindow(GameObject window)
    {

        GameManager gameManager = FindObjectOfType<GameManager>();
        SelectionManager selectionManager = FindObjectOfType<SelectionManager>();

        if (!window.activeSelf)
        {
            if (gameManager != null) { gameManager.PauseTime(); }
            if (selectionManager != null) { selectionManager.enableSelection = false; }
            window.SetActive(true);
        }
        else
        {
            if (gameManager != null) { gameManager.UnPauseTime(); }
            if (selectionManager != null) { selectionManager.enableSelection = true; }
            window.SetActive(false);
        }

    }

    public void BackToMainMenu()
    {

        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);

    }

}
