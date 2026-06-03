# C# to Robot Framework: Line-by-Line Mapping Reference

## Purpose
This document provides direct C# → Robot Framework translations for each statement in the Edit* methods.
Use this as your implementation checklist.

---

## 1. ScrollFindProperty - Complete Mapping

### Section 1.1: Initialization & Focus

**C# (Lines 597-608)**
```csharp
void ScrollFindProperty(string property, string sub = null, bool opensub = false) {
    AutomationElement aproperties = IDE_Main.PropertyWindow
        .FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
    
    Mouse.Position = aproperties.BoundingRectangle.Center();
    Mouse.Click();
    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
    
    AutomationElement afirst = aproperties
        .FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
    AutomationElement aproperty = aproperties
        .FindFirstChild(cf => cf.ByName(property));
    AutomationElement alast, asub = null;
```

**Robot Framework Translation**
```robot
Scroll Find Property
    [Arguments]    ${property_name}    ${sub_property}=${NONE}    ${open_sub}=${FALSE}
    [Documentation]    Scrolls PropertyWindow to find property and optionally expands it
    
    # Get PropertyWindow table
    ${property_table}=    Get PropertyWindow Table
    
    # Center & click to focus
    ${center_pos}=    Get Element Center    ${property_table}
    Move Mouse To    ${center_pos}
    Click Mouse
    Sleep    0.1s
    
    # Get reference to first DataItem (for scroll-to-top verification)
    ${first_item}=    Find First Child    ${property_table}    ControlType.DataItem
    
    # Try to find property (may not be visible yet)
    ${target_property}=    Find First Child    ${property_table}    Name=${property_name}
    
    # Initialize variables
    ${last_item}=    ${NONE}
    ${target_sub}=    ${NONE}
```

### Section 1.2: Scroll Up Until First Item Visible

**C# (Lines 609-616)**
```csharp
    while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
        Mouse.Scroll(1d);
        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
        afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
    }
    aproperty = aproperties.FindFirstChild(cf => cf.ByName(property));
    if (aproperty == null)
        return;
```

**Robot Framework Translation**
```robot
    # Scroll UP until first item is visible in viewport
    WHILE    NOT ${first_item.IntersectsWith($property_table)}
        Scroll Mouse    1.0
        Sleep    0.1s
        ${first_item}=    Find First Child    ${property_table}    ControlType.DataItem
    END
    
    # Attempt to find target property
    ${target_property}=    Find First Child    ${property_table}    Name=${property_name}
    
    # Exit if property not found
    IF    ${target_property} == ${NONE}
        Return From Keyword
    END
```

### Section 1.3: Find Sub-Property (If Requested)

**C# (Lines 617-631)**
```csharp
    if (sub != null) {
        asub = aproperty.FindFirstChild(cf => cf.ByName(sub));
        while (asub == null || !aproperties.BoundingRectangle.IntersectsWith(asub.BoundingRectangle)) {
            Mouse.Scroll(-1d);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            aproperty = aproperties.FindFirstChild(cf => cf.ByName(property));
            asub = aproperty.FindFirstChild(cf => cf.ByName(sub));
            alast = aproperties.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem)).Last();
            if (asub == null && aproperties.BoundingRectangle.IntersectsWith(alast.BoundingRectangle))
                return;
        }
    }
```

**Robot Framework Translation**
```robot
    IF    ${sub_property} != ${NONE}
        # Try to find sub-property under main property
        ${target_sub}=    Find First Child    ${target_property}    Name=${sub_property}
        
        # Scroll DOWN until sub-property is visible AND intersecting with viewport
        WHILE    ${target_sub} == ${NONE} OR NOT ${target_sub.IntersectsWith($property_table)}
            Scroll Mouse    -1.0
            Sleep    0.1s
            
            # Refresh references (re-find elements)
            ${target_property}=    Find First Child    ${property_table}    Name=${property_name}
            ${target_sub}=    Find First Child    ${target_property}    Name=${sub_property}
            
            # Get last item to check if we've reached end of list
            @{all_items}=    Find All Children    ${property_table}    ControlType.DataItem
            ${last_item}=    Get From List    ${all_items}    -1
            
            # Exit if sub-property doesn't exist (reached end of list)
            IF    ${target_sub} == ${NONE} AND ${last_item.IntersectsWith($property_table)}
                Return From Keyword
            END
        END
    END
```

