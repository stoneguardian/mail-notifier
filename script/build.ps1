param(
    [Parameter()]
    [ValidateSet('debug', 'release')]
    [string] $Configuration = 'debug',

    [Parameter()]
    [string] $OutputPath = $(Resolve-Path "$PSScriptRoot/../src/bin"), # Configuration will be appended,

    [Parameter()]
    [string] $ProjectDirectory = $(Resolve-Path "$PSScriptRoot/../src"),

    [Parameter()]
    [switch] $AndRun
)

$OutputPath += "/$Configuration"

$csprojFile = Get-ChildItem -Path $ProjectDirectory -Filter '*.csproj'
if ($null -eq $csprojFile)
{
    Write-Error -Message "Could not find a csproj-file in: $ProjectDirectory" -ErrorAction Stop
}

Write-Host "---Cleaning:"
dotnet clean $ProjectDirectory --output $OutputPath --configuration $Configuration


Write-Host "`n---Building:"
if ($Configuration -eq 'debug')
{
    dotnet build $ProjectDirectory --output $OutputPath
}
elseif ($Configuration -eq 'release')
{
    # If release, copy in dependent .dlls also
    dotnet publish $ProjectDirectory --output $OutputPath
}
else
{
    Write-Error -Message "Unknown configuration: $Configuration" -ErrorAction Stop
}

# Run resulting dll
if ($AndRun)
{
    $outputSubDir = Get-ChildItem -Path $OutputPath -Directory | Select-Object -First 1

    if ($null -ne $outputSubDir)
    {
        $dllPath = "$($outputSubDir.FullName)/$($csprojFile.BaseName).dll"
    }
    else
    { 
        $dllPath = "$OutputPath/$($csprojFile.BaseName).dll"
    }
    
    if (-not (Test-Path $dllPath))
    {
        Write-Error -Message "Could not find dll: $dllPath" -ErrorAction Stop
    }

    Write-Host "`n---Running $dllPath`:"
    dotnet $dllPath
}

