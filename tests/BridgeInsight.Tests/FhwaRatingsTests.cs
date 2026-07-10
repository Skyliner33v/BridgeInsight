using BridgeInsight.Reference;
using Xunit;

namespace BridgeInsight.Tests;

public class FhwaRatingsTests
{
    [Theory]
    [InlineData(9, "Excellent")]
    [InlineData(7, "Good")]
    [InlineData(5, "Fair")]
    [InlineData(4, "Poor")]
    [InlineData(2, "Critical")]
    [InlineData(0, "Failed")]
    public void GetRatingLabel_KnownRatings_ReturnsLabel(int rating, string expected)
    {
        Assert.Equal(expected, FhwaRatings.GetRatingLabel(rating));
    }

    [Theory]
    [InlineData(null)]
    [InlineData(10)]  // above the 0-9 scale
    [InlineData(-1)]  // below the 0-9 scale
    public void GetRatingLabel_MissingOrOutOfRange_ReturnsNA(int? rating)
    {
        Assert.Equal("N/A", FhwaRatings.GetRatingLabel(rating));
    }

    [Fact]
    public void GetRatingDescription_KnownRating_ReturnsFullDescription()
    {
        var description = FhwaRatings.GetRatingDescription(4);

        Assert.StartsWith("Poor Condition", description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(11)]
    public void GetRatingDescription_MissingOrUnknown_ReturnsNotRated(int? rating)
    {
        Assert.Equal("Not Rated", FhwaRatings.GetRatingDescription(rating));
    }

    [Theory]
    [InlineData(9, "rating-good")]
    [InlineData(8, "rating-good")]
    [InlineData(7, "rating-good")]
    [InlineData(6, "rating-fair")]
    [InlineData(5, "rating-fair")]
    [InlineData(4, "rating-poor")]
    [InlineData(3, "rating-critical")]
    [InlineData(1, "rating-critical")]
    [InlineData(0, "rating-critical")]
    public void GetRatingCssClass_MapsRatingBands(int rating, string expected)
    {
        Assert.Equal(expected, FhwaRatings.GetRatingCssClass(rating));
    }

    [Fact]
    public void GetRatingCssClass_Null_ReturnsNa()
    {
        Assert.Equal("rating-na", FhwaRatings.GetRatingCssClass(null));
    }

    [Fact]
    public void ConditionScales_CoverAllTenRatingCodes()
    {
        for (var rating = 0; rating <= 9; rating++)
        {
            Assert.True(FhwaRatings.ConditionRatings.ContainsKey(rating));
            Assert.True(FhwaRatings.ConditionLabels.ContainsKey(rating));
        }
    }

    [Fact]
    public void GetFullReferenceText_IncludesConditionAndScourScales()
    {
        var text = FhwaRatings.GetFullReferenceText();

        Assert.Contains("FHWA NBI Condition Rating Scale", text);
        Assert.Contains("Structurally Deficient", text);
        Assert.Contains("Scour Critical Bridges (Item 113)", text);
    }
}
