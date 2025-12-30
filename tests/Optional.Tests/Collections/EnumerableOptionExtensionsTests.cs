using Toarnbeike.Optional.Collections;

namespace Toarnbeike.Optional.Tests.Collections;

/// <summary>
/// Tests are deliberately written to use an <see langword="int"/> because with structs 
/// a distinction is made between default and not having a value.
/// E.g. the first element of an <see cref="IEnumerable{int}"/> might return 0,
/// which is explicitly not the same as <c>Option<int>.None()</c>.
/// </summary>
public class EnumerableOptionExtensionsTests
{
    private readonly IEnumerable<int> _optionsWithValues = [0, 1, 2, 3, 4];
    private readonly IEnumerable<int> _optionsWithoutValues = [];

    private readonly Func<int, bool> _lessThenTwo = x => x < 2;
    private readonly Func<int, bool> _noMatches = x => x > 4;
    private readonly Func<int, bool> _throwingPredicate = _ => throw new ShouldAssertException("Method should not be called.");


    [Test]
    public void FirstOrNone_Should_ReturnFirstValue_WhenExists()
    {
        var first = _optionsWithValues.FirstOrNone();
        first.ShouldBe(0);
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_WhenNoValues()
    {
        var first = _optionsWithoutValues.FirstOrNone();
        first.ShouldBe(Option<int>.None());
    }

    [Test]
    public void FirstOrNone_Should_ReturnFirstValue_MatchingPredicate()
    {
        var first = _optionsWithValues.FirstOrNone(_lessThenTwo);
        first.ShouldBe(0);
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_WhenNoValuesMatchPredicate()
    {
        var first = _optionsWithValues.FirstOrNone(_noMatches);
        first.ShouldBe(Option.None);
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_WhenNoValues_WithoutCheckingPredicate()
    {
        var first = _optionsWithoutValues.FirstOrNone(_throwingPredicate);
        first.ShouldBe(Option.None);
    }

    [Test]
    public void LastOrNone_Should_ReturnLastValue_WhenExists()
    {
        var last = _optionsWithValues.LastOrNone();
        last.ShouldBe(4);
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_WhenNoValues()
    {
        var last = _optionsWithoutValues.LastOrNone();
        last.ShouldBe(Option<int>.None());
    }

    [Test]
    public void LastOrNone_Should_ReturnLastValue_MatchingPredicate()
    {
        var last = _optionsWithValues.LastOrNone(_lessThenTwo);
        last.ShouldBe(1);
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_WhenNoValuesMatchPredicate()
    {
        var last = _optionsWithValues.LastOrNone(_noMatches);
        last.ShouldBe(Option.None);
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_WhenNoValues_WithoutCheckingPredicate()
    {
        var last = _optionsWithoutValues.LastOrNone(_throwingPredicate);
        last.ShouldBe(Option.None);
    }
}