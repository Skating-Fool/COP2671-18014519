using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class Track : MonoBehaviour
{

    private LineRenderer debugLineRenderer;

    public GameObject pointPrefab;

    public bool showTrackLine = false;

    public bool loop = false;

    public List<Transform> points;

    void Start()
    {
        debugLineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {

        if (Application.isEditor && !Application.isPlaying)
        {
            // Runs Only In Editor


        }
        else if (Application.isPlaying)
        {
            // Runs Only In Playmode

        }

        // Runs in Both
        debugLineRenderer.enabled = showTrackLine;
        debugLineRenderer.loop = loop;

        if (showTrackLine)
        {
            UpdatePoints();
        }

    }

    public GameObject GetNextPoint(int ID)
    {
        if ((ID + 1) < points.Count)
        {
            return points[ID + 1].gameObject;
        }
        else if (loop)
        {
            return points[0].gameObject;
        }

        return points[ID].gameObject;
        
    }

    public void UpdatePoints()
    {

        List<Vector3> pointPositions = new List<Vector3>();
        int id = 0;
        foreach (Transform t in points)
        {

            if (t != null)
            {
                TrackPointData trackPointData = t.GetComponent<TrackPointData>();

                if (trackPointData == null)
                {
                    t.AddComponent<TrackPointData>();
                }

                if (trackPointData.trackController == null)
                {
                    trackPointData.trackController = this;
                }

                if (trackPointData.trackController.gameObject.Equals(gameObject))
                {
                    trackPointData.ID = id;
                    id++;
                }

                pointPositions.Add(t.position);
                
            }

        }

        if (showTrackLine)
        {
            debugLineRenderer.positionCount = pointPositions.Count;
            debugLineRenderer.SetPositions(pointPositions.ToArray());
        }

    }

    public GameObject CreateNewPoint()
    {

        GameObject newPoint;

        if (points.Count >= 1)
        {

            newPoint = Instantiate(pointPrefab, points[points.Count - 1].position, points[points.Count - 1].rotation, transform);

            //Transform nextPos = newPoint.transform;

            if (points.Count == 1)
            {
                newPoint.transform.position += newPoint.transform.forward;
            }
            else if (points.Count >= 2)
            {
                newPoint.transform.position = points[points.Count - 2].position;
                newPoint.transform.LookAt(points[points.Count - 1].position);
                newPoint.transform.position = points[points.Count - 1].position;
                newPoint.transform.position += newPoint.transform.forward;
            }

        }
        else
        {
            newPoint = Instantiate(pointPrefab, transform);
        }

        // Update Point List
        newPoint.name = $"Point {points.Count}";
        points.Add(newPoint.transform);

        UpdatePoints();
        return newPoint;
    }

    public void RemoveLast()
    {
        DestroyImmediate(points[^1].gameObject.GetComponent<TrackPointData>());
        points.RemoveAt(points.Count - 1);
        UpdatePoints();
    }

    public void RemoveFirst()
    {
        DestroyImmediate(points[0].gameObject.GetComponent<TrackPointData>());
        points.RemoveAt(0);
        UpdatePoints();
    }

    public void DeleteLast()
    {
        DestroyImmediate(points[^1].gameObject);
        points.RemoveAt(points.Count - 1);
        UpdatePoints();
    }

    public void DeleteFirst()
    {
        DestroyImmediate(points[0].gameObject);
        points.RemoveAt(0);
        UpdatePoints();
    }

    public GameObject GetFirst()
    {
        return points[0].gameObject;
    }

    public GameObject GetLast()
    {
        return points[^1].gameObject;
    }

}
