using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelSelect : MonoBehaviour
{

    public void LoadScene(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }

}