### Section 1.4: Find Property Only (If No Sub-Property)

**C# (Lines 632-638)**
```csharp
    else {
        while (aproperty == null || !aproperties.BoundingRectangle.IntersectsWith(aproperty.BoundingRectangle)) {
            Mouse.Scroll(-1d);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            aproperty = aproperties.FindFirstChild(cf => cf.ByName(property));
            alast = aproperties.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem)).Last();
            if (aproperty == null && aproperties.BoundingRectangle.IntersectsWith(alast.BoundingRectangle))
                return;
        }
    }
```

**Robot Framework Translation**
```robot
    ELSE IF    ${sub_property} == ${NONE}
        # Scroll DOWN until property is visible AND intersecting with viewport
        WHILE    ${target_property} == ${NONE} OR NOT ${target_property.IntersectsWith($property_table)}
            Scroll Mouse    -1.0
            Sleep    0.1s
            
            # Refresh property reference
            ${target_property}=    Find First Child    ${property_table}    Name=${property_name}
            
            # Get last item to check if we've reached end of list
            @{all_items}=    Find All Children    ${property_table}    ControlType.DataItem
            ${last_item}=    Get From List    ${all_items}    -1
            
            # Exit if property doesn't exist (reached end of list)
            IF    ${target_property} == ${NONE} AND ${last_item.IntersectsWith($property_table)}
                Return From Keyword
            END
        END
    END
```

### Section 1.5: Final Adjustment & Optional Expand

**C# (Lines 639-645)**
```csharp
    Mouse.Scroll(-2d);
    if (opensub) {
        Mouse.Click(new Point {X = asub.BoundingRectangle.Left + 5, Y = asub.BoundingRectangle.Top + 5});
        Mouse.Scroll(-2d);
        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
    }
}
```

**Robot Framework Translation**
```robot
    # Final scroll adjustment (move viewport down a bit)
    Scroll Mouse    -2.0
    
    # Optionally expand the sub-property group
    IF    ${open_sub} == ${TRUE}
        # Click at (Left+5, Top+5) to expand group
        ${expand_pos}=    Create Dictionary    
        ...    x=${target_sub.Left + 5}
        ...    y=${target_sub.Top + 5}
        Move Mouse To    ${expand_pos}
        Click Mouse
        
        Scroll Mouse    -2.0
        Sleep    0.1s
    END
```

---

## 2. EditSize - Complete Mapping

### Section 2.1: Path A - Content/Area Mode

**C# (Lines 497-515)**
```csharp
void EditSize(int width = -1, int height = -1, bool content = false, bool area = false) {
    AutomationElement aproperties = IDE_Main.PropertyWindow
        .FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
    Mouse.Position = aproperties.BoundingRectangle.Center();
    Mouse.Click();
    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
    AutomationElement afirst = aproperties
        .FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
    
    if (content || area) {
        AutomationElement aproperty = aproperties
            .FindFirstChild(cf => cf.ByName(content?"Property":"Layout"));
        AutomationElement aheight = aproperty.FindFirstChild(cf => cf.ByName("height"));
        AutomationElement awidth = aproperty.FindFirstChild(cf => cf.ByName("width"));
        
        if (width != -1) {
            Mouse.DoubleClick(new Point {
                X = awidth.BoundingRectangle.Right - 20, 
                Y = awidth.BoundingRectangle.Top + awidth.BoundingRectangle.Height/2});
            Keyboard.Type("" + width);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
        }
        if (height != -1) {
            Mouse.DoubleClick(new Point {
                X = aheight.BoundingRectangle.Right - 20, 
                Y = aheight.BoundingRectangle.Top + aheight.BoundingRectangle.Height/2});
            Keyboard.Type("" + height);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
        }
    }
```

