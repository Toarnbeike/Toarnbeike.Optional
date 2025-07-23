[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
![Build](https://github.com/toarnbeike/toarnbeike.optional/actions/workflows/build.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/Toarnbeike.Optional.svg)](https://www.nuget.org/packages/Toarnbeike.Optional)

⚠️ This library is currently under active development and subject to change.

# Toarnbeike.Optional

Toarnbeike.Optional is a lightweight and expressive C# implementation of the Option (a.k.a. Maybe) monad, designed to improve safety and clarity when working with values that may or may not be present.

It embraces functional programming paradigms by making the absence of a value explicit and composable—eliminating the need for null checks or exceptions in many scenarios.

> Developed by Toarnbeike for personal and professional use in domain-driven and functional designs.

## Inspiration

Toarnbeike.Optional draws inspiration from the following projects and authors:
- [Zoran Horvat](https://www.youtube.com/@zoran-horvat)
- [nlkl.Optional](https://github.com/nlkl/Optional)
- [DotnetFunctional.Maybe](https://github.com/dotnetfunctional/Maybe)

## Features

- Simple API for creating and consuming optional values
- Implicit conversion operators from values and `Option.None`
- Extensions for working with collections of Option&lt;T&gt;  
- [Upcoming] Extension methods following functional conventions (F#, Haskell)
- [Upcoming] Support for asynchronous operations returning Option&lt;T&gt;
- [Planned] Integration with unit testing frameworks (e.g., Shouldly)
 
## Syntax

A generic option can either contain a value or no value at all. 
Options can be created using the `Some` or `None` methods.
There are also implicit conversion operators to create options from their types, or from `Option.None` shorthand.

```csharp
var option = Option<int>.Some(1);	// An option of an int with the value 1
var option = Option.Some(1);		// An option of an int with the value 1, directly using the type inference
var option = Option<int>.None();	// An option of an int with no value
Option<int> option = 1;			// Implicit conversion from int to Option<int>
Option<int> option = Option.None;	// Implicit conversion from Option.None to Option<int>
```

Conversion back to the original type can be done using the `TryGetValue(out TValue value)` method, which returns a boolean indicating success.
```csharp
var option = Option.Some("Hello");

if (option.TryGetValue(out string value))
{
	Console.WriteLine(value); // Outputs: Hello
}
```

## Collections
To work with collections of options, the library provides extension methods that allow you to manipulate sequences of `Option<T>` in a functional style.

### Working with `IEnumerable<Option<T>>`

The `Toarnbeike.Optional.Collections` namespace provided LINQ-like utilities for sequences of `Option<T>`, making it easier to work with collections of optional values.

#### Available Methods


| Method									| Description																					|
|-------------------------------------------|-----------------------------------------------------------------------------------------------|
| `Values()`								| Returns a sequence of all values contained in the options, excluding `None`					|
| `WhereValues(predicate)`					| Filters the options, returning only those that contain a value								|
| `SelectValues(map)`						| Projects each option to the result of the map function, otherwise omitting the entry			|
| `CountValues()`/`CountValues(predicate)`	| Returns the number of present values, optionally matching a predicate							|
| `AnyValues()`/`AnyValues(predicate)`		| Returns a boolean indicating if there is any present value, optionally matching a predicate	|
| `AllValues()`/`AllValues(predicate)`		| Returns a boolean indicating if all values are present, optionally matching a predicate		|
| `FirstOrNone()`/`FirstOrNone(predicate)`	| Returns the first matching present value, optionally matching a predicate, or `Option.None`	|
| `LastOrNone()`/`LastOrNone(predicate)`	| Returns the last matching present value, optionally matching a predicate, or `Option.None`	|

#### Example Usage

```csharp
using Toarnbeike.Optional.Collections;

var options = new List<Option<int>>([1, Option.None, 3]);

var values = options.Values();					// [1, 3]
var filtered = options.WhereValues(i => i > 1);			// [3]
var doubled = options.SelectValues(i = > i * 2);		// [2,6] 
var evenCount = options.CountValues(i => i % 2 == 0);		// Option.None
var first = options.FirstOrNone(i => i > 2);			// 3 (as Option<int>)
var last = options.LastOrNone(i => i > 0);			// 3 (as Option<int>)
```

These helpers reduce boilerplate when working with `IEnumerable<Option<T>>` and follow familiar LINQ conventions.

### Bonus: Extensions on regular `IEnumerable<T>`

You can also use the `Toarnbeike.Optional.Collections` namespace to extend regular `IEnumerable<T>` with similar methods, allowing you to work with collections of regular values as if they were options.
```csharp
using Toarnbeike.Optional.Collections;
var numbers = new List<int> { 1, 2, 3 };

var first = options.FirstOrNone(i => i > 4);			// Option.None
var last = options.LastOrNone(i => i > 0);			// 3 (as regular int)
```

## Conclusion

This library is designed to streamline working with options in C#, providing robust support for functional programming paradigms.