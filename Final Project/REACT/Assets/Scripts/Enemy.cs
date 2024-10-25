using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Path path;
    public float speed = 5;
    public float health = 100;

    public int targetIndex = 0;
    public Vector3 targetPos;

    void Start()
    {
        targetPos = path.points[targetIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            Debug.Log(targetIndex);
            if (path.loop)
            {
                targetIndex += 1;
                targetIndex = targetIndex % path.points.Length;
                targetPos = path.points[targetIndex].position;
            }
            else if(targetIndex < path.points.Length - 1)
            {
                targetIndex += 1;
                targetPos = path.points[targetIndex].position;
            }

        }

    }

}
