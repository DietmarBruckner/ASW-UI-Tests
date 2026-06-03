# Property Editing Functionality - Flowchart & Analysis

## Executive Summary

This document provides a detailed flowchart and line-by-line analysis of the C# property editing methods that need to be replicated in Robot Framework.

---

## 1. Overall Architecture

```
┌─────────────────────────────────────────────────────────┐
│         Property Editing System (IDEMain)               │
└─────────────────────────────────────────────────────────┘
              ↓
┌─────────────────────────────────────────────────────────┐
│         IDE_Main.PropertyWindow (Table)                  │
│                                                         │
│  ├─ Layout (Group)                                      │
│  │  ├─ Size (Group)                                     │
│  │  │  ├─ width (Value)                                 │
│  │  │  └─ height (Value)                                │
│  │  └─ Position (Group)                                 │
│  │     ├─ top (Value)                                   │
│  │     └─ left (Value)                                  │
│  ├─ Appearance (Group)                                  │
│  │  └─ Text (Group)                                     │
│  │     └─ Default (Value)                               │
│  └─ Data (Group)                                        │
│     └─ value (Group)                                    │
│        └─ Binding (Value) → [Select Variable Modal]     │
│                                                         │
│  Note: All elements are TableDataItems in scrollable    │
│        table. Some values require Property/Layout       │
│        level checks (content/area flags)                │
└─────────────────────────────────────────────────────────┘
```

---

## 2. Scroll Find Property - Master Helper

### Flowchart
```
START ScrollFindProperty(property, sub=null, opensub=false)
  │
  ├─→ [1] Get PropertyWindow Table → aproperties
  │
  ├─→ [2] Center mouse on aproperties & click (focus)
  │        └─ Sleep 100ms
  │
  ├─→ [3] Get first DataItem (afirst)
  │
  ├─→ [4] SCROLL UP UNTIL FIRST ITEM VISIBLE
  │        │
  │        ├─ WHILE NOT(afirst intersects aproperties):
  │        │  ├─ Mouse.Scroll(+1.0)  [scroll up]
  │        │  ├─ Sleep 100ms
  │        │  └─ Refresh afirst
  │        │
  │        └─ [Continue after loop]
  │
  ├─→ [5] Find property by name
  │        aproperty = aproperties.FindFirstChild(cf => cf.ByName(property))
  │
  ├─→ [6] IF aproperty == null THEN Return
  │
  ├─→ [7] IF sub != null (Looking for sub-property)
  │        │
  │        ├─ Find sub: asub = aproperty.FindFirstChild(cf => cf.ByName(sub))
  │        │
  │        ├─ WHILE (asub == null OR NOT intersecting):
  │        │  │
  │        │  ├─ Mouse.Scroll(-1.0)  [scroll down]
  │        │  ├─ Sleep 100ms
  │        │  │
  │        │  ├─ Refresh aproperty (re-find it)
  │        │  ├─ Refresh asub
  │        │  ├─ Get last DataItem (alast)
  │        │  │
  │        │  ├─ IF asub==null AND alast intersecting
  │        │  │  ├─ Property doesn't exist
  │        │  │  └─ Return (early exit)
  │        │  │
  │        │  └─ [Try scrolling again]
  │        │
  │        └─ [asub now visible and intersecting]
  │
  ├─→ [8] ELSE (No sub-property, just property)
  │        │
  │        ├─ WHILE (aproperty == null OR NOT intersecting):
  │        │  │
  │        │  ├─ Mouse.Scroll(-1.0)  [scroll down]
  │        │  ├─ Sleep 100ms
  │        │  │
  │        │  ├─ Refresh aproperty
  │        │  ├─ Get last DataItem (alast)
  │        │  │
  │        │  ├─ IF aproperty==null AND alast intersecting
  │        │  │  ├─ Property doesn't exist
  │        │  │  └─ Return (early exit)
  │        │  │
  │        │  └─ [Try scrolling again]
  │        │
  │        └─ [aproperty now visible and intersecting]
  │
  ├─→ [9] Mouse.Scroll(-2.0)  [final adjustment scroll]
  │
  ├─→ [10] IF opensub == true
  │         │
  │         ├─ Click at: (asub.Left+5, asub.Top+5)
  │         │            [expand the property group]
  │         ├─ Mouse.Scroll(-2.0)
  │         └─ Sleep 100ms
  │
  └─→ END

KEY BEHAVIORS:
- Handles scrolling in BOTH directions
- Uses intersection checks to verify visibility
- Stops scrolling if end of list reached
- Can expand groups by clicking on them
```

