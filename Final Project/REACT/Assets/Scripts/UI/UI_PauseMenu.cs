using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject[] disableOnEnable;

    private GameManager gameManager;
    private SelectionManager selectionManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    private void OnEnable()
    {
        foreach (GameObject item in disableOnEnable)
        {
            item.SetActive(false);
        }
    }

    public void ToggleWindow(GameObject window)
    {

        if (!window.activeSelf)
        {
            if (selectionManager != null) { selectionManager.enableSelection = false; }
            window.SetActive(true);
        }
        else
        {
            if (selectionManager != null) { selectionManager.enableSelection = true; }
            window.SetActive(false);
        }

    }

    public void BackToMainMenu()
    {

        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);

    }

}
