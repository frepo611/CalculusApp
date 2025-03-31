using CalculusApp.Models;

namespace CalculusApp.Services;

public static class TaskGeneratorFactory
{
    public static ICalculusTaskGenerator CreateTaskGenerator(Operation selectedOperation)
    {
        return selectedOperation.Name.ToLower() switch
        {
            "simplify" => new SimplifyTaskGenerator(),
            "factor" => new FactorTaskGenerator(),
            "derive" => new DeriveTaskGenerator(),
            "integrate" => new IntegrateTaskGenerator(),
            "tangent" => new TangentTaskGenerator(),
            "area" => new AreaUnderCurveTaskGenerator(),
            _ => throw new ArgumentException("Invalid operation", nameof(selectedOperation))
        };
    }

    private class DeriveTaskGenerator : ICalculusTaskGenerator
    {
        public CalculusTask GenerateTask(string expression, string firstParameter, string secondParameter)
        {
            return new CalculusTask
            {
                Operation = "derive",
                Parameters = $"{expression}"
            };
        }
    }

    private class SimplifyTaskGenerator : ICalculusTaskGenerator
    {
        public CalculusTask GenerateTask(string expression, string firstParameter, string secondParameter)
        {
            return new CalculusTask
            {
                Operation = "simplify",
                Parameters = $"{expression}"
            };
        }
    }

    private class FactorTaskGenerator : ICalculusTaskGenerator
    {
        public CalculusTask GenerateTask(string expression, string firstParameter, string secondParameter)
        {
            return new CalculusTask
            {
                Operation = "factor",
                Parameters = $"{expression}"
            };
        }
    }

    private class IntegrateTaskGenerator : ICalculusTaskGenerator

    {
        public CalculusTask GenerateTask(string expression, string firstParameter, string secondParameter)
        {
            return new CalculusTask
            {
                Operation = "integrate",
                Parameters = $"{expression}"
            };
        }
    }
    private class TangentTaskGenerator : ICalculusTaskGenerator

    {
        public CalculusTask GenerateTask(string expression, string firstParameter, string secondParameter)
        {
            return new CalculusTask
            {
                Operation = "tangent",
                Parameters = $"{firstParameter}|{expression}"
            };
        }
    }
    private class AreaUnderCurveTaskGenerator : ICalculusTaskGenerator

    {
        public CalculusTask GenerateTask(string expression, string firstParameter, string secondParameter)
        {
            return new CalculusTask
            {
                Operation = "area",
                Parameters = $"{firstParameter}:{secondParameter}|{expression}"
            };
        }
    }
}