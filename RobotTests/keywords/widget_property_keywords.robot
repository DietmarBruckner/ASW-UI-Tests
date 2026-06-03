*** Settings ***
Documentation       Keywords for editing widget properties in the Automation Studio Property Window.
...                 Mirrors the C# MappView.ScrollFindProperty / EditSize / EditPosition /
...                 EditValue / EditText methods, using the FlaUILib HTTP server for
...                 Property-Window access and robotframework-flaui for element interaction.
Resource            ${CURDIR}/ide_keywords.robot
Library             FlaUILibrary    uia=UIA2


*** Keywords ***

# ── Internal helper ───────────────────────────────────────────────────────────

Scroll Find Property
    [Documentation]    Scrolls the Property Window table to make ${property_name} visible,
    ...                then optionally scrolls further to reveal the child ${sub_property}.
    ...                When ${open_sub}=True the sub-property group header is clicked to
    ...                expand it, mirroring MappView.ScrollFindProperty().
    [Arguments]    ${property_name}    ${sub_property}=${NONE}    ${open_sub}=${FALSE}
    # Focus the table so scroll events land on it
    ${pw_xpath}=    FlaUILib.Get Property Window XPath
    Click    ${pw_xpath}

    # ── Scroll UP until the first DataItem is visible (= top of list) ─────────
    ${first_visible}=    Set Variable    ${FALSE}
    WHILE    not ${first_visible}
        @{items}=    Find All Elements    ${pw_xpath}/DataItem
        ${first}=    Get From List    ${items}    0
        @{rect}=     Get Bounding Rectangle From Element    ${pw_xpath}
        @{frect}=    Get Bounding Rectangle From Element    ${first.Xpath}
        # rect: [left, top, width, height]; frect same
        ${pw_bottom}=    Evaluate    ${rect}[1] + ${rect}[3]
        ${fi_top}=       Evaluate    ${frect}[1]
        ${fi_bottom}=    Evaluate    ${frect}[1] + ${frect}[3]
        IF    ${fi_top} >= ${rect}[1] and ${fi_bottom} <= ${pw_bottom}
            ${first_visible}=    Set Variable    ${TRUE}
        ELSE
            Scroll Up    ${pw_xpath}    1
        END
    END

    # ── Find the target property group ────────────────────────────────────────
    @{groups}=    Find All Elements    ${pw_xpath}/DataItem[@Name="${property_name}"]
    ${count}=     Get Length    ${groups}
    IF    ${count} == 0
        Log    Property '${property_name}' not found in Property Window.    WARN
        RETURN
    END
    ${prop}=    Set Variable    ${groups}[0]

    # ── No sub-property: scroll DOWN until group header is visible ────────────
    IF    $sub_property is None
        ${visible}=    Set Variable    ${FALSE}
        WHILE    not ${visible}
            @{rect}=     Get Bounding Rectangle From Element    ${pw_xpath}
            @{prect}=    Get Bounding Rectangle From Element    ${prop.Xpath}
            ${pw_top}=     Evaluate    ${rect}[1]
            ${pw_bottom}=  Evaluate    ${rect}[1] + ${rect}[3]
            ${p_top}=      Evaluate    ${prect}[1]
            ${p_bottom}=   Evaluate    ${prect}[1] + ${prect}[3]
            IF    ${p_top} >= ${pw_top} and ${p_bottom} <= ${pw_bottom}
                ${visible}=    Set Variable    ${TRUE}
            ELSE
                # Check if we have already scrolled to the end
                @{all_items}=    Find All Elements    ${pw_xpath}/DataItem
                ${last}=         Get From List    ${all_items}    -1
                @{lrect}=        Get Bounding Rectangle From Element    ${last.Xpath}
                ${l_bottom}=     Evaluate    ${lrect}[1] + ${lrect}[3]
                IF    ${p_top} < ${pw_top} and ${l_bottom} <= ${pw_bottom}
                    Log    Property '${property_name}' not visible; reached end of list.    WARN
                    RETURN
                END
                Scroll Down    ${pw_xpath}    1
                @{groups}=    Find All Elements    ${pw_xpath}/DataItem[@Name="${property_name}"]
                ${prop}=      Set Variable    ${groups}[0]
            END
        END
        Scroll Down    ${pw_xpath}    2
        RETURN
    END

    # ── Sub-property: scroll DOWN until sub is found and visible ─────────────
    ${sub_visible}=    Set Variable    ${FALSE}
    WHILE    not ${sub_visible}
        @{subs}=    Find All Elements    ${prop.Xpath}/DataItem[@Name="${sub_property}"]
        ${sub_count}=    Get Length    ${subs}
        IF    ${sub_count} > 0
            ${sub}=    Set Variable    ${subs}[0]
            @{rect}=     Get Bounding Rectangle From Element    ${pw_xpath}
            @{srect}=    Get Bounding Rectangle From Element    ${sub.Xpath}
            ${pw_top}=     Evaluate    ${rect}[1]
            ${pw_bottom}=  Evaluate    ${rect}[1] + ${rect}[3]
            ${s_top}=      Evaluate    ${srect}[1]
            ${s_bottom}=   Evaluate    ${srect}[1] + ${srect}[3]
            IF    ${s_top} >= ${pw_top} and ${s_bottom} <= ${pw_bottom}
                ${sub_visible}=    Set Variable    ${TRUE}
            ELSE
                Scroll Down    ${pw_xpath}    1
                @{groups}=    Find All Elements    ${pw_xpath}/DataItem[@Name="${property_name}"]
                ${prop}=      Set Variable    ${groups}[0]
            END
        ELSE
            # Sub not yet revealed – check whether we are at end of list
            @{all_items}=    Find All Elements    ${pw_xpath}/DataItem
            ${last}=         Get From List    ${all_items}    -1
            @{lrect}=        Get Bounding Rectangle From Element    ${last.Xpath}
            @{rect}=         Get Bounding Rectangle From Element    ${pw_xpath}
            ${pw_bottom}=    Evaluate    ${rect}[1] + ${rect}[3]
            ${l_bottom}=     Evaluate    ${lrect}[1] + ${lrect}[3]
            IF    ${l_bottom} <= ${pw_bottom}
                Log    Sub-property '${sub_property}' not found; reached end of list.    WARN
                RETURN
            END
            Scroll Down    ${pw_xpath}    1
            @{groups}=    Find All Elements    ${pw_xpath}/DataItem[@Name="${property_name}"]
            ${prop}=      Set Variable    ${groups}[0]
        END
    END

    Scroll Down    ${pw_xpath}    2

    IF    ${open_sub}
        # Click the expand arrow (left edge +5px, top+5px) of the sub-property header
        @{srect}=    Get Bounding Rectangle From Element    ${sub.Xpath}
        # srect: [left, top, width, height] — use Mouse Move To absolute coords + click
        ${click_x}=    Evaluate    ${srect}[0] + 5
        ${click_y}=    Evaluate    ${srect}[1] + 5
        Mouse Move To Point    ${click_x}    ${click_y}
        Mouse Click At Point   ${click_x}    ${click_y}
        Scroll Down    ${pw_xpath}    2
    END


