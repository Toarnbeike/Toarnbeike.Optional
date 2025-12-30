namespace Toarnbeike.Optional.Extensions;

public static class TapExtensions
{
    /// <param name="option">The option to operate on.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void Tap(Action<TValue> action)
        {
            if (option.TryGetValue(out var value))
            {
                action(value);
            }
        }

        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task TapAsync(Func<TValue, Task> action)
        {
            if (option.TryGetValue(out var value))
            {
                await action(value).ConfigureAwait(false);
            }
        }
    }

    /// <param name="optionTask">The task returning an option to operate on.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task Tap(Action<TValue> action)
        {
            var option = await optionTask.ConfigureAwait(false);
            option.Tap(action);
        }

        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task TapAsync(Func<TValue, Task> action)
        {
            var option = await optionTask.ConfigureAwait(false);
            await option.TapAsync(action).ConfigureAwait(false);
        }
    }
}
