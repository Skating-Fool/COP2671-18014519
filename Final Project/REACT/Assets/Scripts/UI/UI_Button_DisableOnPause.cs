using UnityEngine;
using UnityEngine.UI;

public class UI_Button_DisableOnPause : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 0.0f)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }
}
