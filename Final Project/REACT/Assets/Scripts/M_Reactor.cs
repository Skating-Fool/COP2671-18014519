using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Reactor : MachineBase
{
    // TODO: Add Health
    // TODO: Make Power Manager
    public int EUPerTick = 5;
    public float DelayBetweenTicks = 1f;

    public bool running = true;

    public override void Start()
    {

        base.Start();

        // TODO: Replace with coroutine
        InvokeRepeating(nameof(GeneratePower), 0f, DelayBetweenTicks);
        
    }

    void Update()
    {


    }

    private void GeneratePower()
    {
        
        if (power < 1000 && running)
        {
            power += EUPerTick;
            if (power > 1000)
            {
                power = maxPower;
            }
        }

    }

}
