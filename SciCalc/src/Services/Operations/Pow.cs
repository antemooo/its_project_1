//==============================================================================
// FILE: Pow.cs
// ⭐ DIFFICULTY: Easy | ⏱️ TIME: 15 minutes
// 🧪 TEST: pow 2 8 → 256.0 | pow 3 2 → 9.0 | pow 5 0 → 1.0
//==============================================================================

using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Power/exponentiation operation (x^y).
/// Example: pow 2 8 → 2^8 = 256
/// </summary>
public sealed class Pow : IOperation
{
    public string Name => "pow";
    public string Help => "pow x y - Calculates x raised to the power of y (x^y)";
    
    /// <summary>
    /// STUDENT TODO: Implement exponentiation.
    /// 1. Validate args.Length == 2 (base and exponent)
    /// 2. Use Math.Pow(base, exponent) from .NET Math library
    /// HINT: Math.Pow returns double, perfect for this use case
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement power operation");
    }
}
