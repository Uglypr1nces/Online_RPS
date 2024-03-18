$MyDir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)
$SourceFolder = Join-Path -Path $MyDir -ChildPath "content"
$TargetFolder = Join-Path -Path $MyDir -ChildPath "\bin\Debug"

Copy-Item -Path $SourceFolder -Destination $TargetFolder -Recurse