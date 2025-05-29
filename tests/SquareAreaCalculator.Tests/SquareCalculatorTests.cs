using FluentAssertions;
using Xunit;

namespace SquareAreaCalculator.Tests;

public class SquareCalculatorTests
{
    [Theory]
    [InlineData(1.0, 1.0)]
    [InlineData(2.0, 4.0)]
    [InlineData(5.0, 25.0)]
    [InlineData(10.0, 100.0)]
    [InlineData(0.5, 0.25)]
    [InlineData(3.14159, 9.869587728)] // Pi as side length
    public void CalculateArea_ValidSideLength_ReturnsCorrectArea(double sideLength, double expectedArea)
    {
        // Act
        var result = SquareCalculator.CalculateArea(sideLength);

        // Assert
        result.Should().BeApproximately(expectedArea, 0.000001);
    }

    [Theory]
    [InlineData(1.0, 1.0)]
    [InlineData(2.5, 6.25)]
    [InlineData(5.5, 30.25)]
    [InlineData(10.75, 115.5625)]
    public void CalculateAreaPrecise_ValidSideLength_ReturnsCorrectArea(double sideLength, double expectedArea)
    {
        // Arrange
        var decimalSideLength = (decimal)sideLength;
        var decimalExpectedArea = (decimal)expectedArea;

        // Act
        var result = SquareCalculator.CalculateAreaPrecise(decimalSideLength);

        // Assert
        result.Should().Be(decimalExpectedArea);
    }

    [Theory]
    [InlineData(1.0, 4.0)]
    [InlineData(2.0, 8.0)]
    [InlineData(5.0, 20.0)]
    [InlineData(10.0, 40.0)]
    [InlineData(0.5, 2.0)]
    public void CalculatePerimeter_ValidSideLength_ReturnsCorrectPerimeter(double sideLength, double expectedPerimeter)
    {
        // Act
        var result = SquareCalculator.CalculatePerimeter(sideLength);

        // Assert
        result.Should().BeApproximately(expectedPerimeter, 0.000001);
    }

    [Theory]
    [InlineData(1.0, 1.41421356)]
    [InlineData(2.0, 2.82842712)]
    [InlineData(5.0, 7.07106781)]
    [InlineData(10.0, 14.14213562)]
    public void CalculateDiagonal_ValidSideLength_ReturnsCorrectDiagonal(double sideLength, double expectedDiagonal)
    {
        // Act
        var result = SquareCalculator.CalculateDiagonal(sideLength);

        // Assert
        result.Should().BeApproximately(expectedDiagonal, 0.00001);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(-5.5)]
    [InlineData(-100.0)]
    public void CalculateArea_NonPositiveSideLength_ThrowsArgumentException(double invalidSideLength)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculateArea(invalidSideLength));
        exception.Message.Should().Contain("Side length must be greater than zero");
        exception.ParamName.Should().Be("sideLength");
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(-5.5)]
    public void CalculateAreaPrecise_NonPositiveSideLength_ThrowsArgumentException(double invalidSideLength)
    {
        // Arrange
        var decimalInvalidSideLength = (decimal)invalidSideLength;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculateAreaPrecise(decimalInvalidSideLength));
        exception.Message.Should().Contain("Side length must be greater than zero");
        exception.ParamName.Should().Be("sideLength");
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(-5.5)]
    public void CalculatePerimeter_NonPositiveSideLength_ThrowsArgumentException(double invalidSideLength)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculatePerimeter(invalidSideLength));
        exception.Message.Should().Contain("Side length must be greater than zero");
        exception.ParamName.Should().Be("sideLength");
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(-5.5)]
    public void CalculateDiagonal_NonPositiveSideLength_ThrowsArgumentException(double invalidSideLength)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculateDiagonal(invalidSideLength));
        exception.Message.Should().Contain("Side length must be greater than zero");
        exception.ParamName.Should().Be("sideLength");
    }

    [Fact]
    public void CalculateArea_NaN_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculateArea(double.NaN));
        exception.Message.Should().Contain("Side length must be a valid number");
        exception.ParamName.Should().Be("sideLength");
    }

    [Fact]
    public void CalculateArea_PositiveInfinity_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculateArea(double.PositiveInfinity));
        exception.Message.Should().Contain("Side length must be a valid number");
        exception.ParamName.Should().Be("sideLength");
    }

    [Fact]
    public void CalculateArea_NegativeInfinity_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculateArea(double.NegativeInfinity));
        exception.Message.Should().Contain("Side length must be a valid number");
        exception.ParamName.Should().Be("sideLength");
    }

    [Fact]
    public void CalculatePerimeter_NaN_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculatePerimeter(double.NaN));
        exception.Message.Should().Contain("Side length must be a valid number");
        exception.ParamName.Should().Be("sideLength");
    }

    [Fact]
    public void CalculateDiagonal_NaN_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => SquareCalculator.CalculateDiagonal(double.NaN));
        exception.Message.Should().Contain("Side length must be a valid number");
        exception.ParamName.Should().Be("sideLength");
    }

    [Fact]
    public void CalculateArea_VeryLargeNumber_ReturnsCorrectResult()
    {
        // Arrange
        double largeSideLength = 1000000.0;
        double expectedArea = 1000000000000.0; // 1 trillion

        // Act
        var result = SquareCalculator.CalculateArea(largeSideLength);

        // Assert
        result.Should().Be(expectedArea);
    }

    [Fact]
    public void CalculateArea_VerySmallNumber_ReturnsCorrectResult()
    {
        // Arrange
        double smallSideLength = 0.001;
        double expectedArea = 0.000001;

        // Act
        var result = SquareCalculator.CalculateArea(smallSideLength);

        // Assert
        result.Should().BeApproximately(expectedArea, 0.000000001);
    }
}
