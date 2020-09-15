using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Top_Left_Corner_Corridor : Basic_Room
{
    public Top_Left_Corner_Corridor(List<GameObject> tiles, string type, int tiles_x, int tiles_z) : base(tiles, type, tiles_x, tiles_z)
    {
        Category = "Corridor";
        Available_Sides = new List<string>() { "Left", "Top" };
    }

    public override void CreateRoom(List<GameObject> tiles)
    {
        Tile newtile;
        float xpos = Position.x, ypos = Position.y, zpos = Position.z;
        newtile = new Tile("Corner_Top_Left", tiles.Where(obj => obj.name == "Corner_Top_Left").First(), xpos, zpos);
        RoomTiles.Add(newtile);
    }
}