**Robot Framework Translation**
```robot
Edit Widget Size
    [Arguments]    ${width}=-1    ${height}=-1    ${is_content}=${FALSE}    ${is_area}=${FALSE}
    [Documentation]    Sets widget size (width and/or height)
    
    # Get PropertyWindow table
    ${property_table}=    Get PropertyWindow Table
    
    # Focus the table
    ${center_pos}=    Get Element Center    ${property_table}
    Move Mouse To    ${center_pos}
    Click Mouse
    Sleep    0.1s
    
    # Get first item (for verification)
    ${first_item}=    Find First Child    ${property_table}    ControlType.DataItem
    
    IF    ${is_content} == ${TRUE} OR ${is_area} == ${TRUE}
        # DIRECT ACCESS MODE (no scroll needed)
        
        # Determine which group to access
        ${group_name}=    Set Variable If    ${is_content}    Property    Layout
        ${target_group}=    Find First Child    ${property_table}    Name=${group_name}
        
        # Get width and height elements (direct children)
        ${width_element}=    Find First Child    ${target_group}    Name=width
        ${height_element}=    Find First Child    ${target_group}    Name=height
        
        # Update width if provided
        IF    ${width} != -1
            ${click_x}=    Evaluate    ${width_element.Right} - 20
            ${click_y}=    Evaluate    ${width_element.Top} + ${width_element.Height}/2
            Double Click At Position    ${click_x}    ${click_y}
            Type Text    ${width}
            Press Key    s'ENTER'
        END
        
        # Update height if provided
        IF    ${height} != -1
            ${click_x}=    Evaluate    ${height_element.Right} - 20
            ${click_y}=    Evaluate    ${height_element.Top} + ${height_element.Height}/2
            Double Click At Position    ${click_x}    ${click_y}
            Type Text    ${height}
            Press Key    s'ENTER'
        END
```

### Section 2.2: Path B - Standard Mode

**C# (Lines 518-532)**
```csharp
    else {
        ScrollFindProperty("Layout", "Size", true);
        AutomationElement alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
        AutomationElement asize = alayout.FindFirstChild(cf => cf.ByName("Size"));
        AutomationElement awidth = asize.FindFirstChild(cf => cf.ByName("width"));
        if (width != -1 && int.Parse(awidth.Patterns.Value.Pattern.Value) != width) {
            Mouse.DoubleClick(new Point {
                X = awidth.BoundingRectangle.Right - 20, 
                Y = awidth.BoundingRectangle.Top + awidth.BoundingRectangle.Height/2});
            Keyboard.Type("" + width);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
        }
        AutomationElement aheight = asize.FindFirstChild(cf => cf.ByName("height"));
        if (height != -1 && int.Parse(aheight.Patterns.Value.Pattern.Value) != height) {
            Mouse.DoubleClick(new Point {
                X = aheight.BoundingRectangle.Right - 20, 
                Y = aheight.BoundingRectangle.Top + aheight.BoundingRectangle.Height/2});
            Keyboard.Type("" + height);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
        }
    }
    TreeConfig.IdeMain.SaveAll();
}
```

