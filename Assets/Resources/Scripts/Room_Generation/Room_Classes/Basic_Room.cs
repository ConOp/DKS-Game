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
    public List<GameObject> Instantiated_Tiles { get; set; }
    public List<string> Available_Sides { get; set; }
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
    public (int, string) CreateOpening()
    {
        int opening;
        int index_x, index_z;
        string side = RandomnessMaestro.OpenRandomAvailableSide(this);
        if (side == "Left")
        {
            index_z = this.Tiles_number_z / 2;
            index_x = 0;


        }
        else if (side == "Top")
        {
            index_z = 0;
            index_x = this.Tiles_number_x / 2;
        }
        else if (side == "Right")
        {
            index_z = this.Tiles_number_z / 2;
            index_x = this.Tiles_number_x - 1;
        }
        else
        {
            index_z = this.Tiles_number_z - 1;
            index_x = this.Tiles_number_x / 2;
        }
        Available_Sides.Remove(side);
        opening = (index_x * Tiles_number_x + index_z);
        return (opening, side);

    }
    public abstract void CreateAdjacentRoom(string side);
}
