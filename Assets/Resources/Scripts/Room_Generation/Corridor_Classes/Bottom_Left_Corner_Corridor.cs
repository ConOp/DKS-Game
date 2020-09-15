using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bottom_Left_Corner_Corridor : Basic_Room
{
    public Bottom_Left_Corner_Corridor(List<GameObject> tiles, string type, int tiles_x, int tiles_z) : base(tiles, type, tiles_x, tiles_z)
    {
        Category = "Corridor";
        Available_Sides = new List<string>() { "Left", "Bottom" };
    }

    public override void CreateRoom(List<GameObject> tiles)
    {
        Tile newtile;
        float xpos = Position.x, ypos = Position.y, zpos = Position.z;
        newtile = new Tile("Corner_Bottom_Left", tiles.Where(obj => obj.name == "Corner_Bottom_Left").First(), xpos, zpos);
        RoomTiles.Add(newtile);
    }
}
