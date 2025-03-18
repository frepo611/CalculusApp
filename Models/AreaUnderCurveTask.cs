namespace CalculusApp.Models;

public readonly struct AreaUnderCurveTask : ICalculusTask
{
    public string Operation { get; } = "area";

    public string Parameters { get; init; }

    public AreaUnderCurveTask(string parameters)
    {
        Parameters = parameters;
    }
}
