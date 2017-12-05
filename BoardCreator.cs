using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour {

    public GameObject[] roomsWithoutOpening;                // An array of room prefabs.
    public GameObject[] roomsWithOpening;
    public GameObject[] roomWithGate;
    public GameObject[] startRoom;                          // A Gameobject of the start room.
    public GameObject[] bossRoom;                            // A Gameobject of the end room.

    public GameObject[] horizontalWall;
    public GameObject[] verticaleWall;    

    public GameObject player;
    public int totalFloors;
    public int totalRoomsAtEachFloor;
    private int checkFloor;

    private GameObject boardHolder;                         // GameObject that acts as a container for all other tiles.
    public int roomWidht = 19;                             // The Lenght of one Room
    public int roomHeight = 19;                            // The Height of the Room

    

    private static GameObject playerInstance;

    // Use this for initialization
    void Start ()
    {
        // Create the board holder.
        boardHolder = new GameObject("BoardHolder");

        InstantiateOuterWalls();
        InstantiateFloors();

        

    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    

    void InstantiateFloors()
    {     
        for (int y = 0; y < totalFloors; y ++)
        {
            
            int randomIndex = Random.Range(0, totalRoomsAtEachFloor);

            if (y == totalFloors -3)
            {
                checkFloor = randomIndex;
            }

            for (int x = 0; x < totalRoomsAtEachFloor; x++)
            {
                //if it is the first floor then spawn or a start room or a room without opening
                if (y == 0)
                {
                    if (randomIndex == x)//if the random number is equal to x 
                    {
                        InstantiateFromArrayRandom(startRoom, x * roomWidht, y, true);//Spawns Startroom   
                    }
                    else
                    {
                        InstantiateFromArrayRandom(roomsWithoutOpening, x * roomWidht, y * roomHeight, false);//Spawns room without opening
                    }
                }
                //if it is the last floor, spawn rooms without an opening + Bossroom
                else if (y == (totalFloors - 1))
                {
                    //if randomIndex is the same as the previous floor
                    if (randomIndex == checkFloor)
                    {
                        randomIndex = randomIndex + 1 % 4;
                    }
                    //if randomIndex equals place a bossRoom on that x position
                    if (randomIndex == x)
                    {
                        InstantiateFromArrayRandom(bossRoom, x * roomWidht, y * roomHeight, false);//Spawns EndRoom.
                    }
                    else
                    {
                        InstantiateFromArrayRandom(roomsWithoutOpening, x * roomWidht, y * roomHeight, false);// Spawns room without opening
                    }
                }
                //else spawn rooms with or without opening 
                else if(y == totalFloors -3)
                {
                    
                    if (randomIndex == x)
                    {
                        InstantiateFromArrayRandom(roomWithGate, x * roomWidht, y * roomHeight, false);//Spawns room with Opening
                    }
                    else
                    {
                        InstantiateFromArrayRandom(roomsWithoutOpening, x * roomWidht, y * roomHeight, false);// Spawns room without opening
                    }
                }
                else if (y == totalFloors - 2)
                {

                    if (checkFloor == x)
                    {
                        //do nothing so there will be an empty spot which is already filled by the gateroom which is 2 big.                        
                    }
                    else
                    {
                        InstantiateFromArrayRandom(roomsWithoutOpening, x * roomWidht, y * roomHeight, false);// Spawns room without opening
                    }
                }
                else
                {
                    if (randomIndex == x)
                    {
                        InstantiateFromArrayRandom(roomsWithoutOpening, x * roomWidht, y * roomHeight, false);//Spawns room with Opening
                    }
                    else
                    {
                        InstantiateFromArrayRandom(roomsWithoutOpening, x * roomWidht, y * roomHeight, false);// Spawns room without opening
                    }
                }             
            }
        }  
    }

    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = 1f;
        float rightEdgeX = roomWidht * totalRoomsAtEachFloor;
        float bottomEdgeY = 0f;
        float topEdgeY = roomHeight * totalFloors;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY + 1, topEdgeY - roomHeight);//left wall
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY + 1, topEdgeY);//right wall

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.        
        InstantiateHorizontalOuterWall(leftEdgeX , rightEdgeX -1f, bottomEdgeY);//bottem wall             
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);//top wall
    }        

    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArrayRandom(verticaleWall, xCoord, currentY, false);

            currentY += roomHeight;
        }
    }

    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArrayRandom(horizontalWall, currentX, yCoord, false);

            currentX += roomWidht;
        }
    }

    void InstantiateFromArrayRandom(GameObject[] prefabs, float xCoord, float yCoord, bool isStartRoom)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position of the rooms to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);
        // The position of the player to be instantiated at is based on the coordinates.
        Vector3 playerPosition = new Vector3(xCoord + 10, yCoord + 2, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject floorInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        floorInstance.transform.parent = boardHolder.transform;

        if (isStartRoom && playerInstance == null)
        {
            
            GameObject playerSpawned = Instantiate(player, playerPosition, Quaternion.identity) as GameObject;
            playerInstance = playerSpawned;
        }
        else
        {
            //Debug.Log("Setting Player to starting room position");
            player.transform.position = playerPosition;
        }       
        
        
    }

    void InstantiateStartRoom(GameObject prefab, float xCoord, float yCoord)
    {
        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject floorInstance = Instantiate(prefab, position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        floorInstance.transform.parent = boardHolder.transform;
    }
}
