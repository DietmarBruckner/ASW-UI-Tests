# Follow-Up Prompt: Backlog Components

Use this prompt only after first-wave validation and review pass.

You are continuing implementation of Automation Studio UI tests in this repository.
The first wave is complete for:
- Automation Studio Core
- Automation Runtime
- OPC UA CS
- mappView
- mappMotion
- Safety
- Diagnostics

Now implement only backlog components discovered during manual mining.

## Inputs

Read and reuse existing assets first:
- MASTER_PROMPT_AutomationStudio_UI_Tests.md
- FIRST_WAVE_EXECUTION_REPORT.md
- RobotTests
- RobotTests/config
- FlaUITests
- agents

## Scope Rules

1. Do not re-implement first-wave components.
2. Reuse first-wave common keywords/config patterns.
3. Add only component-specific deltas.
4. Keep deterministic UI automation focus.
5. Flag ambiguous manual steps as Needs Human Clarification.

## Backlog Components to Process

- Machine Vision
- Hydraulics and valve control
- ACOPOStrak
- Advanced axis coupling/cam
- POWERLINK infrastructure/redundancy
- Simulation/virtualization modules
- Condition monitoring specialization

## Required Method

1. Re-run scenario extraction for backlog manuals with traceability.
2. Classify reusable vs new keywords.
3. Extend RobotTests/tests and RobotTests/keywords only where needed.
4. Extend RobotTests/config with per-component versions and required settings.
5. Validate each new scenario against source evidence.
6. Perform review for naming, structure, duplication, and determinism.

## Required Deliverables

1. Backlog Component Inventory
2. Backlog Scenario Catalog by Component
3. Reused Keyword Map and New Keyword Additions
4. Added/Updated Test and Keyword Files
5. Added/Updated Config Files
6. Validation Report
7. Review Report
8. Open Clarifications/Blockers

## Prioritization Rule

Implement in this order unless evidence quality demands a change:
1. Advanced motion coupling and ACOPOStrak
2. Machine Vision
3. POWERLINK infrastructure
4. Simulation modules
5. Hydraulics and condition monitoring

## Completion Gate

Complete only when each implemented scenario has:
- source manual traceability,
- deterministic and testable UI steps,
- reusable keyword mapping,
- and review/validation pass results.
