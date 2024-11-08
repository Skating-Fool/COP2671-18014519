using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool waveActive;
    public int wave = 1;
    public float waveTime;
    public float waveScale;

    public int keyWaveInterval = 5;
    public int startingDifficulty;
    public int difficulty;

    public TrackSpawner[] spawners;

    private int enemyCount;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (!waveActive)
        {
            StartWave();
        }
    }

    public void StartWave(int? waveOverride = null)
    {

        if (waveOverride != null)
        {
            wave = waveOverride.Value;
        }
        else
        {
            wave++;
        }

        waveActive = true;

    }

}
