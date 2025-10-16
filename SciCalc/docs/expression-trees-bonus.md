# 🌟 Bonus Challenge: Expression Trees

> **Difficulty**: ⭐⭐⭐ Advanced  
> **Prerequisites**: Completed the basic calculator, comfortable with recursion and pattern matching  
> **Time**: 3-5 hours additional work  

## 📚 What You'll Learn

This bonus challenge introduces **Expression Trees** (also called Abstract Syntax Trees - AST), a powerful way to represent and evaluate mathematical expressions. You'll explore:

- **Algebraic Data Types (ADT)** using C# records
- **Recursive data structures** (trees)
- **Pattern matching** with switch expressions
- **Functional programming** concepts in C#
- **Tree traversal** algorithms
- Foundation concepts for **compilers** and **interpreters**

---

## 🎯 The Challenge

Transform your calculator from handling **flat operations**:
```
> add 5 3 2
10
```

To handling **nested expressions**:
```
> eval (5 + 3) * 2
16

> eval (10 / 2) + (3 * 4)
17

> eval sqrt(16) + pow(2, 3)
12
```

---

## 🧠 Conceptual Foundation

### What is an Expression Tree?

An expression like `(5 + 3) * 2` can be represented as a tree:

```
       *
      / \
     +   2
    / \
   5   3
```

Each node is either:
- **Leaf**: A constant value (`5`, `3`, `2`)
- **Branch**: An operation with child expressions (`+`, `*`)

### Why Use Trees?

1. **Naturally represents nested structure**: `(a + b) * (c - d)`
2. **Separates parsing from evaluation**: Build tree first, evaluate later
3. **Enables transformations**: Simplify, differentiate, optimize
4. **Foundation for programming languages**: Every compiler builds ASTs

---

## 💻 Implementation Guide

### Step 1: Define Expression Types

We use C# **records** to create an algebraic data type:

```csharp
namespace SciCalc.ExpressionTree;

/// <summary>
/// Base type for all expressions.
/// This is an "algebraic data type" - a type defined by its variants.
/// 
/// PATTERN: Algebraic Data Type (ADT)
/// Common in functional programming languages like F#, Haskell, OCaml.
/// C# records make this pattern elegant and type-safe.
/// </summary>
public abstract record Expr;

/// <summary>
/// A constant numeric value (leaf node in the tree).
/// Example: 5, 3.14, -2.5
/// </summary>
/// <param name="Value">The numeric value</param>
public record Const(double Value) : Expr
{
    public override string ToString() => Value.ToString();
}

/// <summary>
/// Addition operation (binary operator).
/// Example: (5 + 3) becomes Add(Const(5), Const(3))
/// </summary>
/// <param name="Left">Left operand</param>
/// <param name="Right">Right operand</param>
public record Add(Expr Left, Expr Right) : Expr
{
    public override string ToString() => $"({Left} + {Right})";
}

/// <summary>
/// Subtraction operation (binary operator).
/// Example: (10 - 3) becomes Sub(Const(10), Const(3))
/// </summary>
public record Sub(Expr Left, Expr Right) : Expr
{
    public override string ToString() => $"({Left} - {Right})";
}

/// <summary>
/// Multiplication operation (binary operator).
/// Example: (5 * 3) becomes Mul(Const(5), Const(3))
/// </summary>
public record Mul(Expr Left, Expr Right) : Expr
{
    public override string ToString() => $"({Left} * {Right})";
}

/// <summary>
/// Division operation (binary operator).
/// Example: (10 / 2) becomes Div(Const(10), Const(2))
/// </summary>
public record Div(Expr Left, Expr Right) : Expr
{
    public override string ToString() => $"({Left} / {Right})";
}

/// <summary>
/// Power operation (binary operator).
/// Example: (2 ^ 8) becomes Pow(Const(2), Const(8))
/// </summary>
public record Pow(Expr Left, Expr Right) : Expr
{
    public override string ToString() => $"pow({Left}, {Right})";
}

/// <summary>
/// Square root operation (unary operator).
/// Example: sqrt(16) becomes Sqrt(Const(16))
/// </summary>
public record Sqrt(Expr Operand) : Expr
{
    public override string ToString() => $"sqrt({Operand})";
}

/// <summary>
/// Natural logarithm operation (unary operator).
/// Example: ln(10) becomes Ln(Const(10))
/// </summary>
public record Ln(Expr Operand) : Expr
{
    public override string ToString() => $"ln({Operand})";
}

/// <summary>
/// Sine function (unary operator).
/// Example: sin(30) becomes Sin(Const(30))
/// </summary>
public record Sin(Expr Operand) : Expr
{
    public override string ToString() => $"sin({Operand})";
}

/// <summary>
/// Cosine function (unary operator).
/// Example: cos(45) becomes Cos(Const(45))
/// </summary>
public record Cos(Expr Operand) : Expr
{
    public override string ToString() => $"cos({Operand})";
}
```

