using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTrainSpawner : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    public Track track;
    public int startPointOverride = 0;
    public float delaySeconds = 1;
    public int limit = 1;
    public bool run = true;

    public List<TrackTrain> trainList = new();

    private protected bool canStart = false;

    private Coroutine spawningCoroutine;

    public virtual void Start()
    {
        spawningCoroutine = StartCoroutine(nameof(SpawnObject));
    }

    public virtual void Update()
    {
        if (canStart && run)
        {
            spawningCoroutine = StartCoroutine(nameof(SpawnObject));
            canStart = false;
        }
    }

    public virtual void RemoveFromListOnDeath(Entity deadEnt)
    {
        TrackTrain train = deadEnt.GetComponentInChildren<TrackTrain>();
        if (train != null)
        {
            trainList.Remove(train);
        }
        else
        {
            // Shouldn't happen
            Debug.LogWarning($"Can't remove null train from list");
        }
    }

    public virtual IEnumerator SpawnObject()
    {

        int count = 0;
        while (run)
        {
            if (count >= limit && limit > 0)
            {
                break;
            }
            else
            {
                GameObject newObject = Instantiate(prefab, transform.position, transform.rotation, transform);
                TrackTrain newTrain = newObject.GetComponent<TrackTrain>();
                Entity newEntity = newObject.GetComponentInChildren<Entity>();
                newTrain.creator = this;
                if (newEntity != null)
                {
                    newEntity.OnDeath.AddListener(RemoveFromListOnDeath);
                }
                newTrain.Track = track;
                newTrain.TargetIndex = startPointOverride;
                count++;
                trainList.Add(newTrain);

                yield return new WaitForSeconds(delaySeconds);
            }
        }

        run = false;
        canStart = true;

    }

}
