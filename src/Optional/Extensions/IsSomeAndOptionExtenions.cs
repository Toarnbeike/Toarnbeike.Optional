namespace Toarnbeike.Optional.Extensions;

public static class IsSomeAndOptionExtensions
{
    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value is equal to the specified <paramref name="expected"/> value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="option">The option to evaluate.</param>
    /// <param name="expected">The value to compare against the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value equals <paramref name="expected"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool IsSomeAnd<TValue>(this Option<TValue> option, TValue expected) =>
        option.TryGetValue(out var value) && EqualityComparer<TValue>.Default.Equals(value, expected);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value is equal to the specified <paramref name="expected"/> value,
    /// using the specified <paramref name="comparer"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="option">The option to evaluate.</param>
    /// <param name="expected">The value to compare against the option's value.</param>
    /// <param name="comparer">The equality comparer to use for the value comparison.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value is considered equal to <paramref name="expected"/> 
    /// according to <paramref name="comparer"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsSomeAnd<TValue>(this Option<TValue> option, TValue expected, IEqualityComparer<TValue> comparer) =>
        option.TryGetValue(out var value) && comparer.Equals(value, expected);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="option">The option to evaluate.</param>
    /// <param name="predicate">The predicate to test the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static bool IsSomeAnd<TValue>(this Option<TValue> option, Func<TValue, bool> predicate) =>
        option.TryGetValue(out var value) && predicate(value);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="option">The option to evaluate.</param>
    /// <param name="predicate">The predicate to test the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static async Task<bool> IsSomeAndAsync<TValue>(this Option<TValue> option, Func<TValue, Task<bool>> predicate) =>
        option.TryGetValue(out var value) && await predicate(value);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value is equal to the specified <paramref name="expected"/> value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="expected">The value to compare against the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value equals <paramref name="expected"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static async Task<bool> IsSomeAnd<TValue>(this Task<Option<TValue>> optionTask, TValue expected) =>
        IsSomeAnd(await optionTask, expected);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value is equal to the specified <paramref name="expected"/> value,
    /// using the specified <paramref name="comparer"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="expected">The value to compare against the option's value.</param>
    /// <param name="comparer">The equality comparer to use for the value comparison.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value is considered equal to <paramref name="expected"/> 
    /// according to <paramref name="comparer"/>; otherwise, <see langword="false"/>.</returns>
    public static async Task<bool> IsSomeAnd<TValue>(this Task<Option<TValue>> optionTask, TValue expected, IEqualityComparer<TValue> comparer) =>
        IsSomeAnd(await optionTask, expected, comparer);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="predicate">The predicate to test the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static async Task<bool> IsSomeAnd<TValue>(this Task<Option<TValue>> optionTask, Func<TValue, bool> predicate) =>
        IsSomeAnd(await optionTask, predicate);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="predicate">The predicate to test the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    public static async Task<bool> IsSomeAndAsync<TValue>(this Task<Option<TValue>> optionTask, Func<TValue, Task<bool>> predicate) =>
        await IsSomeAndAsync(await optionTask, predicate);
}
