using DocumentService.Core.Abstractions;
using FluentAssertions;

namespace DocumentService.Core.Tests.Abstractions;

public class DocumentIdTests
{
    [Fact]
    public void Ctor_NullInput_ThrowsArgumentException()
    {
        var actual = () =>
        {
            _ = new DocumentId(null);
        };

        actual.Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData("Id1", "id1", true)]
    [InlineData("Id1", "id2", false)]
    public void OperatorEquals_CompareIds_ReturnsSuccess(string leftValue, string rightValue, bool expected)
    {
        var leftId = new DocumentId(leftValue);
        var rightId = new DocumentId(rightValue);

        var actual = leftId == rightId;

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetHashCode_NoInput_CreatesHash()
    {
        var price = new DocumentId("SomeId");

        _ = price.GetHashCode();
    }

    [Fact]
    public void GetHashCode_SameValues_CreatesSameHash()
    {
        var sameId = "SameId";
        var id1 = new DocumentId(sameId.ToLower());
        var id2 = new DocumentId(sameId.ToUpper());

        id1.GetHashCode().Should().Be(id2.GetHashCode());
    }
}