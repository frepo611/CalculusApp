namespace CalculusApp.Models;

public readonly struct IntegrateTask : ICalculusTask
{
    public string Operation { get; } = "integrate";
    public string Parameters { get; init; }

    public IntegrateTask(string parameters)
    {
        Parameters = parameters;
    }
}
