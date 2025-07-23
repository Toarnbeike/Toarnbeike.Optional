using Shouldly;

namespace Toarnbeike.Optional.Tests;

public class OptionOfTValueTests
{
    private const string _testValue = "test";

    [Fact]
    public void Some_Should_CreateOptionWithValue()
    {
        var option = Option<string>.Some(_testValue);

        option.HasValue.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Fact]
    public void Some_Should_ThrowArgumentNullException_WhenValueIsNull()
    {
        Should.Throw<ArgumentNullException>(() => Option<string>.Some(null!));
    }

    [Fact]
    public void None_Should_CreateOptionWithoutValue()
    {
        var option = Option<string>.None();

        option.HasValue.ShouldBeFalse();
        option.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public void Value_Should_ImplicitlyConvertToOption()
    {
        Option<string> option = _testValue;

        option.HasValue.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Fact]
    public void TryGetValue_Should_ReturnTrue_WhenOptionHasValue()
    {
        var option = Option<string>.Some(_testValue);
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Fact]
    public void TryGetValue_Should_ReturnFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.TryGetValue(out var actual).ShouldBeFalse();
    }

    [Fact]
    public void EqualValue_Should_ReturnTrue_WhenOptionHasEqualValue()
    {
        var option = Option<string>.Some(_testValue);
        option.EqualValue(_testValue).ShouldBeTrue();
    }

    [Fact]
    public void EqualValue_Should_ReturnFalse_WhenValuesDoNotMatch()
    {
        var option = Option<string>.Some(_testValue);
        option.EqualValue("something else").ShouldBeFalse();
    }

    [Fact]
    public void EqualValue_Should_ReturnFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.EqualValue(_testValue).ShouldBeFalse();
    }

    [Fact]
    public void EqualValue_Should_ReturnFalse_WhenComparedWithNull()
    {
        var option = Option<string>.Some("value");
        option.EqualValue(null!).ShouldBeFalse();
    }

    [Fact]
    public void ToString_Should_ReturnValue_WhenOptionHasValue()
    {
        var option = Option<string>.Some(_testValue);
        option.ToString().ShouldBe(_testValue);
    }

    [Fact]
    public void ToString_Should_ReturnEmptyString_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.ToString().ShouldBe(string.Empty);
    }

    [Fact]
    public void ToString_Should_ReturnTypeName_WhenValueToStringIsNull()
    {
        var option = Option.Some(new NullToStringObject());
        option.ToString().ShouldBe(nameof(NullToStringObject));
    }

    [Fact]
    public void Equals_Should_ReturnTrue_WhenOptionsHaveSameValue()
    {
        var option1 = Option<string>.Some(_testValue);
        Option<string> option2 = _testValue;

        option1.ShouldBe(option2);
        (option1 == option2).ShouldBeTrue();
        (option1 != option2).ShouldBeFalse();
        option1.Equals(option2).ShouldBeTrue();
        option1.Equals((object)option2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_Should_ReturnTrue_WhenOptionsHaveNoValue()
    {
        var option1 = Option<string>.None();
        Option<string> option2 = Option.None;

        option1.ShouldBe(option2);
        (option1 == option2).ShouldBeTrue();
        (option1 != option2).ShouldBeFalse();
        option1.Equals(option2).ShouldBeTrue();
        option1.Equals((object)option2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_WhenOtherIsNull()
    {
        var option1 = Option<string>.None();
        Option<string>? option2 = null;

        option1.ShouldNotBe(option2);
        (option1 == option2).ShouldBeFalse();
        (option1 != option2).ShouldBeTrue();
        option1.Equals(option2).ShouldBeFalse();
        option1.Equals((object?)option2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_WhenOptionsHaveDifferentValues()
    {
        var option1 = Option<string>.Some(_testValue);
        Option<string> option2 = "different value";

        option1.ShouldNotBe(option2);
        (option1 == option2).ShouldBeFalse();
        (option1 != option2).ShouldBeTrue();
        option1.Equals(option2).ShouldBeFalse();
        option1.Equals((object)option2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_WhenOneIsSome_AndOtherIsNone()
    {
        Option<string> option1 = Option.None;
        Option<string> option2 = "different value";

        option1.ShouldNotBe(option2);
        (option1 == option2).ShouldBeFalse();
        (option1 != option2).ShouldBeTrue();
        option1.Equals(option2).ShouldBeFalse();
        option1.Equals((object)option2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_Should_UseReferenceEquality_ForSameInstance()
    {
        var option = Option<string>.Some(_testValue);
        option.Equals(option).ShouldBeTrue();
    }

    [Fact]
    public void GetHashCode_Should_ReturnHashCodeOfInnerValue_IfSome()
    {
        var option1 = Option<string>.Some(_testValue);
        option1.GetHashCode().ShouldBe(_testValue.GetHashCode());
    }

    [Fact]
    public void GetHashCode_Should_ReturnZero_IfNone()
    {
        var option1 = Option<string>.None();
        option1.GetHashCode().ShouldBe(0);
    }

    /// <summary>
    /// Test class to verify behavior of <see cref="Option{TValue}.ToString()"/> when <c>TValue.ToString()</c> returns <see langword="null"/>.
    /// </summary>
    private class NullToStringObject
    {
        public override string? ToString() => null;
    }
}
