﻿namespace Toarnbeike.Optional.Extensions;

public static class ReduceOptionExtensions
{
    /// <summary>
    /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
    /// </summary>
    /// <remarks> Similar to <see cref="OrElseOptionExtensions.OrElse{TValue}(Option{TValue}, TValue)"/>, but returns 
    /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
    /// <param name="option">this option to work on.</param>
    /// <param name="orElse">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty.</returns>
    public static TValue Reduce<TValue>(this Option<TValue> option, TValue orElse) =>
        option.TryGetValue(out var value) ? value : orElse;

    /// <summary>
    /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
    /// </summary>
    /// <remarks> Similar to <see cref="OrElseOptionExtensions.OrElse{TValue}(Option{TValue}, Func{TValue})"/>, but returns 
    /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
    /// <param name="option">this option to work on.</param>
    /// <param name="orElseFunction">The function to generate the value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty.</returns>
    public static TValue Reduce<TValue>(this Option<TValue> option, Func<TValue> orElseFunction) =>
        option.TryGetValue(out var value) ? value : orElseFunction();

    /// <summary>
    /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
    /// </summary>
    /// <remarks> Similar to <see cref="OrElseOptionExtensions.OrElseAsync{TValue}(Option{TValue}, Func{Task{TValue}})"/>, but returns 
    /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
    /// <param name="option">this option to work on.</param>
    /// <param name="orElseTask">The task to generate the value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty.</returns>
    public static async Task<TValue> ReduceAsync<TValue>(this Option<TValue> option, Func<Task<TValue>> orElseTask) =>
        option.TryGetValue(out var value) ? value : await orElseTask();

    /// <summary>
    /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
    /// </summary>
    /// <remarks> Similar to <see cref="OrElseOptionExtensions.OrElse{TValue}(Task{Option{TValue}}, TValue)"/>, but returns 
    /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="orElse">The value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty.</returns>
    public static async Task<TValue> Reduce<TValue>(this Task<Option<TValue>> optionTask, TValue orElse) =>
        Reduce(await optionTask, orElse);

    /// <summary>
    /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
    /// </summary>
    /// <remarks> Similar to <see cref="OrElseOptionExtensions.OrElse{TValue}(Task{Option{TValue}}, Func{TValue})"/>, but returns 
    /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="orElseFunc">Function to calculate the value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty.</returns>
    public static async Task<TValue> Reduce<TValue>(this Task<Option<TValue>> optionTask, Func<TValue> orElseFunc) =>
        Reduce(await optionTask, orElseFunc);

    /// <summary>
    /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
    /// </summary>
    /// <remarks> Similar to <see cref="OrElseOptionExtensions.OrElseAsync{TValue}(Task{Option{TValue}}, Func{Task{TValue}})"/>, but returns 
    /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <param name="orElseTask">The task to generate the value to use if this is empty.</param>
    /// <returns>The value if provided, or the alternative if empty.</returns>
    public static async Task<TValue> ReduceAsync<TValue>(this Task<Option<TValue>> optionTask, Func<Task<TValue>> orElseTask) =>
        await ReduceAsync(await optionTask, orElseTask);
}
