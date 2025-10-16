//==============================================================================
// FILE: Sub.cs
// ⭐ DIFFICULTY: Easy
// ⏱️ ESTIMATED TIME: 15 minutes
// 🧪 TEST CASES:
//   sub 10 3        → 7.0
//   sub 10 3 2      → 5.0
//   sub 5           → 5.0
//   sub             → 0.0
//==============================================================================

using System;
using System.Linq;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Subtraction operation - subtracts numbers sequentially.
/// Example: sub 10 3 2 → 5 (10 - 3 - 2)
/// </summary>
public sealed class Sub : IOperation
{
    public string Name => "sub";
    public string Help => "sub x y [z ...] - Subtracts numbers from first";
    
    /// <summary>
    /// STUDENT TODO: Implement subtraction.
    /// 
    /// REQUIREMENTS:
    /// 1. Handle 0 arguments → return 0
    /// 2. Handle 1 argument → return that number
    /// 3. Handle multiple → subtract all from first: a - b - c - d
    /// 
    /// HINTS:
    /// - Start with result = args[0]
    /// - Loop from index 1 to end: result -= args[i]
    /// - Or use LINQ: args.Skip(1).Aggregate(args[0], (a, b) => a - b)
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("STUDENT TODO: Implement subtraction");
    }
}
