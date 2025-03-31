using CalculusApp.Models;

namespace CalculusApp.Services
{
    public interface ICalculusTaskGenerator
    {
        CalculusTask GenerateTask(string expression, string firstParameter, string secondParameter);
    }
}