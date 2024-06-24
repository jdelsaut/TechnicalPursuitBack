$match = "ServiceTemplate" 
$replacement = Read-Host "Please enter a solution name"

$files = Get-ChildItem $(get-location) -Recurse | where {$_.name -like "*"+$match+"*"}

$files |
    Sort-Object -Descending -Property { $_.FullName } |
    Rename-Item -newname { $_.name -replace $match, $replacement } -force


$files = Get-ChildItem $(get-location) -include *.cs, *.csproj, *.sln, Dockerfile, *.yml, *.json -Recurse 

foreach($file in $files) 
{ 
    ((Get-Content $file.fullname -Raw) -replace $match, $replacement) | set-content -NoNewline $file.fullname 
}

write-host "Applying formating, please wait."

cd $replacement

dotnet format

read-host -prompt "Done! Press any key to close."