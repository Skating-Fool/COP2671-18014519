using UnityEngine;

public class Machine_Power_Multiplier : Entity
{

    [SerializeField] private float powerMultiplyAmount = 2.0f;

    private Machine_Reactor reactor;

    public override void Start()
    {

        base.Start();
        reactor = FindAnyObjectByType<Machine_Reactor>();
        if (reactor != null)
        {
            reactor.EUPerTick = reactor.EUPerTick * powerMultiplyAmount;
        }
        // :3

    }

    public override string SelectionData
    {
        get
        {
            string text =
                $"<color=red>Health: <color=white>{health} / {maxHealth}\n" +
                $"<color=#FFE600>Multiply Amount: <color=white>{powerMultiplyAmount}\n";
            return externalSelectionText + text;
        }
    }

}
