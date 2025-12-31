namespace Toarnbeike.Optional.Extensions;

public static class TapIfNoneExtensions
{
    /// <param name="option">The option to operate on.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void TapIfNone(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (!option.HasValue)
            {
                action();
            }
        }

        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task TapIfNoneAsync(Func<Task> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (!option.HasValue)
            {
                await action().ConfigureAwait(false);
            }
        }
    }

    /// <param name="optionTask">The task returning an option to operate on.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task TapIfNone(Action action)
        {
            var option = await optionTask.ConfigureAwait(false);
            option.TapIfNone(action);
        }

        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task TapIfNoneAsync(Func<Task> action)
        {
            var option = await optionTask.ConfigureAwait(false);
            await option.TapIfNoneAsync(action).ConfigureAwait(false);
        }
    }
}