namespace Toarnbeike.Optional.TestExtensions.Tests;

/// <summary>
/// Tests for the <see cref="OptionAssertions"/> class.
/// </summary>
public class OptionAssertionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Option<string> _someForComparer = "hello";
    private readonly Option<string> _noneForComparer = Option.None;
    private readonly IEqualityComparer<string> _caseInsensitive = new CaseInsensitiveStringComparer();

    [Fact]
    public void ShouldBeSome_Should_ReturnTValue_WhenOptionIsSome()
    {
        var result = _some.ShouldBeSome();
        result.ShouldBe(1);
    }

    [Fact]
    public void ShouldBeSome_Should_ThrowException_WhenOptionIsNone()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSome());
        exception.Message.ShouldContain($"Option<{typeof(int).Name}>");
        exception.Message.ShouldContain("but it was None.");
    }

    [Fact]
    public void ShouldBeSome_Should_ReturnCustomMessage_WhenProvided()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSome(message));
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ReturnTValue_WhenOptionIsSomeAndValueMatches()
    {
        var result = _some.ShouldBeSomeWithValue(1);
        result.ShouldBe(1);
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ThrowException_WhenOptionIsSomeAndValueDoenstMatch()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _some.ShouldBeSomeWithValue(0));
        exception.Message.ShouldContain($"Option<{typeof(int).Name}>");
        exception.Message.ShouldContain("0"); // expected value
        exception.Message.ShouldContain("but it was not.");
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ThrowException_WhenOptionIsNone()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSomeWithValue(0));
        exception.Message.ShouldContain($"Option<{typeof(int).Name}>");
        exception.Message.ShouldContain("but it was not.");
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ReturnCustomMessage_WhenProvided()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSomeWithValue(1, message));
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ReturnTValue_WhenOptionIsSomeAndValueMatchesCustomComparer()
    {
        var result = _someForComparer.ShouldBeSomeWithValue("HELLO", _caseInsensitive);
        result.ShouldBe("hello");
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ThrowException_WhenOptionIsSomeAndValueDoenstMatchCustomComparer()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _someForComparer.ShouldBeSomeWithValue("Different", _caseInsensitive));
        exception.Message.ShouldContain($"Option<{typeof(string).Name}>");
        exception.Message.ShouldContain("Different"); // expected value
        exception.Message.ShouldContain("but it was hello.");
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ThrowException_WhenOptionIsNone_WithCustomComparer()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _noneForComparer.ShouldBeSomeWithValue("hello", _caseInsensitive));
        exception.Message.ShouldContain($"Option<{typeof(string).Name}>");
        exception.Message.ShouldContain("but it was .");
    }

    [Fact]
    public void ShouldBeSomeWithValue_Should_ReturnCustomMessage_WhenProvidedWithCustomerComparer()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<OptionAssertionException>(() => _noneForComparer.ShouldBeSomeWithValue("hello", _caseInsensitive, message));
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void ShouldBeSomeThatMatches_Should_ReturnTValue_WhenOptionIsSomeAndPredicateMatches()
    {
        var result = _some.ShouldBeSomeThatMatches(value => value > 0);
        result.ShouldBe(1);
    }

    [Fact]
    public void ShouldBeSomeThatMatches_Should_ThrowException_WhenOptionIsSomeAndValueDoenstMatch()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _some.ShouldBeSomeThatMatches(value => value < 0));
        exception.Message.ShouldContain($"Option<{typeof(int).Name}>");
        exception.Message.ShouldContain("to match predicate, but it didn't.");
    }

    [Fact]
    public void ShouldBeSomeThatMatches_Should_ThrowException_WhenOptionIsNone()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSomeThatMatches(value => value > 0));
        exception.Message.ShouldContain($"Option<{typeof(int).Name}>");
        exception.Message.ShouldContain("to match predicate, but it didn't.");
    }

    [Fact]
    public void ShouldBeSomeThatMatches_Should_ReturnCustomMessage_WhenProvided()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSomeThatMatches(value => value > 0, message));
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void ShouldBeSomeAndSatisfy_Should_ReturnTValue_WhenOptionIsSomeAndAssertPasses()
    {
        var result = _some.ShouldBeSomeAndSatisfy(value => value.ShouldBeGreaterThan(0));
        result.ShouldBe(1);
    }

    [Fact]
    public void ShouldBeSomeAndSatisfy_Should_ThrowException_WhenOptionIsSomeAndAssertFails()
    {
        // no asserts on message, message is provided by ShouldBeGreaterThan from Shouldly and might change
        Should.Throw<Exception>(() => _some.ShouldBeSomeAndSatisfy(value => value.ShouldBe(2)));
    }

    [Fact]
    public void ShouldBeSomeAndSatisfy_Should_ThrowException_WhenOptionIsNone()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSomeAndSatisfy(value => value.ShouldBeOfType<int>()));
        exception.Message.ShouldContain($"Option<{typeof(int).Name}>");
        exception.Message.ShouldContain("but it was None.");
    }

    [Fact]
    public void ShouldBeSomeAndSatisfy_Should_ReturnCustomMessage_WhenProvided()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<OptionAssertionException>(() => _none.ShouldBeSomeAndSatisfy(value => value.ShouldBeOfType<int>(), message));
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void ShouldBeNone_Should_NotThrow_WhenOptionIsNone()
    {
        Should.NotThrow(() => _none.ShouldBeNone());
    }

    [Fact]
    public void ShouldBeNone_Should_ThrowException_WhenOptionIsSome()
    {
        var exception = Should.Throw<OptionAssertionException>(() => _some.ShouldBeNone());
        exception.Message.ShouldContain($"Option<{typeof(int).Name}>");
        exception.Message.ShouldContain("but it was Some with value 1.");
    }

    [Fact]
    public void ShouldBeNone_Should_ReturnCustomMessage_WhenProvided()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<OptionAssertionException>(() => _some.ShouldBeNone(message));
        exception.Message.ShouldBe(message);
    }

    private sealed class CaseInsensitiveStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y) =>
            string.Equals(x, y, StringComparison.OrdinalIgnoreCase);

        public int GetHashCode(string obj) =>
            obj.ToLowerInvariant().GetHashCode();
    }
}