**Key Concepts:**
- `abstract record Expr` = base type (like an interface, but for data)
- Each concrete record inherits from `Expr`
- Records automatically generate equality, ToString, and deconstruction
- This is the **Composite Pattern** from design patterns

---

### Step 2: Implement the Evaluator

Now we use **pattern matching** to recursively evaluate the tree:

```csharp
namespace SciCalc.ExpressionTree;

/// <summary>
/// Evaluator for expression trees using pattern matching.
/// 
/// ALGORITHM: Recursive tree traversal
/// - Base case: Const(v) returns v
/// - Recursive case: Evaluate children, apply operator
/// 
/// TIME COMPLEXITY: O(n) where n = number of nodes in tree
/// SPACE COMPLEXITY: O(h) where h = height of tree (recursion stack)
/// </summary>
public static class Evaluator
{
    /// <summary>
    /// Current angle mode for trigonometric functions.
    /// Default is Radians.
    /// </summary>
    public static AngleMode Mode { get; set; } = AngleMode.Radians;

    /// <summary>
    /// Recursively evaluates an expression tree to a numeric result.
    /// 
    /// This is a classic example of the VISITOR PATTERN implemented
    /// with pattern matching instead of double dispatch.
    /// </summary>
    /// <param name="expr">The expression to evaluate</param>
    /// <returns>The computed numeric result</returns>
    /// <exception cref="DivideByZeroException">When dividing by zero</exception>
    /// <exception cref="ArgumentException">When domain error occurs (sqrt of negative, etc.)</exception>
    public static double Eval(Expr expr) => expr switch
    {
        // BASE CASE: Constant value
        // Just return the value - no computation needed
        Const(var value) => value,

        // BINARY OPERATIONS: Evaluate both children, then apply operator
        // Note: These use RECURSION to handle nested expressions
        
        Add(var left, var right) => Eval(left) + Eval(right),
        
        Sub(var left, var right) => Eval(left) - Eval(right),
        
        Mul(var left, var right) => Eval(left) * Eval(right),
        
        Div(var left, var right) => 
            EvalDiv(Eval(left), Eval(right)),
        
        Pow(var left, var right) => 
            Math.Pow(Eval(left), Eval(right)),

        // UNARY OPERATIONS: Evaluate single child, then apply function
        
        Sqrt(var operand) => EvalSqrt(Eval(operand)),
        
        Ln(var operand) => EvalLn(Eval(operand)),
        
        Sin(var operand) => EvalSin(Eval(operand)),
        
        Cos(var operand) => EvalCos(Eval(operand)),

        // EXHAUSTIVE MATCHING: Catch anything we forgot
        _ => throw new NotSupportedException($"Unknown expression type: {expr.GetType().Name}")
    };

    // Helper methods for validation and domain checking

    private static double EvalDiv(double numerator, double denominator)
    {
        if (Math.Abs(denominator) < 1e-10)
            throw new DivideByZeroException("Cannot divide by zero");
        return numerator / denominator;
    }

    private static double EvalSqrt(double value)
    {
        if (value < 0)
            throw new ArgumentException($"Cannot take square root of negative number: {value}");
        return Math.Sqrt(value);
    }

    private static double EvalLn(double value)
    {
        if (value <= 0)
            throw new ArgumentException($"Cannot take natural log of non-positive number: {value}");
        return Math.Log(value);
    }

    private static double EvalSin(double angle)
    {
        var radians = Mode == AngleMode.Degrees ? angle * Math.PI / 180 : angle;
        return Math.Sin(radians);
    }

    private static double EvalCos(double angle)
    {
        var radians = Mode == AngleMode.Degrees ? angle * Math.PI / 180 : angle;
        return Math.Cos(radians);
    }
}
```

