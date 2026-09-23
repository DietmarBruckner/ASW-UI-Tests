$projectPath = "C:\Temp\automation-studio-tests\Basic_Project\Basic_Project"
$testPath = $PSScriptRoot
$resultPath = "C:\Temp\results"
$numberOfTestRuns = 150
$cpuTypesPath = Join-Path $testPath "config\General\CPU_types.robot"
$cpuTypes = Get-Content $cpuTypesPath |
    Where-Object { $_ -match '^\.\.\.\s+(\S+)' } |
    ForEach-Object { $Matches[1] }

if ($cpuTypes.Count -eq 0)
{
    throw "No supported CPU types found in $cpuTypesPath"
}


#cleanup old results
Remove-Item $resultPath -Recurse -Force


$runCounter = 0

do
{
    $runCounter++
    $cpuType = $cpuTypes[($runCounter - 1) % $cpuTypes.Count]
    Write-Host ("Current run: " + $runCounter.ToString())
    Write-Host ("CPU type: " + $cpuType)

    #cleanup
    Remove-Item ($testPath + "\results\*") -Recurse -Force | Out-Null
    Remove-Item $projectPath -Recurse -Force | Out-Null

    #test
    robot --variable ("CPU_TYPE:" + $cpuType) --outputdir ($resultPath + "\" + $runCounter.ToString() + "\as") ($testPath + "\RobotTests\tests\Automation Studio\Automation Studio_tests.robot")
    robot --variable ("CPU_TYPE:" + $cpuType) --outputdir ($resultPath + "\" + $runCounter.ToString() + "\ar") ($testPath + "\RobotTests\tests\Automation Runtime\Automation Runtime.robot")
    robot --variable ("CPU_TYPE:" + $cpuType) --outputdir ($resultPath + "\" + $runCounter.ToString() + "\opcua") ($testPath + "\RobotTests\tests\OPC UA CS\OPC UA CS_tests.robot")
    robot --variable ("CPU_TYPE:" + $cpuType) --outputdir ($resultPath + "\" + $runCounter.ToString() + "\mappview") ($testPath + "\RobotTests\tests\mappview\mappView_tests.robot")

    #close AS
    robot --variable ("CPU_TYPE:" + $cpuType) --outputdir ($resultPath + "\" + $runCounter.ToString() + "\close_all") ($testPath + "\RobotTests\tests\close_all\close_all.robot")

    Start-Sleep -Seconds 5
}
until( $runCounter -ge $numberOfTestRuns )