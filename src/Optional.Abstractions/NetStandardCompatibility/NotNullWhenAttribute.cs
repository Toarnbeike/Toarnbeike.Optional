#if NETSTANDARD2_0
// ReSharper disable once CheckNamespace
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Diagnostics.CodeAnalysis;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Polyfill attribute to indicate that an output parameter is not null when the method returns a specified boolean value.
/// Does nothing under .NET Standard 2.0, but allows the use of nullable reference types annotations in the public API of this library.
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class NotNullWhenAttribute : Attribute
{
    public NotNullWhenAttribute(bool _) { }
}
#endif