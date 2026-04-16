namespace Toarnbeike.Optional.TestExtensions;

/// <summary>
/// Provides assertion methods for Option{TValue} to use in tests.
/// </summary>
public static class TestExtensions
{
    /// <param name="option">The option to assert on.</param>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Asserts that the option is Some; throws an exception if it is None.
        /// </summary>
        /// <param name="message">Optional custom failure message.</param>
        /// <returns>The inner value if present.</returns>
        /// <exception cref="AssertionFailedException">Thrown if the Option is None.</exception>
        public TValue ShouldBeSome(string? message = null)
        {
            return option.TryGetValue(out var value)
                ? value
                : throw new AssertionFailedException(message ?? $"Expected Option<{typeof(TValue).Name}> to be Some, but it was None.");
        }

        /// <summary>
        /// Asserts that the option is None; throws an exception if it is Some.
        /// </summary>
        /// <param name="message">Optional custom failure message.</param>
        /// <exception cref="AssertionFailedException">Thrown if the Option is Some.</exception>
        public void ShouldBeNone(string? message = null)
        {
            if (option.TryGetValue(out var value))
            {
                throw new AssertionFailedException(message ?? $"Expected Option<{typeof(TValue).Name}> to be None, but it was Some with value {value}.");
            }
        }

        /// <summary>
        /// Asserts that the option is the expected value; throws an exception if it is None or a different value.
        /// </summary>
        /// <param name="expected">The expected value of the option.</param>
        /// <param name="message">Optional custom failure message.</param>
        /// <returns>The inner value if present.</returns>
        /// <exception cref="AssertionFailedException">Thrown if the Option is None or Some other than expected.</exception>
        [Obsolete("Optional.TestExtensions is not an assertion library. Use ShouldBeSome().ShouldBe(expected).")]
        public TValue ShouldBeSomeWithValue(TValue expected, string? message = null)
        {
            return option.TryGetValue(out var value) && EqualityComparer<TValue>.Default.Equals(value, expected)
                ? value
                : throw new AssertionFailedException(message ?? $"Expected Option<{typeof(TValue).Name}> to be {expected}, but it was {value}.");
        }

        /// <summary>
        /// Asserts that the option is the expected value; throws an exception if it is None or a different value.
        /// </summary>
        /// <param name="expected">The expected value of the option.</param>
        /// <param name="comparer">The equality comparer to use for the value comparison.</param>
        /// <param name="message">Optional custom failure message.</param>
        /// <returns>The inner value if present.</returns>
        /// <exception cref="AssertionFailedException">Thrown if the Option is None or Some other than expected.</exception>
        [Obsolete("Optional.TestExtensions is not an assertion library. Use ShouldBeSome().ShouldBe(expected).")]
        public TValue ShouldBeSomeWithValue(TValue expected, IEqualityComparer<TValue> comparer, string? message = null)
        {
            return option.TryGetValue(out var value) && comparer.Equals(value, expected)
                ? value
                : throw new AssertionFailedException(message ?? $"Expected Option<{typeof(TValue).Name}> to be {expected}, but it was {value}.");
        }

        /// <summary>
        /// Asserts that the option is the expected value; throws an exception if it is None or a different value.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="message">Optional custom failure message.</param>
        /// <returns>The inner value if present.</returns>
        /// <exception cref="AssertionFailedException">Thrown if the Option is None or Some other than expected.</exception>
        [Obsolete("Optional.TestExtensions is not an assertion library. Use ShouldBeSome().Should(predicate).")]
        public TValue ShouldBeSomeThatMatches(Func<TValue, bool> predicate, string? message = null)
        {
            return option.TryGetValue(out var value) && predicate(value)
                ? value
                : throw new AssertionFailedException(message ?? $"Expected Option<{typeof(TValue).Name}> to match predicate, but it didn't.");
        }

        /// <summary>
        /// Asserts that the option is Some and satisfies the specified assertion; throws an exception if it is None or the assertion fails.
        /// </summary>
        /// <param name="assert">Secondary assertion on TVale, given that Option is Some.</param>
        /// <param name="message">Optional custom failure message.</param>
        /// <returns>The inner value if present.</returns>
        /// <exception cref="AssertionFailedException">Thrown if the Option is None.</exception>
        [Obsolete("Optional.TestExtensions is not an assertion library. Use ShouldBeSome().Should(assert).")]
        public TValue ShouldBeSomeAndSatisfy(Action<TValue> assert, string? message = null)
        {
            var value = option.ShouldBeSome(message);
            assert(value);
            return value;
        }
    }
}