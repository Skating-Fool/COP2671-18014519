using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUD : MonoBehaviour
{
    
    public ResourceManager resourceManager;
    public GameManager gameManager;

    public TMP_Text EU_Text;
    public TMP_Text EnemyCount_Text;

    private string EU_Text_init;
    private string EnemyCount_Init;

    void Start()
    {
        if (resourceManager == null)
        {
            resourceManager = FindObjectOfType<ResourceManager>();
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        EU_Text_init = EU_Text.text;
        EnemyCount_Init = EnemyCount_Text.text;

    }

    // Update is called once per frame
    void Update()
    {
        EU_Text.text = $"{EU_Text_init}{resourceManager.power}/{resourceManager.powerCapacity}";
        EnemyCount_Text.text = EnemyCount_Init + gameManager.enemyCount.ToString();
    }
}
