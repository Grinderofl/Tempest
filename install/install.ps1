if(!$PSScriptRoot){
    $PSScriptRoot = Split-Path $MyInvocation.MyCommand.Path -Parent
}

$TEMPEST_DIR = Join-Path $PSScriptRoot ""

$NUGET_EXE = Join-Path $TEMPEST_DIR "nuget.exe"
$NUGET_URL = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$PACKAGES_FILE = Join-Path $TEMPEST_DIR "packages.config"




if ((Test-Path $PSScriptRoot) -and !(Test-Path $TEMPEST_DIR)) {
    Write "Creating tenpest directory..."
    New-Item -Path $TEMPEST_DIR -Type directory | out-null
}

if (!(Test-Path $PACKAGES_FILE)) {
    Write "Downloading packages.config..."
    try { (New-Object System.Net.WebClient).DownloadFile("https://raw.githubusercontent.com/Grinderofl/Tempest/develop/install/packages.config", $PACKAGES_FILE) } catch {
        Throw "Could not download packages.config."
    }
}


if (!(Test-Path $NUGET_EXE)) {
    Write "Trying to find nuget.exe in PATH..."
    $existingPaths = $Env:Path -Split ';' | Where-Object { (![string]::IsNullOrEmpty($_)) -and (Test-Path $_) }
    $NUGET_EXE_IN_PATH = Get-ChildItem -Path $existingPaths -Filter "nuget.exe" | Select -First 1
    if ($NUGET_EXE_IN_PATH -ne $null -and (Test-Path $NUGET_EXE_IN_PATH.FullName)) {
        Write "Found in PATH at $($NUGET_EXE_IN_PATH.FullName)."
        $NUGET_EXE = $NUGET_EXE_IN_PATH.FullName
    }
}

if (!(Test-Path $NUGET_EXE)) {
    Write "Downloading NuGet.exe..."
    try {
        (New-Object System.Net.WebClient).DownloadFile($NUGET_URL, $NUGET_EXE)
    } catch {
        Throw "Could not download NuGet.exe."
    }
}

$ENV:NUGET_EXE = $NUGET_EXE

Push-Location
Set-Location $TEMPEST_DIR


try {
	Write "Downloading from NuGet..."
	$NuGetOutput = Invoke-Expression "&`"$NUGET_EXE`" install -ExcludeVersion -OutputDirectory `"$TEMPEST_DIR`""

	if ($LASTEXITCODE -ne 0) {
		Throw "An error occured while restoring NuGet tools."
	}

	Write ($NuGetOutput | out-string)
	$currentPath = (Get-Item -Path ".\" -Verbose).FullName
	Write $currentPath

	#[Environment]::SetEnvironmentVariable("Path", $env:Path + ";" + $currentPath, [EnvironmentVariableTarget]::User)

	# $oldpath = (Get-ItemProperty -Path �Registry::HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment� -Name PATH).path
	# $currentPath = $MyInvocation.MyCommand.Path
	# $newpath = �$currentPath;$oldpath�
	# Set-ItemProperty -Path �Registry::HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment� -Name PATH �Value $newPath

	$env:Path += ";$currentPath"
}
Finally{
	Pop-Location
}