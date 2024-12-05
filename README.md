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
'o' - Progress the game to the win screen of the current stage.
'p' - Pause the game.

CODE METRICS SPRINT 4
https://buckeyemailosu-my.sharepoint.com/:x:/g/personal/kosht_7_buckeyemail_osu_edu/EX1qp_xiM_hKntkkmaw8_fUBvLlKoFlNqe77B9zOtpyT3w?e=9hrGEA&nav=MTVfezM2MzkyNUVFLTExMTktNEM1Ni05NkY1LTMwMTdDRDBFMDk2Q30


CODE REVIEWS:

SPRINT 5:

Author: Zol Chen 
Date: 12/4/2024 
Sprint #: 5 
File Name: WinMenu.cs 
File Author: Jacob 
Review Time :21 min

The code is pretty good with how simple it is; the code mostly used the code in Microsoft framework, which makes the code very easy to understand if they are new to the team. There are places that it can improve on, such as how much information is passed to the file. To construct the class, it requires the whole "game1" class to be passed in, which is very inefficient. Furthermore, it does not make use of the sprite render class, which is designed to be used for animating the sprite in the project. Lastly, there are places where the code pieces can be turned into methods, which make the code shorter since it is sitting at 256 lines. If I were to change some part of the code, I would start by using sprite render, which will move the need for the draw function and their frame #, remove "game1", and use player state to get all of the player state information, and lastly remove some of the switch statement and make it a method, which shortens the code a bit. Overall it is not bad, but there are many places that can use the resources that are already in the project

Author: Trevor Kosht 
Date: 12/4/2024 
Sprint #: 5 
File Name: BossLogic.cs
File Author: DJ
Review Time: 10 minutes

Readability Review:
This file is pretty readable but there are a few magic numbers which could be made as constants. The biggest thing with the readbality is the cyclomatic complexity, there are many if statments involved in this file. This could be split up into different files as states where the behavior is set in the states. Like a Phase 1 states file, that runs all of the phase 1 behavior, then a phase 2 that will check the health threshold and once it meets it then it signals to BossLogic that its in phase 2 and does the rest of the logic.


Author: Trevor Kosht
Date: 12/4/2024 
Sprint #: 5 
File Name: BossLogic.cs
File Author: DJ
Review Time: 10 minutes

Maintainability Review:
This file could get really complex if more checks were needed. because of the pseudo state machine you will need an increase in if statements, which will make the cyclomatic complexity even higher and increase the overall lines of code. If there was a dictionary of states and then in the update it cycles through all the states update method and thats where the logic takes place all you would have to do is make an additional state file with the logic. You could also move out all of that animation setting into the state files to reduce some of the coupling with that.

Author of the code review: David Dermanelian
Date of the code review: 12/4/2024
Sprint number: 5
Name of the .cs file being reviewed: BossEnemyFactory.cs
Author of the .cs file being reviewed: Ben
Number of minutes taken to complete the review: 17 minutes

Readable:
The code is well-organized, with a clear structure and modularity in how different enemy types are handled within the CreateEnemy method. Constants like GROUND_HEIGHT and FALL_SPEED make the logic easier to follow and reusable. Variable names are descriptive and self-explanatory, helping to clarify the purpose of each part of the code. The use of an enum for BossEnemyType keeps the enemy type selection clean and manageable. Additionally, the animation setup, though repetitive, is consistent, and the methods like addAnimation and setAnimation are straightforward and readable.
Unreadable:
The method is too long and has a lot of repeated logic for animation and component setup, making it hard to follow. Magic numbers and string keys scattered throughout make the code less clear and prone to errors. The switch cases are overly detailed, and the lack of helper methods makes the logic feel cluttered. Centralizing the configuration for enemy types or extracting repeated patterns into methods would make the code more maintainable and readable. 
Author of the code review: David Dermanelian
Date of the code review: 12/4/2024
Sprint number: 5
Name of the .cs file being reviewed: FlyingFlower.cs
Author of the .cs file being reviewed: Ben

The FlyingFlower class is moderately well-structured, with encapsulated methods for movement, updates, and attacks. Constants like FLIGHT_HEIGHT, SPRING_CONST, and MOVEMENT_RANGE make the behavior of the enemy predictable and reusable. However, the logic in methods like Move and Update is densely packed and includes conditional branches that could benefit from being split into helper methods. Repeated retrievals of components like SpriteRenderer and HealthComponent across different methods make the code harder to maintain and slightly less efficient. Extracting these calls to fields or caching components would simplify the logic. Additionally, the use of magic numbers in some calculations (e.g., rotationAngle *= 0.375) reduces readability and makes adjustments more error-prone.

