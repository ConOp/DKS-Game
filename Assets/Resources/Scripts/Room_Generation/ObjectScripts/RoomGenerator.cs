using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RoomGenerator : MonoBehaviour
{
    List<GameObject> alltiles; //all tile prefabs from project folder.
    List<IRoom> allrooms;  //all instantiated rooms in the game.
    public int RoomNumber=2;

    List<(string  , float)> roomsizes = new List<(string , float )> { 
       ("Huge_Room",1f ), 
       ("Big_Room", 40f),
       ("Small_Room", 30f),
       ("Medium_Room",59f), 
    };
    /*
     * Huge Room x->30-35 z->30-35 p=1%
     * Big Room x-> 23-29 z->23-29  p=10%
     * Medium Room x->16-22 z->16-22 p=59%
     * Small Room x-> 9-15 z->9-15   p=30%
     * 
     * Big Hallway x-> 3 z->15-19
     * Medium Hallway x-> 3 z->10-14
     * Small Hallway x->3 z->5-9
     */
    void Start()
    {
        alltiles = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Tiles"))); // Load all room prefabs from the project folder.
        allrooms = new List<IRoom>();
        InstantiateRoom("SpawningRoom", new Vector3(0, 0, 0), alltiles, 0, 0);
        IRoom Current_Room = allrooms[0];


    }

    void Update()
    {
        
    }

    /// <summary>
    /// Instantiates Room based on the type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <param name="tiles"></param>
    /// <param name="tiles_x"></param>
    /// <param name="tiles_z"></param>
    void InstantiateRoom(string type,Vector3 pos,List<GameObject> tiles,int tiles_x,int tiles_z)
    {
        var Spawn_Room = RoomFactory.Build(type,pos, alltiles,tiles_x,tiles_z); //Using the Room Factory we construct the room.
        GameObject gr = new GameObject(Spawn_Room.Type); //Parent object to all tiles.
        foreach (Tile tile in Spawn_Room.RoomTiles)
        {
            //Instantiate every tile.
            Spawn_Room.RoomObject = Instantiate(tile.Objtile, new Vector3(tile.Position_X, 0, tile.Position_Z), new Quaternion(), gr.transform);
        }
        allrooms.Add(Spawn_Room);
    }


   
}
