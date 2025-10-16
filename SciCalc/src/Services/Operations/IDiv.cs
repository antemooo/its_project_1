//==============================================================================
// FILE: IDiv.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 20 minutes
// 🧪 TEST: idiv 7 2 → 3.0 | idiv 10 3 → 3.0 | idiv 10 0 → ERROR
//==============================================================================

using System;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Integer division operation - divides and truncates to whole number.
/// Example: idiv 7 2 → 3 (not 3.5, truncates decimal part)
/// Different from regular division which returns 3.5
/// </summary>
public sealed class IDiv : IOperation
{
    public string Name => "idiv";
    public string Help => "idiv a b - Integer division (discards remainder)";
    
    /// <summary>
    /// STUDENT TODO: Implement integer division.
    /// 1. Validate args.Length == 2
    /// 2. Check args[1] != 0 (division by zero error!)
    /// 3. Divide args[0] / args[1] and use Math.Truncate() to remove decimals
    /// HINT: Math.Truncate rounds toward zero (7.9 → 7, -7.9 → -7)
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement integer division with error handling");
    }
}
