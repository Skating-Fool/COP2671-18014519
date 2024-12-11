using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_RestartLevelButton : MonoBehaviour
{

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

}
