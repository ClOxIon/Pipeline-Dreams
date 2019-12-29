# Define your folder locations:
$CategoryFolder = '.'
$Prefix = 'Instruction'
$Suffix = '.cs'
$Template = 'InstructionTemplate'
$Marker = 'XXX'
# Get existing files into arrays:
$SourcePics = Get-ChildItem -Path $CategoryFolder -Recurse
$arrayFromFile = @(Get-Content '.\.catalog')
# Loop all files in the source folder:
foreach($file in $arrayFromFile)
{
    if($file -eq "")
    {
    Continue
    }
    # Using the basename property (which ignores file extension), if the $FinalPics 
    # array contains a basename equal to the basename of $file, then copy it:
    if(-not ($SourcePics.BaseName -contains "$($Prefix)$($file)$($Suffix)"))
    {
        ((Get-Content -path "$($CategoryFolder)\$($Template)" -Raw) -replace "$($Marker)","$($file)") | Set-Content -Path "$($CategoryFolder)\$($Prefix)$($file)$($Suffix)"
    }
}