```mermaid
graph TD
    Start([Start: Edit Property]) --> GetTable["Get PropertyWindow Table"]
    GetTable --> Focus["Focus Table<br/>Mouse.Click @ Center<br/>Sleep 100ms"]
    
    Focus --> Decision1{"Method<br/>Type?"}
    
    Decision1 -->|ScrollFindProperty| ScrollFlow["Scroll Find Property Flow"]
    Decision1 -->|Direct Access| DirectFlow["Direct Access<br/>content/area=true"]
    
    ScrollFlow --> SF1["GET PropertyWindow Table"]
    SF1 --> SF2["Click Center & Focus<br/>Sleep 100ms"]
    SF2 --> SF3["Get First DataItem"]
    SF3 --> SF4["Scroll UP until<br/>First Item Visible<br/>Scroll(+1)<br/>Sleep 100ms"]
    SF4 --> SF5["Find Property<br/>by Name"]
    SF5 --> SF6{"Property<br/>Found?"}
    SF6 -->|No| Return1["Return<br/>Early Exit"]
    SF6 -->|Yes| SF7{"Sub-Property<br/>Requested?"}
    
    SF7 -->|Yes| SF8["Find Sub-Property"]
    SF8 --> SF9["Scroll DOWN until<br/>Visible<br/>Scroll(-1)<br/>Sleep 100ms"]
    SF9 --> SF10{"Sub Found &<br/>Intersecting?"}
    SF10 -->|No & At End| Return2["Return<br/>Early Exit"]
    SF10 -->|Not Ready| SF9
    SF10 -->|Yes| SF11["Scroll(-2)<br/>Adjustment"]
    
    SF7 -->|No| SF9B["Scroll DOWN until<br/>Visible<br/>Scroll(-1)<br/>Sleep 100ms"]
    SF9B --> SF10B{"Property<br/>Intersecting?"}
    SF10B -->|No & At End| Return3["Return"]
    SF10B -->|Not Ready| SF9B
    SF10B -->|Yes| SF11
    
    SF11 --> OpenDecision{"opensub<br/>Flag?"}
    OpenDecision -->|Yes| OpenSub["Click @ Left+5, Top+5<br/>Expand Group<br/>Scroll(-2)<br/>Sleep 100ms"]
    OpenDecision -->|No| Return4["Return"]
    OpenSub --> Return4
    
    DirectFlow --> D1["Navigate Direct Path<br/>content? → Property : Layout"]
    D1 --> D2["Get width/height Elements"]
    D2 --> D3{"Update<br/>Needed?"}
    D3 -->|Yes| D4["Double-Click @ Right-20<br/>Type Value<br/>Press ENTER"]
    D3 -->|No| D5["Skip"]
    D4 --> D5
    D5 --> UpdateValue["Get & Update<br/>Value Field"]
    
    Return1 --> EditValue["Execute Edit<br/>Double-Click @ Right-20<br/>Type Value"]
    Return2 --> EditValue
    Return3 --> EditValue
    Return4 --> EditValue
    UpdateValue --> EditValue
    
    EditValue --> KeyPress["Press ENTER"]
    KeyPress --> SaveAll["TreeConfig.SaveAll()"]
    SaveAll --> End([End])
    
    style ScrollFlow fill:#e1f5ff
    style DirectFlow fill:#f3e5f5
    style EditValue fill:#e8f5e9
    style SaveAll fill:#fff3e0
```

---

# Summary: Property Selection Replication Plan

## What We're Replicating

You want to translate 5 interconnected C# methods from `MappView.cs` into Robot Framework keywords:

| Method | Purpose | Complexity |
|--------|---------|-----------|
| **ScrollFindProperty()** | Helper: Locates and optionally expands properties in scrollable table | High |
| **EditSize()** | Sets widget width/height | Medium |
| **EditPosition()** | Sets widget top/left position | Medium |
| **EditValue()** | Sets data binding (triggers modal) | High |
| **EditText()** | Sets widget text label | Low |

## Key Findings

### Similarity to Existing Code

Your existing Robot keywords already handle similar patterns:

1. **Add User Role in Role.role** (component_keywords.robot:98)
   - Uses: Click, TreeItem selection, Double-Click, Type, ENTER
   - Different: Works with ConfigWorkspace tree (simpler structure)

2. **Fill TMX Entries** (component_keywords.robot:41)
   - Uses: Find elements, double-click, type, press keys
   - Different: Works with table rows (simpler navigation)

**Difference**: Your new keywords must handle scrollable, hierarchical tables with:
- Variable visibility (requires scrolling to see)
- Deep nesting (Group → Sub-Group → Property)
- Pixel-perfect click positioning
- Conditional logic based on flags