**Robot Framework Translation**
```robot
    ELSE
        # SCROLL MODE (standard navigation)
        Scroll Find Property    Layout    Size    ${TRUE}
        
        # Navigate hierarchy: Layout → Size → width/height
        ${property_table}=    Get PropertyWindow Table
        ${layout_element}=    Find First Child    ${property_table}    Name=Layout
        ${size_element}=    Find First Child    ${layout_element}    Name=Size
        ${width_element}=    Find First Child    ${size_element}    Name=width
        
        # Update width if provided AND value is different
        IF    ${width} != -1
            ${current_width}=    Get Element Value    ${width_element}
            ${current_width_int}=    Convert To Integer    ${current_width}
            IF    ${current_width_int} != ${width}
                ${click_x}=    Evaluate    ${width_element.Right} - 20
                ${click_y}=    Evaluate    ${width_element.Top} + ${width_element.Height}/2
                Double Click At Position    ${click_x}    ${click_y}
                Type Text    ${width}
                Press Key    s'ENTER'
            END
        END
        
        # Get and update height if provided AND value is different
        ${height_element}=    Find First Child    ${size_element}    Name=height
        IF    ${height} != -1
            ${current_height}=    Get Element Value    ${height_element}
            ${current_height_int}=    Convert To Integer    ${current_height}
            IF    ${current_height_int} != ${height}
                ${click_x}=    Evaluate    ${height_element.Right} - 20
                ${click_y}=    Evaluate    ${height_element.Top} + ${height_element.Height}/2
                Double Click At Position    ${click_x}    ${click_y}
                Type Text    ${height}
                Press Key    s'ENTER'
            END
        END
    END
    
    # Save all changes
    Save All IDE Changes
```

---

## 3. EditPosition - Complete Mapping

**C# (Lines 536-566)**
```csharp
void EditPosition(int top = -1, int left = -1, bool area = false) {
    AutomationElement aproperties = IDE_Main.PropertyWindow
        .FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
    AutomationElement afirst = aproperties
        .FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
    AutomationElement alayout, aposition = null, atop;
    
    if (area) {
        ScrollFindProperty("Layout", opensub:true);
        alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
    }
    else {
        ScrollFindProperty("Layout", "Position", true);
        alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
        aposition = alayout.FindFirstChild(cf => cf.ByName("Position"));
        atop = aposition.FindFirstChild(cf => cf.ByName("top"));
        if (atop == null || !aproperties.BoundingRectangle.IntersectsWith(atop.BoundingRectangle))
            Mouse.Click(new Point {X = aposition.BoundingRectangle.Left + 5, Y = aposition.BoundingRectangle.Top + 5});
        Mouse.Scroll(-2d);
        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
    }
    
    atop = (area?alayout:aposition).FindFirstChild(cf => cf.ByName("top"));
    AutomationElement aleft = (area?alayout:aposition).FindFirstChild(cf => cf.ByName("left"));
    
    if (top != -1 && int.Parse(atop.Patterns.Value.Pattern.Value) != top) {
        Mouse.DoubleClick(new Point {X = atop.BoundingRectangle.Right - 20, Y = atop.BoundingRectangle.Top + atop.BoundingRectangle.Height/2});
        Keyboard.Type("" + top);
        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
    }
    if (left != -1 && int.Parse(aleft.Patterns.Value.Pattern.Value) != left) {
        Mouse.DoubleClick(new Point {X = aleft.BoundingRectangle.Right - 20, Y = aleft.BoundingRectangle.Top + aleft.BoundingRectangle.Height/2});
        Keyboard.Type("" + left);
        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
    }
    TreeConfig.IdeMain.SaveAll();
}
```

