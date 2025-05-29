# Square Area Calculator

[![CI/CD Pipeline](https://github.com/yourusername/square-area-calculator/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/yourusername/square-area-calculator/actions/workflows/ci-cd.yml)
[![NuGet Version](https://img.shields.io/nuget/v/SquareAreaCalculator.svg)](https://www.nuget.org/packages/SquareAreaCalculator/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/SquareAreaCalculator.svg)](https://www.nuget.org/packages/SquareAreaCalculator/)

A simple, efficient, and well-tested .NET library for calculating geometric properties of squares.

## Features

- ✅ Calculate square area with double precision
- ✅ Calculate square area with decimal precision for financial calculations
- ✅ Calculate square perimeter
- ✅ Calculate square diagonal length
- ✅ Input validation with meaningful error messages
- ✅ Comprehensive XML documentation
- ✅ 100% test coverage
- ✅ .NET 8.0 support

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package SquareAreaCalculator
```

Or via Package Manager Console in Visual Studio:

```powershell
Install-Package SquareAreaCalculator
```

## Quick Start

```csharp
using SquareAreaCalculator;

// Calculate area
double area = SquareCalculator.CalculateArea(5.0);
Console.WriteLine($"Area: {area}"); // Output: Area: 25

// Calculate area with decimal precision
decimal preciseArea = SquareCalculator.CalculateAreaPrecise(5.5m);
Console.WriteLine($"Precise Area: {preciseArea}"); // Output: Precise Area: 30.25

// Calculate perimeter
double perimeter = SquareCalculator.CalculatePerimeter(4.0);
Console.WriteLine($"Perimeter: {perimeter}"); // Output: Perimeter: 16

// Calculate diagonal
double diagonal = SquareCalculator.CalculateDiagonal(5.0);
Console.WriteLine($"Diagonal: {diagonal:F2}"); // Output: Diagonal: 7.07
```

## API Reference

### SquareCalculator.CalculateArea(double sideLength)

Calculates the area of a square using double precision.

**Parameters:**
- `sideLength` (double): The length of the square's side. Must be greater than zero.

**Returns:**
- `double`: The area of the square.

**Exceptions:**
- `ArgumentException`: Thrown when the side length is less than or equal to zero, or is not a valid number (NaN, Infinity).

### SquareCalculator.CalculateAreaPrecise(decimal sideLength)

Calculates the area of a square using decimal precision for high-precision calculations.

**Parameters:**
- `sideLength` (decimal): The length of the square's side. Must be greater than zero.

**Returns:**
- `decimal`: The area of the square with decimal precision.

**Exceptions:**
- `ArgumentException`: Thrown when the side length is less than or equal to zero.

### SquareCalculator.CalculatePerimeter(double sideLength)

Calculates the perimeter of a square.

**Parameters:**
- `sideLength` (double): The length of the square's side. Must be greater than zero.

**Returns:**
- `double`: The perimeter of the square.

**Exceptions:**
- `ArgumentException`: Thrown when the side length is less than or equal to zero, or is not a valid number (NaN, Infinity).

### SquareCalculator.CalculateDiagonal(double sideLength)

Calculates the diagonal length of a square.

**Parameters:**
- `sideLength` (double): The length of the square's side. Must be greater than zero.

**Returns:**
- `double`: The diagonal length of the square.

**Exceptions:**
- `ArgumentException`: Thrown when the side length is less than or equal to zero, or is not a valid number (NaN, Infinity).

## Examples

### Basic Usage

```csharp
using SquareAreaCalculator;

class Program
{
    static void Main()
    {
        try
        {
            double sideLength = 6.0;
            
            var area = SquareCalculator.CalculateArea(sideLength);
            var perimeter = SquareCalculator.CalculatePerimeter(sideLength);
            var diagonal = SquareCalculator.CalculateDiagonal(sideLength);
            
            Console.WriteLine($"Square with side length {sideLength}:");
            Console.WriteLine($"  Area: {area}");
            Console.WriteLine($"  Perimeter: {perimeter}");
            Console.WriteLine($"  Diagonal: {diagonal:F2}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### High-Precision Financial Calculations

```csharp
using SquareAreaCalculator;

// For financial calculations where precision matters
decimal plotSize = 15.75m;
decimal area = SquareCalculator.CalculateAreaPrecise(plotSize);
Console.WriteLine($"Plot area: {area} square meters");
```

### Error Handling

```csharp
using SquareAreaCalculator;

try
{
    // This will throw an ArgumentException
    double invalidArea = SquareCalculator.CalculateArea(-5.0);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid input: {ex.Message}");
    // Output: Invalid input: Side length must be greater than zero.
}
```

## Development

### Prerequisites

- .NET 8.0 SDK
- Git

### Building the Project

```bash
git clone https://github.com/yourusername/square-area-calculator.git
cd square-area-calculator
dotnet restore
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Creating a NuGet Package

```bash
dotnet pack src/SquareAreaCalculator/SquareAreaCalculator.csproj --configuration Release
```

## CI/CD Pipeline

This project uses GitHub Actions for continuous integration and deployment:

- **On Push to Main**: Runs tests and builds the package
- **On Push to Develop**: Runs tests, builds, and publishes preview packages
- **On Release**: Runs tests, builds, and publishes stable packages to NuGet
- **Security Scanning**: Checks for vulnerable dependencies

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Add tests for your changes
5. Commit your changes (`git commit -m 'Add some amazing feature'`)
6. Push to the branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Changelog

### Version 1.0.0
- Initial release
- Basic square area calculation functionality
- Comprehensive input validation
- Support for both double and decimal precision
- Added perimeter and diagonal calculations
- 100% test coverage
- Complete XML documentation

## Support

If you encounter any issues or have questions, please [open an issue](https://github.com/yourusername/square-area-calculator/issues) on GitHub.
