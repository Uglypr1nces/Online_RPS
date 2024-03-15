
$MyDir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)
$SourceFolder = Join-Path -Path $MyDir -ChildPath "images"
<<<<<<< HEAD
$VideoFolder = Join-Path -Path $MyDir -ChildPath "stuff"
=======
>>>>>>> 812c62abc979bbafde868e33b0ea461726a42ff6
$TargetFolder = Join-Path -Path $MyDir -ChildPath "Online_RPS\bin\Debug"

Copy-Item -Path $SourceFolder -Destination $TargetFolder -Recurse
Copy-Item -Path $VideoFolder -Destination $TargetFolder -Recurse