### Line-by-Line Mapping

```csharp
C# Code                                    Robot Framework Translation
─────────────────────────────────────────  ──────────────────────────────────
// Get PropertyWindow table                Find Property Window Table
aproperties = IDE_Main.PropertyWindow      ${property_table}=    Find PropertyWindow Table
  .FindFirstDescendant(
    cf => cf.ByControlType(ControlType.Table))

Mouse.Position = aproperties...Center()    Set Mouse Position    ${property_table.Center}
Mouse.Click()                              Click Mouse
Sleep(100ms)                               Sleep    0.1s

afirst = aproperties.FindFirstChild(...)   ${first_item}=    Find DataItem    ${property_table}

while (!intersects(afirst)):               WHILE    NOT ${first_item.Intersects}
  Mouse.Scroll(1d)                         Scroll    1.0    [UP]
  Sleep(100ms)                             Sleep    0.1s
  afirst = ... [refresh]                   Update ${first_item}

aproperty = aproperties                    ${property}=    Find Element By Name
  .FindFirstChild(                         ...    ${property_name}
    cf => cf.ByName(property))

if (aproperty == null) return;             IF    ${property} == ${NONE}
                                           Return From Keyword
```

---

## 3. Edit Size

### Flowchart
```
START EditSize(width, height, content, area)
  │
  ├─→ [1] Get PropertyWindow table → aproperties
  │
  ├─→ [2] Center & click aproperties (focus)
  │        └─ Sleep 100ms
  │
  ├─→ [3] IF content OR area == true
  │        │
  │        ├─ Find "Property" (if content) OR "Layout" (if area)
  │        ├─ Get width child element (direct children)
  │        ├─ Get height child element (direct children)
  │        │
  │        ├─ IF width != -1:
  │        │  ├─ Double-click at (awidth.Right-20, awidth.Center.Y)
  │        │  ├─ Type width value
  │        │  └─ Press ENTER
  │        │
  │        └─ IF height != -1:
  │           ├─ Double-click at (aheight.Right-20, aheight.Center.Y)
  │           ├─ Type height value
  │           └─ Press ENTER
  │
  └─→ [4] ELSE (Standard widget mode)
           │
           ├─ ScrollFindProperty("Layout", "Size", opensub=true)
           │
           ├─ Get Layout element
           ├─ Get Size element (under Layout)
           ├─ Get width element (under Size)
           │
           ├─ IF width != -1 AND current_width != width:
           │  ├─ Double-click at (awidth.Right-20, awidth.Center.Y)
           │  ├─ Type width value
           │  └─ Press ENTER
           │
           ├─ Get height element (under Size)
           │
           └─ IF height != -1 AND current_height != height:
              ├─ Double-click at (aheight.Right-20, aheight.Center.Y)
              ├─ Type height value
              └─ Press ENTER
  │
  ├─→ [5] TreeConfig.SaveAll()
  │
  └─→ END

CRITICAL DETAILS:
- content=true  → "Property" group
- content=false → "Layout" group
- area=true     → "Layout" group  
- Direct access when content/area=true (no scroll)
- Scroll+expand when content/area=false
- Parse current value to check if update needed (standard mode)
- Right-20 = 20px from right edge of field (edit box trigger)
```

---

## 4. Edit Position