### Common Pattern Across All Edit* Methods

```
1. Get PropertyWindow table
   ↓
2. Navigate to target property (scroll if needed)
   ↓
3. Locate value field (double-click point)
   ↓
4. Double-click field @ precise position (Right-20 px)
   ↓
5. Type value (with prefix if text)
   ↓
6. Press ENTER
   ↓

### Scroll Logic - The Tricky Part
```
START (items may be above or below viewport)
  ↓
IF sub-property:
  ↓
FINAL ADJUSTMENT: Scroll(-2)
  ↓
END

### Content vs Area Flags
- **content/area = false**: Standard scroll path - handles all cases

- **Standard**: Layout → Size → width/height

## Implementation Plan Overview

### Phase 1: Foundation (1-2 hours)
1. **Extend Python wrapper**: Add `GetPropertyWindowXPath()` function
2. **Test xpath access**: Verify we can get PropertyWindow table from Robot

### Phase 2: Helper Keywords (2-3 hours)
1. **Scroll Find Property** - The lynchpin, most complex
2. **Property Table Focus** - Repeated pattern extracted
3. **Get Property Value** - Parse current value
4. **Edit Property Field** - Double-click + type + ENTER

### Phase 3: Main Keywords (2-3 hours)
1. **Edit Widget Size** - Uses both direct and scroll paths
2. **Edit Widget Position** - Similar to Size, extra visibility checks
3. **Edit Widget Text** - Simple, just scroll to path
4. **Edit Widget Value** - Complex, involves modal window

### Phase 4: Integration & Testing (2-3 hours)
1. Unit test each keyword individually
2. Test with actual Automation Studio UI
3. Validate mouse/keyboard sequences
4. Compare with C# execution videos/logs

**Total Estimated Time**: 7-11 hours development + testing

## File Structure

```
NEW FILE: RobotTests/keywords/widget_property_keywords.robot
├─ Scroll Find Property                 (foundation)
├─ Edit Widget Size                     (composite)
├─ Edit Widget Position                 (composite)
├─ Edit Widget Value                    (composite + modal)
├─ Edit Widget Text                     (composite)
└─ Helper Keywords
   ├─ Focus Property Window
   ├─ Get Property Element
   ├─ Edit Property Field Value
   └─ etc.

MODIFIED FILES:
├─ FlaUILibrary/python/flaui_wrapper.py (add GetPropertyWindowXPath)
└─ component_keywords.robot             (maybe reference new keywords)
```

## Critical Success Factors

1. **Pixel-Perfect Mouse Control**
   - Right - 20 = where the value edit field actually is
   - Left + 5, Top + 5 = collapse/expand buttons
   - These must be exact

2. **Scroll Logic Accuracy**
   - Must handle both "property above viewport" and "property below viewport"
   - Must recognize when property doesn't exist
   - Must stop at list boundaries

3. **Timing**
   - 100ms sleeps critical for UI responsiveness
   - 500ms polling for modals
   - Cannot go faster without risking missed elements

4. **Element Intersection Checks**
   - Core to determining visibility
   - Must replicate C#'s `IntersectsWith()` behavior
   - FlaUILibrary must support this

5. **Conditional Paths**
   - content vs area vs standard flags change navigation
   - Modal window handling is unique to EditValue
   - Text prefix "$IAT/" required for EditText

## References & Examples

- ✅ Element navigation: MappView.cs lines 455-495

**C# to Review**:
- MappView.cs lines 495-641 (all Edit* methods and ScrollFindProperty)
- IDE_Main.cs lines 920-960 (reference for AddRole pattern)

**FlaUILibrary Keywords to Leverage**:
- Click, Double Click
- Find One Element, Find All Elements
- Press Key
- Sleep
- Mouse positioning

## Risks & Mitigations

| Risk | Mitigation |
|------|-----------|
| Pixel positioning off by a few px | Use visual debugging, capture coordinates |

## Deliverables

1. ✅ **This Plan Document** - Comprehensive breakdown
2. ✅ **Flowchart** - Visual representation of logic
3. ⏳ **widget_property_keywords.robot** - All new keywords
4. ⏳ **Enhanced Python wrapper** - GetPropertyWindowXPath()
5. ⏳ **Unit tests** - Validate each keyword
6. ⏳ **Integration tests** - Full scenario testing

---

**Next Steps**:
1. Review this plan and flowchart
2. Approve the structure and approach
3. Start Phase 1: Python wrapper enhancement
4. Proceed sequentially through phases
