namespace CalculusApp.Models;

public class CalculusTaskFactory
{
    public ICalculusTask CreateCalculusTask(Operation operation, string parameters)
    {
        return operation.Name.ToLower() switch
        {
            "derive" => new DeriveTask(parameters),
            "integrate" => new IntegrateTask(parameters),
            "factor" => new FactorTask(parameters),
            "simplify" => new SimplifyTask(parameters),
            "find tangent" => new FindTangentTask(parameters),
            "area under curve" => new AreaUnderCurveTask(parameters),
            _ => throw new ArgumentException($"Unknown operation: {operation.Name}")
        };
    }
}
