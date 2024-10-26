using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Track track;
    public float speed = 5;
    public float health = 100;
    public float maxHealth = 100;

    public int targetIndex = 0;
    public Vector3 targetPos;

    public Color healthColor;

    private Renderer meshRenderer;

    public float cycleSpeed = 0.01f;
    private float i = 0;

    void Start()
    {
        SelectionManager.OnSelect.AddListener(OnSelectEvent);
        meshRenderer = GetComponent<Renderer>();
        targetPos = track.points[targetIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {

            if (targetIndex < track.points.Count)
            {
                GameObject nextPoint = track.getNextPoint(targetIndex);
                TrackPointData trackPointData = nextPoint.GetComponent<TrackPointData>();
                
                track = trackPointData.trackController;
                targetIndex = trackPointData.ID;
                targetPos = track.points[targetIndex].position;
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
        i += cycleSpeed;
        health = (Mathf.Sin(i) * (maxHealth/2)) + (maxHealth / 2);
        healthColor.r = 255 - (health / maxHealth * 255);
        healthColor.g = health / maxHealth * 255;
        meshRenderer.material.SetColor("_Color", healthColor);

    }

    void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {
            Debug.Log("Click Event Not Implemented Yet");
        }
    }

}
