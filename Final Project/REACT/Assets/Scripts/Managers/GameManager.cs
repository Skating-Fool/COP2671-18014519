using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public bool waveActive;
    public int wave = 1;
    public float initalWaveTime = 30.0f;
    public float timeAddEachWave = 15.0f;
    public float waveTime;
    public float waveTimeLeft;
    public float waveScale;

    public string playerTeam;

    public int keyWaveInterval = 5;
    public int startingDifficulty;
    public int difficulty;

    [SerializeField] private float defaultTimeScale = 1.0f;

    public TrackSpawner[] spawners;

    public int enemyCount;

    public List<Entity> enemies = new();

    public UnityEvent OnWaveFail;
    public UnityEvent OnWaveComplete;

    // I like the idea of multiple resourceManagers, one for each team,
    // -but i'm making this a backlog thing for now.
    //public List<ResourceManager> resourceManagers;
    public ResourceManager resourceManager;

    void Start()
    {

        OnWaveComplete ??= new UnityEvent();
        OnWaveFail ??= new UnityEvent();

        Time.timeScale = defaultTimeScale;

        //resourceManagers = new List<ResourceManager>(FindObjectsOfType<ResourceManager>());
        if (resourceManager == null)
        {
            resourceManager = FindObjectOfType<ResourceManager>();
        }

        waveTime = initalWaveTime + (wave * timeAddEachWave);
        waveTimeLeft = waveTime;

    }

    void Update()
    {


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

        if (waveActive)
        {

            if (waveTimeLeft > 0)
            {
                waveTimeLeft -= Time.deltaTime;
            }
            else
            {

                waveTimeLeft = 0;

                foreach (TrackSpawner spawner in spawners)
                {
                    spawner.run = false;
                }

            }

            if (enemyCount == 0)
            {
                WaveComplete();
            }

        }

    }

    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }

    public void UnPauseTime()
    {
        Time.timeScale = defaultTimeScale;
    }

    public void WaveFail()
    {

        OnWaveFail.Invoke();
        waveActive = false;

        Debug.Log("Wave Failed");

        foreach (TrackSpawner spawner in spawners)
        {
            spawner.run = false;
        }

    }

    public void WaveComplete()
    {

        OnWaveComplete.Invoke();
        waveActive = false;
        wave++;
        waveTime = initalWaveTime + (wave * timeAddEachWave);
        waveTimeLeft = waveTime;

        Debug.Log("Wave Complete");

    }

    public void StartWave()
    {

        waveActive = true;

        foreach (TrackSpawner spawner in spawners)
        {
            spawner.run = true;
        }

    }

}