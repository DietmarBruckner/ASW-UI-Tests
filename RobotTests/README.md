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
│   └── integration/                 # Multi-component integration tests
│       └── integration_tests.robot
│
├── keywords/                        # Custom Robot Framework keyword libraries
│   ├── project_keywords.robot       # Project creation and management keywords
│   ├── ide_keywords.robot           # IDE interaction keywords (menus, views, dialogs)
│   ├── component_keywords.robot     # Component setup and configuration keywords
│   └── widget_keywords.robot        # Widget testing and management keywords
│
├── config/                          # Test configuration and data files
│   ├── hardware_config.robot        # Hardware and environment variables
│   └── Widgets.txt                  # List of all supported MappView widgets (85+ types)
│
├── libraries/                       # FlaUILibrary wrapper (Phase 2)
│   └── FlaUILibrary/
│       └── README.md                # FlaUILibrary implementation (pending)
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
  - Directory and file organization
  - Configuration files (hardware_config.robot, robot.cfg)
  - Template keyword files
  - Template test files

- **Phase 2**: FlaUILibrary wrapper development (PENDING)
  - C# or Python wrapper exposing FlaUI to Robot Framework
  - Core keywords: element finding, clicking, typing, waiting
  - Component-specific keywords

- **Phase 3**: Robot keyword implementation (PENDING)
  - Conversion of C# utilities to Robot keywords
  - Implementation of project_keywords.robot, ide_keywords.robot, etc.

- **Phase 4**: Test case conversion (PENDING)
  - Conversion of C# test logic to .robot test files
  - Parametrized testing of 85+ widget types

- **Phase 5**: Verification and migration (PENDING)
  - Execute Robot test suite against Automation Studio 6
  - Validate results and cross-reference with FlaUITests

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

3. (Phase 2): Place FlaUILibrary in `libraries/FlaUILibrary/`

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
| `integration/` | Multi-component workflows | Full stack projects, build→transfer→deploy workflows |

### Test Tags

Tests are tagged for filtering:
- `smoke` — Quick sanity checks
- `regression` — Comprehensive test coverage
- `project-creation` — Project creation tests
- `mappview`, `opcua`, `automationruntime` — Component-specific
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

- ✅ **Phase 1**: Project structure and templates completed
- ⏳ **Phase 2**: FlaUILibrary wrapper (pending agent implementation)
- ⏳ **Phase 3**: Keyword implementation (pending agent implementation)
- ⏳ **Phase 4**: Test case conversion (pending agent implementation)
- ⏳ **Phase 5**: Verification and migration (pending final testing)

## Next Steps

1. **Implement FlaUILibrary** (Phase 2) — Use agent instruction to create the wrapper
2. **Implement Keywords** (Phase 3) — Convert C# logic to Robot keywords
3. **Convert Tests** (Phase 4) — Convert FlaUITests C# to .robot files
4. **Verify and Migrate** (Phase 5) — Execute and validate test suite

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
