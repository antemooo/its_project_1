//==============================================================================
// FILE: Div.cs
// ⭐ DIFFICULTY: Medium | ⏱️ TIME: 20-30 minutes
// 🧪 TEST: div 10 2 → 5.0 | div 100 2 5 → 10.0 | div 10 0 → ERROR
//==============================================================================

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
    /// STUDENT TODO: Implement division with validation.
    /// 1. Check args.Length >= 2, throw ArgumentException if not
    /// 2. Check each divisor != 0, throw DivideByZeroException if zero
    /// 3. Divide first by all subsequent: args[0] / args[1] / args[2] ...
    /// HINT: Use Skip(1).Aggregate() or a loop with validation
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement division with error handling");
    }
}