**Key Concepts:**
- **Pattern Matching**: The `switch` expression matches on record types
- **Recursion**: Each binary operator calls `Eval` on its children
- **Exhaustiveness**: The `_` case ensures all types are handled
- **Positional Patterns**: `Add(var left, var right)` deconstructs the record

---

### Step 3: Build Expression Trees Manually

Before implementing a parser, test with manual tree construction:

```csharp
using SciCalc.ExpressionTree;

namespace SciCalc.Examples;

public static class ExpressionExamples
{
    /// <summary>
    /// Example: (5 + 3) * 2
    /// Tree structure:
    ///       *
    ///      / \
    ///     +   2
    ///    / \
    ///   5   3
    /// </summary>
    public static void Example1()
    {
        var expr = new Mul(
            new Add(
                new Const(5),
                new Const(3)
            ),
            new Const(2)
        );

        Console.WriteLine($"Expression: {expr}");
        Console.WriteLine($"Result: {Evaluator.Eval(expr)}");
        // Output:
        // Expression: ((5 + 3) * 2)
        // Result: 16
    }

    /// <summary>
    /// Example: sqrt(16) + pow(2, 3)
    /// Tree structure:
    ///       +
    ///      / \
    ///   sqrt  pow
    ///    |    / \
    ///   16   2   3
    /// </summary>
    public static void Example2()
    {
        var expr = new Add(
            new Sqrt(new Const(16)),
            new Pow(new Const(2), new Const(3))
        );

        Console.WriteLine($"Expression: {expr}");
        Console.WriteLine($"Result: {Evaluator.Eval(expr)}");
        // Output:
        // Expression: (sqrt(16) + pow(2, 3))
        // Result: 12
    }

    /// <summary>
    /// Example: (10 / 2) + (3 * 4)
    /// Tree structure:
    ///       +
    ///      / \
    ///     /   *
    ///    / \  / \
    ///   10 2 3  4
    /// </summary>
    public static void Example3()
    {
        var expr = new Add(
            new Div(new Const(10), new Const(2)),
            new Mul(new Const(3), new Const(4))
        );

        Console.WriteLine($"Expression: {expr}");
        Console.WriteLine($"Result: {Evaluator.Eval(expr)}");
        // Output:
        // Expression: ((10 / 2) + (3 * 4))
        // Result: 17
    }

    /// <summary>
    /// Example: Nested operations: ((2 + 3) * (4 - 1)) / 5
    /// Tree structure:
    ///         /
    ///        / \
    ///       *   5
    ///      / \
    ///     +   -
    ///    / \ / \
    ///   2  3 4  1
    /// </summary>
    public static void Example4()
    {
        var expr = new Div(
            new Mul(
                new Add(new Const(2), new Const(3)),
                new Sub(new Const(4), new Const(1))
            ),
            new Const(5)
        );

        Console.WriteLine($"Expression: {expr}");
        Console.WriteLine($"Result: {Evaluator.Eval(expr)}");
        // Output:
        // Expression: (((2 + 3) * (4 - 1)) / 5)
        // Result: 3
    }
}
```

---

### Step 4: Simple Parser (Prefix Notation)

The hardest part is parsing infix notation `(5 + 3) * 2`. Let's start with **prefix notation** (Polish notation), which is much easier:

