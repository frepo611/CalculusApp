namespace CalculusApp.Models;

public readonly struct DeriveTask : ICalculusTask
{
    public string Operation { get; } = "derive";

    public string Parameters { get; init; }

    public DeriveTask(string parameters)
    {
        Parameters = parameters;
    }
}
