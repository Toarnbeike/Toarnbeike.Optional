namespace Toarnbeike.Optional.TestExtensions;

/// <summary>
/// Exception thrown when an assertion on an <see cref="Option{TValue}"/> fails.
/// </summary>
/// <param name="message">Custom message regarding the failure</param>
public class OptionAssertionException(string message) : Exception(message) { }