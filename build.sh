#!/bin/bash

# Build script for Square Area Calculator NuGet package

set -e

CONFIGURATION="Release"
VERSION="1.0.0"
OUTPUT_PATH="./artifacts"
CLEAN=false
TEST=false
PACK=false
PUBLISH=false

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -c|--configuration)
            CONFIGURATION="$2"
            shift 2
            ;;
        -v|--version)
            VERSION="$2"
            shift 2
            ;;
        -o|--output)
            OUTPUT_PATH="$2"
            shift 2
            ;;
        --clean)
            CLEAN=true
            shift
            ;;
        --test)
            TEST=true
            shift
            ;;
        --pack)
            PACK=true
            shift
            ;;
        --publish)
            PUBLISH=true
            shift
            ;;
        -h|--help)
            echo "Usage: $0 [options]"
            echo "Options:"
            echo "  -c, --configuration <config>  Build configuration (Debug/Release) [default: Release]"
            echo "  -v, --version <version>       Package version [default: 1.0.0]"
            echo "  -o, --output <path>          Output path for packages [default: ./artifacts]"
            echo "  --clean                      Clean before build"
            echo "  --test                       Run tests"
            echo "  --pack                       Create NuGet package"
            echo "  --publish                    Publish to NuGet (requires NUGET_API_KEY)"
            echo "  -h, --help                   Show this help message"
            exit 0
            ;;
        *)
            echo "Unknown option: $1"
            exit 1
            ;;
    esac
done

echo "Square Area Calculator Build Script"
echo "===================================="

# Clean
if [ "$CLEAN" = true ]; then
    echo "Cleaning solution..."
    dotnet clean --configuration "$CONFIGURATION"
    if [ -d "$OUTPUT_PATH" ]; then
        rm -rf "$OUTPUT_PATH"
    fi
fi

# Restore packages
echo "Restoring NuGet packages..."
dotnet restore

# Build
echo "Building solution..."
dotnet build --no-restore --configuration "$CONFIGURATION"

# Test
if [ "$TEST" = true ]; then
    echo "Running tests..."
    dotnet test --no-build --configuration "$CONFIGURATION" --verbosity normal --collect:"XPlat Code Coverage" --results-directory ./coverage
    
    # Generate coverage report if reportgenerator is available
    if command -v reportgenerator &> /dev/null; then
        echo "Generating coverage report..."
        reportgenerator -reports:"./coverage/**/coverage.cobertura.xml" -targetdir:"./coverage/report" -reporttypes:"Html;Cobertura"
        echo "Coverage report generated at: ./coverage/report/index.html"
    fi
fi

# Pack
if [ "$PACK" = true ]; then
    echo "Creating NuGet package..."
    mkdir -p "$OUTPUT_PATH"
    dotnet pack "src/SquareAreaCalculator/SquareAreaCalculator.csproj" --no-build --configuration "$CONFIGURATION" --output "$OUTPUT_PATH" -p:PackageVersion="$VERSION"
    
    # List created packages
    echo "Created packages:"
    find "$OUTPUT_PATH" -name "*.nupkg" -exec basename {} \; | sed 's/^/  - /'
fi

# Publish
if [ "$PUBLISH" = true ]; then
    if [ "$PACK" != true ]; then
        echo "Pack must be enabled to publish packages!"
        exit 1
    fi
    
    echo "Publishing to NuGet..."
    if [ -z "$NUGET_API_KEY" ]; then
        echo "NUGET_API_KEY environment variable not set!"
        exit 1
    fi
    
    find "$OUTPUT_PATH" -name "*.nupkg" | while read -r package; do
        echo "Publishing $(basename "$package")..."
        dotnet nuget push "$package" --api-key "$NUGET_API_KEY" --source https://api.nuget.org/v3/index.json --skip-duplicate
    done
fi

echo "Build completed successfully!"
