$projectPath = "C:\Temp\automation-studio-tests\Basic_Project\Basic_Project"
$testPath = "C:\Temp\ASW-UI-Tests"
$resultPath = "C:\Temp\results"
$numberOfTestRuns = 1000



$runCounter = 0
do
{
    $runCounter++
    Write-Host ("Current run: " + $runCounter.ToString())

    #cleanup
    Remove-Item ($testPath + "\results\*") -Recurse -Force
    Remove-Item $projectPath -Recurse -Force

    #start server
    $serverID = Start-Process ($testPath + "\RobotTests\libraries\FlaUILibrary\bin\Release\net481\FlaUILibrary.exe")

    #test
    robot ($projectPath + "\RobotTests\tests\Automation Studio\Automation Studio_tests.robot")
    Copy-Item ($testPath + "\results") ($resultPath + "\" + $runCounter.ToString() + "\as") -Recurse -Force | Out-Null
    robot ($projectPath + "\RobotTests\tests\Automation Runtime\Automation Runtime.robot")
    Copy-Item ($testPath + "\results") ($resultPath + "\" + $runCounter.ToString() + "\ar") -Recurse -Force | Out-Null
    robot ($projectPath + "\RobotTests\tests\OPC UA CS\OPC UA CS_tests.robot")
    Copy-Item ($testPath + "\results") ($resultPath + "\" + $runCounter.ToString() + "\opcua") -Recurse -Force | Out-Null
    robot ($projectPath + "\RobotTests\tests\mappview\mappView_tests.robot")
    Copy-Item ($testPath + "\results") ($resultPath + "\" + $runCounter.ToString() + "\mappview") -Recurse -Force | Out-Null

    #close AS
    robot ($projectPath + "\RobotTests\tests\close_all\close_all.robot")
    
    #stop server
    Stop-Process -Id $serverID.Id -Force
}
until( $runCounter -ge $numberOfTestRuns )