```csharp
namespace SciCalc.ExpressionTree;

/// <summary>
/// Simple parser for prefix notation expressions.
/// 
/// PREFIX NOTATION (Polish Notation):
/// - Operator comes BEFORE operands
/// - No parentheses needed
/// - Easy to parse with recursion
/// 
/// Examples:
///   + 5 3        means (5 + 3)
///   * + 5 3 2    means (5 + 3) * 2
///   + sqrt 16 pow 2 3    means sqrt(16) + pow(2, 3)
/// 
/// ALGORITHM: Recursive descent parsing
/// TIME COMPLEXITY: O(n) where n = number of tokens
/// </summary>
public static class PrefixParser
{
    /// <summary>
    /// Parse a prefix notation expression string into an expression tree.
    /// </summary>
    /// <param name="input">Space-separated tokens in prefix notation</param>
    /// <returns>The parsed expression tree</returns>
    /// <exception cref="ArgumentException">If syntax is invalid</exception>
    public static Expr Parse(string input)
    {
        var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var index = 0;
        return ParseExpr(tokens, ref index);
    }

    /// <summary>
    /// Recursively parse an expression from the token stream.
    /// </summary>
    private static Expr ParseExpr(string[] tokens, ref int index)
    {
        if (index >= tokens.Length)
            throw new ArgumentException("Unexpected end of expression");

        var token = tokens[index++];

        // Try to parse as a number (base case)
        if (double.TryParse(token, out var value))
            return new Const(value);

        // Binary operators (need 2 operands)
        return token switch
        {
            "+" => new Add(ParseExpr(tokens, ref index), ParseExpr(tokens, ref index)),
            "-" => new Sub(ParseExpr(tokens, ref index), ParseExpr(tokens, ref index)),
            "*" => new Mul(ParseExpr(tokens, ref index), ParseExpr(tokens, ref index)),
            "/" => new Div(ParseExpr(tokens, ref index), ParseExpr(tokens, ref index)),
            "pow" => new Pow(ParseExpr(tokens, ref index), ParseExpr(tokens, ref index)),
            
            // Unary operators (need 1 operand)
            "sqrt" => new Sqrt(ParseExpr(tokens, ref index)),
            "ln" => new Ln(ParseExpr(tokens, ref index)),
            "sin" => new Sin(ParseExpr(tokens, ref index)),
            "cos" => new Cos(ParseExpr(tokens, ref index)),
            
            _ => throw new ArgumentException($"Unknown operator: {token}")
        };
    }

    /// <summary>
    /// Parse and evaluate in one step.
    /// </summary>
    public static double ParseAndEval(string input)
    {
        var expr = Parse(input);
        return Evaluator.Eval(expr);
    }
}
```

**Examples of Prefix Notation:**
```csharp
// (5 + 3) becomes: + 5 3
PrefixParser.ParseAndEval("+ 5 3")  // 8

// (5 + 3) * 2 becomes: * + 5 3 2
PrefixParser.ParseAndEval("* + 5 3 2")  // 16

// sqrt(16) + pow(2, 3) becomes: + sqrt 16 pow 2 3
PrefixParser.ParseAndEval("+ sqrt 16 pow 2 3")  // 12
```

---

### Step 5: Advanced - Infix Parser (Challenge!)

Parsing infix notation `(5 + 3) * 2` requires handling:
1. **Operator precedence**: `*` before `+`
2. **Parentheses**: `(5 + 3)` forces order
3. **Associativity**: `5 - 3 - 1` is left-to-right

This typically uses the **Shunting Yard Algorithm** by Dijkstra:

