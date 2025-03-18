namespace CalculusApp.Models
{
    public interface ICalculusTask
    {
        string Operation { get; }
        string Parameters { get; }
    }
}