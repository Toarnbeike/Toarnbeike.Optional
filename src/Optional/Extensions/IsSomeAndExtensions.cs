using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Optional.Extensions;

public static class IsSomeAndExtensions
{
    /// <param name="option">The option to evaluate.</param>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate to test the option's value.</param>
        /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
        /// otherwise, <see langword="false"/>.</returns>
        public bool IsSomeAnd(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return option.TryGetValue(out var value) && predicate(value);
        }

        /// <summary>
        /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate to test the option's value.</param>
        /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
        /// otherwise, <see langword="false"/>.</returns>
        public async Task<bool> IsSomeAndAsync(Func<TValue, Task<bool>> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return option.TryGetValue(out var value) && await predicate(value).ConfigureAwait(false);
        }
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate to test the option's value.</param>
        /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
        /// otherwise, <see langword="false"/>.</returns>
        public async Task<bool> IsSomeAnd(Func<TValue, bool> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.IsSomeAnd(predicate);
        }

        /// <summary>
        /// Determines whether the option is <c>Some</c> and its value satisfies the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate to test the option's value.</param>
        /// <returns><see langword="true"/> if the option is <c>Some</c> and the value satisfies <paramref name="predicate"/>; 
        /// otherwise, <see langword="false"/>.</returns>
        public async Task<bool> IsSomeAndAsync(Func<TValue, Task<bool>> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.IsSomeAndAsync(predicate).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value is equal to the specified <paramref name="expected"/> value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="option">The option to evaluate.</param>
    /// <param name="expected">The value to compare against the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value equals <paramref name="expected"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    [ExcludeFromCodeCoverage(Justification = "Obsolete")]
    [Obsolete("Will be removed in the next major version. Use IsSomeAnd(Func<TValue, bool> predicate) for filtering Options, or TextExtensions.ShouldlBeSomeWithValue(value) for assertions.")]
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
    [ExcludeFromCodeCoverage(Justification = "Obsolete")]
    [Obsolete("Will be removed in the next major version. Use IsSomeAnd(Func<TValue, bool> predicate) for filtering Options, or TextExtensions.ShouldlBeSomeWithValue(value) for assertions.")]
    public static bool IsSomeAnd<TValue>(this Option<TValue> option, TValue expected, IEqualityComparer<TValue> comparer) =>
        option.TryGetValue(out var value) && comparer.Equals(value, expected);

    /// <summary>
    /// Determines whether the option is <c>Some</c> and its value is equal to the specified <paramref name="expected"/> value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="expected">The value to compare against the option's value.</param>
    /// <returns><see langword="true"/> if the option is <c>Some</c> and the value equals <paramref name="expected"/>; 
    /// otherwise, <see langword="false"/>.</returns>
    [ExcludeFromCodeCoverage(Justification = "Obsolete")]
    [Obsolete("Will be removed in the next major version. Use IsSomeAnd(Func<TValue, bool> predicate) for filtering Options, or TextExtensions.ShouldlBeSomeWithValue(value) for assertions.")]
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
    [ExcludeFromCodeCoverage(Justification = "Obsolete")]
    [Obsolete("Will be removed in the next major version. Use IsSomeAnd(Func<TValue, bool> predicate) for filtering Options, or TextExtensions.ShouldlBeSomeWithValue(value) for assertions.")]
    public static async Task<bool> IsSomeAnd<TValue>(this Task<Option<TValue>> optionTask, TValue expected, IEqualityComparer<TValue> comparer) =>
        IsSomeAnd(await optionTask, expected, comparer);

}