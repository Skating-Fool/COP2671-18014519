using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    public Track track;
    public int startPointOverride = 0;
    public float delaySeconds = 1;
    public int limit = 1;
    public bool run = true;

    private bool canStart = false;

    private Coroutine spawningCoroutine;

    void Start()
    {

        spawningCoroutine = StartCoroutine(nameof(SpawnObject));

    }

    void Update()
    {
        if (canStart && run)
        {
            spawningCoroutine = StartCoroutine(nameof(SpawnObject));
            canStart = false;
        }
    }

    private IEnumerator SpawnObject()
    {

        int count = 0;
        while (run)
        {
            if (count >= limit)
            {
                break;
            }
            else
            {
                GameObject newObject = Instantiate(prefab, transform.position, transform.rotation, transform);
                TrackTrain newTrain = newObject.GetComponent<TrackTrain>();
                newTrain.Track = track;
                newTrain.targetIndex = startPointOverride;
                count++;
                yield return new WaitForSeconds(delaySeconds);
            }
        }

        run = false;
        canStart = true;
        yield break; // Exit

    }

}
