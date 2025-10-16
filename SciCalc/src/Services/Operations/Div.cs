using System;
using System.Linq;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Division operation - divides the first number by all subsequent numbers.
/// Example: div 100 2 5 → 100 / 2 / 5 = 10
/// Demonstrates: Error handling for division by zero
/// </summary>
public sealed class Div : IOperation
{
    public string Name => "div";
    public string Help => "div a b [c ...] - Divides a by b, then by c, etc. (a/b/c/...)";
    
    /// <summary>
    /// Performs sequential division with error checking.
    /// Throws DivideByZeroException if any divisor is zero.
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length < 2)
            throw new ArgumentException("Division requires at least 2 arguments");
        
        // Divide first number by each subsequent number
        return args.Skip(1).Aggregate(args[0], (accumulator, x) =>
        {
            if (x == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return accumulator / x;
        });
    }
}
