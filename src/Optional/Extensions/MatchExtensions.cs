namespace Toarnbeike.Optional.Extensions;

public static class MatchExtensions
{
    /// <param name="option">this option to work on.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Option<TIn> option)
    {
        /// <summary>
        /// Matches the option and produces a value of type <typeparamref name="TOut"/>.
        /// </summary>
        /// <remarks> Only the matching function is executed. </remarks>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>An instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public TOut Match<TOut>(Func<TIn, TOut> whenSome, Func<TOut> whenNone) =>
            option.TryGetValue(out var value) ? whenSome(value) : whenNone();

        /// <summary>
        /// Matches the option and produces a value of type <typeparamref name="TOut"/>.
        /// </summary>
        /// <remarks> Only the matching function is executed. </remarks>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>A Task of an instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public async Task<TOut> MatchAsync<TOut>(Func<TIn, Task<TOut>> whenSome, Func<Task<TOut>> whenNone) =>
            option.TryGetValue(out var value) ? await whenSome(value).ConfigureAwait(false) : await whenNone().ConfigureAwait(false);
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Task<Option<TIn>> optionTask)
    {
        /// <summary>
        /// Matches the option and produces a value of type <typeparamref name="TOut"/>.
        /// </summary>
        /// <remarks> Only the matching function is executed. </remarks>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>A Task of an instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public async Task<TOut> Match<TOut>(Func<TIn, TOut> whenSome, Func<TOut> whenNone)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Match(whenSome, whenNone);
        }

        /// <summary>
        /// Matches the option and produces a value of type <typeparamref name="TOut"/>.
        /// </summary>
        /// <remarks> Only the matching function is executed. </remarks>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>A Task of an instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public async Task<TOut> MatchAsync<TOut>(Func<TIn, Task<TOut>> whenSome, Func<Task<TOut>> whenNone)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.MatchAsync(whenSome, whenNone).ConfigureAwait(false);
        }
    }
}
