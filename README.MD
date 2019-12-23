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


