# COMP4478-UnityProjects

## Exercise 2
A 2D game where the player controls a fishing net as they try to catch fish moving across the screen.

### Player
The player moves the position of the net using the WASD keys. The net can be rotated with the Q and E keys. The net is prevented from travelling outside of the camera viewport.  

To detect fish caught in the net, an edge collider is applied to the back of the netting and calculations are performed on collisions with fish to ensure fish are not “caught” if they collide with the outside of the net.  

To ensure that the netting of the net is rendered above the fish, dynamic layer sorting is applied to nearby fish.
### Fish
Fish are continuously spawned off screen to the right of the camera view port. Spawn position and frequency are randomized to create more unpredictable behaviour.  

The fish move in a sine wave pattern to simulate swimming behaviour. The amplitude and offset of the wave, as well as the fish’s speed, is randomized to create unique behaviours for each fish. As the fish follows the wave pattern, the sprite is rotated to match the wave pattern. To ensure the fish do not travel above the water’s surface, a downward force is applied to the fish if it reaches a defined threshold.  

When the fish collides with the net, it will aggressively flap back and forth to try to escape. If the fish is still in the net after a few seconds, the fish will be “caught” and its value will be added to the player’s score.

### Bombs

A cannon operated by two angry fish will occasionally appear from the bottom right of the screen. Once in position, and after a randomized delay, the cannon will fire a bomb in the direction of the net. The fast-moving bombs rotate as they move. After firing, the fish cannoneers retreat back off the screen.  

If a bomb collides with the net an explosion occurs, and the game is over.

### End Screen
The player’s score is displayed, and they have the option to play again.

### Audio
Background music is played while the player is in either of the menus. Ambient noise fitting the theme is played in the background while playing.  

Button clicking, fish flapping, cannon moving, cannon firing, bombs exploding, and fish being caught all have their own sound effects.

