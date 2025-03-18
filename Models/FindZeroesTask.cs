namespace CalculusApp.Models;

public readonly struct FindTangentTask : ICalculusTask
{
    public string Operation { get; } = "tangent";

    public string Parameters { get; init; }

    public FindTangentTask(string parameters)
    {
        Parameters = parameters;
    }
}
