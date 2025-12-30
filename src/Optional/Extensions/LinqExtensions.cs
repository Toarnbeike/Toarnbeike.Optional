namespace Toarnbeike.Optional.Extensions;

/// <summary>
/// LINQ-style extension methods for Option{T}.
/// Enables usage in query expressions.
/// </summary>
public static class OptionLinqExtensions
{
    /// <summary>
    /// Maps the value of the option to a new value if present.
    /// </summary>
    public static Option<TResult> Select<TSource, TResult>(
        this Option<TSource> source,
        Func<TSource, TResult> selector) =>
        source.Map(selector);

    /// <summary>
    /// Flattens nested options and binds to a new option.
    /// Enables LINQ's "from ... in ... select" query syntax.
    /// </summary>
    public static Option<TResult> SelectMany<TSource, TResult>(
        this Option<TSource> source,
        Func<TSource, Option<TResult>> selector) =>
        source.Bind(selector);

    /// <summary>
    /// Overload for SelectMany to support projection in query expressions.
    /// </summary>
    public static Option<TResult> SelectMany<TSource, TIntermediate, TResult>(
        this Option<TSource> source,
        Func<TSource, Option<TIntermediate>> bind,
        Func<TSource, TIntermediate, TResult> project) =>
        source.Bind(s => bind(s).Map(i => project(s, i)));

    /// <summary>
    /// Filters the option based on a predicate.
    /// Equivalent to Check in your API.
    /// </summary>
    public static Option<TSource> Where<TSource>(
        this Option<TSource> source,
        Func<TSource, bool> predicate) =>
        source.Check(predicate);
}