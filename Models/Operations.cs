namespace CalculusApp.Models;
public static class Operations
{
    public static readonly Operation Derive = new Operation { Name = "Derive"};
    public static readonly Operation Integrate = new Operation { Name = "Integrate" };
    public static readonly Operation Factor = new Operation { Name = "Factor" };
    public static readonly Operation Simplify = new Operation { Name = "Simplify" };
    public static readonly Operation FindTangent = new Operation { Name = "Find Tangent" };
    public static readonly Operation AreaUnderCurve = new Operation { Name = "Area Under Curve" };
   

    public static IEnumerable<Operation> GetAllOperations()
    {
        return new List<Operation>
        {
            Derive,
            Integrate,
            Factor,
            Simplify,
            FindTangent,
            AreaUnderCurve,
        };
    }
}
