using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Top_Right_Corner_Corridor : Basic_Room
{
    // Start is called before the first frame update
    public Top_Right_Corner_Corridor(List<GameObject> tiles, string type, int tiles_x, int tiles_z) : base(tiles, type, tiles_x, tiles_z)
    {
        Available_Sides = new List<string>() { "Top", "Right" };
    }

    public override void CreateRoom(List<GameObject> tiles)
    {
        Tile newtile;
        float xpos = Position.x, ypos = Position.y, zpos = Position.z;
        newtile = new Tile("Corner_Top_Right", tiles.Where(obj => obj.name == "Corner_Top_Right").First(), xpos, zpos);
        RoomTiles.Add(newtile);
    }
}
