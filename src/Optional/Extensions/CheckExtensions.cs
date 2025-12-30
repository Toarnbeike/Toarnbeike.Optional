namespace Toarnbeike.Optional.Extensions;

public static class CheckExtensions
{
    /// <param name="option">The option this method is applied to.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public Option<TValue> Check(Func<TValue, bool> predicate) =>
            !option.TryGetValue(out var value) ? Option.None : predicate(value) ? option : Option.None;

        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.        
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate) =>
            !option.TryGetValue(out var value) ? Option.None : await predicate(value).ConfigureAwait(false) ? option : Option.None;
    }

    /// <param name="optionTask">The Task{Option} this method is applied to.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> Check(Func<TValue, bool> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Check(predicate);
        }

        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.CheckAsync(predicate).ConfigureAwait(false);
        }
    }
}