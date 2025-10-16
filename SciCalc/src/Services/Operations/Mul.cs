using System;
using System.Linq;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Multiplication operation - multiplies all provided numbers.
/// Example: mul 2 3 4 → 2 * 3 * 4 = 24
/// </summary>
public sealed class Mul : IOperation
{
    public string Name => "mul";
    public string Help => "mul x y [z ...] - Multiplies all numbers together";
    
    /// <summary>
    /// Multiplies all numbers together.
    /// Starts with 1.0 (identity element for multiplication).
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length == 0) return 0;
        
        // Aggregate with initial value 1.0, multiply each element
        return args.Aggregate(1.0, (accumulator, x) => accumulator * x);
    }
}
