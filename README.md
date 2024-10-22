# CSE-3902

Player controls
Left and right arrow move player left and right.

All of the arrow key change the facing direction. only the up and down arrow key need to be held for continuously facing up/dowm.

Left shift for dash depending on the left right facing direction.

'z' makes the player jump. and 'x' let the player shoot projectile.

Number keys (1, 2, 3, 4, 5.) should be used to have CupHead switch to a different projectile type.
As long as projectiles are unlocked.

Unlock shot patterns by picking up items.

Use 'e' to cause CupHead to become damaged.

Other controls
Use 'q' to quit and 'r' to reset the program back to its initial state

CODE METRICS SPRINT 3
https://buckeyemailosu-my.sharepoint.com/:x:/g/personal/kosht_7_buckeyemail_osu_edu/EYji1WgDIPFOlO-ko4bz78YBTk0KmtQ1vpitdDOARLHwoQ?e=7tevZt

CODE REVIEW:

Trevor -
I am reviewing BothersomeBlueberry, 
When collision detection got added, it was not done so in a seperate file, this causes a lot of condition statememts which upped our Cyclomatic Complexity. To solve this we can remove many of the collision conditionals to another file. 
BothersomeBlueberry also has a decent amount of coupling, My removing the reached edge and other collision detection into a seperate file it will reduce coupling. 
With these simple fixes BothersomeBlueberry will have increased code quality.
