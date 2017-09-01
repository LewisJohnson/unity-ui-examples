# Documentation Styleguide

These are the guidelines for writing documentation for an example.

## Titles

* Each document must have a single `#`-level title at the top.
* The phrase "UI" must be capatalised. 
* Sub-chapters need to increase the number of `#` in the title according to
  their nesting depth.
* All words in the page's title must be capitalised, except for conjunctions
  like "of" and "and".

## Document flow

You can use this as a template:

```markdown
# Examples Project Name

**GIF OR IMAGE**

### Description
...

### Supported aspect ratios
* 5:4 ❌
* 4:3 ❌
* 3:2 ❌
* 16:10 ✅
* 16:9 ✅

### Supported Platforms
* Editor ❌
* Standalone ✅
* UWP ❓

**Mobile**
iOS ❓
Android ✅

**Console**
* Xbox One ❓
* PS4 ❓

### Parameters
...

```

## Parameters
In the parameters section, you should list all fields/properties that are configurable (Including private fields exposed with [Serialize]). An exmaple of a field to show would be FOV for a camera.

* Make sure you add a short description.
* Sub classes should be in **bold**.
* Fields/properties should be indented.
* Sub classes fields/properties should be double indented.

See [HERE](https://github.com/LewisJohnson/unity-ui-examples/blob/master/Assets/ScreenSpace/PercentageBased/README.md) for a good example.
