using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bottom_Right_Corner_Corridor : Basic_Room
{


    public Bottom_Right_Corner_Corridor(Vector3 position, List<GameObject> tiles, string type, int tiles_x, int tiles_z) : base(position, tiles, type, tiles_x, tiles_z)
    {

    }


    public override void CreateRoom(List<GameObject> tiles)
    {
        Tile newtile;
        float xpos = Position.x, ypos = Position.y, zpos = Position.z;
        for (int i = 0; i < Tiles_number_x; i++)
        {
            for (int j = 0; j < Tiles_number_z; j++)
            {
                if (i == Tiles_number_x - 1 && j == Tiles_number_z - 1)
                {
                    newtile = new Tile("Right_Bottom_Corner", tiles.Where(obj => obj.name == "Right_Bottom_Corner").First(), xpos, zpos);
                }
                else if (j == Tiles_number_z - 1)
                {
                    newtile = new Tile("Right_Wall", tiles.Where(obj => obj.name == "Right_Wall").First(), xpos, zpos);
                }
                else if (i == Tiles_number_x - 1)
                {
                    newtile = new Tile("Bottom_Wall", tiles.Where(obj => obj.name == "Bottom_Wall").First(), xpos, zpos);
                }
                else
                {
                    newtile = new Tile("Center", tiles.Where(obj => obj.name == "Center").First(), xpos, zpos);
                }

                RoomTiles.Add(newtile);

                xpos += Tile.X_length;

            }
            xpos = Position.x;
            zpos -= Tile.Z_length;
        }

    }
}
