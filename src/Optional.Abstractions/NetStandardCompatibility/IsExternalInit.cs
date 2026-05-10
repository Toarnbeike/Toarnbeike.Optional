using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Runtime.CompilerServices;
#pragma warning restore IDE0130 // Namespace does not match folder structure

// ReSharper disable once UnusedType.Global
/// <summary>
/// Allows records with init-only properties to be used in .NET Standard 2.0 by providing a definition for the <c>IsExternalInit</c> type that the C# compiler looks for when compiling records with init-only properties.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class IsExternalInit;
