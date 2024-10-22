# CSE-3902

Player controls
Left and right arrow move player left and right.

All of the arrow key change the facing direction. only the up and down arrow key need to be held for continuously facing up/dowm.

Left shift for dash depending on the left right facing direction.

'z' makes the player jump. and 'x' let the player shoot projectile.

Number keys (1, 2, 3, 4, 5.) should be used to have CupHead switch to a different projectile type.
As long as projectiles are unlocked.

Unlock shot patterns by picking up items.

CupHead will take damage from all enemies and projectiles

Other controls
Use 'q' to quit and 'r' to reset the program back to its initial state

CODE METRICS SPRINT 3
https://buckeyemailosu-my.sharepoint.com/:x:/g/personal/kosht_7_buckeyemail_osu_edu/EYji1WgDIPFOlO-ko4bz78YBTk0KmtQ1vpitdDOARLHwoQ?e=7tevZt

CODE REVIEW:

Trevor -
I am reviewing BothersomeBlueberry, 
When collision detection got added, it was not done so in a seperate file, this causes a lot of condition statememts which upped our Cyclomatic Complexity. To solve this we can remove many of the collision conditionals to another file. 
BothersomeBlueberry also has a decent amount of coupling, by removing the reached edge and other collision detection into a seperate file it will reduce coupling. 
With these simple fixes BothersomeBlueberry will have increased code quality.

Zol - 
I am reviewing ChaserProjectile.cs,
I would say most of the code is readable with the update method a bit long and complex. This makes the code harder to maintain. The update method also has a bunch of statements so it could add some Cyclomatic Complexity. The coupling isn't too bad, since it is only used in the projectile factory. There aren't many lines of code since it only includes 1 type of projectile. Overall, other than the update method, the file is very well done, and the update method can improve by moving some blocks out and making them methods.

DJ -
Several aspects make the quality of the TerribleTulip class questionable, particularly with respect to coupling and maintainability. The coupling is 18, meaning this class depends on or is closely connected with several outer systems: a player, projectiles, visual effects, and different renderers. In such a situation, this class will hardly be modifiable or reusable independently. Such strong dependence increases the risk of cascading changes at every modification of related classes. Also, the score of 65/100 for maintainability suggests that it works but is neither clear nor flexible. Poor separation of concerns is an issue: such as mixing shooting logic with animation management, and most of the code logics depend on hard values. This makes it inflexible and difficult to read. Refactoring might involve breaking down responsibilities into smaller, more modular components and reducing coupling for better maintainability.

Ben -
File: PlayerMovement.cs
Maintainability: 58
Cyclomatic complexity: 45
Depth of inheritance: 1
Coupling: 10

This file scored worse than average on maintainability and cyclomatic complexity, though the depth of inheritance and coupling scores are good. 

This file handles the logic for each action the player can take, like moving. The high cyclomatic complexity makes sense for this type of file since the player has many different states and animations it can be in, some of which are dependent on other states like the ducking while shooting animation. The cyclomatic complexity could be reduced by increasing the number of states that need to be handled, and making them all more specific. However this just shunts the cyclomatic complexity over to the PlayerState code, these conditional logic statements have to happen somewhere. 

The maintainability of this code is a little low due to the fact that it is designed to handle a set and predetermined number of states and actions that the player could take. It could be hard to edit or add on to the methods defined here, since it could be easy to cause a bug in the player behavior, since a lot of it is handled here.
