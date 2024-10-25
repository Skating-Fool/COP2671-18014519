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

    public Transform[] points;

    void Start()
    {
        debugLineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {

        if (Application.isEditor && !Application.isPlaying)
        {
            // Runs In Editor

            debugLineRenderer.enabled = debugLines;

        }
        else
        {
            // Runs In Playmode
        }

        // Runs in Both

        List<Vector3> pointPositions = new List<Vector3>();
        foreach (Transform t in points)
        {
            pointPositions.Add(t.position);
        }
        debugLineRenderer.SetPositions(pointPositions.ToArray());

    }

}
