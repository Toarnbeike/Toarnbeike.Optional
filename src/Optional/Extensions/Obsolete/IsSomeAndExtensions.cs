namespace Toarnbeike.Optional.Extensions;

[Obsolete("Will be removed in the next version. Use TextExtensions.ShouldBeSome() for assertions, and Reduce() or Match() when closing the Option<> Monad.")]
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
        [Obsolete("Will be removed in the next version. Use TextExtensions.ShouldBeSome() for assertions, and Reduce() or Match() when closing the Option<> Monad.")]
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
        [Obsolete("Will be removed in the next version. Use TextExtensions.ShouldBeSome() for assertions, and Reduce() or Match() when closing the Option<> Monad.")]
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
        [Obsolete("Will be removed in the next version. Use TextExtensions.ShouldBeSome() for assertions, and Reduce() or Match() when closing the Option<> Monad.")]
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
        [Obsolete("Will be removed in the next version. Use TextExtensions.ShouldBeSome() for assertions, and Reduce() or Match() when closing the Option<> Monad.")]
        public async Task<bool> IsSomeAndAsync(Func<TValue, Task<bool>> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.IsSomeAndAsync(predicate).ConfigureAwait(false);
        }
    }
}