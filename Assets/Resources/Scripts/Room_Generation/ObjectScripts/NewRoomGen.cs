using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewRoomGen : MonoBehaviour
{
    List<GameObject> alltiles; //all tile prefabs from project folder.
    List<IRoom> allrooms;  //all instantiated rooms in the game.
    public int RoomNumber = 20;
    // Start is called before the first frame update
    void Start()
    {
        alltiles = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Tiles"))); // Load all room prefabs from the project folder.
        allrooms = new List<IRoom>();
        InstantiateRoom("SpawningRoom", new Vector3(0, 0, 0), alltiles,5, 5);
        MakeOpening(allrooms[0]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InstantiateRoom(string type, Vector3 pos, List<GameObject> tiles, int tiles_x, int tiles_z)
    {
        var Spawn_Room = RoomFactory.Build(type, pos, alltiles, tiles_x, tiles_z); //Using the Room Factory we construct the room.
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

            int tile=room.CreateOpening();
            //Tile to be placed.
            Tile t= new Tile("Center", alltiles.Where(obj => obj.name == "Center").First(), room.RoomTiles[tile].Position_X, room.RoomTiles[tile].Position_Z);
            //Replace tile.
            room.RoomTiles[tile] = t;
            //Destroy old tile.
            Destroy(room.Instantiated_Tiles[tile]);
            //Instantiate new tile.
            room.Instantiated_Tiles[tile]= Instantiate(t.Objtile, new Vector3(t.Position_X, 0, t.Position_Z), new Quaternion(),room.RoomObject.transform);
    }
}