```csharp
namespace SciCalc.ExpressionTree;

/// <summary>
/// Parser for infix notation using the Shunting Yard algorithm.
/// 
/// SHUNTING YARD ALGORITHM (Edsger Dijkstra):
/// Converts infix notation to postfix (RPN), then builds expression tree.
/// 
/// OPERATOR PRECEDENCE (high to low):
/// 1. Functions: sqrt, ln, sin, cos
/// 2. Exponentiation: ^
/// 3. Multiplication/Division: *, /
/// 4. Addition/Subtraction: +, -
/// 
/// TIME COMPLEXITY: O(n) where n = number of tokens
/// SPACE COMPLEXITY: O(n) for operator and output stacks
/// </summary>
public static class InfixParser
{
    private static readonly Dictionary<string, int> Precedence = new()
    {
        { "+", 1 }, { "-", 1 },
        { "*", 2 }, { "/", 2 },
        { "^", 3 },
        { "sqrt", 4 }, { "ln", 4 }, { "sin", 4 }, { "cos", 4 }
    };

    /// <summary>
    /// Tokenize infix expression, handling numbers, operators, and parentheses.
    /// </summary>
    private static List<string> Tokenize(string input)
    {
        var tokens = new List<string>();
        var i = 0;
        
        while (i < input.Length)
        {
            var ch = input[i];
            
            // Skip whitespace
            if (char.IsWhiteSpace(ch))
            {
                i++;
                continue;
            }
            
            // Numbers (including decimals)
            if (char.IsDigit(ch) || ch == '.')
            {
                var start = i;
                while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.'))
                    i++;
                tokens.Add(input[start..i]);
                continue;
            }
            
            // Operators and parentheses
            if ("+-*/^()".Contains(ch))
            {
                tokens.Add(ch.ToString());
                i++;
                continue;
            }
            
            // Function names (letters)
            if (char.IsLetter(ch))
            {
                var start = i;
                while (i < input.Length && char.IsLetter(input[i]))
                    i++;
                tokens.Add(input[start..i]);
                continue;
            }
            
            throw new ArgumentException($"Unexpected character: {ch}");
        }
        
        return tokens;
    }

    /// <summary>
    /// Convert infix tokens to postfix (Reverse Polish Notation).
    /// Uses the Shunting Yard algorithm.
    /// </summary>
    private static List<string> ToPostfix(List<string> tokens)
    {
        var output = new List<string>();
        var operators = new Stack<string>();

        foreach (var token in tokens)
        {
            // Numbers go directly to output
            if (double.TryParse(token, out _))
            {
                output.Add(token);
            }
            // Functions go on operator stack
            else if (Precedence.ContainsKey(token) && token.Length > 1)
            {
                operators.Push(token);
            }
            // Operators
            else if (Precedence.ContainsKey(token))
            {
                while (operators.Count > 0 && 
                       operators.Peek() != "(" &&
                       Precedence.GetValueOrDefault(operators.Peek(), 0) >= Precedence[token])
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
            // Left parenthesis
            else if (token == "(")
            {
                operators.Push(token);
            }
            // Right parenthesis
            else if (token == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    output.Add(operators.Pop());
                }
                if (operators.Count == 0)
                    throw new ArgumentException("Mismatched parentheses");
                operators.Pop(); // Remove the '('
            }
            else
            {
                throw new ArgumentException($"Unknown token: {token}");
            }
        }

        // Pop remaining operators
        while (operators.Count > 0)
        {
            var op = operators.Pop();
            if (op == "(")
                throw new ArgumentException("Mismatched parentheses");
            output.Add(op);
        }

        return output;
    }

    /// <summary>
    /// Build expression tree from postfix tokens.
    /// </summary>
    private static Expr BuildTree(List<string> postfix)
    {
        var stack = new Stack<Expr>();

        foreach (var token in postfix)
        {
            // Numbers become constants
            if (double.TryParse(token, out var value))
            {
                stack.Push(new Const(value));
            }
            // Unary operators
            else if (token is "sqrt" or "ln" or "sin" or "cos")
            {
                if (stack.Count < 1)
                    throw new ArgumentException($"Not enough operands for {token}");
                    
                var operand = stack.Pop();
                stack.Push(token switch
                {
                    "sqrt" => new Sqrt(operand),
                    "ln" => new Ln(operand),
                    "sin" => new Sin(operand),
                    "cos" => new Cos(operand),
                    _ => throw new NotSupportedException()
                });
            }
            // Binary operators
            else
            {
                if (stack.Count < 2)
                    throw new ArgumentException($"Not enough operands for {token}");
                    
                var right = stack.Pop();
                var left = stack.Pop();
                
                stack.Push(token switch
                {
                    "+" => new Add(left, right),
                    "-" => new Sub(left, right),
                    "*" => new Mul(left, right),
                    "/" => new Div(left, right),
                    "^" => new Pow(left, right),
                    _ => throw new ArgumentException($"Unknown operator: {token}")
                });
            }
        }

        if (stack.Count != 1)
            throw new ArgumentException("Invalid expression");

        return stack.Pop();
    }

    /// <summary>
    /// Parse infix expression string into expression tree.
    /// </summary>
    public static Expr Parse(string input)
    {
        var tokens = Tokenize(input);
        var postfix = ToPostfix(tokens);
        return BuildTree(postfix);
    }

    /// <summary>
    /// Parse and evaluate infix expression.
    /// </summary>
    public static double ParseAndEval(string input)
    {
        var expr = Parse(input);
        return Evaluator.Eval(expr);
    }
}
```

