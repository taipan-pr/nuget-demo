# Setup Guide for Square Area Calculator NuGet Package

This guide will help you set up and publish your Square Area Calculator NuGet package.

## Prerequisites

- .NET 8.0 SDK or later
- Git
- GitHub account
- NuGet.org account (for publishing)

## Project Structure

```
nuget-demo/
├── .github/
│   └── workflows/
│       └── ci-cd.yml              # GitHub Actions CI/CD pipeline
├── src/
│   └── SquareAreaCalculator/
│       ├── SquareAreaCalculator.csproj  # Main library project
│       └── SquareCalculator.cs          # Calculator implementation
├── tests/
│   └── SquareAreaCalculator.Tests/
│       ├── SquareAreaCalculator.Tests.csproj  # Test project
│       └── SquareCalculatorTests.cs           # Unit tests
├── Directory.Build.props          # Common build properties
├── global.json                    # .NET SDK version
├── NuGet.config                   # NuGet configuration
├── code-analysis.ruleset          # Code analysis rules
├── README.md                      # Package documentation
├── LICENSE                        # MIT license
├── build.ps1                      # PowerShell build script
├── build.sh                       # Bash build script
└── nuget-demo.slnx               # Solution file
```

## Local Development

### 1. Build the Project

Using PowerShell:
```powershell
./build.ps1 --clean --test --pack
```

Using Bash:
```bash
./build.sh --clean --test --pack
```

Or using dotnet CLI:
```bash
dotnet restore
dotnet build
dotnet test
dotnet pack src/SquareAreaCalculator/SquareAreaCalculator.csproj -o ./artifacts
```

### 2. Run Tests

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### 3. Test Package Locally

After building, you can test the package locally:

```bash
# Install the package from local artifacts
dotnet add package SquareAreaCalculator --source ./artifacts --version 1.0.0

# Or create a test console app
dotnet new console -n TestApp
cd TestApp
dotnet add package SquareAreaCalculator --source ../artifacts --version 1.0.0
```

## GitHub Setup

### 1. Create Repository

1. Create a new repository on GitHub (e.g., `square-area-calculator`)
2. Update the URLs in `src/SquareAreaCalculator/SquareAreaCalculator.csproj`:
   - Replace `yourusername` with your GitHub username
   - Replace repository name as needed

### 2. Configure GitHub Secrets

For the CI/CD pipeline to work, you need to set up these GitHub repository secrets:

1. Go to your repository → Settings → Secrets and variables → Actions
2. Add the following secrets:

#### Required Secrets:
- `NUGET_API_KEY`: Your NuGet.org API key

#### Getting NuGet API Key:
1. Go to [NuGet.org](https://www.nuget.org/)
2. Sign in to your account
3. Go to Account Settings → API Keys
4. Create a new API key with "Push new packages and package versions" permission
5. Copy the generated key and add it to GitHub secrets

### 3. Push to GitHub

```bash
git init
git add .
git commit -m "Initial commit: Square Area Calculator NuGet package"
git branch -M main
git remote add origin https://github.com/yourusername/square-area-calculator.git
git push -u origin main
```

## CI/CD Pipeline

The GitHub Actions workflow will automatically:

### On Push to Main:
- Run tests
- Build the project
- Create NuGet package (artifact only)

### On Push to Develop:
- Run tests
- Build the project
- Create and publish preview package to NuGet

### On Release:
- Run tests
- Build the project
- Create and publish stable package to NuGet

## Publishing Workflow

### 1. Development Workflow

```bash
# Work on develop branch
git checkout -b develop
git push -u origin develop

# Make changes, commit, and push
git add .
git commit -m "Add new feature"
git push origin develop

# This will trigger preview package publication
```

### 2. Release Workflow

```bash
# Merge develop to main
git checkout main
git merge develop
git push origin main

# Create a release tag
git tag v1.0.0
git push origin v1.0.0

# Or create release through GitHub UI
# Go to GitHub → Releases → Create a new release
# Tag: v1.0.0
# Title: Version 1.0.0
# Description: Initial release
```

## Package Customization

### 1. Update Package Metadata

Edit `src/SquareAreaCalculator/SquareAreaCalculator.csproj`:

```xml
<PackageId>YourPackageName</PackageId>
<PackageVersion>1.0.0</PackageVersion>
<Authors>Your Name</Authors>
<Description>Your package description</Description>
<PackageTags>your;tags;here</PackageTags>
<PackageProjectUrl>https://github.com/yourusername/your-repo</PackageProjectUrl>
```

### 2. Update Version

You can update versions in several ways:

#### Option 1: Update project file
```xml
<PackageVersion>1.1.0</PackageVersion>
```

#### Option 2: Use MSBuild property
```bash
dotnet pack -p:PackageVersion=1.1.0
```

#### Option 3: Use build scripts
```bash
./build.sh --pack --version 1.1.0
```

## Testing Your Package

### 1. Create Test Project

```bash
mkdir TestSquareCalculator
cd TestSquareCalculator
dotnet new console
dotnet add package SquareAreaCalculator
```

### 2. Test Code

```csharp
using SquareAreaCalculator;

Console.WriteLine("Testing Square Area Calculator");

try
{
    double area = SquareCalculator.CalculateArea(5.0);
    Console.WriteLine($"Area of square with side 5.0: {area}");
    
    decimal preciseArea = SquareCalculator.CalculateAreaPrecise(5.5m);
    Console.WriteLine($"Precise area of square with side 5.5: {preciseArea}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Troubleshooting

### Common Issues:

1. **Build Failures**: Check .NET SDK version matches global.json
2. **Test Failures**: Ensure all dependencies are restored
3. **Package Upload Fails**: Verify NuGet API key is correct and has proper permissions
4. **GitHub Actions Fail**: Check secrets are properly configured

### Useful Commands:

```bash
# Check .NET version
dotnet --version

# Clean all build artifacts
dotnet clean

# Restore packages
dotnet restore

# Build with verbose output
dotnet build --verbosity detailed

# Run specific tests
dotnet test --filter "CalculateArea_ValidSideLength_ReturnsCorrectArea"

# Check package contents
dotnet tool install -g NuGetPackageExplorer.Console
npe list ./artifacts/SquareAreaCalculator.1.0.0.nupkg
```

## Next Steps

1. Customize the package for your specific needs
2. Add more functionality to the calculator
3. Improve documentation and examples
4. Consider adding performance benchmarks
5. Add integration tests
6. Set up code coverage reporting
7. Consider adding XML documentation generation

Your NuGet package is now ready for development and publication! 🚀
