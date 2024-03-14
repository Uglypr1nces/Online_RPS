<<<<<<< HEAD
$MyDir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)
$SourceFolder = Join-Path -Path $MyDir -ChildPath "images"
$TargetFolder = Join-Path -Path $MyDir -ChildPath "Online_RPS\Online_RPS\bin\Debug"

Get-ChildItem -Path $SourceFolder -Filter '*.jpg' -File |
    ForEach-Object {
        # Join-Path simply returns a string containing the combined path
        # The BaseName property is the filename without extension
        $ChildPath   = Join-Path -Path $_.BaseName -ChildPath $_.BaseName
        $Destination = Join-Path -Path $TargetFolder -ChildPath $ChildPath
        # Create the directory if it doesn't already exits
        # Using -Force will not give an error if the folder already exists
        $null = New-Item -Path $Destination -ItemType Directory -Force
        $_ | Copy-Item -Destination $Destination
    }


=======
$MyDir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)
$SourceFolder = Join-Path -Path $MyDir -ChildPath "images"
$TargetFolder = Join-Path -Path $MyDir -ChildPath "Online_TTT\bin\Debug"

Copy-Item -Path $SourceFolder -Destination $TargetFolder -Recurse


>>>>>>> 237e7cf32d2bcdb10cda17eca36f59e0a0130f33
