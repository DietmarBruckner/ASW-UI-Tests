# QUICK REFERENCE: Updated Plan with Existing Keywords

**Status**: Plan finalized and significantly simplified  
**Date**: June 2, 2026  
**Impact**: 40% time reduction, ~200 fewer code lines

---

## 🎯 TL;DR - What You Need to Know

### Most Keywords Already Exist in FlaUILibrary

| Operation | Keyword | Notes |
|-----------|---------|-------|
| Click element center | `Click ${xpath}` | Auto-centers |
| Double-click element center | `Double Click ${xpath}` | Auto-centers |
| Move mouse | `Move To ${xpath}` | No click |
| Get rectangle bounds | `@{rect}= Get Rectangle Bounding From Element ${xpath}` | Returns [left, top, width, height] |
| Get element value | `${val}= Get Property From Element ${xpath} VALUE` | Replaces int.Parse |
| Scroll down | `Scroll Down ${xpath} 1` | Existing keyword |
| Scroll up | `Scroll Up ${xpath} 1` | Existing keyword |
| Save all | `FlaUILib.Click Toolbar Button Save All` | Existing |
| Wait for modal | `${appeared}= FlaUILib.Wait For Dialog ${title} 10` | Existing |
| Navigate tree | `Activate Tree Leaf @{path}` | Already widely used |

### Only ONE New Python Function Needed

```python
def get_property_window_xpath():
    """Returns xpath to IDE_Main.PropertyWindow table"""
    return xpath_string
```

---

## 📊 Updated Timeline

| Phase | Duration | What You Do |
|-------|----------|-----------|
| **Phase 1: Infrastructure** | 30 min | Add GetPropertyWindowXPath() |
| **Phase 2: ScrollFindProperty** | 1.5-2 hrs | Create core helper using existing keywords |
| **Phase 3: Edit Methods** | 3-4 hrs | EditSize, EditPosition, EditValue, EditText |
| **Phase 4: Testing** | 1-2 hrs | Unit + integration testing |
| **TOTAL** | **6-8.5 hrs** | ✅ 40% faster than original estimate |

---

## 📋 Code Simplifications

### Example 1: Focus PropertyWindow

**OLD (Complex)**:
```robot
${property_table}=    Get PropertyWindow Table     # Custom keyword
${center_pos}=    Get Element Center    ${property_table}  # Custom calculation
Move Mouse To    ${center_pos}
Click Mouse
```

**NEW (Simple)**:
```robot
${property_window_xpath}=    Get PropertyWindow XPath    # NEW wrapper
Click    ${property_window_xpath}                         # Existing keyword
```

### Example 2: Scroll and Find

**OLD (Complex)**:
```robot
WHILE    NOT ${first_item.IntersectsWith($property_table)}  # Custom check
    Scroll Mouse    1.0                                     # Custom keyword
    Sleep    0.1s
    ${first_item}=    Update First Item Reference
END
```

**NEW (Simple)**:
```robot
WHILE    (intersection check)
    Scroll Up    ${property_table_xpath}    1    # Existing keyword
    ${first_item}=    Find First Child    ${property_table_xpath}
END
```

### Example 3: Update Value

**OLD (Complex)**:
```robot
Double Click At Position    ${click_x}    ${click_y}    # Custom keyword
${current}=    Get Element Value    ${element}         # Custom keyword
```

**NEW (Simple)**:
```robot
Double Click    ${element_xpath}                                     # Existing
${current}=    Get Property From Element    ${element_xpath}    VALUE  # Existing
```

### Example 4: Wait for Modal

**OLD (Complex)**:
```robot
${modal_window}=    Wait For Modal Window    "Select Variable"    10    # Custom loop
Activate Tree Leaf In Modal    ${modal}    @{path}                      # Custom wrapper
```

**NEW (Simple)**:
```robot
${appeared}=    FlaUILib.Wait For Dialog    Select Variable    10    # Existing
Activate Tree Leaf    @{path}                                       # Existing
```

---

## 🚀 Implementation Checklist

### Phase 1: Infrastructure (30 min)
- [ ] Add `get_property_window_xpath()` to FlaUILibrary Python wrapper
- [ ] Test: `${xpath}= Get Property Window XPath` returns valid xpath
- [ ] Verify: Can find elements using that xpath

