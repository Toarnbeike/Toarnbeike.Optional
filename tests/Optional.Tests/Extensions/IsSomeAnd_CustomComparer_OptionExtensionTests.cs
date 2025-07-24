using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="IsSomeAndOptionExtensions"/> with a custom comparer.
/// </summary>
public class IsSomeAnd_CustomComparer_OptionExtensionTests
{
    private readonly Option<string> _some = "hello";
    private readonly Option<string> _none = Option.None;

    private readonly string match = "HELLO";
    private readonly string noMatch = "world";

    private readonly Task<Option<string>> _someAsync = Task.FromResult(Option.Some("hello"));
    private readonly Task<Option<string>> _noneAsync = Task.FromResult(Option<string>.None());

    private readonly IEqualityComparer<string> _caseInsensitive = new CaseInsensitiveStringComparer();

    [Fact]
    public void IsSomeAnd_WithComparer_ReturnsTrue_WhenEqual()
    {
        _some.IsSomeAnd(match, _caseInsensitive).ShouldBeTrue();
    }

    [Fact]
    public void IsSomeAnd_WithComparer_ReturnsFalse_WhenNotEqual()
    {
        _some.IsSomeAnd(noMatch, _caseInsensitive).ShouldBeFalse();
    }

    [Fact]
    public void IsSomeAnd_WithComparer_ReturnsFalse_WhenNone()
    {
        _none.IsSomeAnd(match, _caseInsensitive).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAnd_WithComparer_ReturnsTrue_WhenEqualTask()
    {
        (await _someAsync.IsSomeAnd(match, _caseInsensitive)).ShouldBeTrue();
    }

    [Fact]
    public async Task IsSomeAnd_WithComparer_ReturnsFalse_WhenNotEqualTask()
    {
        (await _someAsync.IsSomeAnd(noMatch, _caseInsensitive)).ShouldBeFalse();
    }

    [Fact]
    public async Task IsSomeAnd_WithComparer_ReturnsFalse_WhenNoneTask()
    {
        (await _noneAsync.IsSomeAnd(match, _caseInsensitive)).ShouldBeFalse();
    }

    private sealed class CaseInsensitiveStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y) =>
            string.Equals(x, y, StringComparison.OrdinalIgnoreCase);

        public int GetHashCode(string obj) =>
            obj.ToLowerInvariant().GetHashCode();
    }
}
