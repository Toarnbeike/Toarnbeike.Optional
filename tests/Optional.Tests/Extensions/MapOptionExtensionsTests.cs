using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Tests.Extensions;

/// <summary>
/// Test for the <see cref="MapOptionExtensions"/> class.
/// </summary>
public class MapOptionExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    private readonly Func<int, double> _selectHalf = x => x / 2.0;
    private readonly Func<int, double> _selectException = x => throw new ShouldAssertException("Func should not be called.");

    private readonly Func<int, Task<double>> _selectHalfAsync;
    private readonly Func<int, Task<double>> _selectExceptionAsync;

    public MapOptionExtensionsTests()
    {
        _selectHalfAsync = x => Task.FromResult(_selectHalf(x));
        _selectExceptionAsync = x => Task.FromResult(_selectException(x));
    }

    [Fact]
    public void Map_Should_ReturnSome_WhenOptionIsSome()
    {
        var result = _some.Map(_selectHalf);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public void Map_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = _none.Map(_selectException);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnSome_WhenOptionIsSome()
    {
        var result = await _some.MapAsync(_selectHalfAsync);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = await _none.MapAsync(_selectExceptionAsync);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task Map_Should_ReturnSome_WhenOptionTaskIsSome()
    {
        var result = await _someAsync.Map(_selectHalf);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public async Task Map_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.Map(_selectException);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnSome_WhenOptionTaskIsSome()
    {
        var result = await _someAsync.MapAsync(_selectHalfAsync);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.MapAsync(_selectExceptionAsync);
        result.ShouldBe(Option.None);
    }
}
