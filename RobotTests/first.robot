*** Settings ***
Library             FlaUILibrary    uia=${UIA}    screenshot_on_failure=False
Library             StringFormat

*** Test Cases ***
First Test
    Log    Hello, World!