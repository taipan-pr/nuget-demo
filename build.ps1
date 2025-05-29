# Build script for Square Area Calculator NuGet package

param(
    [string]$Configuration = "Release",
    [string]$Version = "1.0.0",
    [switch]$Clean,
    [switch]$Test,
    [switch]$Pack,
    [switch]$Publish,
    [string]$OutputPath = "./artifacts"
)

Write-Host "Square Area Calculator Build Script" -ForegroundColor Green
Write-Host "====================================" -ForegroundColor Green

# Clean
if ($Clean) {
    Write-Host "Cleaning solution..." -ForegroundColor Yellow
    dotnet clean --configuration $Configuration
    if (Test-Path $OutputPath) {
        Remove-Item $OutputPath -Recurse -Force
    }
}

# Restore packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "Package restore failed!" -ForegroundColor Red
    exit 1
}

# Build
Write-Host "Building solution..." -ForegroundColor Yellow
dotnet build --no-restore --configuration $Configuration
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

# Test
if ($Test) {
    Write-Host "Running tests..." -ForegroundColor Yellow
    dotnet test --no-build --configuration $Configuration --verbosity normal --collect:"XPlat Code Coverage" --results-directory ./coverage
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Tests failed!" -ForegroundColor Red
        exit 1
    }
    
    # Generate coverage report if reportgenerator is available
    if (Get-Command "reportgenerator" -ErrorAction SilentlyContinue) {
        Write-Host "Generating coverage report..." -ForegroundColor Yellow
        reportgenerator -reports:"./coverage/**/coverage.cobertura.xml" -targetdir:"./coverage/report" -reporttypes:"Html;Cobertura"
        Write-Host "Coverage report generated at: ./coverage/report/index.html" -ForegroundColor Green
    }
}

# Pack
if ($Pack) {
    Write-Host "Creating NuGet package..." -ForegroundColor Yellow
    New-Item -ItemType Directory -Force -Path $OutputPath | Out-Null
    dotnet pack "src/SquareAreaCalculator/SquareAreaCalculator.csproj" --no-build --configuration $Configuration --output $OutputPath -p:PackageVersion=$Version
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Package creation failed!" -ForegroundColor Red
        exit 1
    }
    
    # List created packages
    $packages = Get-ChildItem -Path $OutputPath -Filter "*.nupkg"
    Write-Host "Created packages:" -ForegroundColor Green
    foreach ($package in $packages) {
        Write-Host "  - $($package.Name)" -ForegroundColor Cyan
    }
}

# Publish
if ($Publish) {
    if (-not $Pack) {
        Write-Host "Pack must be enabled to publish packages!" -ForegroundColor Red
        exit 1
    }
    
    Write-Host "Publishing to NuGet..." -ForegroundColor Yellow
    $apiKey = $env:NUGET_API_KEY
    if (-not $apiKey) {
        Write-Host "NUGET_API_KEY environment variable not set!" -ForegroundColor Red
        exit 1
    }
    
    $packages = Get-ChildItem -Path $OutputPath -Filter "*.nupkg"
    foreach ($package in $packages) {
        Write-Host "Publishing $($package.Name)..." -ForegroundColor Yellow
        dotnet nuget push $package.FullName --api-key $apiKey --source https://api.nuget.org/v3/index.json --skip-duplicate
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Failed to publish $($package.Name)!" -ForegroundColor Red
            exit 1
        }
    }
}

Write-Host "Build completed successfully!" -ForegroundColor Green
