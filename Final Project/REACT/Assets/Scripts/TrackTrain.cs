using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class TrackTrain : MonoBehaviour
{

    public Track _track;
    public float speed = 5;

    private int _targetIndex = 0;
    private Vector3 targetPos;

    public int targetIndex
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Track)
        {
            targetPos = Track.points[targetIndex].position;
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

                if (targetIndex < Track.points.Count)
                {
                    GameObject nextPoint = Track.getNextPoint(targetIndex);
                    TrackPointData trackPointData = nextPoint.GetComponent<TrackPointData>();

                    Track = trackPointData.trackController;
                    targetIndex = trackPointData.ID;
                    targetPos = Track.points[targetIndex].position;
                }
                /*
                if (path.loop)
                {
                    targetIndex += 1;
                    TrackPointData trackPointData = path.points[targetIndex].GetComponent<TrackPointData>();
                    path = trackPointData.trackScript;
                    targetIndex = trackPointData.ID;
                    targetIndex = targetIndex % path.points.Count;
                    targetPos = path.points[targetIndex].position;
                }
                else if(targetIndex < path.points.Count - 1)
                {
                    targetIndex += 1;
                    TrackPointData trackPointData = path.points[targetIndex].GetComponent<TrackPointData>();
                    path = trackPointData.trackScript;
                    targetIndex = trackPointData.ID;
                    targetPos = path.points[targetIndex].position;
                }
                */
            }
        }
    }
}
