//==============================================================================
// FILE: Mul.cs
// ⭐ DIFFICULTY: Easy | ⏱️ TIME: 15 minutes
// 🧪 TEST: mul 2 3 4 → 24.0 | mul 5 → 5.0 | mul → 1.0
//==============================================================================

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
    /// STUDENT TODO: Implement multiplication.
    /// HINT: Use Aggregate: args.Aggregate(1.0, (acc, x) => acc * x)
    /// Empty array should return 1 (multiplicative identity).
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement multiplication");
    }
}
