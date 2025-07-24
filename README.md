[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
![Build](https://github.com/toarnbeike/toarnbeike.optional/actions/workflows/build.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Optional.svg)](https://www.nuget.org/packages/Toarnbeike.Optional)

⚠️ This library is under **active development** and may evolve over time.

# Toarnbeike.Optional

**Toarnbeike.Optional** is a lightweight, expressive C# implementation of the [Option (Maybe) monad](https://en.wikipedia.org/wiki/Option_type).  

It improves code clarity and safety by making the **absence of a value explicit**, replacing null checks and exceptions with a functional approach.
> Developed by Toarnbeike for personal and professional use in domain-driven and functional designs.

---

## Features

- Fluent API for working with optional values
- Implicit conversion from values and `Option.None`
- Extension methods inspired by F# and Haskell
- Full support for `Task<Option<T>>` async scenarios
- Rich LINQ-style extensions for `IEnumerable<Option<T>>`
- [Planned] Linq integration (query syntax)
- [Planned] Integration with assertion libraries like Shouldly

---

## Inspiration

This project draws ideas from:
- [Zoran Horvat (youtube)](https://www.youtube.com/@zoran-horvat)
- [nlkl.Optional (github)](https://github.com/nlkl/Optional)
- [DotnetFunctional.Maybe (github)](https://github.com/dotnetfunctional/Maybe)
 
---

## Getting Started

```bash
dotnet add package Toarnbeike.Optional
```
Then import the namespace in your code:
```csharp
using Toarnbeike.Optional;			// Base namespace for Option<T>
using Toarnbeike.Optional.Collections;		// For collection extensions
using Toarnbeike.Optional.Extensions;		// For functional extensions on Option<T>
```

---

## Basic Usage

An option can represent either a value (`Some`) or no value (`None`):

```csharp
var option1 = Option<int>.Some(1);
var option2 = Option.Some(1);        // Type inferred
var option3 = Option<int>.None();

Option<int> option4 = 1;             // Implicit conversion
Option<int> option5 = Option.None;
```

Extracting the value:
```csharp
if (option1.TryGetValue(out var value))
{
    Console.WriteLine(value); // Outputs: 1
}
```

---

## Extension Methods
The `Toarnbeike.Optional.Extensions` namespace includes rich extensions for `Option<T>`:

### Available Extensions
| Method			| Description								    |
|-------------------|-----------------------------------------------|
| `AsNullable()`	| Convert to nullable						    |
| `AsOption()`		| Convert from nullable						    |
| `Map(...)`		| Transforms the inner value 			        |
| `Bind(...)`		| Chain operations returning `Option<T>`        |
| `Check(...)`		| Filter by predicate					        |
| `IsSomeAnd(...)`  | Check if value matches value or predicate     |
| `Match(...)`      | Pattern match: Some/ None                     |
| `Reduce(...)`		| Fallback to a value if empty      		    |
| `ReduceOrThrow()`	| Get value or throw						    |
| `OrElse(...)`    	| Return current option or fallback if `None`   |
| `Tap(...)`		| Execute side-effect on value				    |
| `TapIfNone()`		| Execute side-effect when empty                |

---

## Collections
The `Toarnbeike.Optional.Collections` namespace adds functional utilities to collections of `Option<T>`:

```csharp
using Toarnbeike.Optional.Collections;

var options = new List<Option<int>>([1, Option.None, 3]);

var values = options.Values();					// [1, 3]
var filtered = options.WhereValues(i => i > 1);			// [3]
var doubled = options.SelectValues(i = > i * 2);		// [2,6] 
var evenCount = options.CountValues(i => i % 2 == 0);		// Option.None
var first = options.FirstOrNone(i => i > 2);			// Option.Some(3)
var last = options.LastOrNone(i => i > 0);			// Opiton.Some(3)
```

### Also works on `IEnumerable<T>`:
```csharp
using Toarnbeike.Optional.Collections;
var numbers = new List<int> { 1, 2, 3 };

var first = options.FirstOrNone(i => i > 4);			// Option.None
var last = options.LastOrNone(i => i > 0);			// 3 (as regular int)
```

---

## Why Option?
Because nulls are dangerous and exceptions are control-flow.
Using Option<T>:

- Makes absence explicit
- Forces handling at compile time
- Composes beautifully in pipelines

> Make illegal states unrepresentable.
