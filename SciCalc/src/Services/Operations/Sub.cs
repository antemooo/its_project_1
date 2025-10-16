using System;
using System.Linq;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Subtraction operation - subtracts all numbers from the first one.
/// Example: sub 10 2 3 → 10 - 2 - 3 = 5
/// </summary>
public sealed class Sub : IOperation
{
    public string Name => "sub";
    public string Help => "sub x y [z ...] - Subtracts all numbers from the first (x - y - z ...)";
    
    /// <summary>
    /// Subtracts all numbers after the first from the first number.
    /// Uses Aggregate for left-to-right operation: ((x - y) - z) - ...
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length == 0) return 0;
        
        // Skip(1) skips the first element, Aggregate applies subtraction left-to-right
        return args.Skip(1).Aggregate(args[0], (accumulator, x) => accumulator - x);
    }
}
