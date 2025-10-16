using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Natural logarithm operation (base e).
/// Example: ln 2.71828 → ≈1 (because e^1 ≈ 2.71828)
/// </summary>
public sealed class Ln : IOperation
{
    public string Name => "ln";
    public string Help => "ln x - Calculates natural logarithm of x (base e)";
    
    /// <summary>
    /// Calculates natural logarithm (base e).
    /// e ≈ 2.71828... is Euler's number, fundamental in calculus.
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length != 1)
            throw new ArgumentException("Natural log requires exactly 1 argument");
        
        if (args[0] <= 0)
            throw new ArgumentException("Logarithm only defined for positive numbers");
        
        return Math.Log(args[0]);
    }
}
