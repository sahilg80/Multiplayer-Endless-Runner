I created this Multiplayer Endless runner game.
I used MVC design pattern.
I used Photon Pun2 for multiplayer solution.

I created a class PhotonManager which is responsible for taking all inputs from UI. This class is also responsible for handling all parts starting from creating the room to load the GameScene for every player.
When user starts the game LobbyScene is loaded and it asks the user to give relevant login details(just enter the name to start the photon connection).
After that photon is connected then one needs to create the room by entering relevant details. 
After creating room all other players can join that room and the player who created the room will be master. Only the room creator can load the GameScene or start the game.

After the game scene is loaded user can start or stop the player movement by pressing “Space” button.
Player has the ability to jump by pressing “UpArrow” key and also to move left and right direction by pressing “LeftArrow” and “RightArrow” key.

As we are following MVC design pattern in the GameScene each script has its own role.
PlayerController script act as Controller component. This component can communicate with View and Model component both. This script takes care of all logic part.
PlayerView script act as View Component. This component can communicate with Controller component only. This script takes care of all visualization part.
PlayerModel script act as model component. This component can communicate with Controller component only. This script is responsible for holding data for GameScene.
Player script is responsible to control the Player of our game which is represented by a Capsule.
MapManager controls the spawning of mapTiles or platform in a random order.
CamerFollower script helps to follow the Player continuously.
InputManager script takes the input from user after the GameScene loaded.
GameController script triggers spawning of the all required objects in the scene.

