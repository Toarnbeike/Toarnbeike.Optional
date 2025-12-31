![CI](https://github.com/Toarnbeike/Toarnbeike.Optional/actions/workflows/build.yaml/badge.svg)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# Toarnbeike.Optional

This package provides a **lightweight and expressive [Option (Maybe) monad](https://en.wikipedia.org/wiki/Option_type) for C#, inspired by functional programming, while remaining idiomatic to the .NET ecosystem.  

An `Option<TValue>` represents a value that can either be there, or be absent, without reling on nulls. 
It improves code clarity and safety by making the **absence of a value explicit**, replacing null checks and exceptions with a functional approach.

## Features

- Fluent API for working with optional values
- Implicit conversion from values and `Option.None`
- Rich functional extensions (Map, Bind, Match, Tap and others)
- Async support for all extensions
- Rich LINQ-style extensions for `IEnumerable<Option<T>>`
- Linq integration (query syntax)
- Test extensions for fluent assertions
- Comprehensive XML documentation and usage examples
- Unit tested with full code coverage
 
---

## Contents
1. [Quick start](#quick-start)
1. [Core concepts](#core-concepts)
1. [Extensions](#extensions)
1. [Collections](#collections)
1. [Linq query syntax](#linq-query-syntax)
1. [Try helpers](#try-helpers)
1. [Test extensions](#test-extensions)
1. [Why Options?](#why-options)

---

## Quick start

This example demonstrates the most common workflow when using Options: construction, transformation and consumption.

```csharp
using Toarnbeike.Optional;			
using Toarnbeike.Optional.Extensions;

Option<string> message = "Hello World!";
Option<string> missing = Option.None;

// Transform the value
var messageLength = message.Map(value => value.Length); // Option<int> with value 12
var newMissing = missing.Map(_ => "Not executed");      // Option<string>, still None.

// Console the value
Console.WriteLine($"Message had {messageLength.Reduce(0)} characters");    // Message had 12 characters
Console.WriteLine($"Missing was replaced by: {newMissing.Reduce("Hello")}; // Missing was replaced by: Hello
```

Key properties of working with Unions:
- `null` values and nullable notition is replaced with `Option`.
- Values are never consumed directly, but always using a `Reduce()` to provide an alternative for a missing value.
- Mapping, binding, etc. leafs the value in an `Option` state, and missing values are still `Option.None`.

---

## Core Concepts

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
| Method			| Returns       | Description								    |
|-------------------|---------------|-----------------------------------------------|
| `AsNullable()`	| `T?`          | Convert to nullable						    |
| `AsOption()`		| `Option<T>`   | Convert from nullable						    |
| `Map(...)`		| `Option<U>`   | Transforms the inner value 			        |
| `Bind(...)`		| `Option<U>`   | Chain operations returning `Option<T>`        |
| `Check(...)`		| `Option<T>`   | Filter by predicate					        |
| `IsSomeAnd(...)`  | `bool`        | Check if value matches value or predicate     |
| `Match(...)`      | `U`           | Pattern match: Some/ None                     |
| `Reduce(...)`		| `T`           | Fallback to a value if empty      		    |
| `OrElse(...)`    	| `Option<T>`   | Return current option or fallback if `None`   |
| `Tap(...)`		| `Option<T>`   | Execute side-effect on value				    |
| `TapIfNone()`		| `Option<T>`   | Execute side-effect when empty                |

All extensions include async overloads and `Task<Union<...>>` variants.

For information per method, see the [Extensions README](docs/Extensions.md).

---

## Collections
The `Toarnbeike.Optional.Collections` namespace contains extension methods to work with `IEnumerable<Option<T>>`:

| Method			  | Returns           | Description								    |
|---------------------|-------------------|---------------------------------------------|
| `Values()`          | `IEnumerable<T>`  | Get all non None values.                    |
| `WhereValues(...)`  | `IEnumerable<T>`  | Get all non None values with predicate      |
| `SelectValues(...)` | `IEnumerable<U>`  | Get all non None values and apply selector  |
| `CountValues()`     | `int`             | Get count of non None values                |
| `AnyValues()`       | `bool`            | Check if collection has any values          |
| `AllValues()`       | `bool`            | Check if collection contains only values    |
| `FirstOrNone()`     | `Option<T>`       | Get first non None value in collection      |
| `LastOrNone()`      | `Option<T>`       | Get last non None value in collection       |

Many of these methods come with predicate overloads to add additional filters, similar to their Linq equivalence.

For information per method, see the [Collections README](docs/Collections.md).

### Collections of T

In addition to extensions on `IEnumerable<Option<T>>`, the `Toarnbeike.Optional.Collections` namespace also contains extension methods that work on `IEnumerable<T>`:

| Method			| Returns       | Description								            |
|-------------------|---------------|-------------------------------------------------------|
| `FirstOrNone()`   | `Option<T>`   | Get the first not null instance or return Option.None |
| `LastOrNone()`    | `Option<T>`   | Get the last not null instance or return Option.None  |
| `SingleOrNone()`  | `Option<T>`   | Get the only instance or return Option.None           |

All these methods also come with predicate overloads to add additional filters.
These methods are also described in more detail in the [Collections README](docs/Collections.md).

---

## Linq query syntax

Toarnbeike.Optional supports optional integration with [C# LINQ query syntax](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/write-linq-queries),
making it easier to compose multiple `Option<T>` computations in a declarative style.

### Why use LINQ Query Syntax?
While method chaining works well for most scenarios, C#`s LINQ query syntax can make some workflows more expressive and readable; especially when you want to:

- Name intermediate options using `let`
- Compose complex Option pipelines in a declarative way
- Avoid deeply nested lambdas in `Bind` and `Map`

### Example:
```csharp
using Toarnbeike.Optional.Linq;

var option =
    from id in GetUserId()
    from user in GetUserById(id)
    let fullName = $"{user.FirstName} {user.LastName}"
    select new UserDto(fullName, user.Email);
```

When comparing that with method chaining, the LINQ query syntax can be more readable, especially for complex workflows:
```csharp
var option = GetUserId()
    .Bind(GetUserById)
    .Map(user =>
    {
        var name = $"{user.FirstName} {user.LastName}";
        return new UserDto(name, user.Email);
    });
```
Which also works, but makes name only available inside the `Map` lambda.

---

## Try Helpers

Use `Option.Try(...)` to wrap exception-throwing code like parsing, config loading, etc.  
Returns `Option.None` when an exception occurs (with optional filtering and logging).

```csharp
var result = Option.Try(() => int.Parse("123")); // Option.Some(123)
var fallback = Option.Try(() => int.Parse("abc")); // Option.None

// With optional filtering and logging
var parsed = Option.Try(
    () => int.Parse("abc"),
    ex => ex is FormatException,
    ex => logger.Warn(ex, "Invalid number")
);
```

---

## Test extensions 

The `Toarnbeike.Optional.TestExtensions` namespace provides simple assertion methods to improve test readability for `Option<T>` instances.
These methods are Test Framework Agnostic, as they throw a custom `AssertionFailedException`. The naming is inspired by [Shouldly](https://docs.shouldly.org/).

The following assertions are included:

| Method                            | Description                                           |
|-----------------------------------|-------------------------------------------------------|
| `ShouldBeSome(...)`               | Asserts that the option is `Some`                     |
| `ShouldBeSomeWithValue(...)`      | Asserts `Some` and compares value with expected.      |
| `ShouldBeSomeThatMatches(...)`    | Asserts `Some` and applies a predicate to the value.  |
| `ShouldBeSomeThatSatisfies(...)`  | Asserts `Some` and applies an additional assertion.   |
| `ShouldBeNone(...)`               | Asserts that the option is `None.`                    |

---

## Why Options?
Because nulls are dangerous and exceptions are control-flow.
Using Option<T>:

- Makes absence explicit
- Forces handling at compile time
- Composes beautifully in pipelines

> Make illegal states unrepresentable.
