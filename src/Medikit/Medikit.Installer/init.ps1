param(
	[Parameter(Mandatory=$true)][string]$inputDir,
	[Parameter(Mandatory=$true)][string]$outputFile
)

function Publish
{
	Write-Information "Publishing..."

	pushd "$inputDir"

	dotnet publish --configuration release

	popd
	Write-Information "Finish to publish"
}

function RunLightForBundle
{
	$wixRoot = $env:WIX

	pushd "$wixRoot\bin"

	$heatOutput = .\heat.exe dir "$inputDir\bin\release\netcoreapp3.1\publish" -var var.Medikit.Installer -dr INSTALLFOLDER -cg Medikit.Installer.Binaries -ag -scom -sreg -sfrag -srd -out $outputFile

	Write-Information "Heat output: $heatOutput"

	popd
}

Publish
RunLightForBundle