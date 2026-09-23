*** Settings ***
Documentation       Close all open projects and ensure Automation Studio is in a clean state.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Library    ${CURDIR}/../../libraries/FlaUILibrary/robot_flaulib.py    server_url=http://localhost:5000



*** Test Cases ***

Close All
    Start FlaUI Server
    Initialize Automation Studio
    Close Automation Studio    save_changes=False
    Stop FlaUI Server
    Log    AS closed
