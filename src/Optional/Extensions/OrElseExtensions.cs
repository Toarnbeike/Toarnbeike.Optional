namespace Toarnbeike.Optional.Extensions;

public static class OrElseExtensions
{
    /// <param name="option">this option to work on.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Option{TValue}, TValue)"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns> The current Option if it has a value; otherwise an Option containing the alternative value. </returns>
        public Option<TValue> OrElse(TValue alternative) =>
            option.TryGetValue(out var value) ? value : Option.Some(alternative);

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Option{TValue}, Func{TValue})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns> The current Option if it has a value; otherwise an Option containing the alternative value. </returns>
        public Option<TValue> OrElse(Func<TValue> alternative)
        {
            ArgumentNullException.ThrowIfNull(alternative);
            return option.TryGetValue(out var value) ? value : Option.Some(alternative());
        }

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.ReduceAsync{TValue}(Option{TValue}, Func{Task{TValue}})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns> The current Option if it has a value; otherwise an Option containing the alternative value. </returns>
        public async Task<Option<TValue>> OrElseAsync(Func<Task<TValue>> alternative)
        {
            ArgumentNullException.ThrowIfNull(alternative);
            return option.TryGetValue(out var value) ? value : Option.Some(await alternative().ConfigureAwait(false));
        }
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Task{Option{TValue}}, TValue)"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns> The current Option if it has a value; otherwise an Option containing the alternative value. </returns>
        public async Task<Option<TValue>> OrElse(TValue alternative)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.OrElse(alternative);
        }

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Task{Option{TValue}}, Func{TValue})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns> The current Option if it has a value; otherwise an Option containing the alternative value. </returns>
        public async Task<Option<TValue>> OrElse(Func<TValue> alternative)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.OrElse(alternative);
        }

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.ReduceAsync{TValue}(Task{Option{TValue}}, Func{Task{TValue}})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns> The current Option if it has a value; otherwise an Option containing the alternative value. </returns>
        public async Task<Option<TValue>> OrElseAsync(Func<Task<TValue>> alternative)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.OrElseAsync(alternative).ConfigureAwait(false);
        }
    }
}