namespace CalculusApp.Models;

public readonly struct FactorTask : ICalculusTask
{
    public string Operation { get; } = "factor";

    public string Parameters { get; init; }

    public FactorTask(string parameters)
    {
        Parameters = parameters;
    }
}
