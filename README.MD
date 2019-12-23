# Game Design Report

## Architecture: MVC
At the end, I have adopted MVC as my architecture of this project, the original choice is MVVM  

However, I think extra C# WPF feature should be mastered before I can implemented state-of-the-art MVVM design as C# support WPF MVVM natively with different built in functions.

## Class Design

### Model

`BasePropsModel.cs`  
These are the basic game object properties that I have implemented, including:
- Push
- Control
- Kill
- Win

`GameStateModel.cs`  
This is the main logic of game status, it can be initialized by reading `filename`  

The progress of the game will be saved in a multidimensional container
`List<List<Stack<string>>>`

<img src="https://github.com/IECUHK/project-part-ii-huehue/blob/master/Assets/Images/Stackexplain.png">

The above graph has illustrated the basic design of the grid inside model

