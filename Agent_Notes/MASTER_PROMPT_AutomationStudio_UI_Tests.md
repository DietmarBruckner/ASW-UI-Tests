# Master Prompt: Build Automation Studio UI Tests (Robot Framework + FlaUI-aligned)

Use this prompt with follow-on coding/research agents.

## Prompt to Use

You are a senior test engineer at B&R Industrial Automation.
Your task is to design and implement a UI test project for Automation Studio where:
- Robot Framework is used for foreground test authoring and orchestration.
- C# + FlaUI patterns are used as background interaction reference and execution guidance.

You are working in this repository and must use only evidence from the repository:
- `/agents` (training manuals in markdown)
- `/RobotTests` (existing Robot Framework skeleton)
- `/RobotTests/config` (configuration skeleton)
- `/FlaUITests` (existing C# FlaUI implementation patterns)

## Non-Negotiable Objectives

1. Analyze manuals in `/agents` and extract actionable UI-driven test scenarios.
2. Organize all extracted scenarios by Automation Studio software components.
3. Implement first-wave outputs for these components:
   - Automation Studio Core
   - Automation Runtime
   - OPC UA CS
   - mappView
   - mappMotion
   - Safety
   - Diagnostics
4. Use all relevant manuals regardless of language.
5. For each test scenario, provide source traceability to manual file + chapter/section.
6. Identify common steps across scenarios and factor them into reusable Robot Framework keywords.
7. Propose per-component configuration structure in `/RobotTests/config`.
8. Align Robot keyword/test granularity with `/FlaUITests/Util/MappView.cs` style:
   - chapter/task-level flow
   - decomposed atomic UI interactions

## Operating Constraints

- Do not invent Automation Studio actions, settings, or menu paths that are not supported by repository evidence.
- If a manual section is ambiguous for deterministic automation, mark it as `Needs Human Clarification`.
- Reuse existing Robot Framework project organization conventions in `/RobotTests`.
- Prefer extending existing keyword files and config structures over introducing parallel structures.
- Keep naming consistent, descriptive, and component-scoped.

## Execution Plan (Mandatory)

### Phase 1: Repository Discovery
1. Inspect `/RobotTests` and document current:
   - test folder structure
   - keyword organization
   - naming/tag conventions
   - resource import conventions
2. Inspect `/RobotTests/config` and document existing per-component/general files.
3. Inspect `/FlaUITests` with emphasis on:
   - `/FlaUITests/Util/MappView.cs`
   - `/FlaUITests/Util/IDE_Main.cs`
   - `/FlaUITests/Util/TreeConfig.cs`
   - `/FlaUITests/Util/ComponentInProject.cs`
4. Build a reference map from C# functions to likely Robot keyword groups.

### Phase 2: Manual Mining and Scenario Extraction
1. Scan all relevant files in `/agents`.
2. Extract UI-automatable content:
   - examples
   - procedures/tasks
   - setup sequences
   - code snippets that imply UI configuration flows
3. Normalize extraction into entries containing:
   - component
   - scenario title
   - objective
   - prerequisites
   - step-by-step UI actions
   - expected result
   - source file and chapter/section

### Phase 3: Component Classification
1. Classify extracted scenarios under first-wave components:
   - Automation Studio Core
   - Automation Runtime
   - OPC UA CS
   - mappView
   - mappMotion
   - Safety
   - Diagnostics
2. If additional components are discovered, put them in `Backlog Components` with:
   - discovered component name
   - evidence sources
   - why not in first wave

### Phase 4: Common-Step Refactoring
1. Analyze all first-wave scenarios for repeated patterns, including:
   - project creation/opening
   - tree navigation
   - component insertion
   - property editing
   - build/compile operations
   - diagnostics/verification actions
2. Define reusable keyword layers:
   - Layer A: atomic UI actions
   - Layer B: component-level actions
   - Layer C: chapter/scenario workflows
3. Refactor scenario definitions to consume reusable keywords.

### Phase 5: Robot Framework Artifact Design/Implementation
1. Propose or create component-focused test suites under `/RobotTests/tests`.
2. Propose or create component/common keyword files under `/RobotTests/keywords`.
3. Ensure each suite has:
   - clear tags
   - setup/teardown strategy
   - traceable test names linked to scenario objectives
4. Keep granularity close to `MappView.cs` chapter-function decomposition.

### Phase 6: Configuration Structure Design/Implementation
1. For each first-wave component, define config files under `/RobotTests/config/<Component>`.
2. Minimum required per component:
   - `versions.robot`
3. Add further config files where evidence supports them, such as:
   - templates
   - CPU/runtime options
   - protocol endpoints
   - widget catalogs
   - user/role/localization settings
4. Distinguish:
   - globally shared config (under `/RobotTests/config/general`)
   - component-local config

### Phase 7: Validation Gates (Must Pass)
Validate all first-wave outputs against this checklist:
1. Every test case maps to at least one manual source.
2. Every source reference includes file path and section/chapter identifier.
3. Every reusable keyword is used by at least one test case.
4. No duplicate keyword responsibility across files without justification.
5. Every config item is justified by manual or C# evidence.
6. No scenario depends on undefined prerequisites.
7. Non-deterministic/manual-only steps are explicitly flagged.

### Phase 8: Review Gates (Must Pass)
Perform a final review and report issues for:
1. Naming consistency (tests, keywords, configs).
2. Folder placement consistency with existing repository structure.
3. Over-fragmentation vs under-modularization in keywords.
4. Coverage gaps in first-wave components.
5. Mismatch between Robot steps and FlaUI interaction granularity.
6. Version assumptions not supported by repository evidence.

### Phase 9: First-Wave Completion Report
Provide a structured report with:
1. Implemented components and scenario counts.
2. Added/updated files list.
3. Validation checklist results.
4. Review checklist results.
5. Open blockers and clarification needs.

### Phase 10: Follow-Up Prompt for Backlog Components
After first-wave success, generate a second prompt titled `Follow-Up Prompt: Backlog Components` that:
1. Targets only components discovered outside first wave.
2. Reuses the same extraction/refactoring/validation/review method.
3. References first-wave common keywords/configs to avoid duplication.
4. Defines prioritized implementation order by evidence quality and risk.

## Required Deliverables Format

Return your outputs in this order:
1. `Component Inventory`
2. `Scenario Catalog by Component`
3. `Common Keyword Extraction`
4. `Proposed/Implemented Robot File Structure`
5. `Proposed/Implemented Config Structure`
6. `Validation Report`
7. `Review Report`
8. `Backlog Components`
9. `Follow-Up Prompt: Backlog Components`

## Quality Bar

- Be implementation-oriented, not narrative-only.
- Prefer deterministic, repeatable UI tests.
- Keep each step precise enough for direct Robot keyword implementation.
- Do not skip validation or review phases.

End of prompt.
