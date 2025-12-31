namespace Toarnbeike.Optional.Tests;

public class OptionTests
{
    private const string _testValue = "test";

    [Test]
    public void Some_Should_CreateOptionWithValue()
    {
        var option = Option<string>.Some(_testValue);

        option.HasValue.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Test]
    public void Some_Should_ThrowArgumentNullException_WhenValueIsNull()
    {
        Should.Throw<ArgumentNullException>(() => Option<string>.Some(null!));
    }

    [Test]
    public void None_Should_CreateOptionWithoutValue()
    {
        var option = Option<string>.None();

        option.HasValue.ShouldBeFalse();
        option.TryGetValue(out var _).ShouldBeFalse();
    }

    [Test]
    public void Value_Should_ImplicitlyConvertToOption()
    {
        Option<string> option = _testValue;

        option.HasValue.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Test]
    public void TryGetValue_Should_ReturnTrue_WhenOptionHasValue()
    {
        var option = Option<string>.Some(_testValue);
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Test]
    public void TryGetValue_Should_ReturnFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.TryGetValue(out var actual).ShouldBeFalse();
    }

    [Test]
    public void EqualValue_Should_ReturnTrue_WhenOptionHasEqualValue()
    {
        var option = Option<string>.Some(_testValue);
        option.EqualValue(_testValue).ShouldBeTrue();
    }

    [Test]
    public void EqualValue_Should_ReturnFalse_WhenValuesDoNotMatch()
    {
        var option = Option<string>.Some(_testValue);
        option.EqualValue("something else").ShouldBeFalse();
    }

    [Test]
    public void EqualValue_Should_ReturnFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.EqualValue(_testValue).ShouldBeFalse();
    }

    [Test]
    public void EqualValue_Should_ReturnFalse_WhenComparedWithNull()
    {
        var option = Option<string>.Some("value");
        option.EqualValue(null!).ShouldBeFalse();
    }

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
    public void Equals_Should_UseReferenceEquality_ForSameInstance()
    {
        var option = Option<string>.Some(_testValue);
        option.Equals(option).ShouldBeTrue();
    }

    [Test]
    public void GetHashCode_Should_ReturnZero_IfNone()
    {
        var option1 = Option<string>.None();
        option1.GetHashCode().ShouldBe(0);
    }

    [Test]
    public void DebuggerToString_ReturnsExpectedFormat_WhenSome()
    {
        var option = Option<string>.Some(_testValue);
        var type = typeof(Option<string>);
        var method = type.GetMethod("DebuggerToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var actual = (string)method!.Invoke(option, null)!;
        actual.ShouldBe("Some(test)");
    }

    [Test]
    public void DebuggerToString_ReturnsExpectedFormat_WhenNone()
    {
        var option = Option<string>.None();
        var type = typeof(Option<string>);
        var method = type.GetMethod("DebuggerToString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var actual = (string)method!.Invoke(option, null)!;
        actual.ShouldBe("None");
    }

    /// <summary>
    /// Test class to verify behavior of <see cref="Option{TValue}.ToString()"/> when <c>TValue.ToString()</c> returns <see langword="null"/>.
    /// </summary>
    private class NullToStringObject
    {
        public override string? ToString() => null;
    }
}
