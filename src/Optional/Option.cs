using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Optional;

/// <summary>
/// Represents an optional value that may or may not be present.
/// Defaults to None, if created using the default(Option{TValue}) struct constructor.
/// </summary>
/// <remarks>The <see cref="Option{TValue}"/> type is a lightweight alternative to nullable types or <see
/// cref="System.Nullable{T}"/>  for representing optional values. It provides methods and operators for safely handling
/// the presence or absence of a value. Use <see cref="Some(TValue)"/> to create an instance with a value, and <see
/// cref="None"/> to create an instance without a value.</remarks>
/// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
[DebuggerDisplay("{DebuggerToString(),nq}")]
public readonly record struct Option<TValue>
{
    /// <summary>
    /// The value of the option if it is present, or null if it is not.
    /// </summary>
    private readonly TValue? _value;

    /// <summary>
    /// Boolean indicating whether the option has a value.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Singleton instance of <see cref="Option{TValue}"/> representing the absence of a value.
    /// </summary>
    private static readonly Option<TValue> _none = new(default, false);

    /// <summary>
    /// Initializes a new instance of the <see cref="Option{TValue}"/> class with the provided value.
    /// </summary>
    /// <remarks>Passing null to <see cref="Some"/> will throw. Use <see cref="Option{TValue}.None"/> instead.</remarks>
    /// <param name="value">The value of the option.</param>
    public static Option<TValue> Some(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        return new Option<TValue>(value, true);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Option{TValue}"/> class with no value.
    /// </summary>
    /// <returns>An <see cref="Option{TValue}"/> instance with no value.</returns>
    public static Option<TValue> None() => _none;

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="TValue"/> to an <see cref="Option{TValue}"/> instance.
    /// </summary>
    /// <remarks>This operator allows seamless conversion of a <typeparamref name="TValue"/> to an <see
    /// cref="Option{TValue}"/>. It is useful for scenarios where optional values are represented using the <see
    /// cref="Option{TValue}"/> type.</remarks>
    /// <param name="value">The value to be wrapped in an <see cref="Option{TValue}"/>. Cannot be null.</param>
    public static implicit operator Option<TValue>(TValue value) => Some(value);

    /// <summary>
    /// Implicitly converts a an <see cref="Option{Types.None}"/> instance to an <see cref="Option{TValue}"/> instance.
    /// </summary>
    /// <remarks>This operator allows seamless conversion from <see cref="Option{Types.None}"/> to <see cref="Option{TValue}"/>.
    /// It is useful for creating an <see cref="Option{TValue}"/> from <see cref="Option.None"/> without providing a <typeparamref name="TValue"/></remarks>
    /// <param name="value">The value to be wrapped in an <see cref="Option{TValue}"/>. Cannot be null.</param>
    public static implicit operator Option<TValue>(Option<Types.None> _) => _none;

    /// <summary>
    /// Private constructor such that the option can only be created through the static methods.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="hasValue"></param>
    private Option(TValue? value, bool hasValue) => (_value, HasValue) = (value, hasValue);

    /// <summary>
    /// Attempts to retrieve the value stored in the current instance.
    /// </summary>
    /// <remarks>This method is useful for safely accessing the value of the instance without throwing an
    /// exception. If the instance does not contain a value, the <paramref name="value"/> parameter will be set to <see
    /// langword="null"/>.</remarks>
    /// <param name="value">When this method returns <see langword="true"/>, contains the value stored in the instance. If the method
    /// returns <see langword="false"/>, the value is <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the instance contains a value; otherwise, <see langword="false"/>.</returns>
    public bool TryGetValue([NotNullWhen(true)] out TValue? value)
    {
        if (HasValue)
        {
            value = _value!;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Determines whether the current <see cref="Option{TValue}"/> contains a value equal to the specified value.
    /// </summary>
    /// <param name="other">The value to compare against the option's inner value.</param>
    /// <returns><see langword="true"/> if the option has a value and it equals <paramref name="other"/>; otherwise, <see langword="false"/>.</returns>
    public bool EqualValue(TValue other) => HasValue && EqualityComparer<TValue>.Default.Equals(_value, other);

    /// <summary>
    /// Value for the debugger display, which shows the value if present or a placeholder if not.
    /// </summary>
    private string DebuggerToString() => TryGetValue(out var value) ? value.ToString() ?? $"Some({typeof(TValue).Name})" : "None";
}