using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Modulo operation - calculates remainder after division.
/// Example: mod 7 3 → 1 (because 7 ÷ 3 = 2 remainder 1)
/// </summary>
public sealed class Mod : IOperation
{
    public string Name => "mod";
    public string Help => "mod a b - Calculates remainder when a is divided by b (a % b)";
    
    /// <summary>
    /// Calculates modulo using the % operator.
    /// Useful for checking divisibility, cycling values, etc.
    /// </summary>
    public double Evaluate(params double[] args)
    {
        if (args.Length != 2)
            throw new ArgumentException("Modulo requires exactly 2 arguments");
        
        return args[0] % args[1];
    }
}
