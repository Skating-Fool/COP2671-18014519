using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool waveActive;
    public int wave = 1;
    public float waveTime;
    public float waveScale;

    public string playerTeam;

    public int keyWaveInterval = 5;
    public int startingDifficulty;
    public int difficulty;

    public TrackSpawner[] spawners;

    public int enemyCount;

    public List<Entity> enemies = new();

    // I like the idea of multiple resourceManagers, one for each team,
    // -but i'm making this a backlog thing for now.
    //public List<ResourceManager> resourceManagers;
    public ResourceManager resourceManager;

    void Start()
    {
        //resourceManagers = new List<ResourceManager>(FindObjectsOfType<ResourceManager>());
        if (resourceManager == null)
        {
            resourceManager = FindObjectOfType<ResourceManager>();
        }
    }
    
    void Update()
    {

        if (!waveActive)
        {
            StartWave();
        }

        enemies.Clear();
        foreach (TrackSpawner spawner in spawners)
        {

            foreach (TrackTrain train in spawner.trainList)
            {

                Entity entity = train.GetComponentInChildren<Entity>();
                if (entity.team != playerTeam)
                {
                    enemies.Add(entity);
                }

            }

        }

        enemyCount = enemies.Count;

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
