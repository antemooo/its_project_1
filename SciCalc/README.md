# Scientific Calculator — Console (C#/.NET 8)

## 1) Purpose
Build a **single-file** console calculator supporting basic arithmetic and at least **five** scientific functions. All operations accept **any number of numeric arguments** where sensible.
# Scientific Calculator - C# Console Application

## 🎯 Project Overview

A console-based scientific calculator that performs basic arithmetic and advanced mathematical operations. Demonstrates the **Strategy Pattern** and **Factory Pattern** with clean OOP design.

### Learning Objectives
- Implement interfaces and polymorphism
- Use the Strategy Pattern (each operation is a strategy)
- Use the Factory Pattern (centralized operation creation)
- Handle variable-arity methods with `params`
- Validate inputs and handle mathematical domain errors

## 🚀 How to Run

### Prerequisites
- .NET 8 SDK installed ([Download here](https://dotnet.microsoft.com/download/dotnet/8.0))
- Terminal/Command Prompt

### Build and Run
```bash
cd SciCalc
  dotnet run --project SciCalc.csproj
```

See `data/examples.txt` for more examples and complete documentation.

## 2) Learning goals
- OOP basics: interfaces, encapsulation, polymorphism.
- Design for extension (Factory + Strategy).
- Robust input parsing and error handling.
- Clean CLI design: help, consistent messages.

## 3) Command model (CLI)
- **Format:** `command [args...]` (space‑separated; decimals use dot `.`).
- **Angle mode:** `mode deg` | `mode rad` (affects `sin|cos|tan`).
- **Core:** `add`, `sub`, `mul`, `div`
- **Scientific (choose ≥5):** `sin`, `cos`, `tan`, `sqrt`, `pow`, `log10`, `ln`, `mod`, `idiv`, `fact`
- **Utility:** `help`, `exit`

**Examples**
```
add 2.5 1 -2 24.5125 0.33
div 7 2
idiv 7 3
mod 7 3
mode deg
sin 30
pow 2 8
sqrt 9
log10 1000
ln 1
fact 5
```

## 4) Behaviour & rules
- Variable arity via `params double[]` where applicable (e.g., `add`, `mul`, `div` chaining).
- Parsing: accept integers or decimals; invariant culture.
- Errors (print friendly message and continue):
  - divide by zero / integer division by zero
  - domain errors (e.g., `sqrt` of negative, `ln` of non‑positive)
  - `fact` only for non‑negative integers
  - not‑a‑number input or wrong arity
- Trig respects current angle mode (default **rad**).

## 5) Design (minimum)
```csharp
public interface IOperation { string Name {get;} string Help {get;} double Evaluate(params double[] args); }
public interface ITrigonometric : IOperation { double Evaluate(double[] args, AngleMode mode); }
public static class OperationFactory { /* string -> IOperation registry */ }
```
- Use **Factory** to register commands and **Strategy** (one class per operation).

## 6) Run
```bash
  dotnet new console -n SciCalc && cd SciCalc
  # Add ScientificCalculator.cs from this template (or replace Program.cs)
  dotnet run --project SciCalc.csproj
```

## 7) One‑week plan
- **D1:** base ops, help, error messages.
- **D2:** trig + angle mode.
- **D3:** sqrt/pow/log/idiv/mod/fact.
- **D4:** refactor to Factory/Strategy; tidy output.
- **D5:** README examples; final test and demo.

## 8) Acceptance checks
- `add 2.5 1 -2 24.5125 0.33` → `26.3425`
- `div 7 2` → `3.5`; `idiv 7 2` → `3`; `mod 7 2` → `1`
- `mode deg` then `sin 30` → `0.5`
- `pow 2 8` → `256`; `sqrt 9` → `3`; `log10 1000` → `3`; `ln 1` → `0`
- `fact 5` → `120`; `fact -1` prints an error.
