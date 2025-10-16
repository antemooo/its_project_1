//==============================================================================
// FILE: Add.cs
// ⭐ DIFFICULTY: Easy | ⏱️ TIME: 10-15 minutes
// 🧪 TEST: add 2 3 → 5.0 | add 1 2 3 4 → 10.0 | add → 0.0
//==============================================================================

using System;
using System.Linq;
using SciCalc.Models;

namespace SciCalc.Services.Operations;

/// <summary>
/// Addition operation - adds all provided numbers together.
/// Demonstrates: Variable arity (accepts any number of arguments)
/// Example: add 1 2 3 4 → 10
/// </summary>
public sealed class Add : IOperation
{
    public string Name => "add";
    public string Help => "add x y [z ...] - Adds all numbers together";
    
    /// <summary>
    /// STUDENT TODO: Implement addition.
    /// HINT: Use LINQ Sum() method. Handle empty array → return 0.
    /// One-liner possible: return args.Length == 0 ? 0 : args.Sum();
    /// </summary>
    public double Evaluate(params double[] args)
    {
        throw new NotImplementedException("TODO: Implement addition");
    }
}
