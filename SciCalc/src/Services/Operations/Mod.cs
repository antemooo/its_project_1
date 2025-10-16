//==============================================================================
// FILE: Mod.cs
// ⭐ DIFFICULTY: Easy | ⏱️ TIME: 15 minutes
// 🧪 TEST: mod 7 3 → 1.0 | mod 10 5 → 0.0 | mod 17 4 → 1.0
//==============================================================================

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
    /// STUDENT TODO: Implement modulo operation.
    /// 1. Validate args.Length == 2
    /// 2. Return args[0] % args[1] (use % operator in C#)
    /// HINT: Modulo is useful for checking divisibility (x % 2 == 0 means even)
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement modulo operation");
    }
}
