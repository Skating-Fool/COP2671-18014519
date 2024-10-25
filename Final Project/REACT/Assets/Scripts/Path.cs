using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class Path : MonoBehaviour
{

    private LineRenderer debugLineRenderer;

    public bool debugLines = false;

    public bool loop = false;

    public Transform[] points;

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
        debugLineRenderer.enabled = debugLines;
        debugLineRenderer.loop = loop;

        if (debugLines)
        {
            updatePoints();
        }

    }

    public void updatePoints()
    {

        debugLineRenderer.positionCount = points.Length;
        List<Vector3> pointPositions = new List<Vector3>();

        foreach (Transform t in points)
        {
            pointPositions.Add(t.position);
        }

        debugLineRenderer.SetPositions(pointPositions.ToArray());

    }

}
