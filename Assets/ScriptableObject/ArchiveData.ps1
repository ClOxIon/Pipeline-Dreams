# Define your folder locations:
$TargetFolder = $PSScriptRoot
$Suffix = '.archive'
# Get existing files into arrays:
$SourcePics = Get-ChildItem -Path $TargetFolder -Recurse
# Remove existing archives
Remove-Item –path $TargetFolder -include *.archive
# Loop all files in the source folder:
foreach($file in $SourcePics)
{
    if($file.Extension -eq $Suffix){
        (Get-Content -path "$($TargetFolder)\$($file)" -Raw)  | Set-Content -Path "$($TargetFolder)\$($file)$($Suffix)"
    }
    if($file.Extension -eq '.asset'){
        (Get-Content -path "$($TargetFolder)\$($file)" -Raw)  | Set-Content -Path "$($TargetFolder)\$($file)$($Suffix)"
    }
    
}