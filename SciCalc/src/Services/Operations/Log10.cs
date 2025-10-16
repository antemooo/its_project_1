//==============================================================================
// FILE: Log10.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 20 minutes
// 🧪 TEST: log10 1000 → 3.0 | log10 100 → 2.0 | log10 -5 → ERROR
//==============================================================================

using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Base-10 logarithm operation.
/// Example: log10 1000 → 3 (because 10^3 = 1000)
/// Demonstrates: Domain validation for logarithms
/// </summary>
public sealed class Log10 : IOperation
{
    public string Name => "log10";
    public string Help => "log10 x - Calculates base-10 logarithm of x";
    
    /// <summary>
    /// STUDENT TODO: Implement base-10 logarithm with validation.
    /// 1. Check args.Length == 1
    /// 2. Validate args[0] > 0 (log only defined for positive numbers!)
    /// 3. Use Math.Log10(x) from .NET library
    /// IMPORTANT: What happens if x ≤ 0? Throw ArgumentException!
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement log10 with domain validation");
    }
}
