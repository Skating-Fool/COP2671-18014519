using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Reactor : MachineBase
{

    public int EUPerTick = 5;
    public float DelayBetweenTicks = 1f;

    public bool running = true;

    public override void Start()
    {

        base.Start();

        InvokeRepeating(nameof(GeneratePower), 0f, DelayBetweenTicks);
        
    }

    void Update()
    {


    }

    private void GeneratePower()
    {
        
        if (powerLevel < 1000 && running)
        {
            powerLevel += EUPerTick;
            if (powerLevel > 1000)
            {
                powerLevel = maxPowerLevel;
            }
        }

    }

}
