# Property Editing Replication - Complete Plan Package

**Generated**: June 2, 2026  
**Project**: ASW-UI-Tests  
**Scope**: Replicate C# MappView property editing methods to Robot Framework

---

## 📋 Documents Created

This plan consists of **4 comprehensive documents** designed to work together:

### 1. **IMPLEMENTATION_PLAN_SUMMARY.md** ← **START HERE**
   - Executive overview and key findings
   - Similarity analysis (vs Add Role/Fill TMX patterns)
   - Common patterns across all methods
   - Implementation strategy (4 phases)
   - Risk analysis and success criteria
   
   **Time to Read**: 15 minutes  
   **Purpose**: Understand the big picture and get approval to proceed

### 2. **PROPERTY_EDITING_FLOWCHART.md**
   - Detailed ASCII flowcharts for each method
   - Section-by-section breakdown
   - Repeated patterns identified
   - Differences table
   - Python wrapper enhancement requirements
   
   **Time to Read**: 20 minutes  
   **Purpose**: Understand the logic flow before coding

### 3. **LINE_BY_LINE_MAPPING.md** ← **REFERENCE DURING CODING**
   - C# code snippets → Robot Framework translations
   - Complete for all 5 methods
   - Implementation checklist
   - New keywords needed
   
   **Time to Read**: 30 minutes (skim) / on-demand reference during dev  
   **Purpose**: Your implementation guide and code template

### 4. **PROPERTY_EDITING_FLOWCHART.md** (Section 10)
   - Mermaid diagram visualization
   - High-level architecture diagram
   
   **Time to Read**: 5 minutes  
   **Purpose**: Visual reference during development

---

## 🎯 Quick Reference

### The 5 Methods You're Replicating

```
┌─────────────────────────────────────────┐
│  MappView.cs Property Editing Methods   │
├─────────────────────────────────────────┤
│ 1. ScrollFindProperty() [FOUNDATION]    │
│    ↓                                    │
│    └─ Helper: locates properties       │
│                                         │
│ 2. EditSize()                           │
│    └─ Uses: ScrollFindProperty          │
│       Sets: Layout→Size→width/height    │
│                                         │
│ 3. EditPosition()                       │
│    └─ Uses: ScrollFindProperty          │
│       Sets: Layout→Position→top/left    │
│                                         │
│ 4. EditValue()                          │
│    └─ Uses: ScrollFindProperty + Modal  │
│       Sets: Data→value→Binding          │
│       Opens: Select Variable window     │
│                                         │
│ 5. EditText()                           │
│    └─ Uses: ScrollFindProperty          │
│       Sets: Appearance→Text→Default     │
│       Prefix: "$IAT/"                   │
└─────────────────────────────────────────┘
```

### Key Insights

| Aspect | Detail |
|--------|--------|
| **Most Complex** | ScrollFindProperty - bidirectional scrolling with intersection checks |
| **Most Unique** | EditValue - involves modal window handling |
| **Most Critical** | Pixel positioning: Right-20 for edit fields, Left+5/Top+5 for expand |
| **Most Common** | Double-click → Type → ENTER sequence |
| **Hardest Part** | Scroll logic and visibility verification |

---

## 🚀 Getting Started

### Step 1: Review & Approval (30 min)
1. Read **IMPLEMENTATION_PLAN_SUMMARY.md** completely
2. Review **PROPERTY_EDITING_FLOWCHART.md** sections 1-6
3. Skim **LINE_BY_LINE_MAPPING.md** to understand structure
4. Approve approach and timeline

### Step 2: Prepare Infrastructure (1-2 hours)
1. Examine FlaUILibrary Python wrapper
2. Add `GetPropertyWindowXPath()` function
3. Test PropertyWindow access from Robot Framework
4. Create helper keywords (Get PropertyWindow Table, etc.)

### Step 3: Implement ScrollFindProperty (2-3 hours)
1. Most critical method - implement first
2. Test each section (scroll up, find property, scroll down, expand)
3. Verify with actual PropertyWindow elements
4. Validate scroll deltas and timing

### Step 4: Implement Edit Methods (6-9 hours)
1. **EditSize** (1-2 hours) - simplest, tests direct + scroll paths
2. **EditPosition** (1-2 hours) - adds visibility checks
3. **EditValue** (2-3 hours) - most complex, includes modal
4. **EditText** (1 hour) - simplest, just text with prefix

