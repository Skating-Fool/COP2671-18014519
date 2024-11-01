using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 100;
    public float maxHealth = 100;

    public Gradient healthGradient = new Gradient();
    public Color healthColor;

    private Renderer meshRenderer;

    public float cycleSpeed = 0.01f;
    private float i = 0;

    void Start()
    {
        SelectionManager.OnSelect.AddListener(OnSelectEvent);
        meshRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        i += cycleSpeed;
        health = (Mathf.Sin(i) * (maxHealth/2)) + (maxHealth / 2);
        //healthColor.r = 255 - (health / maxHealth * 255);
        //healthColor.g = health / maxHealth * 255;

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
