using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="IsSomeAndExtensions"/>
/// </summary>
public class IsSomeAndExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    [Test]
    [Obsolete("Tested code has become obsolete")]
    public void IsSomeAnd_ShouldReturnTrue_WhenOptionIsSomeAndValueMatches()
    {
        _some.IsSomeAnd(1).ShouldBeTrue();
    }

    [Test]
    [Obsolete("Tested code has become obsolete")]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsSomeButValueDoesNotMatch()
    {
        _some.IsSomeAnd(2).ShouldBeFalse();
    }

    [Test]
    [Obsolete("Tested code has become obsolete")]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsNone()
    {
        _none.IsSomeAnd(1).ShouldBeFalse();
    }

    [Test]
    public void IsSomeAnd_ShouldReturnTrue_WhenOptionIsSomeAndPredicateMatches()
    {
        _some.IsSomeAnd(x => x > 0).ShouldBeTrue();
    }

    [Test]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsSomeButPredicateDoesNotMatch()
    {
        _some.IsSomeAnd(x => x < 0).ShouldBeFalse();
    }

    [Test]
    public void IsSomeAnd_ShouldReturnFalse_WhenOptionIsNoneWithPredicate()
    {
        _none.IsSomeAnd(x => x > 0).ShouldBeFalse();
    }

    [Test]
    public async Task IsSomeAndAsync_ShouldReturnTrue_WhenOptionIsSomeAndValueMatches()
    {
        (await _some.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeTrue();
    }

    [Test]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionIsSomeButPredicateDoesNotMatch()
    {
        (await _some.IsSomeAndAsync(x => Task.FromResult(x < 0))).ShouldBeFalse();
    }

    [Test]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionIsNoneWithPredicate()
    {
        (await _none.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeFalse();
    }

    [Test]
    [Obsolete("Tested code has become obsolete")]
    public async Task IsSomeAnd_ShouldReturnTrue_WhenOptionTaskIsSomeAndValueMatches()
    {
        (await _someAsync.IsSomeAnd(1)).ShouldBeTrue();
    }

    [Test]
    [Obsolete("Tested code has become obsolete")]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsSomeButValueDoesNotMatch()
    {
        (await _someAsync.IsSomeAnd(2)).ShouldBeFalse();
    }

    [Test]
    [Obsolete("Tested code has become obsolete")]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsNone()
    {
        (await _noneAsync.IsSomeAnd(1)).ShouldBeFalse();
    }

    [Test]
    public async Task IsSomeAnd_ShouldReturnTrue_WhenOptionTaskIsSomeAndPredicateMatches()
    {
        (await _someAsync.IsSomeAnd(x => x > 0)).ShouldBeTrue();
    }

    [Test]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsSomeButPredicateDoesNotMatch()
    {
        (await _someAsync.IsSomeAnd(x => x < 0)).ShouldBeFalse();
    }

    [Test]
    public async Task IsSomeAnd_ShouldReturnFalse_WhenOptionTaskIsNoneWithPredicate()
    {
        (await _noneAsync.IsSomeAnd(x => x > 0)).ShouldBeFalse();
    }

    [Test]
    public async Task IsSomeAndAsync_ShouldReturnTrue_WhenOptionTaskIsSomeAndValueMatches()
    {
        (await _someAsync.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeTrue();
    }

    [Test]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionTaskIsSomeButPredicateDoesNotMatch()
    {
        (await _someAsync.IsSomeAndAsync(x => Task.FromResult(x < 0))).ShouldBeFalse();
    }

    [Test]
    public async Task IsSomeAndAsync_ShouldReturnFalse_WhenOptionTaskIsNoneWithPredicate()
    {
        (await _noneAsync.IsSomeAndAsync(x => Task.FromResult(x > 0))).ShouldBeFalse();
    }
}