### Phase 2: ScrollFindProperty (1.5-2 hours)
- [ ] Create keyword structure
- [ ] Scroll up until first item visible (use `Scroll Up`)
- [ ] Find property (use `Find First Child`)
- [ ] Scroll down if needed (use `Scroll Down`)
- [ ] Expand if requested (use `Click` on expand button)
- [ ] Test with actual PropertyWindow

### Phase 3: EditSize (1 hour)
- [ ] Scroll to Layout→Size (use ScrollFindProperty)
- [ ] Get width/height elements (use `Find First Child`)
- [ ] Double-click (use `Double Click`)
- [ ] Type value (use `Type Text`)
- [ ] Press ENTER (use `Press Key`)
- [ ] Save (use `FlaUILib.Click Toolbar Button Save All`)

### Phase 4: EditPosition (1 hour)
- [ ] Similar to EditSize
- [ ] Add visibility check (use `Get Rectangle Bounding From Element`)
- [ ] Navigate Layout→Position

### Phase 5: EditValue (1-1.5 hours)
- [ ] Scroll to Data→value→Binding
- [ ] Double-click to open modal
- [ ] Wait for modal (use `FlaUILib.Wait For Dialog`)
- [ ] Navigate tree in modal (use `Activate Tree Leaf`)

### Phase 6: EditText (30 min)
- [ ] Scroll to Appearance→Text→Default
- [ ] Double-click
- [ ] Type with "$IAT/" prefix
- [ ] Press ENTER
- [ ] Save

### Phase 7: Testing (1-2 hours)
- [ ] Unit test each keyword
- [ ] Integration test
- [ ] Scenario test

---

## 📚 Reference Examples from Codebase

These Robot files already use the keywords you need:

**component_keywords.robot**:
```robot
Click Into IDE                           # Uses Click
Double Click    ${xpath}                 # Uses Double Click
@{all_rows}=    Find All Elements       # Uses Find
FlaUILib.Wait For Dialog                 # Uses Wait For Dialog
Activate Tree Leaf                       # Uses Activate Tree Leaf
```

**All patterns you need already exist in the codebase!**

---

## 📖 Updated Documentation

New update document: **PLAN_UPDATE_SIMPLIFIED_KEYWORDS.md**

This explains:
- What keywords already exist
- What was simplified
- Exact savings (40% time, ~200 lines)
- Translation examples
- Updated checklist

---

## ✅ What Changed vs Original Plan

| Aspect | Before | After | Change |
|--------|--------|-------|--------|
| New keywords to create | 10+ | 1 Python function | 90% reduction |
| Code lines to write | ~500 | ~300-400 | 200 lines saved |
| Development time | 10-16 hrs | 6-8.5 hrs | 40% faster |
| Python wrapper functions | Multiple | 1 (GetPropertyWindowXPath) | Minimal |
| Testing complexity | High | Medium | Simpler |
| Reliability | Depends on custom code | Uses FlaUILibrary (battle-tested) | Improved |

---

## 🎯 Start Here

1. **Read**: PLAN_UPDATE_SIMPLIFIED_KEYWORDS.md (10 min)
2. **Review**: IMPLEMENTATION_PLAN_SUMMARY.md (updated section on timeline)
3. **Reference**: LINE_BY_LINE_MAPPING.md (updated Section 6)
4. **Implement**: Follow the simplified checklist above

---

## 💡 Key Insight

The reason this simplified so much:

> You don't need to reinvent the wheel. FlaUILibrary already handles:
> - Element clicking (auto-centers)
> - Element bounds (Get Rectangle)
> - Element values (Get Property)
> - Scrolling (Scroll Up/Down)
> - Modal detection (Wait For Dialog)
> - Tree navigation (Activate Tree Leaf)
>
> All you need is the PropertyWindow xpath!

---

**This is a MUCH MORE ACHIEVABLE project now!** 🎉

Estimated development: 6-8.5 hours vs 10-16 hours  
Code to write: 300-400 lines vs 500 lines  
New Python functions: 1 vs 10+

You've got this! Let me know when you're ready to start Phase 1.
