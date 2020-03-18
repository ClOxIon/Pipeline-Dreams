# Define your folder locations:
$TargetFolder = $PSScriptRoot
$Suffix = '.archive.txt'
# Get existing files into arrays:
$SourcePics = Get-ChildItem -Path $TargetFolder -Recurse
# Loop all files in the source folder:
foreach($file in $SourcePics)
{
    if($file.Extension -eq '.asset'){
        (Get-Content -path "$($TargetFolder)\$($file)" -Raw)  | Set-Content -Path "$($TargetFolder)\$($file)$($Suffix)"
    }
    
}