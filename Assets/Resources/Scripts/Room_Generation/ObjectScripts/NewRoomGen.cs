using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewRoomGen : MonoBehaviour
{
    List<IRoom> allrooms;  //All instantiated rooms in the game.
    public int RoomNumber = 20; //Target room number.
    private void Awake()
    {
        PrefabManager.LoadPrefabs();//Load all the prefabs from the project at the very start.
    }


    void Start()
    {
        allrooms = new List<IRoom>();
        CreateDungeon(RoomNumber);
        
    }


    public void CreateDungeon(int room_number)
    {
        //Construct Spawn Room.
        InstantiateRoom("SpawningRoom", new Vector3(0, 0, 0), PrefabManager.GetAllTiles(), 5, 5);
        bool found;
        while (RoomNumber > 0)
        {
            int openroomindex=0;
            found = false;
            foreach (IRoom room in allrooms)
            {
                if (room.Available_Sides.Count > 0)
                {
                    openroomindex = allrooms.IndexOf(room);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Debug.LogError("Finished dungeon generator without reaching the room goal.");
                break;
            }
            MakeOpening(allrooms[openroomindex]);
            RoomNumber--;
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
    void InstantiateRoom(string type, Vector3 pos, List<GameObject> tiles, int tiles_x, int tiles_z)
    {
        var Spawn_Room = RoomFactory.Build(type, pos, tiles, tiles_x, tiles_z); //Using the Room Factory we construct the room.
        GameObject gr = new GameObject(Spawn_Room.Type); //Parent object to all tiles.
        List<GameObject> instantiated_tiles = new List<GameObject>();
        foreach (Tile tile in Spawn_Room.RoomTiles)
        {
            //Instantiate every tile.
           instantiated_tiles.Add(Instantiate(tile.Objtile, new Vector3(tile.Position_X, 0, tile.Position_Z), new Quaternion(), gr.transform));
        }
        Spawn_Room.Instantiated_Tiles = instantiated_tiles;
        Spawn_Room.RoomObject = gr;
        allrooms.Add(Spawn_Room);
    }

    /// <summary>
    /// Create doors in room on available sides.
    /// </summary>
    /// <param name="room"></param>
    void MakeOpening(IRoom room)
    {

            (int tile,string side)=room.CreateOpening();
            //Tile to be placed.
            Tile t= new Tile("Center", PrefabManager.GetAllTiles().Where(obj => obj.name == "Center").First(), room.RoomTiles[tile].Position_X, room.RoomTiles[tile].Position_Z);
            //Replace tile.
            room.RoomTiles[tile] = t;
            //Destroy old tile.
            Destroy(room.Instantiated_Tiles[tile]);
            //Instantiate new tile.
            room.Instantiated_Tiles[tile]= Instantiate(t.Objtile, new Vector3(t.Position_X, 0, t.Position_Z), new Quaternion(),room.RoomObject.transform);
    }

}
