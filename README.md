# CSE-3902

Player controls
To move CupHead use the left and right arrow keys

Use left shift to dash

'z' makes the player jump.
'x' makes the player shoot equipped projectile.

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
Author: Trevor Kosht
Date: 10/21/2024
Sprint #: 3
File Name: BothersomeBlueberry.cs
File Author: Ben Vidmar-McEwen
Review Time: 10 minutes

When collision detection got added, it was not done so in a seperate file, this causes a lot of condition statememts which upped our Cyclomatic Complexity. To solve this we can remove many of the collision conditionals to another file. 
BothersomeBlueberry also has a decent amount of coupling, by removing the reached edge and other collision detection into a seperate file it will reduce coupling. 
With these simple fixes BothersomeBlueberry will have increased code quality.

Zol - 
Author: Zol Chen
Date: 10/21/2024
Sprint #: 3
File Name: ChaserProjectile.cs
File Author: Trevor K
Review Time: 10 minutes

I would say most of the code is readable with the update method a bit long and complex. This makes the code harder to maintain. The update method also has a bunch of statements so it could add some Cyclomatic Complexity. The coupling isn't too bad, since it is only used in the projectile factory. There aren't many lines of code since it only includes 1 type of projectile. Overall, other than the update method, the file is very well done, and the update method can improve by moving some blocks out and making them methods.

DJ -
Author: DJ
Date: 10/21/2024
Sprint #: 3
File Name: TerribleTulip.cs
File Author: Trevor Kosht, Ben Vidmar-McEwen
Review Time: 10 minutes

Several aspects make the quality of the TerribleTulip class questionable, particularly with respect to coupling and maintainability. The coupling is 18, meaning this class depends on or is closely connected with several outer systems: a player, projectiles, visual effects, and different renderers. In such a situation, this class will hardly be modifiable or reusable independently. Such strong dependence increases the risk of cascading changes at every modification of related classes. Also, the score of 65/100 for maintainability suggests that it works but is neither clear nor flexible. Poor separation of concerns is an issue: such as mixing shooting logic with animation management, and most of the code logics depend on hard values. This makes it inflexible and difficult to read. Refactoring might involve breaking down responsibilities into smaller, more modular components and reducing coupling for better maintainability.

Ben -
Author: Ben Vidmar-McEwen
Date: 10/21/2024
Sprint #: 3
File Name: PlayerMovement.cs
File Author: Zol Chen
Review Time: 10 minutes

Maintainability: 58
Cyclomatic complexity: 45
Depth of inheritance: 1
Coupling: 10

This file scored worse than average on maintainability and cyclomatic complexity, though the depth of inheritance and coupling scores are good. 

This file handles the logic for each action the player can take, like moving. The high cyclomatic complexity makes sense for this type of file since the player has many different states and animations it can be in, some of which are dependent on other states like the ducking while shooting animation. The cyclomatic complexity could be reduced by increasing the number of states that need to be handled, and making them all more specific. However this just shunts the cyclomatic complexity over to the PlayerState code, these conditional logic statements have to happen somewhere. 

The maintainability of this code is a little low due to the fact that it is designed to handle a set and predetermined number of states and actions that the player could take. It could be hard to edit or add on to the methods defined here, since it could be easy to cause a bug in the player behavior, since a lot of it is handled here.

Jacob - 
Readability Code Review
Author: Jacob Subler
Date: 10/21/2024
Sprint #: 3
File Name: PlayerMovement.cs
File Author: Zol Chen
Review Time: 10 minutes
Comments: There are lots of if/else statements, many of which are nested. This adds some complexity to reading the code because you have to follow long trails of conditionals to follow the logic of the class code. There are also many different methods inside the PlayerMovement class which makes it harder to locate specific code you are looking for. We could benefit from possibly breaking this class into smaller pieces to make the class length longer and the code more readable.


Maintainability Code Review
Author: Jacob Subler
Date: 10/21/2024
Sprint #: 3
File Name: PlayerCollision.cs
File Author: Zol Chen
Review Time: 10 minutes
Comments: The PlayerCollision class could benefit from reworking to improve Maintainability. There is a separate function for colliding with all the different GameObjects which significantly increases the length of the class and suggests for any future collisions to be handled, another function would need to be created and added to the switch case. We could look to consolidate the collision functions into less overall functions by grouping similar logic.


SPRINT REFLECTION:
The team excelled this sprint. While the team handled some tough challenges the final product of the sprint is something we are all very proud of. Some key moments of the development of this cycle were, seeing the level come together for the first time, added in different types of colliders like a slope, fixing difficult enemy ai, and adding a ton of polish to the game that makes it feel alive. 
The team needed to adjust a bit this sprint. Features like directional shooting and a paralax background had to be pushed to a later date, but the team still has plans to add these features. 
