using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Factorial operation - multiplies all positive integers up to n.
/// Example: fact 5 → 5! = 5 × 4 × 3 × 2 × 1 = 120
/// Used in combinatorics, probability, and series calculations.
/// </summary>
public sealed class Fact : IOperation
{
    public string Name => "fact";
    public string Help => "fact n - Calculates factorial (n!) for non-negative integers";
    
    /// <summary>
    /// Calculates factorial with strict validation.
    /// Requirements:
    /// - n must be non-negative (factorial of negative numbers is undefined)
    /// - n must be a whole number (no decimals)
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length != 1)
            throw new ArgumentException("Factorial requires exactly 1 argument");
        
        var n = args[0];
        
        // Check if n is a non-negative integer
        if (n < 0)
            throw new ArgumentException("Factorial is only defined for non-negative numbers");
        
        if (n != Math.Truncate(n))
            throw new ArgumentException("Factorial requires an integer (no decimal part)");
        
        // Calculate factorial using a loop
        long result = 1;
        for (int i = 1; i <= (int)n; i++)
        {
            result *= i;
        }
        
        return result;
    }
}
