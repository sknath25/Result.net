namespace SResult.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        TalkToMe(null)
             .OnFailure((reason) => { Assert.Equal("Input cannot be null", reason); })
             .OnSuccess(() => { Assert.Fail(); });
    }

    [Fact]
    public void Test2()
    {
        TalkToMe(string.Empty)
            .OnFailure((reason) => { Assert.Equal("Input cannot be null", reason); })
            .OnSuccess(() => { Assert.Fail(); });
    }

    [Fact]
    public void Test3()
    {
        const string expected = "Some value";
        TalkToMe(expected)
            .OnFailure(() => { Assert.Fail(); })
            .OnSuccess((actual) => { Assert.Equal(expected, actual); });

    }

    [Fact]
    public void Test4()
    {
        const int expected = 1;
        var result = Result<int, string>.CreateSuccessResult(expected);
        result
            .OnSuccess((actual) => { Assert.Equal(expected, actual); })
            .OnFailure(() => { Assert.Fail(); });

    }

    [Fact]
    public void Test5()
    {
        const string expected = "Worthless";
        var result = Result<int, string>.CreateFailureResult(expected);
        result
            .OnSuccess(() => { Assert.Fail(); })
            .OnFailure((actual) => { Assert.Equal(expected, actual); });
    }

    [Fact]
    public void Test6()
    {
        try
        {
            var result = Result<string, string>.CreateSuccessResult(null);
            Assert.Fail();
        }
        catch (ArgumentNullException)
        {
            Assert.True(true);
        }
    }

    [Fact]
    public void Test7()
    {
        try
        {
            var result = Result<string, string>.CreateFailureResult(null);
            Assert.Fail();
        }
        catch (ArgumentNullException)
        {
            {
                Assert.True(true);
            }
        }
    }

    private static Result<string, string> TalkToMe(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<string, string>.CreateFailureResult("Input cannot be null");
        }

        return Result<string, string>.CreateSuccessResult(value);
    }
}