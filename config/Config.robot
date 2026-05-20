*** Settings ***
Resource            ${CURDIR}\\General\\CPU_types.robot
Resource            ${CURDIR}\\Automation Studio\\all.robot
Resource            ${CURDIR}\\Automation Runtime\\versions.robot
Resource            ${CURDIR}\\Diagnostics\\targets.robot
Resource            ${CURDIR}\\mappMotion\\all.robot
Resource            ${CURDIR}\\mappView\\all.robot
Resource            ${CURDIR}\\OPC UA CS\\all.robot

*** Variables ***
# Hardware Configuration for Automation Studio 6 Tests
# These variables define the target hardware and IDE settings

# CPU Configuration
${CPU_TYPE}                         ${DEFAULT_CPU_TYPE}
${CPU_SERIAL}                       -

# Automation Studio IDE Configuration
${AS_IDE_PATH}                      C:\\Program Files (x86)\\BRAutomation\\AS6
${AS_VERSION}                       ${DEFAULT_AS_VERSION}
${AS_WORKING_VERSION}               6.3
${AS_DEFAULT_TIMEOUT}               10
${AS_NETWORK_CONFIG}                Ethernet

# Automation Runtime Configuration
${AR_VERSION}                       ${DEFAULT_AR_VERSION}

# MappMotion Configuration
${USE_MAPPMOTION}                   True
${MOT_VERSION}                      ${DEFAULT_MOT_VERSION}

# MappView Configuration
${USE_MAPPVIEW}                     True
${VIEW_VERSION}                     ${DEFAULT_VIEW_VERSION}
${MAPPVIEW_HTTP_PORT}               8080
${MAPPVIEW_HTTPS_PORT}              8443
${MAPPVIEW_PROTOCOL}                HTTP

# OPC UA CS Configuration
${USE_OPCUACS}                      True
${OPCUA_SERVER_PORT}                4840

# Project Settings
${PROJECT_NAME_PREFIX}              RobotTest
${PROJECT_TEMP_PATH}                C:\\temp\\automation-studio-tests\\
${PROJECT_BACKUP_PATH}              C:\\temp\\automation-studio-backups\\

# Logging and Diagnostics
${TEST_LOG_LEVEL}                   INFO
${SCREENSHOT_ON_FAILURE}            True
${CAPTURE_IDE_STATE_ON_ERROR}       True
${TARGET_CONTROLLER_IP}             192.168.1.10
