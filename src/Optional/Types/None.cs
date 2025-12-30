namespace Toarnbeike.Optional.Types;

/// <summary>
/// Marker type representing the absence of a value in <see cref="Option{TValue}"/>.
/// </summary>
/// <remarks>
/// Used internally to support conversion from <see cref="Option.None"/> to <see cref="Option{TValue}.None()"/>.
/// Not intended for use outside the <see cref="Optional"/> namespace.
/// </remarks>
public readonly record struct None;