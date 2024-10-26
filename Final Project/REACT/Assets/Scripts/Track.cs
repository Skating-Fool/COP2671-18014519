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

    public void UpdatePoints()
    {

        
        List<Vector3> pointPositions = new List<Vector3>();

        foreach (Transform t in points)
        {
            if (t != null)
            {
                pointPositions.Add(t.position);
            }
        }
        debugLineRenderer.positionCount = pointPositions.Count;
        debugLineRenderer.SetPositions(pointPositions.ToArray());

    }

    public void CreateNewPoint()
    {

        GameObject newPoint;

        if (points.Count >= 1)
        {

            newPoint = Instantiate(pointPrefab, points[points.Count - 1].position, points[points.Count - 1].rotation, transform);

            Transform nextPos = newPoint.transform;

            if (points.Count == 1)
            {
                newPoint.transform.position += newPoint.transform.forward;
            }
            else if (points.Count >= 2)
            {
                newPoint.transform.position = points[points.Count - 2].position;
                newPoint.transform.LookAt(points[points.Count - 1].position);
                newPoint.transform.position = points[points.Count - 1].position;
                //newPoint.transform.Rotate(new Vector3(0, newPoint.transform.rotation.y + 180, 0));
                //newPoint.transform.rotation = Quaternion.Inverse(newPoint.transform.rotation);
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

    }

    public void RemoveLast()
    {
        points.RemoveAt(points.Count - 1);
    }

    public void RemoveFirst()
    {
        points.RemoveAt(0);
    }

    public void DeleteLast()
    {
        DestroyImmediate(points[points.Count - 1].gameObject);
        points.RemoveAt(points.Count - 1);
    }

    public void DeleteFirst()
    {
        DestroyImmediate(points[0].gameObject);
        points.RemoveAt(0);
    }

}