### Step 5: Integration & Testing (2-3 hours)
1. Unit tests for each keyword
2. Integration testing with real UI
3. Scenario-based testing
4. Performance validation

**Total Timeline**: 11-17 hours (assuming continuous work)

---

## 📚 Reference by Task

### "How do I implement ScrollFindProperty?"
→ **LINE_BY_LINE_MAPPING.md Section 1** (lines 597-641)  
→ **PROPERTY_EDITING_FLOWCHART.md Section 2**

### "What's the overall flow?"
→ **PROPERTY_EDITING_FLOWCHART.md Section 1** (Architecture)  
→ **PROPERTY_EDITING_FLOWCHART.md Section 10** (Mermaid diagram)

### "What are the mouse/keyboard interactions?"
→ **LINE_BY_LINE_MAPPING.md** (all sections show exact coordinates)  
→ **PROPERTY_EDITING_FLOWCHART.md Section 7** (Common patterns)

### "What new Python functions do I need?"
→ **PROPERTY_EDITING_FLOWCHART.md Section 8** (Python Wrapper section)  
→ **LINE_BY_LINE_MAPPING.md Section 6** (New keywords needed)

### "How does this compare to existing patterns?"
→ **IMPLEMENTATION_PLAN_SUMMARY.md Section 3** (Similarity to existing code)

### "What could go wrong?"
→ **IMPLEMENTATION_PLAN_SUMMARY.md Section 7** (Risks & Mitigations)

### "How do I validate my implementation?"
→ **PROPERTY_EDITING_FLOWCHART.md Section 9** (Validation checklist)  
→ **LINE_BY_LINE_MAPPING.md Section 6** (Implementation checklist)

---

## 🔧 Key Implementation Details

### Critical Success Factors

```
1. PIXEL POSITIONING
   Right - 20  = Value field click position
   Left + 5    = Expand button X position
   Top + 5     = Expand button Y position
   Center.Y    = Vertical middle of field
   ⚠️ Must match C# exactly or UI won't respond

2. SCROLL LOGIC
   Scroll(+1.0) = Scroll UP (show earlier items)
   Scroll(-1.0) = Scroll DOWN (show later items)
   Scroll(-2.0) = Final adjustment
   ⚠️ Direction is opposite of expected (FlaUI convention)

3. VISIBILITY CHECKS
   Element.BoundingRectangle.IntersectsWith(Container.BoundingRectangle)
   ⚠️ Must return TRUE for element to be clickable

4. TIMING
   100ms after Mouse.Click/Scroll
   500ms polling for modal windows
   ⚠️ Cannot go faster without missing UI updates

5. VALUE PARSING
   int.Parse(element.Patterns.Value.Pattern.Value)
   ⚠️ C# converts string to int for comparison
```

### Element Hierarchy Reference

```
PropertyWindow (UIAutomation Table)
├─ Layout (DataItem)
│  ├─ Size (DataItem)
│  │  ├─ width (DataItem → Value)
│  │  └─ height (DataItem → Value)
│  ├─ Position (DataItem)
│  │  ├─ top (DataItem → Value)
│  │  └─ left (DataItem → Value)
│  └─ [other Layout sub-properties]
├─ Appearance (DataItem)
│  └─ Text (DataItem)
│     └─ Default (DataItem → Value)
├─ Data (DataItem)
│  └─ value (DataItem)
│     └─ Binding (DataItem → Value) [→ Select Variable Modal]
└─ [other top-level properties]
```

---

## ✅ Validation Gates

Before moving to next phase, verify:

**Phase 1 Completion Gate:**
- [ ] PropertyWindow xpath accessible via Python
- [ ] Can locate PropertyWindow from Robot
- [ ] Can find DataItem, Table, and named elements
- [ ] Element properties (BoundingRectangle, etc.) accessible

**Phase 2 Completion Gate:**
- [ ] ScrollFindProperty works for simple properties
- [ ] Scroll logic verified with actual UI
- [ ] Scroll deltas (+1, -1, -2) working correctly
- [ ] IntersectsWith() logic confirmed

