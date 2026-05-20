# RobotTests - Automation Studio UI Test Suite

Converted Robot Framework test suite for Automation Studio 6, migrated from FlaUITests C# project.

## Project Structure

```
RobotTests/
├── tests/                           # Test case files organized by functional area
│   ├── project_creation/            # Project setup and initialization tests
│   │   └── project_creation.robot
│   ├── mappview/                    # MappView component and visualization tests
│   │   └── mappview_tests.robot
│   ├── opcua/                       # OPCUA component tests
│   │   └── opcua_tests.robot
│   ├── automationruntime/           # AutomationRuntime component tests
│   │   └── automationruntime_tests.robot
│   ├── mappmotion/                  # mappMotion axis and commissioning tests
│   │   └── mappmotion_tests.robot
│   ├── safety/                      # mapp Safety and SafeDESIGNER tests
│   │   └── safety_tests.robot
│   ├── diagnostics/                 # Diagnostics and SDM tests
│   │   └── diagnostics_tests.robot
│   └── integration/                 # Multi-component integration tests
│       └── integration_tests.robot
│
├── keywords/                        # Custom Robot Framework keyword libraries
│   ├── project_keywords.robot       # Project creation and management keywords
│   ├── ide_keywords.robot           # IDE interaction keywords (menus, views, dialogs)
│   ├── component_keywords.robot     # Component setup and configuration keywords
│   └── widget_keywords.robot        # Widget testing and management keywords
│   ├── motion_keywords.robot        # mappMotion and axis-specific workflows
│   ├── safety_keywords.robot        # mapp Safety-specific workflows
│   └── diagnostics_keywords.robot   # SDM and diagnostics workflows
│
├── config/                          # Test configuration and data files
│   ├── hardware_config.robot        # Hardware and environment variables
│   ├── mappMotion/versions.robot    # mappMotion training/manual versions
│   ├── Safety/versions.robot        # mapp Safety training/manual versions
│   ├── Diagnostics/versions.robot   # Diagnostics training/manual versions
│   └── Widgets.txt                  # List of all supported MappView widgets (85+ types)
│
├── libraries/                       # FlaUILibrary wrapper and HTTP server
│   └── FlaUILibrary/
│       ├── robot_flaulib.py         # Robot Framework Python client (auto-start + retry)
│       ├── FlaUILibraryServer.cs    # C# HTTP keyword server
│       └── README.md                # Wrapper/server usage and troubleshooting
│
├── resources/                       # Screenshots, logs, and test artifacts
├── results/                         # Test execution outputs
│   ├── log.html
│   ├── report.html
│   └── xunit.xml
│
├── requirements.txt                 # Python and Robot Framework dependencies
├── robot.cfg                        # Robot Framework configuration
└── README.md                        # This file
```

## Phases Overview

This is a multi-phase conversion project:

- **Phase 1**: Project structure setup (COMPLETED)
  - Directory and file organization finalized
  - Configuration files expanded and normalized
  - Component test suites created under `tests/`

- **Phase 2**: FlaUILibrary wrapper development (COMPLETED)
  - Python Robot client rewritten for robust server lifecycle management
  - C# HTTP server extended with health endpoint and resilient bool parsing
  - Close-application flow improved with save-prompt handling

- **Phase 3**: Robot keyword implementation (COMPLETED for current scope)
  - Core keywords in place for project/IDE/component/widget
  - Added `motion_keywords.robot`, `safety_keywords.robot`, and `diagnostics_keywords.robot`

- **Phase 4**: Test case conversion (COMPLETED for current scope)
  - Functional suites implemented for project creation, mappView, OPC UA, Automation Runtime
  - Added first-wave suites for mappMotion, Safety, and Diagnostics
  - Integration coverage added for multi-component workflows

- **Phase 5**: Verification and migration (IN PROGRESS)
  - Dry-run validation currently passing for all committed suites
  - Full runtime execution in live AS environment remains as final operational gate

## Getting Started

### Prerequisites

- Python 3.8+
- Robot Framework 7.0+
- Automation Studio 6 IDE (running on Windows)
- FlaUI library (will be wrapped in Phase 2)
- Tesseract OCR (for text recognition in UI)

### Installation

1. Install dependencies:
   ```bash
   pip install -r requirements.txt
   ```

2. Configure environment:
   - Review [config/hardware_config.robot](config/hardware_config.robot) for AS IDE path, CPU type, ports, etc.
   - Ensure Automation Studio 6 is installed at the configured path

3. Build the FlaUI wrapper server (Release/net48) in `libraries/FlaUILibrary/`
4. Ensure the generated `FlaUILibrary.exe` is available for wrapper auto-start

### Running Tests

#### Run all tests:
```bash
robot tests/
```

#### Run specific functional area:
```bash
robot tests/project_creation/
robot tests/mappview/
robot tests/opcua/
robot tests/automationruntime/
robot tests/mappmotion/
robot tests/safety/
robot tests/diagnostics/
robot tests/integration/
```

#### Run with specific tags:
```bash
robot --include smoke tests/         # Run smoke tests
robot --include regression tests/    # Run regression tests
robot --exclude skip tests/          # Exclude skipped tests
```

#### Generate HTML report after execution:
Reports are automatically generated in `results/` directory:
- `report.html` — Test execution summary
- `log.html` — Detailed test logs with screenshots
- `xunit.xml` — Jenkins/CI integration format

## Test Organization

Tests are organized by **functional area** (component focus):

