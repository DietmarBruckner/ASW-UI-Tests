*** Settings ***
Documentation       Close all open projects and ensure Automation Studio is in a clean state.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Library    ${CURDIR}/../../libraries/FlaUILibrary/robot_flaulib.py    server_url=http://localhost:5000
Suite Teardown      FlaUILib.Check App Alive


*** Test Cases ***

Close All
    Initialize Automation Studio
    Close Automation Studio    save_changes=False
    Log    AS closed
