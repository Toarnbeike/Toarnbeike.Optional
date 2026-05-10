namespace Toarnbeike.Optional.Extensions;

public static class AsOptionExtensions
{
    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static Option<TValue> AsOption<TValue>(this TValue? value) 
        where TValue : class =>
        value is not null ? value : Option.None;

    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static async Task<Option<TValue>> AsOption<TValue>(this Task<TValue?> task)
        where TValue : class => 
        (await task.ConfigureAwait(false)).AsOption();

    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static Option<TValue> AsOption<TValue>(this Nullable<TValue> value)
        where TValue : struct =>
        value is not null ? value.Value : Option.None;

    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static async Task<Option<TValue>> AsOption<TValue>(this Task<Nullable<TValue>> task)
        where TValue : struct =>
        (await task.ConfigureAwait(false)).AsOption();
}
