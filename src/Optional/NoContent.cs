using System.ComponentModel;

namespace Toarnbeike.Optional;

/// <summary>
/// Marker type representing the absence of a value in <see cref="Option{TValue}"/>.
/// </summary>
/// <remarks>
/// Used internally to support conversion from <see cref="Option.None"/> to <see cref="Option{TValue}.None()"/>.
/// Not intended for use outside the <see cref="Toarnbeike.Optional"/> namespace.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed record NoContent;
