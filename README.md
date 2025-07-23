[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
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
- [Upcoming] Extension methods following functional conventions (F#, Haskell)
- [Upcoming] Support for asynchronous operations returning Option<T>  
- [Upcoming] Extensions for working with collections of Option<T>  
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

## Conclusion

This library is designed to streamline working with options in C#, providing robust support for functional programming paradigms.