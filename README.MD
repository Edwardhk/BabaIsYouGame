# Game Design Report

## Architecture: MVC
At the end, I have adopted MVC as my architecture of this project, the original choice is MVVM  

However, I think extra C# WPF feature should be mastered before I can implemented state-of-the-art MVVM design as C# support WPF MVVM natively with different built in functions.

## Class Design

### Model

#### `BasePropsModel.cs`  
These are the basic game object properties that I have implemented, including:
- Push
- Control
- Kill
- Win

#### `GameStateModel.cs`  
This is the main logic of game status, it can be initialized by reading `filename`  

The progress of the game will be saved in a multidimensional container
`List<List<Stack<string>>>`

<img src="https://github.com/IECUHK/project-part-ii-huehue/blob/master/Assets/Images/Stackexplain.png">

The above graph has illustrated the basic design of the grid inside model

### View

#### `TileView.cs`  
This is the basic unit for every grid rendered, it contains self-defined height and width.  

As aforementioned, each grid is implmeneted in `Stack` container, my very first implementation includes only the `Peek()` element, as it is easier to implement.

However, I discovered that multi-layer rendering is available in the original game, therefore, I have used `DrawingGroup` to consolidate all the requried images.

Layers: `Uri(@'image.path')` > `BitmapImage()` > `ImageDrawing()` > `DrawingGroup`

Using the above layer, single `Image` can be added to the Children of Grid.

#### `TileMapView.cs`  
This is the 2D map of the `TileView`, the key methods will be `UpdateViews()`.  

It clears all the children of the grid and re-render the whole View for every movement.

### Controller

#### `BaseRuleObject.cs`  
The fundentmental `RuleBaseObject` is implemented here. Similar to the model, basic game properties have been added here, for instance:

- CanPush
- CanControl
- CanKill
- CanWin
- CanStop

All the above entries will be initialized to `false` by default

#### `MainController.cs`  
Most of the game logics are implemented here, all rule should be recalculated for each movement of the player.  

The flow of calculating the rule:  
`ResetRules() > RecalculateRules()`

In the `ResetRule`, list of prefix and suffix are defined:  

```
_rulePrefix {
    "T_BABA", "T_BLANK", "T_BALL", "T_ROCK",
    "T_TREE", "T_MOUNT", "T_WATER"
};

_ruleSuffix {
    "T_WIN", "T_YOU", "T_KILL",
    "T_PUSH", "T_STOP"
};
```

Satifying any of the following structure can be considered `rule`:  
```
1. Horizontal rule (LR rule)
[prefix] [IS] [suffix]

2. Vertical rule (UD rule)
[prefix]
  [IS]
[suffix]
```

By this setup, shape like these can be correctly executed:  
```
       [prefix]
[prefix] [IS] [suffix]
       [suffix]
``` 

All the rule will be stored inside `_rulesDict`, which is `Dictionary<string, RuleBaseObject>`, there are two types of key, for instance:

`T_BABA`: The textbox rule object of BABA   
`BABA`: The underlying object of BABA, i.e. the BABA

<img src="https://github.com/IECUHK/project-part-ii-huehue/blob/master/Assets/Images/Keyexplain.png">


