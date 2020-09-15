using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Vertical_Corridor : Basic_Room
{
    public Vertical_Corridor(List<GameObject> tiles, string type, int tiles_x, int tiles_z) : base(tiles, type, tiles_x, tiles_z)
    {
        Available_Sides = new List<string>() { "Top", "Bottom" };
    }

    public override void CreateRoom(List<GameObject> tiles)
    {
        Tile newtile;
        float xpos = Position.x, ypos = Position.y, zpos = Position.z;
        for (int i = 0; i < Tiles_number_z; i++)
        {
            newtile = new Tile("Vertical_Wall", tiles.Where(obj => obj.name == "Corridor_Vertical").First(), xpos, zpos);
            RoomTiles.Add(newtile);
            zpos -= Tile.Z_length;
        }

    }
}

