using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Power/exponentiation operation (x^y).
/// Example: pow 2 8 → 2^8 = 256
/// </summary>
public sealed class Pow : IOperation
{
    public string Name => "pow";
    public string Help => "pow x y - Calculates x raised to the power of y (x^y)";
    
    /// <summary>
    /// Calculates x raised to the power of y.
    /// Uses Math.Pow() from the .NET Math library.
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length != 2)
            throw new ArgumentException("Power requires exactly 2 arguments (base and exponent)");
        
        return Math.Pow(args[0], args[1]);
    }
}
