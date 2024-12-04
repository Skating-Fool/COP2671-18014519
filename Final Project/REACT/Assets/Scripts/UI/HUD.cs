using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class HUD : MonoBehaviour
{
    
    public ResourceManager resourceManager;
    public GameManager gameManager;

    public UI_BarDisplay EU_Bar;
    public TMP_Text EnemyCount_Text;

    public TMP_Text MetalCount_Text;

    private string EnemyCount_Init;
    private string MetalCount_Init;

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

        EU_Bar.Min = 0;
        EU_Bar.Max = resourceManager.powerCapacity;
        EnemyCount_Init = EnemyCount_Text.text;
        MetalCount_Init = MetalCount_Text.text;

    }

    // Update is called once per frame
    void Update()
    {

        EU_Bar.Data = resourceManager.power;
        EnemyCount_Text.text = $"{EnemyCount_Init}{gameManager.enemyCount}";
        MetalCount_Text.text = $"{MetalCount_Init}{resourceManager.metal}";

    }
}