Hypothetical Change and Support:
The current implementation, while modular for the single Attack method, does not readily support adding or switching between additional attaclk patterns. This would require significant refactoring to generalize the attack logic. A better approach would be to encapsulate attack behavior in a strategy or state pattern, allowing for interchangeable attack styles. Similarly, introducing a central animation manager for the class would streamline the animation transitions and reduce the repeated calls to setAnimation. These adjustments would make the code more extensible for future changes

Jacob Subler
12/4/2024
Sprint 5
VinesAttackState.cs
DJ
14 minutes

Readability Review:
Regarding readability for the VinesAttackState class, while the logic is not super difficult to follow as someone with context of the different parts of the project, I think there would be a benefit to clarifying variable names and conditionals for the sake of potential future or outside developers. Equivalency checks for boss.phase and attackPart using numbers can make sense if you know what enemy is using the vine attacks, but it is unclear exactly what the action phases and attacks are meant to do. Using names to describe the different phases like vineRetracting or vineExtendingPhase. Since the animation names are written in to the class, it makes it easier to match the actions to the numbers, but could benefit from a bit of clarification for those who are not actively working on the project.

Jacob Subler
12/4/2024
Sprint 5
WinMenu.cs
Jacob
10 minutes

Maintainability Review:
This specific menu class contains a lot of hard-coded values for Rectangles and score/grade calculations that could be split out into config files or other helper classes. The Rectangles for example could be stored in a config file separaretly since they are likely not to change much but could be stored alongside the hard coded rectangle values for all the different menus cleaning up all of the menu files. There are also some helper methods in WinMenu.cs that are used for reformatting or calculating a displayable score out of known player information like timePlayed, coins collected, heatlh, and parry count. These functions don't necessarily need to exist in the WinMenu.cs file, although they wouldn't be used in the other menus, but it would shorten up the WinMenu.cs class significantly and reduce the complexity of responsibilities that are put on the class itself.

Ben Vidmar-McEwen
12/4/2024
Sprint 5
VinesAttackState.cs
DJ
10 minutes
Readability comments:
This file contains a lot of nested conditional logic and loops to deal with the vine attack in the boss level. Unfortunately this contributes to the file having readability issues, especially since many of the if statements are consecutive if blocks to work properly, rather than an if-else statement, making it a bit harder to track the flow of conditional logic. There are some small logical reconfigurations that could make this file more readable, as well as updating some of the variable names. The different logical blocks also aren't spaced out, which would also improve visual clarity and make it easier to tell them apart and trace control flow. Especially since so many of them are consecutive if blocks it makes it a bit harder to tell right away that they are not just a single if-elseif-else block.

Ben Vidmar-McEwen
12/4/2024
Sprint 5
WinMenu.cs
Jacob
Code maintainability comments:
This file has a bit of awkwardly designed code simply by necessity: it needs to calculate a bunch of stats and then format them in a specific way for the win screen, so there has to be a lot of hard-coded values for the formatting to get everything lined up properly on screen, since the menu is made up of a lot of parts. That being said, some of the code that handles getting and calculating game stats and score could be extracted out into its own class. Right now the file is handling both formatting and drawing the menu as well as calculating statistics, and separating those into two classes would increase cohesion and improve readability, as well as making everything more maintainable. Since the file is trying to handle two fairly distinct tasks, it would make sense to separate them while still maintaining full functionality, since it would be very easy for the WinMenu class to just pull values from the stat handler class and then print them on screen.

SPRINT 5 REVIEW

This sprint was rough on the team, communications fell apart and things were delayed quite a bit. With finals approarching and break being in the middle people put things off for a bit. I think the boss fight turned out great, all of the expected behavior was in and it played nice. There could be a few improvemnets made to make it a bit easier and there could maybe be something to reduce to computer usage since if you dont have a powerful computer then some of the boss sprites will not render. 

SPRINT 4

Author: Trevor Kosht
Date: 11/12/2024
Sprint #: 4
File Name: Game1.cs
File Author: Everyone
Review Time: 10 minutes

Game1.cs Has become a bit of a mess and a hodgpodge of many different things. Most of which do not belong here. This has caused a pretty terrible maintainabity score. Looking at LoadContent(), is where we see a lot of stuff that should be handled in a different location. Like all of the player being loaded here. Player loading should happen in a player loader method that returns a functional player. Same with UI. The Update() function should remove a lot of the logic and use different controllers that already exist to handle it. With the removal into sepereate files Game1.cs would have lower cyclomatic complexity, and much higher maintainability. 

Author: Zol Chen
Date: 11/12/2024
Sprint #: 4
File Name: UI.cs 
File Author: Trevor Kosht
Review Time :12 min

I believe the quality of this file to be pretty good. The simplistic approach to drawing the card lowers the amount of code that needs to be written. However there are some aspect that it can imporve on. For example there is a ton of texture passed into the constuctor. It can be lowered by calling texture2d storage. It would also be easier to maintain if the numbers at on top and not hard coded into the methods. lastly many parts of the DrawScoreUI can be split up into more sub methods to improve readability and maintainability


