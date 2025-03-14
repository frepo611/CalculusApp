namespace CalculusApp;
public static class Operations
{
    public static readonly Operation Derive = new Operation { Name = "Derive", Endpoint = "derive" };
    public static readonly Operation Integrate = new Operation { Name = "Integrate", Endpoint = "integrate" };
    public static readonly Operation Factor = new Operation { Name = "Factor", Endpoint = "factor" };
    public static readonly Operation Simplify = new Operation { Name = "Simplify", Endpoint = "simplify" };
    //public static readonly Operation FindZeros = new Operation { Name = "Find 0's", Endpoint = "zeroes" };
    public static readonly Operation FindTangent = new Operation { Name = "Find Tangent", Endpoint = "tangent" };
    public static readonly Operation AreaUnderCurve = new Operation { Name = "Area Under Curve", Endpoint = "area" };
    //public static readonly Operation Cosine = new Operation { Name = "Cosine", Endpoint = "cos" };
    //public static readonly Operation Sine = new Operation { Name = "Sine", Endpoint = "sin" };
    //public static readonly Operation Tangent = new Operation { Name = "Tangent", Endpoint = "tan" };
    //public static readonly Operation InverseCosine = new Operation { Name = "Inverse Cosine", Endpoint = "arccos" };
    //public static readonly Operation InverseSine = new Operation { Name = "Inverse Sine", Endpoint = "arcsin" };
    //public static readonly Operation InverseTangent = new Operation { Name = "Inverse Tangent", Endpoint = "arctan" };
    //public static readonly Operation AbsoluteValue = new Operation { Name = "Absolute Value", Endpoint = "abs" };
    //public static readonly Operation Logarithm = new Operation { Name = "Logarithm", Endpoint = "log" };

    public static IEnumerable<Operation> GetAllOperations()
    {
        return new List<Operation>
        {
            Derive,
            Integrate,
            Factor,
            Simplify,
            //FindZeros,
            FindTangent,
            AreaUnderCurve,
            //Cosine,
            //Sine,
            //Tangent,
            //InverseCosine,
            //InverseSine,
            //InverseTangent,
            //AbsoluteValue,
            //Logarithm
        };
    }
}