# ── Public editing keywords ───────────────────────────────────────────────────

Edit Widget Size
    [Documentation]    Sets width and/or height of the selected widget in the Property Window.
    ...                Pass -1 to leave a dimension unchanged.
    ...                ${is_content}=True targets the "Property" group directly (no scroll).
    ...                ${is_area}=True targets the "Layout" group directly (no scroll).
    [Arguments]    ${width}=-1    ${height}=-1    ${is_content}=${FALSE}    ${is_area}=${FALSE}
    ${pw_xpath}=    FlaUILib.Get Property Window XPath
    Click    ${pw_xpath}

    IF    ${is_content} or ${is_area}
        # Direct access – no scrolling needed
        ${group_name}=    Set Variable If    ${is_content}    Property    Layout
        ${group_xpath}=    Set Variable    ${pw_xpath}/DataItem[@Name="${group_name}"]
        ${width_xpath}=    Set Variable    ${group_xpath}/DataItem[@Name="width"]
        ${height_xpath}=    Set Variable    ${group_xpath}/DataItem[@Name="height"]
        IF    ${width} != -1
            Double Click    ${width_xpath}
            Press Key    t'${width}'
            Press Key    s'ENTER'
        END
        IF    ${height} != -1
            Double Click    ${height_xpath}
            Press Key    t'${height}'
            Press Key    s'ENTER'
        END
    ELSE
        Scroll Find Property    Layout    Size    ${TRUE}
        ${layout_xpath}=    Set Variable    ${pw_xpath}/DataItem[@Name="Layout"]
        ${size_xpath}=      Set Variable    ${layout_xpath}/DataItem[@Name="Size"]
        IF    ${width} != -1
            ${width_xpath}=    Set Variable    ${size_xpath}/DataItem[@Name="width"]
            ${cur}=    Get Value From Element    ${width_xpath}
            IF    '${cur}' != '${width}'
                Double Click    ${width_xpath}
                Press Key    t'${width}'
                Press Key    s'ENTER'
            END
        END
        IF    ${height} != -1
            ${height_xpath}=    Set Variable    ${size_xpath}/DataItem[@Name="height"]
            ${cur}=    Get Value From Element    ${height_xpath}
            IF    '${cur}' != '${height}'
                Double Click    ${height_xpath}
                Press Key    t'${height}'
                Press Key    s'ENTER'
            END
        END
    END
    FlaUILib.Click Toolbar Button    Save All


