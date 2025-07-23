﻿using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="CheckOptionExtensions"/> class.
/// </summary>
public class CheckOptionExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    private readonly Func<int, bool> _checkTrue = x => x > 0;
    private readonly Func<int, bool> _checkFalse = x => x < 0;
    private readonly Func<int, bool> _checkException = x => throw new ShouldAssertException("Func should not be called.");

    private readonly Func<int, Task<bool>> _checkTrueAsync;
    private readonly Func<int, Task<bool>> _checkFalseAsync;
    private readonly Func<int, Task<bool>> _checkExceptionAsync;

    public CheckOptionExtensionsTests()
    {
        _checkTrueAsync = x => Task.FromResult(_checkTrue(x));
        _checkFalseAsync = x => Task.FromResult(_checkFalse(x));
        _checkExceptionAsync = x => Task.FromResult(_checkException(x));
    }

    [Fact]
    public void Check_Should_ReturnSome_WhenOptionIsSome_AndFuncReturnsSome()
    {
        var result = _some.Check(_checkTrue);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(1);
    }

    [Fact]
    public void Check_Should_ReturnNone_WhenOptionIsSome_AndFuncReturnsNone()
    {
        var result = _some.Check(_checkFalse);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public void Check_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = _none.Check(_checkException);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task CheckAsync_Should_ReturnSome_WhenOptionIsSome_AndFuncReturnsSome()
    {
        var result = await _some.CheckAsync(_checkTrueAsync);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(1);
    }

    [Fact]
    public async Task CheckAsync_Should_ReturnNone_WhenOptionIsSome_AndFuncReturnsNone()
    {
        var result = await _some.CheckAsync(_checkFalseAsync);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task CheckAsync_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = await _none.CheckAsync(_checkExceptionAsync);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task Check_Should_ReturnSome_WhenOptionTaskIsSome_AndFuncReturnsSome()
    {
        var result = await _someAsync.Check(_checkTrue);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(1);
    }

    [Fact]
    public async Task Check_Should_ReturnNone_WhenOptionTaskIsSome_AndFuncReturnsNone()
    {
        var result = await _someAsync.Check(_checkFalse);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task Check_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.Check(_checkException);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task CheckAsync_Should_ReturnSome_WhenOptionTaskIsSome_AndFuncReturnsSome()
    {
        var result = await _someAsync.CheckAsync(_checkTrueAsync);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(1);
    }

    [Fact]
    public async Task CheckAsync_Should_ReturnNone_WhenOptionTaskIsSome_AndFuncReturnsNone()
    {
        var result = await _someAsync.CheckAsync(_checkFalseAsync);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task CheckAsync_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.CheckAsync(_checkExceptionAsync);
        result.ShouldBe(Option.None);
    }
}
