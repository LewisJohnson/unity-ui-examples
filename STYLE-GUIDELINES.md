# Code style guidelines

Guidelines are guidelines, not rules. But we have some rules here too, and they really should be followed.


## Rules

### Rule 1:
The ```using UnityEditor``` statement **MUST** be wrapper in a ```#if UNITY_EDITOR``` region.
```C#
#if UNITY_EDITOR
using UnityEditor;
#endif 
```

## Guidelines

### Guideline 1:
Explicit types are preferred. This helps remove guessing work, instead of doing this
```C#
var fillColorArray= new Color(2, 123, 93);
var chukSize = 48;
var isLifeMeaningfull = false;
var chunks= new List<int>();
```

please do **this **
```C#
int chukSize = 48;
bool isLifeMeaningfull = false;
Color fillColorArray = new Color(2, 123, 93);
List<int> chunks = new List<int>();
```

### Guideline 2:
Some people think round brackets (US: parentheses) are redundant and unnecessary. I think, especially when looking at others code, it helps shown what the arithmetic expression is doing. So instead of this;

```c#
int foo = 10 + 3 / 39 + 19 + 23 + 10 * 2;
```

please do **this**
```c#
int foo = (((((10 + 3) / 39) + 19) + 23) + 10) * 2;
```
