properties {
	$base_dir = resolve-path .
	$build_dir = "$base_dir\build"
	$source_dir = "$base_dir\src"
	$result_dir = "$build_dir\results"
	$global:config = "debug"
	$tag = $(git tag -l --points-at HEAD)
	$revision = @{ $true = "{0:00000}" -f [convert]::ToInt32("0" + $env:APPVEYOR_BUILD_NUMBER, 10); $false = "local" }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
	$suffix = @{ $true = ""; $false = "ci-$revision"}[$tag -ne $NULL -and $revision -ne "local"]
	$commitHash = $(git rev-parse --short HEAD)
	$buildSuffix = @{ $true = "$($suffix)-$($commitHash)"; $false = "$($branch)-$($commitHash)" }[$suffix -ne ""]
    $versionSuffix = @{ $true = "--version-suffix=$($suffix)"; $false = ""}[$suffix -ne ""]
}


task default -depends local
task local -depends compile, test
task ci -depends clean, release, local, pack, publish

task publish {
    Push-Location -Path $source_dir\Medikit\Medikit.Website
	exec { npm install }
	exec { npm run build-azure }
	Pop-Location
	exec { dotnet publish $source_dir\Medikit\Medikit.Api.Host\Medikit.Api.Host.csproj -c $config -o $result_dir\services\Medikit.Api.Host }
	exec { dotnet publish $source_dir\Medikit\Medikit.Website\Medikit.Website.csproj -c $config -o $result_dir\services\Medikit.Website }
	exec { msbuild $source_dir\Medikit\Medikit.Installer\Medikit.Installer.wixproj /p:Configuration=Release }
	exec { msbuild $source_dir\Medikit\Medikit.Bundle\Medikit.Bundle.wixproj /p:Configuration=Release }
	Copy-Item "$source_dir\Medikit\Medikit.Installer\bin\Release\Medikit.Installer.msi" -Destination "$result_dir"
	Copy-Item "$source_dir\Medikit\Medikit.Bundle\bin\Release\Medikit.Bundle.exe" -Destination "$result_dir"
}

task clean {
	rd "$source_dir\artifacts" -recurse -force  -ErrorAction SilentlyContinue | out-null
	rd "$base_dir\build" -recurse -force  -ErrorAction SilentlyContinue | out-null
}

task release {
    $global:config = "release"
}

task compile -depends clean {
	echo "build: Tag is $tag"
	echo "build: Package version suffix is $suffix"
	echo "build: Build version suffix is $buildSuffix" 
	
	exec { dotnet --version }
	exec { dotnet --info }

	exec { msbuild -version }
	
    exec { dotnet build .\Medikit.EHealth.sln -c $config --version-suffix=$buildSuffix }
}
 
task pack -depends compile {

}

task test {
    Push-Location -Path $base_dir\tests\Medikit.Api.Acceptance.Tests

    try {
        exec { & dotnet test -c $config --no-build --no-restore }
    } finally {
        Pop-Location
    }
}