**Robot Framework Translation**
```robot
Edit Widget Position
    [Arguments]    ${top}=-1    ${left}=-1    ${is_area}=${FALSE}
    [Documentation]    Sets widget position (top and/or left)
    
    # Get PropertyWindow table
    ${property_table}=    Get PropertyWindow Table
    ${first_item}=    Find First Child    ${property_table}    ControlType.DataItem
    
    # Navigate based on area flag
    IF    ${is_area} == ${TRUE}
        # Area mode: Just open Layout
        Scroll Find Property    Layout    ${NONE}    ${TRUE}
        ${layout_element}=    Find First Child    ${property_table}    Name=Layout
        
    ELSE
        # Standard mode: Navigate to Layout → Position
        Scroll Find Property    Layout    Position    ${TRUE}
        
        ${layout_element}=    Find First Child    ${property_table}    Name=Layout
        ${position_element}=    Find First Child    ${layout_element}    Name=Position
        ${top_element}=    Find First Child    ${position_element}    Name=top
        
        # CRITICAL: Check if top element is visible; if not, click Position to expand
        ${top_intersects}=    Element Intersects Container    ${top_element}    ${property_table}
        IF    ${top_element} == ${NONE} OR ${top_intersects} == ${FALSE}
            # Click Position group to expand it (Left+5, Top+5)
            ${click_x}=    Evaluate    ${position_element.Left} + 5
            ${click_y}=    Evaluate    ${position_element.Top} + 5
            Move Mouse To    ${click_x}    ${click_y}
            Click Mouse
        END
        
        Scroll Mouse    -2.0
        Sleep    0.1s
    END
    
    IF    ${is_area} == ${TRUE}
        ${parent_for_values}=    Set Variable    ${layout_element}
    ELSE
        ${parent_for_values}=    Set Variable    ${position_element}
    END
    # Get top and left elements
    ${top_element}=    Find First Child    ${parent_for_values}    Name=top
    ${left_element}=    Find First Child    ${parent_for_values}    Name=left
    
    # Update top if provided AND different
    IF    ${top} != -1
        ${current_top}=    Get Element Value    ${top_element}
        ${current_top_int}=    Convert To Integer    ${current_top}
        IF    ${current_top_int} != ${top}
            ${click_x}=    Evaluate    ${top_element.Right} - 20
            ${click_y}=    Evaluate    ${top_element.Top} + ${top_element.Height}/2
            Double Click At Position    ${click_x}    ${click_y}
            Type Text    ${top}
            Press Key    s'ENTER'
        END
    END
    
    # Update left if provided AND different
    IF    ${left} != -1
        ${current_left}=    Get Element Value    ${left_element}
        ${current_left_int}=    Convert To Integer    ${current_left}
        IF    ${current_left_int} != ${left}
            ${click_x}=    Evaluate    ${left_element.Right} - 20
            ${click_y}=    Evaluate    ${left_element.Top} + ${left_element.Height}/2
            Double Click At Position    ${click_x}    ${click_y}
            Type Text    ${left}
            Press Key    s'ENTER'
        END
    END
    
    Save All IDE Changes
```

---

## 4. EditValue - Complete Mapping

**C# (Lines 568-584)**
```csharp
void EditValue(string variablestring) {
    ScrollFindProperty("Data", "value", true);
    AutomationElement aproperties = IDE_Main.PropertyWindow
        .FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
    AutomationElement adata = aproperties.FindFirstChild(cf => cf.ByName("Data"));
    if (adata == null)
        return;
    AutomationElement avalue = adata.FindFirstChild(cf => cf.ByName("value"));
    if (avalue == null)
        return;
    AutomationElement abinding = avalue.FindFirstChild(cf => cf.ByName("Binding"));
    Mouse.DoubleClick(new Point {X = abinding.BoundingRectangle.Right - 20, Y = abinding.BoundingRectangle.Top + abinding.BoundingRectangle.Height/2});
    FlaUI.Core.AutomationElements.Window selectVariableWindow;
    while ((selectVariableWindow = TreeConfig.IdeMain.GetModalWindow("Select Variable")) == null)
        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
    TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.BindingWindow, new List<string> { "BR_Visualizat", "BR_" + variablestring, "BR_value"}, out var e, selectVariableWindow);
    TreeConfig.IdeMain.SaveAll();
}
```