Edit Widget Position
    [Documentation]    Sets top and/or left position of the selected widget.
    ...                Pass -1 to leave a value unchanged.
    ...                ${is_area}=True reads position directly from the "Layout" group
    ...                instead of the nested "Layout > Position" group.
    [Arguments]    ${top}=-1    ${left}=-1    ${is_area}=${FALSE}
    ${pw_xpath}=    FlaUILib.Get Property Window XPath
    Click    ${pw_xpath}

    IF    ${is_area}
        Scroll Find Property    Layout    open_sub=${TRUE}
        ${parent_xpath}=    Set Variable    ${pw_xpath}/DataItem[@Name="Layout"]
    ELSE
        Scroll Find Property    Layout    Position    ${TRUE}
        ${layout_xpath}=    Set Variable    ${pw_xpath}/DataItem[@Name="Layout"]
        ${parent_xpath}=    Set Variable    ${layout_xpath}/DataItem[@Name="Position"]
        # If top is not yet visible, click the Position header to expand and scroll
        ${top_xpath}=    Set Variable    ${parent_xpath}/DataItem[@Name="top"]
        @{tops}=    Find All Elements    ${top_xpath}
        ${top_count}=    Get Length    ${tops}
        IF    ${top_count} == 0
            @{prect}=    Get Bounding Rectangle From Element    ${parent_xpath}
            ${click_x}=    Evaluate    ${prect}[0] + 5
            ${click_y}=    Evaluate    ${prect}[1] + 5
            Mouse Move To Point    ${click_x}    ${click_y}
            Mouse Click At Point   ${click_x}    ${click_y}
            Scroll Down    ${pw_xpath}    2
        ELSE
            @{rect}=     Get Bounding Rectangle From Element    ${pw_xpath}
            @{trect}=    Get Bounding Rectangle From Element    ${tops}[0].Xpath
            ${pw_top}=     Evaluate    ${rect}[1]
            ${pw_bottom}=  Evaluate    ${rect}[1] + ${rect}[3]
            ${t_top}=      Evaluate    ${trect}[1]
            ${t_bottom}=   Evaluate    ${trect}[1] + ${trect}[3]
            IF    not (${t_top} >= ${pw_top} and ${t_bottom} <= ${pw_bottom})
                @{prect}=    Get Bounding Rectangle From Element    ${parent_xpath}
                ${click_x}=    Evaluate    ${prect}[0] + 5
                ${click_y}=    Evaluate    ${prect}[1] + 5
                Mouse Move To Point    ${click_x}    ${click_y}
                Mouse Click At Point   ${click_x}    ${click_y}
                Scroll Down    ${pw_xpath}    2
            END
        END
    END

    IF    ${top} != -1
        ${top_xpath}=    Set Variable    ${parent_xpath}/DataItem[@Name="top"]
        ${cur}=    Get Value From Element    ${top_xpath}
        IF    '${cur}' != '${top}'
            Double Click    ${top_xpath}
            Press Key    t'${top}'
            Press Key    s'ENTER'
        END
    END
    IF    ${left} != -1
        ${left_xpath}=    Set Variable    ${parent_xpath}/DataItem[@Name="left"]
        ${cur}=    Get Value From Element    ${left_xpath}
        IF    '${cur}' != '${left}'
            Double Click    ${left_xpath}
            Press Key    t'${left}'
            Press Key    s'ENTER'
        END
    END
    FlaUILib.Click Toolbar Button    Save All


Edit Widget Value
    [Documentation]    Binds the widget value property to a PLC variable via the
    ...                "Select Variable" dialog. ${variable_string} is the PLC variable
    ...                name used to build the tree path:
    ...                BR_Visualizat > BR_${variable_string} > BR_value
    [Arguments]    ${variable_string}
    Scroll Find Property    Data    value    ${TRUE}
    ${pw_xpath}=      FlaUILib.Get Property Window XPath
    ${binding_xpath}=    Set Variable
    ...    ${pw_xpath}/DataItem[@Name="Data"]/DataItem[@Name="value"]/DataItem[@Name="Binding"]
    Double Click    ${binding_xpath}
    ${appeared}=    FlaUILib.Wait For Dialog    Select Variable    15
    IF    not ${appeared}
        Fail    "Select Variable" dialog did not appear
    END
    FlaUILib.Activate Tree Leaf    Binding Window
    ...    BR_Visualizat|BR_${variable_string}|BR_value
    FlaUILib.Click Toolbar Button    Save All


Edit Widget Text
    [Documentation]    Sets the widget's display text to "$IAT/${text}" via the
    ...                Appearance > Text > Default property field.
    [Arguments]    ${text}
    Scroll Find Property    Appearance    Text    ${TRUE}
    ${pw_xpath}=       FlaUILib.Get Property Window XPath
    ${default_xpath}=    Set Variable
    ...    ${pw_xpath}/DataItem[@Name="Appearance"]/DataItem[@Name="Text"]/DataItem[@Name="Default"]
    Double Click    ${default_xpath}
    Press Key    t'$IAT/${text}'
    Press Key    s'ENTER'
    FlaUILib.Click Toolbar Button    Save All

Edit Widget Name
    [Documentation]    Sets the widget name (not the display text) to ${name} via the
    ...                first "Name" property field at the top of the Property Window.
    [Arguments]    ${name}
    ${pw_xpath}=    FlaUILib.Get Property Window XPath
    ${name_xpath}=  Set Variable    ${pw_xpath}/DataItem[@Name="Name"]
    Double Click    ${name_xpath}
    Press Key    t'${name}'
    Press Key    s'ENTER'
    FlaUILib.Click Toolbar Button    Save All