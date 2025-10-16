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
    /// Adds all numbers. Uses LINQ's Sum() method for clean code.
    /// Returns 0 if no arguments provided (identity element for addition).
    /// </summary>
    public double Evaluate(params double[] args)
    {
        return args.Length == 0 ? 0 : args.Sum();
    }
}
