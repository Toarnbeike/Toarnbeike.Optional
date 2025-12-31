namespace Toarnbeike.Optional.Collections;

/// <summary>
/// Extension methods for working with collections of <see cref="Option{TValue}"/>.
/// </summary>
public static class CollectionExtensions
{
    extension<TValue>(IEnumerable<Option<TValue>> source)
    {
        /// <summary>
        /// Returns all elements of the sequence that have a value.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the values of.</param>
        public IEnumerable<TValue> Values() =>
            source.WhereValues(value => true);

        /// <summary>
        /// Returns all elements of the sequence that have a value.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the values of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public IEnumerable<TValue> WhereValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            foreach (var option in source)
            {
                if (option.TryGetValue(out var value) && predicate(value))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Projects the values of the <see cref="Option{TValue}"/> elements in the source sequence  into a new form using
        /// the specified selector function.
        /// </summary>
        /// <typeparam name="TValue">The type of the value contained in the <see cref="Option{TValue}"/> elements.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the selector function.</typeparam>
        /// <param name="source">The sequence of <see cref="Option{TValue}"/> elements to process.</param>
        /// <param name="selector">A transform function to apply to each value contained in the <see cref="Option{TValue}"/> elements.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the results of applying the selector function  to the values of the
        /// <see cref="Option{TValue}"/> elements in the source sequence.</returns>
        public IEnumerable<TResult> SelectValues<TResult>(Func<TValue, TResult> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);

            return source.Values().Select(selector);
        }

        /// <summary>
        /// Returns the number of entries in the sequence that contain a value.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the count of.</param>
        public int CountValues() =>
            source.Count(option => option.HasValue);

        /// <summary>
        /// Returns the number of entries in the sequence that contain a value.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the count of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public int CountValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.Count(option => option.TryGetValue(out var value) && predicate(value));
        }

        /// <summary>
        /// Returns true if there are any non-empty entries in the sequence.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to check if it has any values.</param>
        public bool AnyValues() =>
            source.Any(option => option.HasValue);

        /// <summary>
        /// Returns true if there are any non-empty entries in the sequence.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to check if it has any values.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public bool AnyValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.Any(option => option.TryGetValue(out var value) && predicate(value));
        }

        /// <summary>
        /// Determines whether all elements in the source sequence have a value.
        /// </summary>
        /// <remarks>This method evaluates whether every element in the source sequence satisfies the
        /// condition  that it has a value. If the sequence is empty, the method returns <see langword="true"/>.</remarks>
        /// <returns><see langword="true"/> if all elements in the source sequence have a value;  otherwise, <see langword="false"/>.</returns>
        public bool AllValues() =>
            source.All(option => option.HasValue);

        /// <summary>
        /// Determines whether all elements in the source sequence have a value that satisfy a condition.
        /// </summary>
        /// <remarks>This method evaluates whether every element in the source sequence satisfies the
        /// condition that it has a value and matches the predicate. If the sequence is empty, the method returns <see langword="true"/>.</remarks>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns><see langword="true"/> if all elements in the source sequence have a value;  otherwise, <see langword="false"/>.</returns>
        public bool AllValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.All(option => option.TryGetValue(out var value) && predicate(value));
        }

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.</summary>
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
        public Option<TValue> FirstOrNone() =>
            source.Where(option => option.HasValue).FirstOrDefault(Option.None);

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.</summary>
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.Where(option => option.TryGetValue(out var value) && predicate(value)).FirstOrDefault(Option.None);
        }

        /// <summary>
        /// <summary>Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.</summary>
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
        public Option<TValue> LastOrNone() =>
            source.Where(option => option.HasValue).LastOrDefault(Option.None);

        /// <summary>
        /// <summary>Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.</summary>
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.Where(option => option.TryGetValue(out var value) && predicate(value)).LastOrDefault(Option.None);
        }
    }
}