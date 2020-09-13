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
            string side=RandomnessMaestro.OpenRandomAvailableSide(allrooms[openroomindex]);
            int openindex = allrooms[openroomindex].CalculateOpening(side);
            Vector3 loc = allrooms[openroomindex].Instantiated_Tiles[openindex].transform.position; //= allrooms[openroomindex].CreateOpening(openindex,side);
            (IRoom newroom,Vector3 newroomloc)=allrooms[openroomindex].CreateAdjacentRoom(side,loc);
            if (newroom != null)
            {
                allrooms[openroomindex].CreateOpening(openindex,side);
                InstantiateIRoom(newroom, newroomloc, PrefabManager.GetAllTiles());
                RoomNumber--;
            }
            else
            {
                allrooms[openroomindex].Available_Sides.Remove(side);
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
    void InstantiateRoom(string type, Vector3 pos, List<GameObject> tiles, int tiles_x, int tiles_z)
    {
        var Spawn_Room = RoomFactory.Build(type, tiles, tiles_x, tiles_z); //Using the Room Factory we construct the room.
        Spawn_Room.Position = pos; //Give the room the desired location.
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
    void InstantiateIRoom(IRoom room, Vector3 pos, List<GameObject> tiles)
    {
        GameObject gr = new GameObject(room.Type); //Parent object to all tiles.
        List<GameObject> instantiated_tiles = new List<GameObject>();
        foreach (Tile tile in room.RoomTiles)
        {
            //Instantiate every tile.
            instantiated_tiles.Add(Instantiate(tile.Objtile, new Vector3(pos.x+tile.Position_X, 0,pos.z+tile.Position_Z), new Quaternion(), gr.transform));
        }
        room.Instantiated_Tiles = instantiated_tiles;
        room.RoomObject = gr;
        allrooms.Add(room);
    }



}
 