//==============================================================================
// FILE: Fact.cs
// ⭐⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 30 minutes
// 🧪 TEST: fact 5 → 120.0 | fact 0 → 1.0 | fact 3.5 → ERROR | fact -2 → ERROR
//==============================================================================

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
    /// STUDENT TODO: Implement factorial with validation.
    /// 
    /// VALIDATION REQUIREMENTS:
    /// 1. Check args.Length == 1
    /// 2. Check n >= 0 (factorial undefined for negative numbers!)
    /// 3. Check n == Math.Truncate(n) (must be whole number, no decimals!)
    /// 
    /// IMPLEMENTATION:
    /// - Use loop: result = 1; for i=1 to n: result *= i
    /// - Special case: 0! = 1 (by definition in math)
    /// 
    /// HINT: Use long for result to avoid overflow for larger numbers
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement factorial with strict validation");
    }
}
