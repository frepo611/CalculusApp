namespace CalculusApp.Models;

public readonly struct SimplifyTask : ICalculusTask
{
    public string Operation { get; } = "simplify";

    public string Parameters { get; init; }
    public SimplifyTask(string parameters)
    {
        Parameters = parameters;
    }
}
