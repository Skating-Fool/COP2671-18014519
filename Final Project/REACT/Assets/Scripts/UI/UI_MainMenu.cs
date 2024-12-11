using UnityEditor;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{

    public void ToggleWindow(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

}
