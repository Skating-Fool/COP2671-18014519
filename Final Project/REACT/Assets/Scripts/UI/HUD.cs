using System;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{

    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private UI_BarDisplay EU_Bar;
    [SerializeField] private TMP_Text EnemyCount_Text;
    [SerializeField] private TMP_Text MetalCount_Text;
    [SerializeField] private TMP_Text WaveNumber_Text;
    [SerializeField] private TMP_Text WaveTime_Text;

    private string EnemyCount_Init;
    private string MetalCount_Init;
    private string WaveNumber_Init;
    private string WaveTime_Init;

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
        WaveNumber_Init = WaveNumber_Text.text;
        WaveTime_Init = WaveTime_Text.text;

    }

    // Update is called once per frame
    void Update()
    {

        EU_Bar.Data = resourceManager.power;
        EnemyCount_Text.text = $"{EnemyCount_Init}{gameManager.enemyCount}";
        MetalCount_Text.text = $"{MetalCount_Init}{resourceManager.metal}";
        WaveNumber_Text.text = $"{gameManager.wave}{WaveNumber_Init}";
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameManager.waveTimeLeft);
        WaveTime_Text.text = $"{timeSpan.Minutes.ToString("00")}:{timeSpan.Seconds.ToString("00")}{WaveTime_Init}";

    }
}