Author: Ben Vidmar-McEwen
Date: 11/12/2024
Sprint #: 4
File Name: PlayerMovement.cs
File Author: Zol Chen
Review Time: 10 minutes

Cyclomatic complexity: 55
Depth of inheritance: 1
Class coupling: 18

This file is designed fairly well overall, this file has to have a high cyclomatic complexity just from the task it handles. However there are some spots that could be improved and refactored slightly to improve complexity and readability. In UpdateFacingDirection an if-else statement can be replaced with one Boolean assignment. Additionally multiple if statements in HandleDucking can be simplified to single if statements. This file has high cyclomatic complexity as required by controlling all the player's movement commands, but small changes could be made to improve design of logic and control flow in some of the methods.

Author: Jacob Subler
Date: 11/12/2024
Sprint #: 4
File Name: MurderousMushroom.cs
File Author: Trevor Kosht
Review Time: 10 minutes
Comments: Large methods like Shoot, HideUnderCap, and EmergeFromCap could be broken down into smaller methods to separate out distinct responsibilities such as projectile behavior and creation, etc, which would make the code easier to read and extend. There are a lot of repeat calls to access the GOManager which could require a lot of refactoring when changes need to be made later. Using constants or enums in place of hardcoded values would further improve clarity and reduce the risk of errors due to changes in the game logic. Lastly, grouping related logic together  would make the class more intuitive and easier to maintain as the project evolves as now the method layout doesn't seem to follow the logic flow as well as it could.

Code Review
Author: David Dermanelian
Date 11/12/2024
Sprintg #: 4
File Name ProjectileFactory
File Author: Trevor Kosht
Review time: 20 minutes
The method has high cyclomatic complexity (22), indicating multiple branching paths and conditional logic, which could be refactored into separate methods or classes to simplify the main flow. Each projectile type’s initialization could be separated into individual helper methods or classes implementing a common interface, following the Factory Method or Strategy design patterns. Additionally, complex calculations for projectile positioning and offset adjustments could be extracted into methods for modularity, reducing the need for inline calculations and enhancing readability. The class coupling (23) is relatively high, with various dependencies on GOManager, BoxCollider, CircleCollider, and SpriteRenderer. By encapsulating animations, colliders, and textures within dedicated configuration classes, we can centralize and simplify component setup, further improving the maintainability index of the code and facilitating future changes to projectile types or behavior.

SPRINT REFLECTION:
This sprint was the last hoorah for the game, in which the team added an abundance of polish. There were many tweaks to animations, effects, enemies and projectiles to make the game play and feel much better. In addition UI and Sounds were a key addition which made the game feel alive. We were able to add some key features from the orginal into the game this sprint as well. Things like directional shooting, and parrying which were massive undertakings. The team focused on improving code quality for many files this sprint with more to come in Sprint 5. 

SPRINT 3

Author: Trevor Kosht
Date: 10/21/2024
Sprint #: 3
File Name: BothersomeBlueberry.cs
File Author: Ben Vidmar-McEwen
Review Time: 10 minutes

When collision detection got added, it was not done so in a seperate file, this causes a lot of condition statememts which upped our Cyclomatic Complexity. To solve this we can remove many of the collision conditionals to another file. 
BothersomeBlueberry also has a decent amount of coupling, by removing the reached edge and other collision detection into a seperate file it will reduce coupling. 
With these simple fixes BothersomeBlueberry will have increased code quality.

Author: Zol Chen
Date: 10/21/2024
Sprint #: 3
File Name: ChaserProjectile.cs
File Author: Trevor K
Review Time: 10 minutes

I would say most of the code is readable with the update method a bit long and complex. This makes the code harder to maintain. The update method also has a bunch of statements so it could add some Cyclomatic Complexity. The coupling isn't too bad, since it is only used in the projectile factory. There aren't many lines of code since it only includes 1 type of projectile. Overall, other than the update method, the file is very well done, and the update method can improve by moving some blocks out and making them methods.

Author: DJ
Date: 10/21/2024
Sprint #: 3
File Name: TerribleTulip.cs
File Author: Trevor Kosht, Ben Vidmar-McEwen
Review Time: 10 minutes

Several aspects make the quality of the TerribleTulip class questionable, particularly with respect to coupling and maintainability. The coupling is 18, meaning this class depends on or is closely connected with several outer systems: a player, projectiles, visual effects, and different renderers. In such a situation, this class will hardly be modifiable or reusable independently. Such strong dependence increases the risk of cascading changes at every modification of related classes. Also, the score of 65/100 for maintainability suggests that it works but is neither clear nor flexible. Poor separation of concerns is an issue: such as mixing shooting logic with animation management, and most of the code logics depend on hard values. This makes it inflexible and difficult to read. Refactoring might involve breaking down responsibilities into smaller, more modular components and reducing coupling for better maintainability.

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
