using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="AsOptionExtensions"/> class.
/// </summary>
public class AsOptionExtensionsTests
{
    private readonly string? _some = "abc";
    private readonly string? _none = null;
    private readonly int? _someValue = 0;
    private readonly int? _noneValue = null;

    private readonly Task<string?> _someAsync = Task.FromResult<string?>("abc");
    private readonly Task<string?> _noneAsync = Task.FromResult<string?>(null);
    private readonly Task<int?> _someValueAsync = Task.FromResult<int?>(1);
    private readonly Task<int?> _noneValueAsync = Task.FromResult<int?>(null);

    [Fact]
    public void AsOption_Should_ReturnSome_WhenStringIsNotNull()
    {
        var result = _some.AsOption();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe("abc");
    }

    [Fact]
    public void AsOption_Should_ReturnNone_WhenStringIsNull()
    {
        var result = _none.AsOption();
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task AsOptionAsync_Should_ReturnSome_WhenStringIsNotNull()
    {
        var result = await _someAsync.AsOption();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe("abc");
    }

    [Fact]
    public async Task AsOptionAsync_Should_ReturnNone_WhenStringIsNull()
    {
        var result = await _noneAsync.AsOption();
        result.ShouldBe(Option.None);
    }

    [Fact]
    public void AsOption_Should_ReturnSome_WhenIntIsNotNull()
    {
        var result = _someValue.AsOption();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0);
    }

    [Fact]
    public void AsOption_Should_ReturnNone_WhenIntIsNull()
    {
        var result = _noneValue.AsOption();
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task AsOptionAsync_Should_ReturnSome_WhenIntIsNotNull()
    {
        var result = await _someValueAsync.AsOption();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(1);
    }

    [Fact]
    public async Task AsOptionAsync_Should_ReturnNone_WhenIntIsNull()
    {
        var result = await _noneValueAsync.AsOption();
        result.ShouldBe(Option.None);
    }
}
