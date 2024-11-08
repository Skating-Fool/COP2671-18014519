using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : Entity
{

    public float Speed
    {
        get
        {
            if (trackTrain != null)
            {
                return trackTrain.speed;
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (trackTrain != null)
            {
                trackTrain.speed = value;
            }
        }

    }

    public Gradient healthGradient = new Gradient();
    public Color healthColor;

    private Renderer meshRenderer;
    private TrackTrain trackTrain;

    void Start()
    {
        trackTrain = GetComponent<TrackTrain>();
        SelectionManager.OnSelect.AddListener(OnSelectEvent);
        meshRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        healthColor = healthGradient.Evaluate(health / maxHealth);

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
