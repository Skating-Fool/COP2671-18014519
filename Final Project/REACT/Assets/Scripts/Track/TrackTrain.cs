using UnityEngine;

public class TrackTrain : MonoBehaviour
{

    public Track _track;
    public float speed = 5;

    private int _targetIndex = 0;
    private Vector3 targetPos;

    public TrackSpawner creator;

    public int TargetIndex
    {
        get { return _targetIndex; }
        set
        {
            if (_track)
            {
                if (value < Track.points.Count && value >= 0)
                {
                    _targetIndex = value;
                }
                else
                {
                    Debug.LogWarning($"Track Point Index Out Of Range ({value})");
                }
            }
            else
            {
                Debug.LogWarning($"This TrackTrain is not on a track, therefore can't go to ({value}) This Point will be clamped");
                _targetIndex = value;
            }

        }
    }

    public Track Track
    {
        get
        {
            return _track;
        }
        set
        {
            _track = value;
            if (_targetIndex >= _track.points.Count)
            {
                _targetIndex = _track.points.Count - 1;
            }
            else if (_targetIndex < 0)
            {
                _targetIndex = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Track)
        {
            targetPos = Track.points[TargetIndex].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Track)
        {
            transform.LookAt(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                if (TargetIndex < Track.points.Count)
                {
                    GameObject nextPoint = Track.GetNextPoint(TargetIndex);
                    TrackPointData trackPointData = nextPoint.GetComponent<TrackPointData>();

                    Track = trackPointData.trackController;
                    TargetIndex = trackPointData.ID;
                    targetPos = Track.points[TargetIndex].position;
                }
            }
        }
    }
}