**Examples:**
```csharp
// Standard infix notation
InfixParser.ParseAndEval("(5 + 3) * 2")  // 16
InfixParser.ParseAndEval("10 / 2 + 3 * 4")  // 17
InfixParser.ParseAndEval("sqrt(16) + 2^3")  // 12
InfixParser.ParseAndEval("((2 + 3) * (4 - 1)) / 5")  // 3
```

---

## 🎨 Advanced Extensions

Once you have the basic expression tree working, try these challenges:

### 1. Symbolic Differentiation

Compute derivatives symbolically:

```csharp
/// <summary>
/// Compute the derivative of an expression with respect to a variable.
/// 
/// RULES (from calculus):
/// - d/dx(c) = 0 (constant rule)
/// - d/dx(x) = 1 (power rule)
/// - d/dx(f + g) = df/dx + dg/dx (sum rule)
/// - d/dx(f * g) = f * dg/dx + g * df/dx (product rule)
/// - d/dx(f / g) = (g * df/dx - f * dg/dx) / g^2 (quotient rule)
/// </summary>
public static Expr Derivative(Expr expr) => expr switch
{
    Const(_) => new Const(0),  // d/dx(5) = 0
    
    Add(var l, var r) => new Add(Derivative(l), Derivative(r)),
    
    Sub(var l, var r) => new Sub(Derivative(l), Derivative(r)),
    
    Mul(var l, var r) => new Add(
        new Mul(l, Derivative(r)),
        new Mul(Derivative(l), r)
    ),  // Product rule
    
    // Add more rules for Div, Pow, Sin, Cos, etc.
    
    _ => throw new NotSupportedException($"Cannot differentiate: {expr}")
};
```

Example:
```csharp
// d/dx(x^2) at x=3
var expr = new Pow(new Var("x"), new Const(2));
var deriv = Derivative(expr);  // 2*x
var result = Evaluator.Eval(deriv, new Dictionary<string, double> { ["x"] = 3 });
// Result: 6
```

### 2. Expression Simplification

Reduce expressions algebraically:

```csharp
/// <summary>
/// Simplify an expression algebraically.
/// </summary>
public static Expr Simplify(Expr expr) => expr switch
{
    // 0 + x = x
    Add(Const(0), var x) => Simplify(x),
    Add(var x, Const(0)) => Simplify(x),
    
    // 1 * x = x
    Mul(Const(1), var x) => Simplify(x),
    Mul(var x, Const(1)) => Simplify(x),
    
    // 0 * x = 0
    Mul(Const(0), _) => new Const(0),
    Mul(_, Const(0)) => new Const(0),
    
    // x - x = 0
    Sub(var l, var r) when l.Equals(r) => new Const(0),
    
    // Recursively simplify children
    Add(var l, var r) => new Add(Simplify(l), Simplify(r)),
    Mul(var l, var r) => new Mul(Simplify(l), Simplify(r)),
    
    // Already simple
    _ => expr
};
```

