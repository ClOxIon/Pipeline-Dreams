# Define your folder locations:
$CatalogFolder = $PSScriptRoot
$Prefix = 'Instruction'
$Suffix = '.cs'
$Template = 'InstructionTemplate'
$Marker = 'XXX'
$arrayFromFile = @(Get-Content "$($CatalogFolder)\refactor.catalog")
# Loop all files in the source folder:
foreach($file in $arrayFromFile)
{
    if($file -eq "")
    {
    Continue
    }
    
        ((Get-Content -path "$($CatalogFolder)\$($Template)" -Raw) -replace "$($Marker)","$($file)") | Set-Content -Path "$($CatalogFolder)\$($Prefix)$($file)$($Suffix)"
    
}