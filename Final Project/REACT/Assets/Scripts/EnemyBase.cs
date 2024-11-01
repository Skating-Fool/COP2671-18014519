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

    void Start()
    {
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
