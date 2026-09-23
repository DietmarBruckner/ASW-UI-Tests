$projectPath = "C:\Temp\automation-studio-tests\Basic_Project\Basic_Project"
$testPath = $PSScriptRoot
$resultPath = "C:\Temp\results"
$numberOfTestRuns = 150


#cleanup old results
Remove-Item $resultPath -Recurse -Force


$runCounter = 0

do
{
    $runCounter++
    Write-Host ("Current run: " + $runCounter.ToString())

    #cleanup
    Remove-Item ($testPath + "\results\*") -Recurse -Force | Out-Null
    Remove-Item $projectPath -Recurse -Force | Out-Null

    #test
    robot --outputdir ($resultPath + "\" + $runCounter.ToString() + "\as") ($testPath + "\RobotTests\tests\Automation Studio\Automation Studio_tests.robot")
    robot --outputdir ($resultPath + "\" + $runCounter.ToString() + "\ar") ($testPath + "\RobotTests\tests\Automation Runtime\Automation Runtime.robot")
    robot --outputdir ($resultPath + "\" + $runCounter.ToString() + "\opcua") ($testPath + "\RobotTests\tests\OPC UA CS\OPC UA CS_tests.robot")
    robot --outputdir ($resultPath + "\" + $runCounter.ToString() + "\mappview") ($testPath + "\RobotTests\tests\mappview\mappView_tests.robot")

    #close AS
    robot --outputdir ($resultPath + "\" + $runCounter.ToString() + "\close_all") ($testPath + "\RobotTests\tests\close_all\close_all.robot")

    Start-Sleep -Seconds 5
}
until( $runCounter -ge $numberOfTestRuns )