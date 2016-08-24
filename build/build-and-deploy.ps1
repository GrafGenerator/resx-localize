Set-StrictMode -Version Latest
Clear
#=============================


Import-Module -Name "$PSScriptRoot\Invoke-MsBuild.psm1" -DisableNameChecking

$moduleName = 'GrafGenerator.ResxLocalize'
$docsFolder = [Environment]::GetFolderPath('MyDocuments')
$installFolder = "$docsFolder\WindowsPowerShell\Modules\$moduleName"
$modulePath = "$installFolder\$moduleName.dll"
$manifestPath = "$installFolder\$moduleName.psd1"

$dependencies = @("Newtonsoft.Json", "ClosedXML", "DocumentFormat.OpenXml")

$rootFolder = "$PSScriptRoot\.."
$artifactsFolder = "$rootFolder\artifacts"
$solutionPath = "$rootFolder\GrafGenerator.ResxLocalize.sln"



# build

$buildSucceeded = Invoke-MsBuild -Path $solutionPath -MsBuildParameters "/target:Clean;Build /property:Configuration=Release;OutDir=$artifactsFolder"

if ($buildSucceeded)
{ Write-Host "Build completed successfully." }
else
{ Write-Host "Build failed. Check the build log file for errors." }


#install

Write-Host "Install module."

#try to create required folders first
New-Item -ItemType File -Path $modulePath -Force | Out-Null

Copy-Item -Path "$artifactsFolder\$moduleName.dll" -Destination $modulePath -Force | Out-Null
Copy-Item -Path "$artifactsFolder\$moduleName.psd1" -Destination $manifestPath -Force | Out-Null

foreach ($dep in $dependencies) {
    Copy-Item -Path "$artifactsFolder\$dep.dll" -Destination $installFolder -Force | Out-Null
}

$dllExist = Test-Path $modulePath
$psdExist = Test-Path $manifestPath

if($dllExist -eq $false -or $psdExist -eq $false){
    Write-Host "Installation failed. Module not installed."
}
else {
    Write-Host "Installation succeeded. Module installed."
    Write-Host "Module path: $modulePath"
}