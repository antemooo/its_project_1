using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Square root operation.
/// Example: sqrt 9 → 3
/// Demonstrates: Domain validation (no negative numbers)
/// </summary>
public sealed class Sqrt : IOperation
{
    public string Name => "sqrt";
    public string Help => "sqrt x - Calculates square root of x";
    
    /// <summary>
    /// Calculates square root with validation.
    /// Square root is only defined for non-negative numbers.
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length != 1)
            throw new ArgumentException("Square root requires exactly 1 argument");
        
        if (args[0] < 0)
            throw new ArgumentException("Cannot take square root of negative number");
        
        return Math.Sqrt(args[0]);
    }
}