**Phase 3 Completion Gate:**
- [ ] EditSize sets values correctly
- [ ] Both direct access and scroll paths work
- [ ] Value comparison (int.Parse) works
- [ ] Pixel positioning verified

**Phase 4 Completion Gate:**
- [ ] EditPosition handles both area modes
- [ ] Visibility check prevents errors
- [ ] Expand logic (Left+5, Top+5 clicks) works
- [ ] Additional scroll (-2.0) adjustment verified

**Phase 5 Completion Gate:**
- [ ] EditValue opens modal
- [ ] Modal window wait loop reliable
- [ ] Tree navigation in modal successful
- [ ] String concatenation ("BR_" + variable) correct

**Phase 6 Completion Gate:**
- [ ] EditText sets values with "$IAT/" prefix
- [ ] Text input and ENTER press sequence correct
- [ ] Values persist after save

**Phase 7 Completion Gate:**
- [ ] All keywords tested individually
- [ ] Integration tests pass
- [ ] Timing matches C# (within 10%)
- [ ] No race conditions or flaky tests

---

## 📞 Support & References

### When Stuck, Consult:
- **C# Original Code**: MappView.cs lines 495-641
- **Similar Robot Pattern**: component_keywords.robot lines 41-120
- **Reference Implementation**: IDE_Main.cs lines 920-960 (AddRole method)

### Key Files to Have Open:
1. MappView.cs (C# source)
2. LINE_BY_LINE_MAPPING.md (your translation guide)
3. PROPERTY_EDITING_FLOWCHART.md (logic reference)
4. component_keywords.robot (pattern examples)
5. FlaUILibrary Python wrapper (for new functions)

### Expected Challenges & Solutions

| Challenge | Solution |
|-----------|----------|
| PropertyWindow xpath different | Use GetPropertyWindowXPath() wrapper function |
| Scroll not working as expected | Check scroll direction; FlaUI inverts convention |
| Elements not visible/clickable | Verify intersection check; may need more scrolling |
| Modal not appearing | Increase timeout; add debug logging |
| Pixel positioning off | Use visual debugger; capture actual coordinates |
| Timing issues/flakiness | Add explicit waits; don't remove 100ms sleeps |
| Value parsing fails | Check element.Patterns.Value availability |

---

## 📊 Estimated Effort Breakdown

```
Infrastructure Setup       : 1-2 hours
ScrollFindProperty        : 2-3 hours (CRITICAL)
EditSize                  : 1-2 hours
EditPosition              : 1-2 hours
EditValue                 : 2-3 hours
EditText                  : 1 hour
Testing & Validation      : 2-3 hours
─────────────────────────────────
TOTAL                     : 10-16 hours
```

---

## 🎓 Learning Outcomes

By completing this project, you will:

1. ✅ Understand FlaUI element hierarchies and navigation
2. ✅ Master bidirectional scrolling in tables
3. ✅ Implement modal window handling in Robot Framework
4. ✅ Translate complex C# UI logic to Robot Framework
5. ✅ Create robust, pixel-perfect element interactions
6. ✅ Handle conditional UI logic branches
7. ✅ Build reusable UI automation keywords

---

## 📝 Next Actions

1. **Immediate** (Today):
   - [ ] Read IMPLEMENTATION_PLAN_SUMMARY.md
   - [ ] Review PROPERTY_EDITING_FLOWCHART.md
   - [ ] Skim LINE_BY_LINE_MAPPING.md
   - [ ] Approve plan and timeline

2. **Short-term** (This week):
   - [ ] Implement Phase 1 (Infrastructure)
   - [ ] Implement Phase 2 (ScrollFindProperty)
   - [ ] Complete validation gates

3. **Medium-term** (Next 1-2 weeks):
   - [ ] Implement Phase 3-4 (Edit methods)
   - [ ] Complete Phase 5-6 (Value & Text)
   - [ ] Conduct Phase 7 (Testing)

4. **Long-term** (Ongoing):
   - [ ] Integrate into test suite
   - [ ] Document usage patterns
   - [ ] Train team on new keywords
   - [ ] Monitor for edge cases

---

**End of Plan Document**

Questions? Refer to the detailed documents or check the "Support & References" section above.

Good luck with the implementation! 🚀
