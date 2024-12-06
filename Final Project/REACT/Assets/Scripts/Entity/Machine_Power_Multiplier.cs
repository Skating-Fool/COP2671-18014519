using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Machine_Power_Multiplier : Entity
{

    [SerializeField] private float powerMultiplyAmount = 2.0f;

    private Machine_Reactor reactor;

    public override void Start()
    {

        base.Start();
        reactor = FindAnyObjectByType<Machine_Reactor>();
        Assert.IsNotNull(reactor);
        reactor.EUPerTick = reactor.EUPerTick * powerMultiplyAmount;
        // :3

    }

    public override void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {

        if (transform.gameObject.Equals(gameObject))
        {

            if (mouseClickNum == 1)
            {
                Debug.Log($"[{name}] Click Event Not Implemented Yet");
            }

        }

    }

}
