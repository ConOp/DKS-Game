using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewRoomGen : MonoBehaviour
{
    public List<IRoom> allrooms;  //All instantiated rooms in the game.
    public static int RoomNumber = 1; //Target room number.
    public static int roomsPlaced = 0;
    GameObject dungeon;
    private void Awake()
    {
        PrefabManager.LoadPrefabs();//Load all the prefabs from the project at the very start.
        DataManager.Initialize_Type_Data();//Load all types data for room and corridors.
    }


    void Start()
    {
        allrooms = new List<IRoom>();
        dungeon = new GameObject("Dungeon");
        CreateDungeon(RoomNumber);
        RefineDungeon();
    }


    public void CreateDungeon(int room_number)
    {
        //Construct Spawn Room.
        InstantiateIRoom(RoomFactory.Build("SpawningRoom", PrefabManager.GetAllRoomTiles(), 5, 5), new Vector3(0, 0, 0), PrefabManager.GetAllRoomTiles());
        bool foundcorridor, foundroom;
        while (true)
        {
            int openroomindex = -1;
            foundcorridor = false;
            foundroom = false;
            //Used to find corridor with available sides.
            for (int i = 0; i < allrooms.Count; i++)
            {
                if (allrooms[i].Available_Sides.Count > 0)
                {
                    if (allrooms[i].Category == "Corridor")
                    {
                        foundcorridor = true;
                        openroomindex = i;
                        break;
                    }
                }
            }
            //Gives priority to available corridors.
            //Used to find available room.
            if (!foundcorridor)
            {
                for (int i = 0; i < allrooms.Count; i++)
                {
                    if (allrooms[i].Available_Sides.Count > 0)
                    {
                        if (allrooms[i].Category == "Room")
                        {
                            foundroom = true;
                            openroomindex = i;
                            break;
                        }
                    }
                }
            }
            //Something went wrong and there is no available side to none of the rooms.
            if (!foundcorridor && !foundroom)
            {
                Debug.LogError("Finished dungeon generator without reaching the room goal.");
                break;
            }
            (IRoom newroom, Vector3 newroomloc) = allrooms[openroomindex].CreateAdjacentRoom();
            if (newroom != null)
            {
                
                if (allrooms[openroomindex].Category == "Room")
                {
                    InstantiateIRoom(newroom, newroomloc, PrefabManager.GetAllRoomTiles());

                    if (newroom.Category == "Room")
                    {
                        roomsPlaced++;
                    }

                }
                else if (allrooms[openroomindex].Category == "Corridor")
                {
                    InstantiateIRoom(newroom, newroomloc, PrefabManager.GetAllRoomTiles());
                    if (newroom.Category == "Room")
                    {
                        roomsPlaced++;
                    }
                }
                if (roomsPlaced >= RoomNumber)
                {
                    break;
                }
            }

        }
    }

    /// <summary>
    /// Build selected room using factory and instantiate it in the world.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <param name="tiles"></param>
    /// <param name="tiles_x"></param>
    /// <param name="tiles_z"></param>
    void InstantiateIRoom(IRoom room, Vector3 pos, List<GameObject> tiles)
    {
        GameObject gr = new GameObject(room.Type); //Parent object to all tiles.
        //Set the center position of the room.
        gr.transform.position = new Vector3(pos.x+(Mathf.Abs((pos.x+room.Tiles_number_x*Tile.X_length)-pos.x)/2), pos.y+2.5f, pos.z - (Mathf.Abs((pos.z + room.Tiles_number_z * Tile.Z_length) - pos.z) / 2));
        List<GameObject> instantiated_tiles = new List<GameObject>();
        foreach (Tile tile in room.RoomTiles)
        {
            //Instantiate every tile.
            instantiated_tiles.Add(Instantiate(tile.Objtile, new Vector3(pos.x + tile.Position.x, 0, pos.z + tile.Position.z), new Quaternion(), gr.transform));
        }
        room.Instantiated_Tiles = instantiated_tiles;
        room.RoomObject = gr;
        if (room.Category.Equals("Room"))
        {
            gr.AddComponent<BoxCollider>();//Create new collider for the room.
            gr.GetComponent<BoxCollider>().size = new Vector3(room.Tiles_number_x * Tile.X_length - 2, 5, room.Tiles_number_z * Tile.Z_length - 2);//Set the size of the collider to cover all the room.
            gr.GetComponent<BoxCollider>().isTrigger = true;//Set collider to trigger.
            gr.AddComponent<RoomEventsHandler>();
        }
        gr.transform.parent = dungeon.transform;
        room.Position = pos;
        allrooms.Add(room);
    }

    /// <summary>
    /// Deletes excess corridors and closes open rooms with walls.
    /// </summary>
    public void RefineDungeon()
    {
        List<IRoom> cleanrooms = new List<IRoom>();
        foreach (IRoom room in allrooms)
        {
            IRoom currrent_room = room;
            
            if (currrent_room.Category == "Corridor")
            {
                //While the corridor is not connecting to both sides.
                while (((Basic_Corridor)currrent_room).Child == null)
                {
                    
                    //If it's parent is a room then it sets the appropriate opening to null and seals it with a wall.
                    if (((Basic_Corridor)currrent_room).Parent.Category == "Room")
                    {
                        if (((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomTop == currrent_room)
                        {
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomTop = null;
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).CloseOpening("Top");
                        }
                        else if (((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomRight == currrent_room)
                        {
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomRight = null;
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).CloseOpening("Right");
                        }
                        else if (((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomBottom == currrent_room)
                        {
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomBottom = null;
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).CloseOpening("Bottom");
                        }
                        else if (((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomLeft == currrent_room)
                        { 
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).CloseOpening("Left");
                            ((Basic_Room)((Basic_Corridor)currrent_room).Parent).AdjRoomLeft = null;
                        }
                        

                    }
                    else
                    {
                        //If it's parent is a corridor then unlink it from the connected side.
                        ((Basic_Corridor)((Basic_Corridor)currrent_room).Parent).Child = null;
                    }
                    //Destroy the corridor.
                    Destroy(currrent_room.RoomObject);
                    currrent_room = ((Basic_Corridor)currrent_room).Parent;
                   
                    if (currrent_room.Category == "Room")
                    {
                        break;
                    }
                }
            }
        }
        //Clean the list of rooms from the deleted ones.
        foreach (IRoom room in allrooms)
        {
            if (room.RoomObject != null)
            {
                cleanrooms.Add(room);
            }
        }
        allrooms = cleanrooms;
    }
}
 