### Flowchart
```
START EditPosition(top, left, area)
  │
  ├─→ [1] Get PropertyWindow table → aproperties
  │        Get first DataItem (afirst)
  │        Declare alayout, aposition, atop
  │
  ├─→ [2] IF area == true
  │        │
  │        ├─ ScrollFindProperty("Layout", opensub=true)
  │        │  [Just open Layout group, no Position sub-property]
  │        │
  │        └─ Get alayout element
  │
  └─→ [3] ELSE (Standard widget positioning)
           │
           ├─ ScrollFindProperty("Layout", "Position", opensub=true)
           │  [Find and open Position under Layout]
           │
           ├─ Get alayout element
           ├─ Get aposition element (under Layout)
           │
           ├─ Get atop element (under Position)
           │
           ├─ [VISIBILITY CHECK]
           │  IF atop == null OR NOT intersecting:
           │  │  ├─ Click at (aposition.Left+5, aposition.Top+5)
           │  │  │  [Click to expand Position group if collapsed]
           │  │  │
           │  │  ├─ Mouse.Scroll(-2.0)
           │  │  └─ Sleep 100ms
           │
           ├─ Refresh atop element
           │
           ├─ Get aleft element
           │
           ├─ IF top != -1 AND current_top != top:
           │  ├─ Double-click at (atop.Right-20, atop.Center.Y)
           │  ├─ Type top value
           │  └─ Press ENTER
           │
           └─ IF left != -1 AND current_left != left:
              ├─ Double-click at (aleft.Right-20, aleft.Center.Y)
              ├─ Type left value
              └─ Press ENTER
  │
  ├─→ [4] TreeConfig.SaveAll()
  │
  └─→ END

CRITICAL DETAILS:
- area=true  → Just open Layout (for area properties)
- area=false → Navigate to Layout→Position
- Extra visibility check before updating values
- May need to click Position group to expand it
- Additional scroll-down adjustment (-2.0)
- Checks current value using int.Parse() before updating
```

---

## 5. Edit Value

### Flowchart
```
START EditValue(variablestring)
  │
  ├─→ [1] ScrollFindProperty("Data", "value", opensub=true)
  │        [Find Data group, then value sub-group, expand it]
  │
  ├─→ [2] Get PropertyWindow table → aproperties
  │
  ├─→ [3] Get Data element
  │        IF Data == null: Return (property not found)
  │
  ├─→ [4] Get value element (under Data)
  │        IF value == null: Return (property not found)
  │
  ├─→ [5] Get Binding element (under value)
  │
  ├─→ [6] Double-click at (abinding.Right-20, abinding.Center.Y)
  │        [Opens "Select Variable" modal window]
  │
  ├─→ [7] WAIT FOR MODAL (with timeout)
  │        selectVariableWindow = null
  │        WHILE selectVariableWindow == null:
  │        │  Sleep 500ms
  │        │  Try: selectVariableWindow = 
  │        │       GetModalWindow("Select Variable")
  │        └─ [Waits for modal to appear]
  │
  ├─→ [8] USE TreeConfig.ActivateTreeLeaf()
  │        Navigate: ["BR_Visualizat", "BR_" + variablestring, "BR_value"]
  │        In the modal window context
  │
  ├─→ [9] TreeConfig.SaveAll()
  │
  └─→ END

CRITICAL DETAILS:
- Most complex of the Edit methods (involves modal)
- Scrolls to Data→value→Binding
- Double-click triggers modal window
- Must wait for "Select Variable" modal to appear
- Uses different navigation (TreeConfig.ActivateTreeLeaf)
- String concatenation: "BR_" + variablestring + "BR_value"
- 500ms polling for modal appearance
```

---

## 6. Edit Text

### Flowchart
```
START EditText(text)
  │
  ├─→ [1] ScrollFindProperty("Appearance", "Text", opensub=true)
  │        [Find Appearance group, then Text sub-group, expand it]
  │
  ├─→ [2] Get PropertyWindow table → aproperties
  │
  ├─→ [3] Get Appearance element
  │
  ├─→ [4] Get Text element (under Appearance)
  │
  ├─→ [5] Get Default element (under Text)
  │
  ├─→ [6] Double-click at (adefault.Right-20, adefault.Center.Y)
  │        [Opens text editing]
  │
  ├─→ [7] Type value: "$IAT/" + text
  │        [PREFIX required: $IAT/]
  │
  ├─→ [8] Press ENTER
  │
  ├─→ [9] TreeConfig.SaveAll()
  │
  └─→ END

CRITICAL DETAILS:
- Simplest of the Edit methods
- Appearance→Text→Default hierarchy
- IMPORTANT: Prefix text with "$IAT/"
- This is a localization/internationalization code
- One double-click and one text entry sequence
```

---

## 7. Commonalities Across All Methods

### Repeated Patterns

