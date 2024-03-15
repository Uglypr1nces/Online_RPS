
$MyDir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)
$SourceFolder = Join-Path -Path $MyDir -ChildPath "images"
$TargetFolder = Join-Path -Path $MyDir -ChildPath "Online_RPS\bin\Debug"

Copy-Item -Path $SourceFolder -Destination $TargetFolder -Recurse

