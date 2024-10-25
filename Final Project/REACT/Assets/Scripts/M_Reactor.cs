using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Reactor : MachineBase 
{

    public int EUPerTick = 5;
    public float DelayBetweenTicks = 1;

    public bool running = true;

    void Start()
    {
        print(SelectionManager.OnSelect);

        SelectionManager.OnSelect.AddListener(OnSelectEvent);
        powerLevel = 1000;
        maxPowerLevel = 1000;
    }

    void Update()
    {

        if (running)
        {
            StartCoroutine(GeneratePower());
        }

    }

    public void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {
            Debug.Log("Click Event Not Implemented Yet");
        }
    }

    private IEnumerator GeneratePower()
    {
        
        if (powerLevel < 1000)
        {
            powerLevel += EUPerTick;
            if (powerLevel > 1000)
            {
                powerLevel = maxPowerLevel;
            }
            new WaitForSeconds(DelayBetweenTicks);
            yield return null;
        }
        else
        {
            new WaitForSeconds(DelayBetweenTicks);
        }
    }

}
