using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Reactor : Entity
{

    public float EUPerTick = 5;
    public float DelayBetweenTicks = 1f;

    public bool running = true;

    public Gradient healthGradient = new Gradient();
    public Color healthColor;

    [SerializeField] private Renderer meshRenderer;

    private bool canGenerate = true;

    public override void Start()
    {

        base.Start();

        meshRenderer = meshRenderer != null ? meshRenderer : GetComponent<Renderer>();

    }

    void Update()
    {
        if (canGenerate)
        {
            StartCoroutine(nameof(GeneratePower));
        }

        healthColor = healthGradient.Evaluate(health / maxHealth);

        meshRenderer.material.SetColor("_Color", healthColor);

    }

    private IEnumerator GeneratePower()
    {
        canGenerate = false;
        if (resourceManager.power < resourceManager.powerCapacity && running)
        {
            resourceManager.power += EUPerTick;
            if (resourceManager.power > resourceManager.powerCapacity)
            {
                resourceManager.power = resourceManager.powerCapacity;
            }
            else if (resourceManager.power < 0)
            {
                // While the normal use of this is to increase power,
                // -if the EUPerTick is negative, for some reason that I haven't thought of yet,
                //  -lets just prevent it from putting us in debt.
                resourceManager.power = 0;
            }
        }
        yield return new WaitForSeconds(DelayBetweenTicks);
        canGenerate = true;
    }

}
