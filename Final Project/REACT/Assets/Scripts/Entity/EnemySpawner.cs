using UnityEngine;

public class EnemySpawner : TrackTrainSpawner
{

    public int enableOnWave = 0;

    private enum LogicalMode
    {
        True,
        False,
        GreaterThan,
        GreaterThanOrEqual,
        Equal,
        LessThanOrEqual,
        LessThan,
        Modulus
    }

    [SerializeField] private LogicalMode logicalMode;

    // More for debug use, I don't enjoy spamming the console with debug prints
    public string debugLogicalOutput = "";

    private GameManager gameManager;

    public override void Start()
    {
        base.Start();
        gameManager = gameManager != null ? gameManager : FindAnyObjectByType<GameManager>();
        gameManager.OnWaveStart.AddListener(EventOnStart);
        gameManager.OnWaveComplete.AddListener(StopSpawning);
        gameManager.OnWaveFail.AddListener(StopSpawning);
    }

    public override void Update()
    {
        base.Update();
    }

    private void EventOnStart()
    {
        Compute();
    }

    private void StopSpawning()
    {
        run = false;
    }

    private void Compute()
    {

        bool output = false;
        if (logicalMode == LogicalMode.True)
        {
            output = true;
        }
        else if (logicalMode == LogicalMode.False)
        {
            output = false;
        }
        else if (logicalMode == LogicalMode.GreaterThan)
        {
            output = (gameManager.wave > enableOnWave);
            debugLogicalOutput = $"{gameManager.wave} > {enableOnWave} = {output}";
        }
        else if (logicalMode == LogicalMode.GreaterThanOrEqual)
        {
            output = (gameManager.wave >= enableOnWave);
            debugLogicalOutput = $"{gameManager.wave} >= {enableOnWave} = {output}";
        }
        else if (logicalMode == LogicalMode.Equal)
        {
            output = (gameManager.wave == enableOnWave);
            debugLogicalOutput = $"{gameManager.wave} == {enableOnWave} = {output}";
        }
        else if (logicalMode == LogicalMode.LessThanOrEqual)
        {
            output = (gameManager.wave <= enableOnWave);
            debugLogicalOutput = $"{gameManager.wave} <= {enableOnWave} = {output}";
        }
        else if (logicalMode == LogicalMode.LessThan)
        {
            output = (gameManager.wave < enableOnWave);
            debugLogicalOutput = $"{gameManager.wave} < {enableOnWave} = {output}";
        }
        else if (logicalMode == LogicalMode.Modulus)
        {
            output = (gameManager.wave % enableOnWave) == 0 && gameManager.wave != 0;
            debugLogicalOutput = $"{gameManager.wave} % {enableOnWave} = " +
                $"{(gameManager.wave % enableOnWave)} == 0 && wave != 0 = {output}";
        }

        run = output;

    }

}
