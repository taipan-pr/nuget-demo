namespace SquareAreaCalculator;

/// <summary>
/// Provides methods for calculating geometric properties of squares.
/// </summary>
public static class SquareCalculator
{
    /// <summary>
    /// Calculates the area of a square given its side length.
    /// </summary>
    /// <param name="sideLength">The length of the square's side. Must be greater than zero.</param>
    /// <returns>The area of the square.</returns>
    /// <exception cref="ArgumentException">Thrown when the side length is less than or equal to zero.</exception>
    /// <exception cref="ArgumentException">Thrown when the side length is not a valid number (NaN or Infinity).</exception>
    /// <example>
    /// <code>
    /// double area = SquareCalculator.CalculateArea(5.0);
    /// Console.WriteLine($"Area: {area}"); // Output: Area: 25
    /// </code>
    /// </example>
    public static double CalculateArea(double sideLength)
    {
        ValidateSideLength(sideLength);
        return sideLength * sideLength;
    }

    /// <summary>
    /// Calculates the area of a square given its side length with high precision using decimal arithmetic.
    /// </summary>
    /// <param name="sideLength">The length of the square's side. Must be greater than zero.</param>
    /// <returns>The area of the square with decimal precision.</returns>
    /// <exception cref="ArgumentException">Thrown when the side length is less than or equal to zero.</exception>
    /// <example>
    /// <code>
    /// decimal area = SquareCalculator.CalculateAreaPrecise(5.5m);
    /// Console.WriteLine($"Area: {area}"); // Output: Area: 30.25
    /// </code>
    /// </example>
    public static decimal CalculateAreaPrecise(decimal sideLength)
    {
        ValidateSideLength(sideLength);
        return sideLength * sideLength;
    }

    /// <summary>
    /// Calculates the perimeter of a square given its side length.
    /// </summary>
    /// <param name="sideLength">The length of the square's side. Must be greater than zero.</param>
    /// <returns>The perimeter of the square.</returns>
    /// <exception cref="ArgumentException">Thrown when the side length is less than or equal to zero.</exception>
    /// <exception cref="ArgumentException">Thrown when the side length is not a valid number (NaN or Infinity).</exception>
    /// <example>
    /// <code>
    /// double perimeter = SquareCalculator.CalculatePerimeter(4.0);
    /// Console.WriteLine($"Perimeter: {perimeter}"); // Output: Perimeter: 16
    /// </code>
    /// </example>
    public static double CalculatePerimeter(double sideLength)
    {
        ValidateSideLength(sideLength);
        return 4 * sideLength;
    }

    /// <summary>
    /// Calculates the diagonal length of a square given its side length.
    /// </summary>
    /// <param name="sideLength">The length of the square's side. Must be greater than zero.</param>
    /// <returns>The diagonal length of the square.</returns>
    /// <exception cref="ArgumentException">Thrown when the side length is less than or equal to zero.</exception>
    /// <exception cref="ArgumentException">Thrown when the side length is not a valid number (NaN or Infinity).</exception>
    /// <example>
    /// <code>
    /// double diagonal = SquareCalculator.CalculateDiagonal(5.0);
    /// Console.WriteLine($"Diagonal: {diagonal:F2}"); // Output: Diagonal: 7.07
    /// </code>
    /// </example>
    public static double CalculateDiagonal(double sideLength)
    {
        ValidateSideLength(sideLength);
        return sideLength * Math.Sqrt(2);
    }

    /// <summary>
    /// Validates that a side length is positive and a valid number.
    /// </summary>
    /// <param name="sideLength">The side length to validate.</param>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    private static void ValidateSideLength(double sideLength)
    {
        if (double.IsNaN(sideLength) || double.IsInfinity(sideLength))
        {
            throw new ArgumentException("Side length must be a valid number.", nameof(sideLength));
        }

        if (sideLength <= 0)
        {
            throw new ArgumentException("Side length must be greater than zero.", nameof(sideLength));
        }
    }

    /// <summary>
    /// Validates that a side length is positive.
    /// </summary>
    /// <param name="sideLength">The side length to validate.</param>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    private static void ValidateSideLength(decimal sideLength)
    {
        if (sideLength <= 0)
        {
            throw new ArgumentException("Side length must be greater than zero.", nameof(sideLength));
        }
    }
}
