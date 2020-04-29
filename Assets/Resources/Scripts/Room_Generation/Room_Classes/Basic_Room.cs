using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic_Room : IRoom
{
    public GameObject RoomObject { get; set; }
    public Vector3 Position { get; set; }
    public string Type { get; set; }
    public int Tiles_number_x { get; set; }
    public int Tiles_number_z { get; set; }
    public List<Tile> RoomTiles { get; set; }

    public Basic_Room(Vector3 position, List<GameObject> tiles, string type, int tiles_x, int tiles_z)
    {
        this.RoomTiles = new List<Tile>();
        this.Type = type;
        this.Tiles_number_x = tiles_x;
        this.Tiles_number_z = tiles_z;
        this.Position = position;
        CreateRoom(tiles);
    }

    public abstract void CreateRoom(List<GameObject> tiles);
}
