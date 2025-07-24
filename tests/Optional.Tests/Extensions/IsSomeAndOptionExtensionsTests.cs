using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="IsSomeAndOptionExtensions"/>
/// </summary>
public class IsSomeAndOptionExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    [Fact]
    public void IsSomeAnd_ShouldReturnTrue_WhenOptionIsSomeAndValueMatches()
    {
        _some.IsSomeAnd(1).ShouldBeTrue();
    }

    [Fact]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsSomeButValueDoesNotMatch()
    {
        _some.IsSomeAnd(2).ShouldBeFalse();
    }

    [Fact]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsNone()
    {
        _none.IsSomeAnd(1).ShouldBeFalse();
    }

    [Fact]
    public void IsSomeAnd_ShouldReturnTrue_WhenOptionIsSomeAndPredicateMatches()
    {
        _some.IsSomeAnd(x => x > 0).ShouldBeTrue();
    }

    [Fact]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsSomeButPredicateDoesNotMatch()
    {
        _some.IsSomeAnd(x => x < 0).ShouldBeFalse();
    }

    [Fact]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsNoneWithPredicate()
    {
        _none.IsSomeAnd(x => x > 0).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAndAsync_ShouldReturnTrue_WhenOptionIsSomeAndValueMatches()
    {
        (await _some.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeTrue();
    }

    [Fact]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionIsSomeButPredicateDoesNotMatch()
    {
        (await _some.IsSomeAndAsync(x => Task.FromResult(x < 0))).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionIsNoneWithPredicate()
    {
        (await _none.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAnd_ShouldReturnTrue_WhenOptionTaskIsSomeAndValueMatches()
    {
        (await _someAsync.IsSomeAnd(1)).ShouldBeTrue();
    }

    [Fact]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsSomeButValueDoesNotMatch()
    {
        (await _someAsync.IsSomeAnd(2)).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsNone()
    {
        (await _noneAsync.IsSomeAnd(1)).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAnd_ShouldReturnTrue_WhenOptionTaskIsSomeAndPredicateMatches()
    {
        (await _someAsync.IsSomeAnd(x => x > 0)).ShouldBeTrue();
    }

    [Fact]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsSomeButPredicateDoesNotMatch()
    {
        (await _someAsync.IsSomeAnd(x => x < 0)).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsNoneWithPredicate()
    {
        (await _noneAsync.IsSomeAnd(x => x > 0)).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAndAsync_ShouldReturnTrue_WhenOptionTaskIsSomeAndValueMatches()
    {
        (await _someAsync.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeTrue();
    }

    [Fact]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionTaskIsSomeButPredicateDoesNotMatch()
    {
        (await _someAsync.IsSomeAndAsync(x => Task.FromResult(x < 0))).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionTaskIsNoneWithPredicate()
    {
        (await _noneAsync.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeFalse();
    }
}