```
PATTERN 1: Property Table Access
├─ aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(Table)
├─ Mouse.Position = aproperties.Center()
├─ Mouse.Click()
└─ Sleep(100ms)

PATTERN 2: Field Value Update
├─ Mouse.DoubleClick(Right-20, Center.Y)
├─ Keyboard.Type(value)
└─ Keyboard.ENTER

PATTERN 3: Hierarchical Navigation
├─ Get Parent Group
├─ Get Child Property
├─ (If sub-property): Get nested element
└─ Return nested element for interaction

PATTERN 4: Scroll Management
├─ ScrollFindProperty() for main navigation
├─ Mouse.Scroll(+1/-1/-2) for fine control
└─ Sleep(100ms) after each scroll

PATTERN 5: Finalization
├─ TreeConfig.SaveAll()
└─ END (implicit)
```

### Differences by Method

```
METHOD          | SCROLL TARGET        | HIERARCHY DEPTH | MODAL | SPECIAL LOGIC
────────────────┼──────────────────────┼─────────────────┼───────┼──────────────────────
EditSize        | Layout→Size          | 3               | No    | content/area flag routing
EditPosition    | Layout→Position      | 3               | No    | Extra visibility check
EditValue       | Data→value→Binding   | 4               | Yes   | Modal window wait + nav
EditText        | Appearance→Text→Def  | 4               | No    | String prefix "$IAT/"
```

---

## 8. Implementation Notes for Robot Framework

### Keyword Structure Template

```robot
[Keyword Name]
    [Documentation]    [Purpose description]
    [Arguments]        ${arg1}    ${arg2}    ${flag1}=${DEFAULT}
    
    ${property_table}=    Get PropertyWindow Table
    Focus Property Window    ${property_table}
    
    Scroll Find Property    ${primary}    ${secondary}    ${expand}
    
    [Get Elements and Navigate]
    ${target_element}=    Get Property Element    ${parent}    ${child}
    
    [Update Value]
    Edit Property Value    ${target_element}    ${new_value}
    
    Save All Changes
```

### Critical Implementation Requirements

1. **Pixel-Perfect Positioning**
   - Right - 20 = Target click position for value fields
   - Left + 5, Top + 5 = Click position for expand/collapsible groups

2. **Timing**
   - 100ms sleep after focus clicks
   - 100ms sleep after scroll operations
   - 500ms polling for modal windows

3. **Scroll Deltas**
   - Scroll(+1.0) = Scroll UP (show earlier items)
   - Scroll(-1.0) = Scroll DOWN (show later items)
   - Scroll(-2.0) = Final adjustment

4. **Element Intersection**
   - Must verify element.BoundingRectangle intersects with container.BoundingRectangle
   - Used for visibility checks

5. **Value Parsing**
   - Some methods parse current values (int.Parse) before updating
   - Prevents unnecessary updates when value unchanged

6. **Type-Specific Handling**
   - text: Prefix with "$IAT/"
   - width/height/top/left: Direct numeric input
   - binding: Triggers modal navigation

### Python Wrapper Enhancement Needed

```python
def get_property_window_xpath():
    """
    Returns the xpath to IDE_Main.PropertyWindow table
    Equivalent to: IDE_Main.PropertyWindow.FindFirstDescendant(
                     cf => cf.ByControlType(ControlType.Table))
    """
    # Implementation needed
    return xpath_to_property_window
```

---

## 9. Validation Checklist

Before implementation, verify:

- [ ] PropertyWindow xpath accessible via new Python function
- [ ] FlaUILibrary supports element.Patterns.Value.Pattern.Value (value parsing)
- [ ] Mouse positioning with Right-20 pixel offset available
- [ ] Scroll delta (+1, -1, -2) support confirmed
- [ ] BoundingRectangle.IntersectsWith() accessible
- [ ] Modal window detection ("Select Variable") functional
- [ ] TreeConfig.ActivateTreeLeaf() available for modal navigation
- [ ] All timing (100ms, 500ms) supported in Robot Framework

---

## 10. File Organization

**Recommended new file:**
```
RobotTests/keywords/widget_property_keywords.robot
```

**Contains:**
- Scroll Find Property (foundation)
- Edit Widget Size
- Edit Widget Position
- Edit Widget Value
- Edit Widget Text
- Supporting helpers

**Links to:**
- component_keywords.robot (existing UI interaction patterns)
- project_keywords.robot (general keywords)
