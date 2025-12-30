using Toarnbeike.Optional.Extensions.Unsafe;

namespace Toarnbeike.Optional.Tests.Extensions.Unsafe;

/// <summary>
/// Tests for the <see cref="ReduceOrThrowExtensions"/> class.
/// </summary>
public class ReduceOrThrowExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    [Test]
    public void ReduceOrThrow_ShouldReturnValue_WhenOptionIsSome()
    {
        _some.ReduceOrThrow().ShouldBe(1);
    }

    [Test]
    public void ReduceOrThrow_ShouldThrow_WhenOptionIsNone()
    {
        Should.Throw<InvalidOperationException>(() => _none.ReduceOrThrow())
            .Message.ShouldBe("Option has no value");
    }

    [Test]
    public void ReduceOrThrow_ShouldThrow_WhenOptionIsNone_WithCustomMessage()
    {
        Should.Throw<InvalidOperationException>(() => _none.ReduceOrThrow("Custom error message"))
            .Message.ShouldBe("Custom error message");
    }

    [Test]
    public async Task ReduceOrThrow_ShouldReturnValue_WhenOptionTaskIsSome()
    {
        (await _someAsync.ReduceOrThrowAsync()).ShouldBe(1);
    }

    [Test]
    public async Task ReduceOrThrow_ShouldThrow_WhenOptionTaskIsNone()
    {
        (await Should.ThrowAsync<InvalidOperationException>(async () => await _noneAsync.ReduceOrThrowAsync()))
            .Message.ShouldBe("Option has no value");
    }

    [Test]
    public async Task ReduceOrThrow_ShouldThrow_WhenOptionTaskIsNone_WithCustomMessage()
    {
        (await Should.ThrowAsync<InvalidOperationException>(async () => await _noneAsync.ReduceOrThrowAsync("Custom error message")))
            .Message.ShouldBe("Custom error message");
    }
}
