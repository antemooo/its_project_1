//==============================================================================
// FILE: Ln.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 20 minutes
// 🧪 TEST: ln 2.71828 → ~1.0 | ln 7.389 → ~2.0 | ln 0 → ERROR
//==============================================================================

using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Natural logarithm operation (base e).
/// Example: ln 2.71828 → ≈1 (because e^1 ≈ 2.71828)
/// </summary>
public sealed class Ln : IOperation
{
    public string Name => "ln";
    public string Help => "ln x - Calculates natural logarithm of x (base e)";
    
    /// <summary>
    /// STUDENT TODO: Implement natural logarithm with validation.
    /// 1. Check args.Length == 1
    /// 2. Validate args[0] > 0 (same domain constraint as log10!)
    /// 3. Use Math.Log(x) - note: Math.Log is natural log, Math.Log10 is base-10
    /// HINT: e ≈ 2.71828 is Euler's number (Math.E in C#)
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement natural logarithm with domain validation");
    }
}
