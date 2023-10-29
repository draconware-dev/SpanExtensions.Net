---
name: Documentation report
about: Report a grammatical mistake or an unclear sentence in either the XML documentationq
  in the C# source files or in any of the github specific documentation
title: Documentation Error
labels: documentation
assignees: ''

---

**Explain the error by: **

1. **Describing the error if it is simple like a missing period or swapped letters: **
- The method summary of Method `X` found in Class `Y` for parameter `Z` misses a period at the end.
- The remarks section of the Method summary for Method `X`in Class `Y` has falsely misspelled _ab_ as _ba_. 
- The sentence "..." at line 33 in [Readme.md](Readme.md) is unclear, as it does not state the expectations clearly/has a confusing structure/leaves room for interpretation where there should be none
  
2. **Contrasting the incorrect with the correct Version:  **
-  The method summary of Method `X` found in Class `Y` for parameter `Z` is as follows: "...", but should be as follows "...". 

As grammatical errors are usually easier to fix than to describe, consider submitting a pull request, that fixes the issue. You can do that here.
