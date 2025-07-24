namespace Toarnbeike.Optional.Extensions;

public static class OrElseOptionExtensions
{
    /// <summary>
    /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
    /// </summary>
    /// <remarks> Similar to <see cref="ReduceOptionExtensions.Reduce{TValue}(Option{TValue}, TValue)"/>, but returns 
    /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
    /// <param name="option">this option to work on.</param>
    /// <param name="alternative">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"></returns>
    public static Option<TValue> OrElse<TValue>(this Option<TValue> option, TValue alternative) =>
        option.TryGetValue(out var value) ? value : alternative;

    /// <summary>
    /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
    /// </summary>
    /// <remarks> Similar to <see cref="ReduceOptionExtensions.Reduce{TValue}(Option{TValue}, Func{TValue})"/>, but returns 
    /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
    /// <param name="option">this option to work on.</param>
    /// <param name="alternative">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"></returns>
    public static Option<TValue> OrElse<TValue>(this Option<TValue> option, Func<TValue> alternative) =>
        option.TryGetValue(out var value) ? value : alternative();

    /// <summary>
    /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
    /// </summary>
    /// <remarks> Similar to <see cref="ReduceOptionExtensions.ReduceAsync{TValue}(Option{TValue}, Func{Task{TValue}})"/>, but returns 
    /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
    /// <param name="option">this option to work on.</param>
    /// <param name="alternative">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"></returns>
    public static async Task<Option<TValue>> OrElseAsync<TValue>(this Option<TValue> option, Func<Task<TValue>> alternative) =>
        option.TryGetValue(out var value) ? value : await alternative();

    /// <summary>
    /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
    /// </summary>
    /// <remarks> Similar to <see cref="ReduceOptionExtensions.Reduce{TValue}(Task{Option{TValue}}, TValue)"/>, but returns 
    /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="alternative">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"></returns>
    public static async Task<Option<TValue>> OrElse<TValue>(this Task<Option<TValue>> optionTask, TValue alternative) =>
        OrElse(await optionTask, alternative);

    /// <summary>
    /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
    /// </summary>
    /// <remarks> Similar to <see cref="ReduceOptionExtensions.Reduce{TValue}(Task{Option{TValue}}, Func{TValue})"/>, but returns 
    /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="alternative">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"></returns>
    public static async Task<Option<TValue>> OrElse<TValue>(this Task<Option<TValue>> optionTask, Func<TValue> alternative) =>
        OrElse(await optionTask, alternative);

    /// <summary>
    /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
    /// </summary>
    /// <remarks> Similar to <see cref="ReduceOptionExtensions.ReduceAsync{TValue}(Task{Option{TValue}}, Func{Task{TValue}})"/>, but returns 
    /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="alternative">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"></returns>
    public static async Task<Option<TValue>> OrElseAsync<TValue>(this Task<Option<TValue>> optionTask, Func<Task<TValue>> alternative) =>
        await OrElseAsync(await optionTask, alternative);
}