| Directory | Purpose | Coverage |
|-----------|---------|----------|
| `project_creation/` | Project setup, initialization, and loading | Create projects, select CPU, load projects |
| `mappview/` | MappView visualization configuration | Protocols, ports, visualizations, widget insertion, properties |
| `opcua/` | OPCUA component setup | Port configuration, component integration |
| `automationruntime/` | AutomationRuntime 6.5+ configuration | Version selection, integration tests |
| `mappmotion/` | Motion axis setup and commissioning smoke flows | Axis creation, base parameter setup |
| `safety/` | mapp Safety and SafeDESIGNER setup flows | Safety component bootstrap, domain creation |
| `diagnostics/` | SDM and diagnostics collection flows | SDM session open, logger capture |
| `integration/` | Multi-component workflows | Full stack projects, build→transfer→deploy workflows |

### Test Tags

Tests are tagged for filtering:
- `smoke` — Quick sanity checks
- `regression` — Comprehensive test coverage
- `project-creation` — Project creation tests
- `mappview`, `opcua`, `automationruntime` — Component-specific
- `mappmotion`, `safety`, `diagnostics` — Additional first-wave component tags
- `integration`, `full-stack` — Multi-component scenarios
- `widget-*` — Widget testing categories

## Keywords Reference

### [project_keywords.robot](keywords/project_keywords.robot)
- `Create New Project In Automation Studio` — Creates a new project
- `Load Existing Project` — Opens an existing project
- `Delete Project` — Removes a project
- `Build Project` — Builds the project
- `Transfer Project To CPU` — Transfers to target CPU

### [ide_keywords.robot](keywords/ide_keywords.robot)
- `Initialize Automation Studio` — Launches the IDE
- `Close Automation Studio` — Closes the IDE
- `Open Menu` / `Click Menu Item` — Menu interactions
- `Navigate To Tree Leaf` — Project tree navigation
- `Handle Modal Dialog` — Generic dialog handling
- `Take IDE Screenshot` — Capture IDE state

### [component_keywords.robot](keywords/component_keywords.robot)
- `Initialize MappView Component` — Sets up MappView
- `Initialize OPCUA Component` — Sets up OPCUA
- `Initialize AutomationRuntime Component` — Sets up AutomationRuntime
- `Create Visualization Project` — Creates MappView visualization
- `Insert MappView Widget` — Adds a widget
- `Build And Transfer Project` — Build and deploy workflow

### [widget_keywords.robot](keywords/widget_keywords.robot)
- `Test Widget Category [Category]` — Tests widgets in a category
- `Insert All Widget Types` — Tests all 85+ widget types
- `Test Single Widget` — Individual widget test
- `Verify Widget Count In Visualization` — Widget count assertion

## Configuration

Edit [config/hardware_config.robot](config/hardware_config.robot) to configure:

```robot
${CPU_TYPE}                         X20CP1684
${AS_IDE_PATH}                      C:\\Program Files (x86)\\BRAutomation\\AS6\\bin-en\\pg.exe
${AS_VERSION}                       6.0
${PROJECT_TEMP_PATH}                C:\\temp\\automation-studio-tests\\
${MAPPVIEW_HTTP_PORT}               8080
${OPCUA_PORT}                       4840
${AUTOMATIONRUNTIME_MIN_VERSION}    6.5
```

## Debugging

### Enable verbose logging:
```bash
robot --loglevel DEBUG tests/
```

### Take screenshots on failure:
Enabled by default in `robot.cfg`. Screenshots are saved to `results/`

### Run with dry-run (syntax check):
```bash
robot --dryrun tests/
```

### Lint Robot files:
```bash
robocop tests/
robocop keywords/
```

## Current Status

- ✅ **Phase 1**: Structure and configuration completed
- ✅ **Phase 2**: FlaUILibrary wrapper/client-server stack completed
- ✅ **Phase 3**: Keyword implementation completed for defined suites
- ✅ **Phase 4**: Test conversion completed for defined suites
- ⏳ **Phase 5**: Live execution verification in target environment pending

### Achieved In This Iteration

- Added and expanded component suites for mappMotion, Safety, and Diagnostics.
- Standardized traceability metadata format across all suites in `tests/`.
- Expanded keyword coverage in:
  - `keywords/motion_keywords.robot`
  - `keywords/safety_keywords.robot`
  - `keywords/diagnostics_keywords.robot`
- Added and normalized config assets for motion/safety/diagnostics and general hardware settings.
- Rewrote `libraries/FlaUILibrary/robot_flaulib.py` with:
  - server auto-start
  - health checking
  - robust bool/time conversion
  - retry-aware HTTP calls
- Updated `libraries/FlaUILibrary/FlaUILibraryServer.cs` with:
  - `GET /ping` endpoint
  - robust bool argument parsing
  - save-prompt aware close flow
- Validation results:
  - FlaUILibrary build: success (0 compile errors)
  - Robot dry-run for `tests/`: 28 passed, 0 failed

## Next Steps

1. Execute full non-dry-run suite in target AS environment and capture runtime evidence.
2. Add targeted tests around wrapper/server edge cases (timeouts, prompt variants, reconnect).
3. Expand coverage for backlog components from follow-up prompt.

## Related Projects

- **FlaUITests** — Original C# UI automation framework (reference/legacy)
- **ASW-UI-Tests.sln** — Solution containing both C# and new Robot Framework tests

## References

- [Robot Framework Documentation](https://robotframework.org/)
- [FlaUI Documentation](https://github.com/FlaUI/FlaUI)
- [Automation Studio 6 Documentation](https://www.br-automation.com/)

---

**Created**: May 18, 2026  
**Version**: 1.0 (Phase 1 - Structure Complete)  
**Status**: Ready for Phase 2 - FlaUILibrary Development
