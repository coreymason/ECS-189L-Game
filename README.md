# Game Basic Information
# Summary
Nine Lives revolves around the idea that we have many lives as players of video games but that as the main character, there are also nine levels or “lives” to live through. We were able to make the first level which emulates the dungeon crawling and fighting of many different types of enemies as a fantasy hero. Dash! Purge the evil that corrupts this dungeon! If you can make it through, then you can truly understand what it means to be a fantasy protagonist.
 
# Gameplay explanation
Use WASD to move your character and use spacebar to dash. Remember that when you dash, you are locked into the direction you are dashing into, so be careful. Also note that you are locked out of the dash for a very short amount of time after each dash. In order to shoot, you simply aim at the desired direction using your mouse, then left click to shoot arrows. Try not to get hit by maneuvering around enemies and using your dash smartly. Be careful of dashing straight to your doom because you didn’t see an enemy in your way!
# Main Roles
Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.
Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content.
Short Description - Long description of your work item that includes how it is relevant to topics discussed in class. link to evidence in your repository
Here is an example:  
Procedural Terrain - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based off the component design pattern and the procedural content generation portions of the course. The PCG terrain generation script.
You should replay any bold text with your own relevant information. Liberally use the template when necessary and appropriate.
## User Interface
Overall - My main effort/challenges with UI revolved mostly around having to learn a whole new section of Unity’s functionality and understanding the editor. This meant that while the physical aspects of my code are very few lines, a lot of the hours of work were put into assigning anchors, spacing out different elements, and activating/deactivating different canvases. I learned from a lot of Unity tutorials online for basic functionality scripts and used custom sprites for each of the buttons. The buttons for all the menus also change their tint when moused over and clicked on through their sprite functionality. Each menu also has about 6 hours worth of work and research put into it aside from actually implementing it. In terms of each of their features, they will be described below. 
Main Menu - The Main Menu’s functionality mostly revolves around being able to start and quit the game. It has a function in the MainMenu UI Script that is called by the Play button that loads the next game scene. Another function for the Quit button, quits the application on the whole and not just the scene. I also learned/was able to implement a Settings button that activates and deactivates canvases while in the UI scene to display an Options Menu and deactivate the Start Menu with the SetActive() boolean. Unfortunately, the options in the settings menu don’t actually work because it’s a different scene as opposed to an in-game canvas and cross-scene implementation was outrageously difficult based on the structure of our game base but the buttons are pressable. [Here’s what the volume would look like if the audio source didn’t have to be cross scene](https://github.com/coreymason/ECS-189L-Game/blob/fbac629c4525278b6ce8f52417e5ce417ba8d123/UnityProject/Assets/Scripts/UI/ChangeVolume.cs#L8)
 
In Game - In Game UI is also fairly simple. There is an image of a heart indicating the health counter in the top right counter as well as a number to show the player how much health they have. [This connects with the player’s health by receiving a signal from the playerObject when the health has changed and converts it to a string to display on the screen.](https://github.com/coreymason/ECS-189L-Game/blob/fbac629c4525278b6ce8f52417e5ce417ba8d123/UnityProject/Assets/Scripts/UI/HealthCounterDisplay.cs#L8)
The health was represented with a number and not a bar because I wanted it to be part of the UI canvas as opposed to a sprite that was randomly placed over the scene and there were a lot of issues with the anchoring and sprite rendering. There is the beginning of a health bar script that didn’t end up getting used because I couldn’t get the scaling set up properly to continue with the actual logic. After about 4 hours of futzing around I decided that the number functionality would be better since it was functional and also used the UI anchoring system. This is the one that relates the most to what we’ve been taught while in the course because it’s very similar to the UI keeping track of treasure in Exercise 1 with some minor changes.
Game Over - The GameOver screen is called in the event of the player winning or losing. It is another menu that lets you restart the game or quit the application. It also has settings that are dysfunctional but exiting the Settings Menu goes back to the Game Over menu as opposed to MainMenu. [The best solution I could find for this was to make the Game Over Menu to have own Settings canvas instead so it could reference back to itself instead of loading the MainMenu.](https://github.com/coreymason/ECS-189L-Game/blob/e87948ece9ef7d69d39feeba33b4a873010e73ce/UnityProject/Assets/Scripts/UI/GameOver.cs#L6)
## Movement/Physics
Normal Movement - The most basic part of any movement/physics system is the movement. The player can use WASD to move the character Up,Left,Down, and Right. The player can also move using the arrow keys, but the WASD keys are recommended. I coded it so that “Horizontal” and “Vertical” of the input manager are captured. Then I multiplied this Vector2 by the normal running speed value. In addition, it was coded so that if there is a nonzero value in the “Horizontal” and “Vertical” section, the overall velocity vector would be multiplied by 0.7 to take the speed increase into the account. There was also smoothdamp adding to smooth the player movement. The difficult part was trying to integrate this with the Factory pattern we used in class. It took a long time to make sure the factory pattern was working properly. After our gameplay testing, the speed of the character was tweaked to make the speed of movement more reasonable. ~2-3 hours 
 
Dash - I implemented a dash that locks the player in a desired direction and moves at great speed. In order to dash, the player must press the desired direction with the WASD or arrow keys then press the space bar. The dash also has a cool down so the player can’t just hold down space bar. The PlayerController has a “dashing” state and a “walking” state, when the space bar is pressed, the PlayerController goes into the dashing state for however many seconds before reverting to the walking state. Before the first gameplay testing players were just holding down space, I realized this made the regular walk command pointless (since the dash at this point was just a massive speed). The lock in effect and cooldown of the dash makes it more skillbased instead of being a mindless speed up. ~2-3 hours 
 
Arrow shooting - I also implemented arrow shooting as a part of my job. Whenever the left click is performed, the arrow simply heads in the direction of the cursor at a stable velocity. Due to time reasons, I created the arrow sprite in GIMP myself instead of giving the Animation and Visuals person unnecessary additional work. The arrow shooting system was later reimplemented which the same logic. ~6 hours 
 
Crosshair - When the game was first merged together, I thought the game looked really bland (no enemies” so I decided to add a crosshair. Adding a crosshair solved two problems: 1) When the player plays on a PC, they can clearly see the cursor that the arrow would shoot to. However, when the player plays on a controller, it is fairly unclear on where the player is shooting. The crosshair helps remedies this problem.   2) The crosshair is just visually pleasing in a (at the time) empty word. Using trigonometry, I made it so that the crosshair lies in between the player and the cursor, the crosshair should always be a constant distance away from the player. In other words, the crosshair lies in a circular pattern around the player. Once again, in order to save the time of the Animation and Visuals person, I just made the crosshair myself. ~6 hours 
 
Overall: Most of my work was the base material that is needed for the game. As such I tested out everything in an empty scene without any tiles or a map. I had to create my own basic art assets for testing until the project was merged (some of my art assets were kept in the final game). A lot of my job actually involved bugfixing, even if the logic for a certain routine was correct, there are many different ways to implement them in Unity (some right, some wrong, some suboptimal). As already mentioned, most of our code used the Factory pattern and was contained into prefabs. While struggling with prefabs admittedly took a long of time, using prefabs helped us save time when we merged all of our work together. 
 
 
## Animation and Visuals
For this game, Xuan
Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.
## Input
The game is able to be controlled through keyboard and controller. For the keyboard control, wasd or arrow keys are used for character movement, space is for dash, mouse for aiming and click for shooting. For the controller, the left joystick is responsible to the character movement, the right joystick is responsible for cross aiming while the dash and shooting is ZR and ZL. 
 
The implementation of the keyboard and mouse input is easy. By adopting and modifying the project 2 code, a simple keyboard character is finished. The aiming part is directly adopt unity input mouseposition variable. 
 
The implementation of the controller is much harder. The controller used in the test is a PS4 controller. The choice of controller is limited by the controller I have. The left joystick In order to use the right joystick, I created two new input axises, Joy X and Joy Y, to take the input. For the dash and fire, I added the controller button to the alternative button in the input setting. 
 
In order to make both the controller and keyboard working individually, I wrote a new script, input manager. This script will be run once the main game scene is loaded. The script will check if there is a controller plugs in by getting the length of controller name and if it is greater than 0. Then according to the result, the input manager will take the variable from the controller or keyboard and store them in its public variables like fire position, movement, fire, dash. Therefore, the movement and physics part can directly access those variables through input manager rather than goes through unity input. 
 
## Game Logic
Overall High Level:
I am utilizing Zenject for its industry quality eventing, factories, pooling, and most importantly, dependency injection. This allows me to not only retain the key benefits of singletons as discussed in class but also avoid pitfalls like unclear dependencies, tight coupling, and untestability (more on these below). I realize that dependency injection in Unity is somewhat of a hotly debated topic but strongly believe it is justified given our scale and desire for clean, well-architected code. For instance, Pokemon Go was built entirely on top of Zenject as explained by [this](https://www.youtube.com/watch?v=8hru629dkRY) GDC talk from 2016.
 
Dependency Injection & Facades:
With dependency injection, I can decouple dependencies from code and strictly define how those dependencies should be found or created in Unity as needed. For example, the Player prefab contains Player.cs (high level player management) and a Zenject Game Object Context, which essentially acts like a local context - making this a facade. The child PlayerController can then  inject InputManager, Player, needed factories, and so on without these polluting the root dependency graph. An additional benefit is that our Player factory will auto link all these dependencies as well. Why is this useful? Consider adding co-op or an AI controlled player for instance, the only change I would have to make is factory making another player (no logic change at all is required) or injecting a different input manager (using an interface, a so-called “AI input manager” could pass in raw input just like a real person) respectively. For brevity, some of the technical specifics are excluded here but those two examples should convey the power of injecting dependencies instead of simpying manually accessing a traditional global singleton. Of course, there are numerous other benefits like dependency validation and making unit testing possible (I can inject a dummy class when testing instead of having to worry about components not existing). Also note that injections operate on a series of rules and bindings so I control how those dependency instances are or aren’t reused (this is how I can emulate a scoped singleton, cache, or never reuse depending on the specific case). Lastly, if we had multiple scenes, I can inject across scenes without issue; this would likely get complicated with traditional singletons and require some unwanted DontDestroyOnLoad logic.
 
Enemy Artificial Intelligence:
For AI, I forwent complex state machines and instead simplified the system into two key states -- a peaceful state and a hostile state. I then add one script of each type onto an enemy prefab in addition to a vision component that handles triggering the transition into hostile state upon sighting a player. As for how AI actually moves, I integrated the A Star Pathfinding Project (another library widely used in industry like Zenject), and then modify the a prefab’s base AIPath settings as well as target destination from code. In doing so, I can achieve some pretty straight forward AI code as seen in Wander and RunAttack.
 
Example in-game scene view (note the pathing in progress and field of view detection area)

 
Event System:
Utilizing Zenject Signals (events), I can send events from anywhere in the codebase and then trigger a respective function call. I use this to simultaneously hookup and decouple UI, camera, and other future systems that would otherwise be coupled to game logic.
 
Factories:
I utilize factories for creating players, projectiles, and enemies. Again, this allows me to decouple prefab instantiation from game logic and simply inject a single factory function into any class that needs the functionality. Furthermore, I can use those factories to automatically inject important variables like a projectile’s direction. In all, it makes for very clean and well organized code.
 
Scenes & Prefabs
Where possible everything is kept in prefabs for ease of creation at runtime, version control, and organization. I also make heavy use of layers, project physics settings, and load a single game scene for simplicity (ideally there would be no scene transitions at all, only additive loading or prefabs but time constraints played a role so we do have a separately loaded main menu scene that transitions into the game).
 
# Sub-Roles
## Audio
The sound style is fast dungeon-y, low res music like chiptune/8bit/etc; this matches our fast paced gameplay and pixel art aesthetic quite well.

Given time constraints (I’ve spent at least a good 70 hours on this project between game logic and handling all git merges myself), I decided to keep the audio system simple so I add audio sources to respective prefabs. I do have an AudioManager that can set global volume and in the future, I would like to add background music management (crossfade, change based on area, etc) and sound effect variants.

Since damage isn’t yet implemented, I haven’t been able to do sound effects on damage/death

Sources
https://soundcloud.com/abstraction/sets/ludum-dare-challenge - Creative Commons
https://freesound.org/people/gezortenplotz/sounds/20131/ - Creative Commons
## Gameplay Testing 
[Link](https://docs.google.com/document/d/1km3UmRIF3YStzEGZ2atN1ifI0mZoJBNFZJMtwSzAHPY/edit#heading=h.rf81nprd8xye)  to Gameplay tests: 

When we did the gameplay test, our game was a mostly incomplete sandbox.  This meant that all the criticisms and praises for our game were going to be similar. We were pleasantly surprised that most of the people actually enjoyed our incomplete game. The art was easy on the eyes and people were pleased by the very high speed of our character. The primary bug was the interaction between the walls and the player’s dash. While people did like to break our game, we also realized that it is not possible to keep the bug and implement it as an actual feature. There was just not enough time. Two other minor movement/physics adjustments we made thanks to the gameplay testing was to fix the amplified diagonal player movement and we also adjusted the distance of the crosshair from the player. Other than the wall collision breakage, the next largest criticism was the lack of any enemies. We eventually were also able to add this feature into our final version of the game. Ultimately, we did realize it is impossible to keep all the features that the players like without compromise. In order to make the combat of our game more fair, we had to tune down the high speed that the tester’s enjoyed during playtesting. If we had more time in the future, we would probably try to make the map larger so that we can keep the high speed of the character that the playtesters enjoyed. We could also try to make the wall dashing an actual mechanic, which was something that was in high demand. This was also the first time we were able to see the game played since we fused all of our role’s parts. This actually gave birth to new bugs, such as the arrows starting out slower than the player then increasing in velocity as they traveled. These bugs were also fixed since the playtesters brought this to our attention. For future reference, we now know how difficult and time consuming it is to merge code from different roles.
 
 
 
## Narrative Design
oDocument how the narrative is present in the game via assets, gameplay systems, and gameplay.
## Press Kit and Trailer
[Press Kit](https://yellowsail.itch.io/nine-lives)
[Trailer](https://youtu.be/3uD3xpNqNT0)

The press kit is published on itch.io. The reason I choose itch.io is both for its outstanding reputation in the indie game community and its easily usability. In the press kit, I include a link to the trailer, basic information about the game such as authors, release date, platforms and price, and some simple description of the game. I intentionally keep the press kit simple so that it is easy to find information. I choose this approach because of the belief that press kit should focus more on providing information rather than attract players. 
 
For the trailer, I choose to present the concept of our game at the beginning. Then show the individual works from a simple graphics all the way to the game logic. I order the trailer in such an order that the game gradually gain more complexity and I hope this may help to both present our progress and to hook the audience. 
 
## Game Feel
Movement - The movement I was given felt a little sluggish. Since we wanted the movement to be quick and have the dash as integral part of the game’s functionality, I upped the speed by about a factor of 2 and made the dash a shorter duration. This means that dash can either be spammed while moving around the map (which feels fun and fluid) or needs to be more carefully timed in order to aim and shoot properly. The movement was also smoothed a little bit in the PlayerController so that it’s less jarring of a stop. 
Screen Shake - I also wrote a script to shake the screen a tad bit when killing an enemy. Since enemies weren’t done at the time of writing the script, I attached the functionality to the MainCamera but disabled it so that when an enemy dies, a signal can be sent to call the camera shake in the Enemy Controller script. It moves the camera a random amount based off a duration of the shaking and the magnitude that we want it to move that are passed into the function call. Because it’s actually a coroutine, it should be easier to call based off of signaling. I wanted enemy deaths to have weight but also not be so overwhelming that the game is unplayable with regards to the camera shake. This seemed like a good middle-ground like the video we watched in class to add a little more “oopmh” and satisfaction to going through the level.
Menu Touchups - I made small touchups on the menu by making the background for the Main Menu a level snippet of the game and adding a large sprite of the main character to make it more interesting to look at. It makes the menu experience feel a little more polished overall even if the functionality is fairly basic. I feel like it adds a bit of “juice” which ultimately is the goal: make our game engaging but not overwhelming!
