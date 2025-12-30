namespace Toarnbeike.Optional.Collections;

/// <summary>
/// Extension methods for working with <see cref="IEnumerable{T}"/> collections.
/// </summary>
public static class EnumerableOptionExtensions
{
    extension<TValue>(IEnumerable<TValue> source)
    {
        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses <see cref="IEnumerable{T}.GetEnumerator"/> to avoid conflict between the first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default because.</remarks>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
        public Option<TValue> FirstOrNone()
        {
            using var enumerator = source.GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current : Option.None;
        }

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses a foreach loop to avoid conflict between the first entity of first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default because. </remarks>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return Option.None;
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the last value of.</param>
        public Option<TValue> LastOrNone()
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return Option<TValue>.None();
            }

            TValue last = enumerator.Current;
            while (enumerator.MoveNext())
            {
                last = enumerator.Current;
            }

            return Option<TValue>.Some(last);
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to return the last value of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue, bool> predicate)
        {
            bool found = false;
            TValue lastMatch = default!;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    lastMatch = item;
                    found = true;
                }
            }

            return found ? lastMatch : Option.None;
        }
    }
}