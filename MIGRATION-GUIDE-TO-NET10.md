# Migration Guide: .NET 8 to .NET 10 / MAUI 8 to MAUI 10

## Overview

This guide walks you through upgrading the project from:
- .NET 8.0.416 to .NET 10.0.100
- MAUI 8.0.100 to MAUI 10 (latest version)
- Nutrient SDK 1.4.0 remains unchanged (compatible)

## Prerequisites

### 1. Install .NET 10 SDK

```bash
# Check if .NET 10 is already installed
dotnet --list-sdks

# If .NET 10 is not installed, use manual installation:
cd /tmp
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0 --install-dir /tmp/dotnet10-temp --no-path

# Copy to main directory:
sudo cp -R /tmp/dotnet10-temp/sdk/10.0.* /usr/local/share/dotnet/sdk/
sudo cp -R /tmp/dotnet10-temp/host /usr/local/share/dotnet/
sudo cp -R /tmp/dotnet10-temp/shared /usr/local/share/dotnet/

# Verify
/usr/local/share/dotnet/dotnet --list-sdks
```

### 2. Install MAUI 10 Workload

```bash
# Install MAUI 10 workload
sudo dotnet workload install maui

# Or update existing MAUI installation
sudo dotnet workload update

# Verify
dotnet workload list
```

## Migration Steps

### Step 1: Update global.json

Create or update the `global.json` file in the project root:

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestFeature",
    "allowPrerelease": false
  }
}
```

Note: Adjust the version number to match your installed .NET 10 SDK version.

### Step 2: Update Catalog.csproj

#### 2.1 Update Target Frameworks

Change all target frameworks from `net8.0-*` to `net10.0-*`:

```xml
<TargetFrameworks>net10.0-android;net10.0-ios;net10.0-maccatalyst</TargetFrameworks>
<TargetFrameworks Condition="$([MSBuild]::IsOSPlatform('windows'))">$(TargetFrameworks);net10.0-windows10.0.19041.0</TargetFrameworks>
```

#### 2.2 Update C# Language Version

```xml
<LangVersion>14.0</LangVersion>
```

#### 2.3 Update NuGet Packages

```xml
<PackageReference Include="CommunityToolkit.Maui" Version="9.1.1" />
<PackageReference Include="CommunityToolkit.Maui.Markup" Version="4.1.0" />
<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="9.0.0" />
<PackageReference Include="Microsoft.Maui.Controls" Version="9.0.10" />
<PackageReference Include="Microsoft.AspNetCore.Components.WebView.Maui" Version="9.0.10" />
```

#### 2.4 Nutrient SDK - No Change Required

The Nutrient SDK 1.4.0 remains unchanged and is compatible with .NET 10:

```xml
<PackageReference Include="Nutrient.MAUI.SDK" Version="1.4.0" />
```

#### 2.5 Update Minimum Platform Versions

.NET 10 requires higher minimum platform versions:

```xml
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'ios'">12.2</SupportedOSPlatformVersion>
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'maccatalyst'">15.0</SupportedOSPlatformVersion>
```

### Step 3: Clean and Restore Project

```bash
# Clean
dotnet clean

# Restore NuGet packages
dotnet restore

# Build for iOS
dotnet build -f net10.0-ios -c Debug

# Build for Android
dotnet build -f net10.0-android -c Debug

# Build for Mac Catalyst
dotnet build -f net10.0-maccatalyst -c Debug
```

## Important Notes

### Nutrient SDK Compatibility

Nutrient.MAUI.SDK 1.4.0 works with .NET 10 because:
- .NET 10 is backwards compatible with .NET 8 libraries
- Nutrient SDK uses .NET Standard 2.0/2.1 internally
- Native iOS/Android bindings are .NET-version-independent
- MAUI 10 supports .NET 8/9 NuGet packages

You may see these warnings (safe to ignore in Debug builds):
```
warning NU1701: Package 'Nutrient.MAUI.SDK 1.4.0' was restored using '.NETFramework,Version=v4.6.1'
warning IL2104: Assembly 'Sdk' produced trim warnings
```

## Troubleshooting

### SDK Not Found

If you see "SDK 10.0.100 not found", reinstall .NET 10 SDK manually (see Prerequisites section).

### MAUI Workload Errors

```bash
sudo dotnet workload uninstall maui
sudo dotnet workload install maui
```

### NuGet Package Restore Errors

```bash
dotnet nuget locals all --clear
dotnet restore --force
```

### iOS Build Errors

If you see errors about SupportedOSPlatformVersion being too low:
- Update iOS minimum to 12.2
- Update Mac Catalyst minimum to 15.0

### Nutrient SDK Issues

If Nutrient SDK doesn't work after migration:
```bash
dotnet nuget locals all --clear
rm -rf obj/ bin/
dotnet restore
dotnet build -f net10.0-ios -c Debug
```

## Rollback to .NET 8

If you need to revert the migration:

1. Update `global.json` back to version "8.0.416"
2. Change all `net10.0-*` back to `net8.0-*` in Catalog.csproj
3. Revert package versions to original values
4. Change LangVersion back to 12.0
5. Revert iOS minimum to 11.0 and Mac Catalyst to 13.1
6. Run `dotnet clean && dotnet restore && dotnet build`

## Version Overview

Before migration:
- .NET SDK: 8.0.416
- MAUI: 8.0.100
- C#: 12.0
- Target Framework: net8.0
- iOS Minimum: 11.0
- Mac Catalyst Minimum: 13.1
- Nutrient SDK: 1.4.0

After migration:
- .NET SDK: 10.0.100
- MAUI: 10.x
- C#: 14.0
- Target Framework: net10.0
- iOS Minimum: 12.2 (required)
- Mac Catalyst Minimum: 15.0 (required)
- Nutrient SDK: 1.4.0 (unchanged, compatible)

## Additional Resources

- [.NET 10 Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- [.NET 10 Release Notes](https://github.com/dotnet/core/tree/main/release-notes/10.0)
- [MAUI Migration Guide](https://learn.microsoft.com/en-us/dotnet/maui/migration/)
- [Nutrient SDK Documentation](https://www.nutrient.io/guides/maui/)

---

Last updated: November 20, 2025

