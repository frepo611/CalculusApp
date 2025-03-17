namespace CalculusApp.Models
{
    public class ExpressionHistory
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? OperationName { get; set; }
        public string? Expression { get; set; }
        public string? Solution { get; set; }
    }
}