**Robot Framework Translation**
```robot
Edit Widget Value
    [Arguments]    ${variable_string}
    [Documentation]    Sets widget data binding value via Select Variable modal
    
    # Scroll to Data → value → Binding and expand
    Scroll Find Property    Data    value    ${TRUE}
    
    # Get PropertyWindow table
    ${property_table}=    Get PropertyWindow Table
    
    # Navigate hierarchy: Data → value → Binding
    ${data_element}=    Find First Child    ${property_table}    Name=Data
    IF    ${data_element} == ${NONE}
        Return From Keyword
    END
    
    ${value_element}=    Find First Child    ${data_element}    Name=value
    IF    ${value_element} == ${NONE}
        Return From Keyword
    END
    
    ${binding_element}=    Find First Child    ${value_element}    Name=Binding
    
    # Double-click opens "Select Variable" modal window
    ${click_x}=    Evaluate    ${binding_element.Right} - 20
    ${click_y}=    Evaluate    ${binding_element.Top} + ${binding_element.Height}/2
    Double Click At Position    ${click_x}    ${click_y}
    
    # Wait for modal window to appear ("Select Variable")
    ${modal_window}=    Wait For Modal Window    Select Variable    timeout=10
    
    # Navigate tree in modal: BR_Visualizat → BR_${variable_string} → BR_value
    @{path_items}=    Create List    BR_Visualizat    BR_${variable_string}    BR_value
    Activate Tree Leaf In Modal    ${modal_window}    @{path_items}
    
    # Save all changes
    Save All IDE Changes
```

---

## 5. EditText - Complete Mapping

**C# (Lines 586-595)**
```csharp
void EditText(string text) {
    ScrollFindProperty("Appearance", "Text", true);
    AutomationElement aproperties = IDE_Main.PropertyWindow
        .FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
    AutomationElement appearance = aproperties.FindFirstChild(cf => cf.ByName("Appearance"));
    AutomationElement atext = appearance.FindFirstChild(cf => cf.ByName("Text"));
    AutomationElement adefault = atext.FindFirstChild(cf => cf.ByName("Default"));
    Mouse.DoubleClick(new Point {X = adefault.BoundingRectangle.Right - 20, Y = adefault.BoundingRectangle.Top + adefault.BoundingRectangle.Height/2});
    Keyboard.Type("$IAT/" + text);
    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
    TreeConfig.IdeMain.SaveAll();
}
```

**Robot Framework Translation**
```robot
Edit Widget Text
    [Arguments]    ${text}
    [Documentation]    Sets widget text in Appearance properties
    
    # Scroll to Appearance → Text → Default and expand
    Scroll Find Property    Appearance    Text    ${TRUE}
    
    # Get PropertyWindow table
    ${property_table}=    Get PropertyWindow Table
    
    # Navigate hierarchy: Appearance → Text → Default
    ${appearance_element}=    Find First Child    ${property_table}    Name=Appearance
    ${text_element}=    Find First Child    ${appearance_element}    Name=Text
    ${default_element}=    Find First Child    ${text_element}    Name=Default
    
    # Double-click to open text field
    ${click_x}=    Evaluate    ${default_element.Right} - 20
    ${click_y}=    Evaluate    ${default_element.Top} + ${default_element.Height}/2
    Double Click At Position    ${click_x}    ${click_y}
    
    # Type text with required prefix
    Type Text    \$IAT/${text}
    
    # Press ENTER to confirm
    Press Key    s'ENTER'
    
    # Save all changes
    Save All IDE Changes
```

---

## 6. New FlaUILibrary Keywords Needed

Based on the mappings above, you'll need these new keywords in FlaUILibrary:

```robot
*** Keywords ***

Get PropertyWindow Table
    [Documentation]    Returns the PropertyWindow table element from IDEMain
    # Calls Python wrapper: GetPropertyWindowXPath()
    ${xpath}=    FlaUILib.Get Property Window XPath
    ${table}=    Find One Element    ${xpath}
    RETURN    ${table}

Get Element Center
    [Arguments]    ${element}
    [Documentation]    Returns the center point of element's bounding rectangle
    ${center}=    Evaluate    {
    ...    'x': (${element.Right} + ${element.Left})/2,
    ...    'y': (${element.Bottom} + ${element.Top})/2
    ...    }
    RETURN    ${center}

Element Intersects Container
    [Arguments]    ${element}    ${container}
    [Documentation]    Checks if element's bounding rect intersects container
    # Uses element.IntersectsWith() from FlaUI
    ${intersects}=    Evaluate    ${element.BoundingRectangle.IntersectsWith($container.BoundingRectangle)}
    RETURN    ${intersects}

Get Element Value
    [Arguments]    ${element}
    [Documentation]    Gets current value from element (int.Parse equivalent)
    # Accesses: element.Patterns.Value.Pattern.Value
    ${value}=    Evaluate    ${element.Value}
    RETURN    ${value}

Scroll Mouse
    [Arguments]    ${delta}
    [Documentation]    Scrolls mouse wheel with delta (+1 up, -1 down)
    # Wraps: Mouse.Scroll(delta)
    FlaUILib.Scroll    ${delta}

Double Click At Position
    [Arguments]    ${x}    ${y}
    [Documentation]    Double-clicks at specific pixel coordinates
    FlaUILib.Double Click    position=(${x}, ${y})

Save All IDE Changes
    [Documentation]    Calls TreeConfig.IdeMain.SaveAll()
    # Python wrapper method
    FlaUILib.Save All

Wait For Modal Window
    [Arguments]    ${title}    ${timeout}=10
    [Documentation]    Waits for modal window with given title
    # Polls: TreeConfig.IdeMain.GetModalWindow(title)
    ${modal}=    FlaUILib.Get Modal Window    ${title}    ${timeout}
    RETURN    ${modal}

Activate Tree Leaf In Modal
    [Arguments]    ${window}    @{path}
    [Documentation]    Activates tree leaf in modal window
    # Calls: TreeConfig.ActivateTreeLeaf(..., window)
    FlaUILib.Activate Tree Leaf    @{path}    in_window=${window}
```

---

## Implementation Checklist

Use this to track your progress:

### Phase 1: Infrastructure
- [ ] Implement scroll-up loop (lines 609-616 C#)
- [ ] Implement find property (lines 617-620)
- [ ] Implement scroll-down for sub-property (lines 621-629)
- [ ] Implement scroll-down for property only (lines 630-635)
- [ ] Implement final adjustment (lines 636-642)
- [ ] Test with actual PropertyWindow elements

### Phase 3: EditSize
- [ ] Implement direct access path (content/area=true)
- [ ] Implement scroll path (content/area=false)
- [ ] Test value comparison logic (int.Parse)
- [ ] Verify pixel positioning (Right-20)
- [ ] Test with various widget sizes

### Phase 4: EditPosition
- [ ] Implement area=true path
- [ ] Implement area=false path
- [ ] Test visibility check (intersection logic)
- [ ] Test Position group expansion (Left+5, Top+5 click)
- [ ] Verify scroll-down adjustment (-2.0)

### Phase 5: EditValue
- [ ] Implement Data → value navigation
- [ ] Implement modal window wait loop
- [ ] Implement tree navigation in modal
- [ ] Test variable string concatenation
- [ ] Test with actual Select Variable modal

### Phase 6: EditText
- [ ] Implement Appearance → Text navigation
- [ ] Test "$IAT/" prefix addition
- [ ] Verify text input and ENTER press
- [ ] Test with various text values

### Phase 7: Testing
- [ ] Unit test each keyword individually
- [ ] Integration test with real UI
- [ ] Verify timing matches C# (100ms, 500ms)
- [ ] Compare mouse/keyboard sequences
- [ ] Full scenario testing

---

**Total Code Lines to Create**: ~500 Robot Framework lines
**Total C# Lines Being Replicated**: ~150 C# lines

This mapping should be your implementation guide!
