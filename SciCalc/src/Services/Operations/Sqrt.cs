//==============================================================================
// FILE: Sqrt.cs
// ⭐ DIFFICULTY: Medium (domain validation)
// ⏱️ ESTIMATED TIME: 20 minutes
// 🧪 TEST CASES:
//   sqrt 16         → 4.0
//   sqrt 2          → 1.414...
//   sqrt 0          → 0.0
//   sqrt -1         → ERROR: negative number
//==============================================================================

using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Square root operation - calculates the square root of a number.
/// Example: sqrt 16 → 4
/// Domain: x ≥ 0
/// </summary>
public sealed class Sqrt : IOperation
{
    public string Name => "sqrt";
    public string Help => "sqrt x - Square root of x";
    
    /// <summary>
    /// STUDENT TODO: Implement square root with domain validation.
    /// 
    /// REQUIREMENTS:
    /// 1. Requires exactly 1 argument (throw ArgumentException)
    /// 2. Argument must be non-negative (throw ArgumentException)
    /// 3. Use Math.Sqrt() to calculate result
    /// 
    /// HINTS:
    /// - Check count: if (args.Length != 1) throw ...
    /// - Check domain: if (args[0] < 0) throw ...
    /// - Calculate: return Math.Sqrt(args[0])
    /// 
    /// MATH NOTE:
    /// Square root is only defined for non-negative real numbers.
    /// For negative numbers, result would be imaginary (not supported here).
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("STUDENT TODO: Implement sqrt with validation");
    }
}
