﻿using Toarnbeike.Optional.Extensions;

namespace Toarnbeike.Optional.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="BindOptionExtensions"/> class.
/// </summary>
public class BindOptionExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    private readonly Func<int, Option<double>> _selectHalf = x => x > 0 ? x / 2.0 : Option.None;
    private readonly Func<int, Option<double>> _selectNone = x => x < 0 ? x / 2.0 : Option.None;
    private readonly Func<int, Option<double>> _selectException = x => throw new ShouldAssertException("Func should not be called.");

    private readonly Func<int, Task<Option<double>>> _selectHalfAsync;
    private readonly Func<int, Task<Option<double>>> _selectNoneAsync;
    private readonly Func<int, Task<Option<double>>> _selectExceptionAsync;

    public BindOptionExtensionsTests()
    {
        _selectHalfAsync = x => Task.FromResult(_selectHalf(x));
        _selectNoneAsync = x => Task.FromResult(_selectNone(x));
        _selectExceptionAsync = x => Task.FromResult(_selectException(x));
    }

    [Fact]
    public void Bind_Should_ReturnSome_WhenOptionIsSome_AndFuncReturnsSome()
    {
        var result = _some.Bind(_selectHalf);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public void Bind_Should_ReturnNone_WhenOptionIsSome_AndFuncReturnsNone()
    {
        var result = _some.Bind(_selectNone);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public void Bind_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = _none.Bind(_selectException);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnSome_WhenOptionIsSome_AndFuncReturnsSome()
    {
        var result = await _some.BindAsync(_selectHalfAsync);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnNone_WhenOptionIsSome_AndFuncReturnsNone()
    {
        var result = await _some.BindAsync(_selectNoneAsync);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = await _none.BindAsync(_selectExceptionAsync);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task Bind_Should_ReturnSome_WhenOptionTaskIsSome_AndFuncReturnsSome()
    {
        var result = await _someAsync.Bind(_selectHalf);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public async Task Bind_Should_ReturnNone_WhenOptionTaskIsSome_AndFuncReturnsNone()
    {
        var result = await _someAsync.Bind(_selectNone);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task Bind_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.Bind(_selectException);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnSome_WhenOptionTaskIsSome_AndFuncReturnsSome()
    {
        var result = await _someAsync.BindAsync(_selectHalfAsync);
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0.5);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnNone_WhenOptionTaskIsSome_AndFuncReturnsNone()
    {
        var result = await _someAsync.BindAsync(_selectNoneAsync);
        result.ShouldBe(Option.None);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.BindAsync(_selectExceptionAsync);
        result.ShouldBe(Option.None);
    }
}
