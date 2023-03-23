# COMP4478 Unity Projects

## Exercise 2
A 2D game where the player controls a fishing net as they try to catch fish moving across the screen.
![menu](https://user-images.githubusercontent.com/47012039/225458324-40f2396c-1e6c-45f1-a2ef-745a8dd8f9af.png)

### Player
The player moves the position of the net using the WASD keys. The net can be rotated with the Q and E keys. The net is prevented from travelling outside of the camera viewport.  

To detect fish caught in the net, an edge collider is applied to the back of the netting and calculations are performed on collisions with fish to ensure fish are not “caught” if they collide with the outside of the net.  

To ensure that the netting of the net is rendered above the fish but not the hoop, dynamic layer sorting is applied to nearby fish.  

![game](https://user-images.githubusercontent.com/47012039/225458451-bc9152a3-485c-453a-85c5-631b479b0f0c.png)

### Fish
Fish are continuously spawned off screen to the right of the camera view port. Spawn position and frequency are randomized to create more unpredictable behaviour.  

The fish move in a sine wave pattern to simulate swimming behaviour. The amplitude and offset of the wave, as well as the fish’s speed, is randomized to create unique behaviours for each fish. As the fish follows the wave pattern, the sprite is rotated to match the wave pattern. To ensure the fish do not travel above the water’s surface, a downward force is applied to the fish if it reaches a defined threshold.  

When the fish collides with the net, it will aggressively flap back and forth to try to escape. If the fish is still in the net after a few seconds, the fish will be “caught” and its value will be added to the player’s score.

### Bombs

A cannon operated by two angry fish will occasionally appear from the bottom right of the screen. Once in position, and after a randomized delay, the cannon will fire a bomb in the direction of the net. The fast-moving bombs rotate as they move. After firing, the fish cannoneers retreat back off the screen.  

If a bomb collides with the net an explosion occurs, and the game is over.

### End Screen
The player’s score is displayed, and they have the option to play again.  

![gameOver](https://user-images.githubusercontent.com/47012039/225458527-9aeefbb2-c4dd-470a-bde5-fc70d1b226ff.png)

### Audio
Background music is played while the player is in either of the menus. Ambient noise fitting the theme is played in the background while playing.  

Button clicking, fish flapping, cannon moving, cannon firing, bombs exploding, and fish being caught all have their own sound effects.

## Assignment 2
A 2D memory game where the player needs to find all of the card pairs before time runs out.

### Menu Scene
On opening the game, the first screen the player sees is a simple menu which describes the goal of the game. There are two buttons, one to exit the game and one to start the game.

![mainMenu](https://user-images.githubusercontent.com/47012039/227080371-13fbc5b8-c1a1-41e8-bf51-f7d92175d737.png)

This scene is reused to display both the winning and losing screens after the player completes a round. The text content of the menu unique to the outcome. If the player wins the game, a section displaying the fastest completion time is displayed which is saved to the user's local machine.

![winMenu](https://user-images.githubusercontent.com/47012039/227080433-ebe2b417-8d02-4917-9827-18fe51868d0d.png)

![lossMenu](https://user-images.githubusercontent.com/47012039/227080442-b2275ef9-c06f-488a-8c4e-66a1367fb31f.png)

### Game Scene

![gameStart](https://user-images.githubusercontent.com/47012039/227080488-ad1c52b4-6143-4d3f-ad70-45d7e6f23ad8.png)

When the user first enters this scene the following tasks are completed sequentially:

1. The list of card face images is loaded from the Resources folder.
2. Iterating over this list, two objects with the properties id (id being the sprite name + either '1' or '2') and sprite for each image are created.
3. The list is shuffled
4. Iterating over the shuffled list, each object is assigned a both an X and Y value signifying their position in the game.

Then, using an asynchronous function, the card game objects are rendered on the screen one by one with a delay between each and an entrance animation for each.

Finally, the countdown timer begins decrementing from its original value of 90 seconds. 

When the player clicks a card, it rotates 180 degrees to present the card face. Upon reaching the halfway point of the rotation, the card back sprite is deactivated and the card face sprite is activated. 

![oneFlip](https://user-images.githubusercontent.com/47012039/227080608-8ce2cc90-1ebb-4237-a354-7a7b7337a29c.png)

When the user matches a pair of cards, both cards rapidly spin and shrink for a short duration before being removed from the game.

When the user incorrectly guesses, the flipped cards rotate back to their face down positions. 

![midGame](https://user-images.githubusercontent.com/47012039/227080650-dc0fcbbb-5544-4538-88fb-6b8ebeab7d88.png)

The game is complete when either the player matches all of the 16 cards or time runs out. 

### Audio
The following events all have their own sound effects: button clicking, card placement, card flipping, card matching, game winning, and game losing.
