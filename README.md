# Strategy_Game_Demo_Project
The game features a 2D strategy interface with a left panel for production and a right panel for information. In the production menu (left panel), players can create buildings. The information menu (right panel) allows players to view details about selected units.

**Controls:**
- Left Mouse Button: Selects a unit on the grid.
- Right Mouse Button: If the selected unit can move, it moves to the clicked location. If it can attack, it attacks the selected unit.

**Implemented Features:**
- [x] Factory Design Pattern
- [x] Lightweight Design Pattern
- [x] Object-Oriented Programming (OOP)
- [x] Singleton Pattern
- [x] Draw Call Optimization: Used sprite atlas
- [x] Events (Action): Publisher-Subscriber Design Pattern
- [x] Object Pooling
- [x] Inheritance
- [x] Polymorphism
- [x] S.O.L.I.D principles

**Extras**
- In the GameplayManager script, an option has been added to determine the movement type of units.
If the orthogonal movement type is selected, units can only move horizontally and vertically.
If the cardinal movement type is selected, units can move in any direction, including diagonally.
- I have created a Scriptable Object for assigning to buildings that can produce soldiers.
In this script, you can configure the following:
- [x] Spawn point direction option, determining the direction in which soldiers will spawn.
- [x] List of producible soldiers.

