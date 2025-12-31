namespace Toarnbeike.Optional.Extensions;

public static class AsNullableExtensions
{
    /// <summary>
    /// Convert the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    /// <remarks>Useful at integration boundaries (e.g. EF Core) that require nullable CLR types. </remarks>
    /// <returns>Returns the <see cref="Nullable{TValue}"/> equivalent to the <see cref="Option{TValue}"/>.</returns>
    public static TValue? AsNullable<TValue>(this Option<TValue> option) where TValue : class =>
        option.TryGetValue(out var value) ? value : null;

    /// <summary>
    /// Convert the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    /// <remarks>This is mostly useful for ORM's like EF Core that must work with nullable types </remarks>
    /// <returns>Returns the <see cref="Nullable{TValue}"/> equivalent to the <see cref="Option{TValue}"/>.</returns>
    public static TValue? AsNullableValue<TValue>(this Option<TValue> option) where TValue : struct =>
        option.TryGetValue(out var value) ? value : null;

    /// <summary>
    /// Awaits the task and converts the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    public static async Task<TValue?> AsNullable<TValue>(this Task<Option<TValue>> optionTask) where TValue : class =>
        AsNullable(await optionTask.ConfigureAwait(false));

    /// <summary>
    /// Awaits the task and converts the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    public static async Task<TValue?> AsNullableValue<TValue>(this Task<Option<TValue>> optionTask) where TValue : struct =>
        AsNullableValue(await optionTask.ConfigureAwait(false));
}
