namespace Toarnbeike.Optional.Collections;

/// <summary>
/// Extension methods for working with <see cref="IEnumerable{T}"/> collections.
/// </summary>
public static class EnumerableOptionExtensions
{
    /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
    extension<TValue>(IEnumerable<TValue> source)
    {
        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses <see cref="IEnumerable{T}.GetEnumerator"/> to avoid conflict between the first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default because.</remarks>
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
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

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
        public Option<TValue> LastOrNone()
        {
            ArgumentNullException.ThrowIfNull(source);

            return source switch
            {
                // Fast path: IList<T>
                IList<TValue> list => list.Count > 0 ? list[^1] : Option.None,
                // Fast path: IReadOnlyList<T>
                IReadOnlyList<TValue> roList => roList.Count > 0 ? roList[^1] : Option.None,
                // Fallback:
                _ => LastOrNoneFromIEnumerable(source)
            };
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            return source switch
            {
                // Fast path: IList<T> (reverse iteration)
                IList<TValue> list => LastOrNoneFromList(list, predicate),
                // Fast path: IReadOnlyList<T> (reverse iteration)
                IReadOnlyList<TValue> roList => LastOrNoneFromReadOnlyList(roList, predicate),
                // Fallback
                _ => LastOrNoneFromIEnumerable(source, predicate)
            };
        }
    }

    private static Option<TValue> LastOrNoneFromList<TValue>(IList<TValue> list, Func<TValue, bool> predicate)
    {
        for (var i = list.Count - 1; i >= 0; i--)
        {
            var item = list[i];
            if (predicate(item))
            {
                return item;
            }
        }

        return Option.None;
    }

    private static Option<TValue> LastOrNoneFromReadOnlyList<TValue>(IReadOnlyList<TValue> roList, Func<TValue, bool> predicate)
    {
        for (var i = roList.Count - 1; i >= 0; i--)
        {
            var item = roList[i];
            if (predicate(item))
            {
                return item;
            }
        }

        return Option.None;
    }

    private static Option<TValue> LastOrNoneFromIEnumerable<TValue>(IEnumerable<TValue> enumerable)
    {
        using var enumerator = enumerable.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            return Option.None;
        }

        var last = enumerator.Current;
        while (enumerator.MoveNext())
        {
            last = enumerator.Current;
        }

        return last;
    }

    private static Option<TValue> LastOrNoneFromIEnumerable<TValue>(IEnumerable<TValue> enumerable, Func<TValue, bool> predicate)
    {
        var found = false;
        TValue lastMatch = default!;

        foreach (var item in enumerable)
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