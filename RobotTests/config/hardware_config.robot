*** Settings ***
Resource            ${CURDIR}\\general\\cpu_types.robot

*** Variables ***
# Hardware Configuration for Automation Studio 6 Tests
# These variables define the target hardware and IDE settings

# CPU Configuration
${CPU_TYPE}                         X20CP1684
${CPU_SERIAL}                       -

# Automation Studio IDE Configuration
${AS_IDE_PATH}                      C:\\Program Files (x86)\\BRAutomation\\AS6\\bin-en\\pg.exe
${AS_VERSION}                       6.0
${AS_DEFAULT_TIMEOUT}               10
${AS_NETWORK_CONFIG}                Ethernet

# MappView Configuration
${MAPPVIEW_HTTP_PORT}               8080
${MAPPVIEW_HTTPS_PORT}              8443
${MAPPVIEW_PROTOCOL}                HTTP

# OPCUA Configuration
${OPCUA_ENABLED}                    True
${OPCUA_PORT}                       4840

# AutomationRuntime Configuration
${AUTOMATIONRUNTIME_MIN_VERSION}    6.5

# Project Settings
${PROJECT_NAME_PREFIX}              RobotTest
${PROJECT_TEMP_PATH}                C:\\temp\\automation-studio-tests\\
${PROJECT_BACKUP_PATH}              C:\\temp\\automation-studio-backups\\

# Logging and Diagnostics
${TEST_LOG_LEVEL}                   INFO
${SCREENSHOT_ON_FAILURE}            True
${CAPTURE_IDE_STATE_ON_ERROR}       True