### 3. Pretty Printing

Generate LaTeX or ASCII art:

```csharp
/// <summary>
/// Convert expression to LaTeX format.
/// </summary>
public static string ToLatex(Expr expr) => expr switch
{
    Const(var v) => v.ToString(),
    Add(var l, var r) => $"{ToLatex(l)} + {ToLatex(r)}",
    Mul(var l, var r) => $"{ToLatex(l)} \\cdot {ToLatex(r)}",
    Div(var l, var r) => $"\\frac{{{ToLatex(l)}}}{{{ToLatex(r)}}}",
    Pow(var l, var r) => $"{ToLatex(l)}^{{{ToLatex(r)}}}",
    Sqrt(var x) => $"\\sqrt{{{ToLatex(x)}}}",
    _ => expr.ToString()
};
```

### 4. Expression Optimization

Constant folding and common subexpression elimination:

```csharp
/// <summary>
/// Optimize expression by evaluating constant subexpressions.
/// </summary>
public static Expr Optimize(Expr expr) => expr switch
{
    // If both children are constants, evaluate now
    Add(Const(var a), Const(var b)) => new Const(a + b),
    Mul(Const(var a), Const(var b)) => new Const(a * b),
    
    // Recursively optimize children
    Add(var l, var r) => new Add(Optimize(l), Optimize(r)),
    Mul(var l, var r) => new Mul(Optimize(l), Optimize(r)),
    
    // Already optimal
    _ => expr
};
```

---

## 🧪 Testing Your Implementation

```csharp
namespace SciCalc.Tests;

public static class ExpressionTreeTests
{
    public static void RunAllTests()
    {
        TestBasicEvaluation();
        TestPrefixParser();
        TestInfixParser();
        TestErrorHandling();
        Console.WriteLine("✅ All tests passed!");
    }

    private static void TestBasicEvaluation()
    {
        // (5 + 3) * 2 = 16
        var expr1 = new Mul(new Add(new Const(5), new Const(3)), new Const(2));
        Assert(Evaluator.Eval(expr1), 16);

        // sqrt(16) + pow(2, 3) = 12
        var expr2 = new Add(new Sqrt(new Const(16)), new Pow(new Const(2), new Const(3)));
        Assert(Evaluator.Eval(expr2), 12);

        // (10 / 2) + (3 * 4) = 17
        var expr3 = new Add(
            new Div(new Const(10), new Const(2)),
            new Mul(new Const(3), new Const(4))
        );
        Assert(Evaluator.Eval(expr3), 17);
    }

    private static void TestPrefixParser()
    {
        Assert(PrefixParser.ParseAndEval("+ 5 3"), 8);
        Assert(PrefixParser.ParseAndEval("* + 5 3 2"), 16);
        Assert(PrefixParser.ParseAndEval("+ sqrt 16 pow 2 3"), 12);
    }

    private static void TestInfixParser()
    {
        Assert(InfixParser.ParseAndEval("(5 + 3) * 2"), 16);
        Assert(InfixParser.ParseAndEval("10 / 2 + 3 * 4"), 17);
        Assert(InfixParser.ParseAndEval("sqrt(16) + 2^3"), 12);
        Assert(InfixParser.ParseAndEval("((2 + 3) * (4 - 1)) / 5"), 3);
    }

    private static void TestErrorHandling()
    {
        try
        {
            var expr = new Div(new Const(10), new Const(0));
            Evaluator.Eval(expr);
            throw new Exception("Should have thrown DivideByZeroException");
        }
        catch (DivideByZeroException) { }

        try
        {
            var expr = new Sqrt(new Const(-1));
            Evaluator.Eval(expr);
            throw new Exception("Should have thrown ArgumentException");
        }
        catch (ArgumentException) { }
    }

    private static void Assert(double actual, double expected)
    {
        if (Math.Abs(actual - expected) > 1e-10)
            throw new Exception($"Expected {expected}, got {actual}");
    }
}
```

