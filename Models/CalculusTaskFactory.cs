namespace CalculusApp.Models;

public class CalculusTaskFactory
{
    public ICalculusTask CreateCalculusTask(Operation operation, string expression, string? firstExtraParameter, string? secondExtraParameter)
    {
        return operation.Name.ToLower() switch
        {
            "derive" => new DeriveTask(expression),
            "integrate" => new IntegrateTask(expression),
            "factor" => new FactorTask(expression),
            "simplify" => new SimplifyTask(expression),
            "find tangent" => new FindTangentTask($"{firstExtraParameter}|{expression}"),
            "area under curve" => new AreaUnderCurveTask($"{firstExtraParameter}:{secondExtraParameter}|{expression}"),
            _ => throw new ArgumentException($"Unknown operation: {operation.Name}")
        };
    }
}
