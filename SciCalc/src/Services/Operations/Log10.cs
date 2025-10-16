using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Base-10 logarithm operation.
/// Example: log10 1000 → 3 (because 10^3 = 1000)
/// Demonstrates: Domain validation for logarithms
/// </summary>
public sealed class Log10 : IOperation
{
    public string Name => "log10";
    public string Help => "log10 x - Calculates base-10 logarithm of x";
    
    /// <summary>
    /// Calculates base-10 logarithm.
    /// Only defined for positive numbers (domain: x > 0).
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length != 1)
            throw new ArgumentException("Log10 requires exactly 1 argument");
        
        if (args[0] <= 0)
            throw new ArgumentException("Logarithm only defined for positive numbers");
        
        return Math.Log10(args[0]);
    }
}
