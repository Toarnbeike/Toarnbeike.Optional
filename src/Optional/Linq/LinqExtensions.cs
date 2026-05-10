using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Linq;

/// <summary>
/// LINQ-style extension methods for Option{T}.
/// Enables usage in query expressions.
/// </summary>
public static class OptionLinqExtensions
{
    extension<TSource>(Option<TSource> source)
    {
        /// <summary>
        /// Maps the value of the option to a new value if present.
        /// </summary>
        public Option<TResult> Select<TResult>(Func<TSource, TResult> selector) =>
            source.Map(selector);

        /// <summary>
        /// Overload for SelectMany to support projection in query expressions.
        /// </summary>
        public Option<TResult> SelectMany<TIntermediate, TResult>(Func<TSource, Option<TIntermediate>> bind,
            Func<TSource, TIntermediate, TResult> project) =>
            source.Bind(s => bind(s).Map(i => project(s, i)));

        /// <summary>
        /// Filters the option based on a predicate.
        /// Equivalent to Check in your API.
        /// </summary>
        public Option<TSource> Where(Func<TSource, bool> predicate) =>
            source.Check(predicate);
    }
}