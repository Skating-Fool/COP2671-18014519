using UnityEngine;

public class PauseGameIfEnabled : MonoBehaviour
{

    public GameObject[] objects;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        bool foundActive = false;
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                if (obj.activeSelf)
                {
                    if (gameManager != null) { gameManager.PauseTime(); }
                    foundActive = true;
                }
            }
        }
        if (!foundActive)
        {
            if (gameManager != null) { gameManager.UnPauseTime(); }
        }
    }
}
