param(
	[Parameter(Mandatory=$true)][string]$inputDir,
	[Parameter(Mandatory=$true)][string]$outputFile
)

function Publish
{
	Write-Host "Publishing..."

	pushd "$inputDir"

	dotnet publish --configuration release

	popd
	Write-Host "Finish to publish"
}

function RunLightForBundle
{
	$wixRoot = $env:WIX
	$exe = $wixRoot + "bin\heat.exe"

	$heatOutput = & $exe dir "$inputDir\bin\release\netcoreapp3.1\publish" -var var.Medikit.Installer -dr INSTALLFOLDER -cg Medikit.Installer.Binaries -ag -scom -sreg -sfrag -srd -out $outputFile

	Write-Host "Heat output: $heatOutput"
}

Publish
RunLightForBundle