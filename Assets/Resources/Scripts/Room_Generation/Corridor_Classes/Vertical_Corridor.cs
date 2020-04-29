using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Vertical_Corridor : Basic_Room
{


    public Vertical_Corridor(Vector3 position, List<GameObject> tiles, string type, int tiles_x, int tiles_z) : base(position, tiles, type, tiles_x, tiles_z)
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
               
                if (j == 0)
                {
                    newtile = new Tile("Left_Wall", tiles.Where(obj => obj.name == "Left_Wall").First(), xpos, zpos);
                }
                else if (j == Tiles_number_z - 1)
                {
                    newtile = new Tile("Right_Wall", tiles.Where(obj => obj.name == "Right_Wall").First(), xpos, zpos);
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
