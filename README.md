3D Platformer Game for the final project for game programming (CS 1163 2023). 

Guidelines : use a menu scene that includes the ability to add a player's name and at least two other scenes. 

Game Rules: 

Basic movement using WASD keys to move and space bar to jump. Added a jump using CTRL to give the player an ability to small jump. 
To complete a level, the player has to collect 5 coins and reach the checkpoint. If at anytime they fall off a platform and/or collide with the water, or they reach 0 health, they restart the level.
There are 4 platformer levels and 4 rooms that are essentially mini games. 

For levels 1-4, they need to avoid any obstacles and not fall into the water. Obstacles include the poisonous trees (trees w/ berries), fire, balls (more details later), logs, enemies, and spikes. Depending on the object it takes off a different amount of health. The trees and log both take off 5 hp, and the enemies and balls take off 10 hp when collided. If the player collides with the water, the level restarts. 

When the player completes a level, they go into a room. Each level and room is scene. 

Each room restarts if the player fails. Once the room/mini game is complete, they move on to the next level. 

Room1 the player has to throw a ball at a stack of cups and make it so they all are on the ground at the end of the 10 seconds. Once complete, the cups all despawn and they have to get the ball into the hoop. Afterwards, they move onto level 2. 

Room2 the player has to complete the maze. The maze has a fixed entry and exit point, but everytime the scene reloads, it creates a new maze as a randomized depth-first search algorithm is used to generate the maze. The player has 90 seconds to complete the maze. Once complete, they move onto level 3. 

Room3 the player has to go around the "level" and pick up associated objects. They have to pick up a phone, a type of tool, a book, a bottle, a cup, and a type of kitchen item. They have to sort out which items they should be picking up and which ones to avoid, as some of them are fake items, and some are decoys. One item can even reset the room from the beginning. The decoy items take 2 health off. However, there are items that will add health to the player as well, to balance it out in a way. 

Once complete with level 4, they move onto the Room4Intro scene. Essentially this scene prepares the player for the last scene, where they will be given a sword and have to fight the dragon. If they kill the dragon, the player is victorious. To make it easier for the player I added a health bar to the dragon so the player can keep track of how much health the dragon has left. 

I used a humanoid character for the player and added animations for running, jumping, idle, throwing a ball, and doing a sword slash. I also added animation to the coin, allowing it to spin (rotate) in the air. The dragon also has animation, for running, idle, walking, 3 different attacks, getting hit, and death. 

There are different small features to the game, but I will only list a couple here. I made it so when the player takes damage, their screen will do a slight flash of red, and fade back to normal. Each level has different "puzzles." For instance, the first level has a timed platform as they have to trigger a plate manually and use the platform within ~7 seconds or else the platform will despawn. Similarly in level 4, they have to push a barrel onto a plate trigger that will activate a platform that they will need to progress in the level. The game also uses cinemachine's freelook camera, as well as a virtual camera for the maze scene. 