---

## 📊 Comparison: Strategy vs Expression Tree

| Aspect | Strategy Pattern | Expression Tree |
|--------|-----------------|-----------------|
| **Simplicity** | ⭐⭐⭐ Easy | ⭐ Complex |
| **Extensibility** | ⭐⭐ Add new classes | ⭐⭐⭐ Add new records |
| **Nested Expressions** | ❌ No | ✅ Yes |
| **Parsing Needed** | Simple (split by space) | Complex (infix/prefix) |
| **Memory Usage** | Low (immediate eval) | High (tree storage) |
| **Transformations** | ❌ Can't transform | ✅ Can simplify, differentiate |
| **Debugging** | Easy (one operation at a time) | Hard (tree structure) |
| **Best For** | CLI calculators | Compilers, symbolic math |

---

## 📚 Further Reading

### Books
- **"Crafting Interpreters"** by Robert Nystrom - Build a complete programming language
- **"Programming Language Pragmatics"** by Michael Scott - Compiler theory
- **"Structure and Interpretation of Computer Programs"** - Classic MIT textbook

### Online Resources
- [Shunting Yard Algorithm](https://en.wikipedia.org/wiki/Shunting-yard_algorithm) - Wikipedia
- [Abstract Syntax Trees](https://en.wikipedia.org/wiki/Abstract_syntax_tree) - Wikipedia
- [C# Pattern Matching](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching) - Microsoft Docs
- [Recursive Descent Parsing](https://en.wikipedia.org/wiki/Recursive_descent_parser) - Wikipedia

### Related Concepts
- **Interpreter Pattern** (Gang of Four design pattern)
- **Visitor Pattern** (traversing tree structures)
- **Compiler Design** (lexing, parsing, code generation)
- **Functional Programming** (immutable data, pattern matching)

---

## 🎯 Submission Checklist

If you complete this bonus challenge, include:

- [ ] All expression record types defined
- [ ] Evaluator with pattern matching
- [ ] At least one parser (prefix or infix)
- [ ] Manual tree construction examples
- [ ] Error handling for domain errors
- [ ] Unit tests for key scenarios
- [ ] Documentation explaining your approach
- [ ] Comparison with original Strategy implementation
- [ ] (Optional) One advanced extension (simplification, differentiation, etc.)

---

## 🏆 Grading Bonus Points

| Achievement | Bonus Points |
|-------------|--------------|
| Basic expression tree + evaluator | +5 |
| Prefix parser working | +3 |
| Infix parser working | +5 |
| One advanced extension | +3 |
| Full test suite | +2 |
| Comprehensive documentation | +2 |
| **Maximum Bonus** | **+20** |

---

## 💬 Discussion Questions

Think about these questions and include answers in your README:

1. **When would you use Strategy Pattern vs Expression Trees?**
   - Consider: complexity, requirements, team experience

2. **What are the tradeoffs of immediate evaluation vs building a tree?**
   - Think about: memory, flexibility, debugging

3. **How could you extend this to support variables?**
   - Example: `eval x^2 + 2x + 1 where x=3`

4. **What real-world applications use expression trees?**
   - Compilers, database query optimizers, spreadsheet formulas, symbolic math software

5. **How does pattern matching compare to the Visitor pattern?**
   - Both solve the "expression problem" differently

---

## 🚀 Get Started!

Ready to dive in? Start with:

1. Create `src/ExpressionTree/` folder
2. Define the basic `Expr` records (Const, Add, Mul)
3. Implement the evaluator
4. Test with manual tree construction
5. Add more operations
6. Implement a parser
7. Choose an advanced extension

Good luck, and have fun exploring the foundations of programming language design! 🎉

---

**Questions?** Ask in the course forum or office hours. This is advanced material, so don't hesitate to reach out for help!
