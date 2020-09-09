using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawningRoom : Basic_Room
{
    
    public SpawningRoom( Vector3 position, List<GameObject> tiles,string type) :base(position,tiles,type,5,5)
    {
        Available_Sides = new List<string>() {"Left", "Top", "Right","Bottom" };
    }

   public override void   CreateRoom(List<GameObject> tiles)
    {
        Tile newtile;
        float xpos = Position.x, ypos = Position.y, zpos = Position.z;
        for (int i = 0; i < Tiles_number_x; i++)
        {
            for (int j = 0; j < Tiles_number_z; j++)
            {
                if (i == (Tiles_number_x - 1) / 2 && j == (Tiles_number_z - 1) / 2)
                {
                    newtile = new Tile("Portal", tiles.Where(obj => obj.name == "Portal").First(),xpos,zpos);
                }
                else if (i == 0 && j == 0)
                {
                    newtile = new Tile("Left_Top_Corner", tiles.Where(obj => obj.name == "Left_Top_Corner").First(), xpos, zpos);
                }
                else if (i == 0 && j == Tiles_number_z - 1)
                {
                    newtile = new Tile("Right_Top_Corner", tiles.Where(obj => obj.name == "Right_Top_Corner").First(), xpos, zpos);
                }
                else if (i == Tiles_number_x - 1 && j == 0)
                {
                    newtile = new Tile("Left_Bottom_Corner", tiles.Where(obj => obj.name == "Left_Bottom_Corner").First(), xpos, zpos);
                }
                else if (i == Tiles_number_x - 1 && j == Tiles_number_z - 1)
                {
                    newtile = new Tile("Right_Bottom_Corner", tiles.Where(obj => obj.name == "Right_Bottom_Corner").First(), xpos, zpos);
                }
                else if (i == 0)
                {
                    newtile = new Tile("Top_Wall", tiles.Where(obj => obj.name == "Top_Wall").First(), xpos, zpos);
                }
                else if (i == Tiles_number_x - 1)
                {
                    newtile = new Tile("Bottom_Wall", tiles.Where(obj => obj.name == "Bottom_Wall").First(), xpos, zpos);
                }
                else if (j == 0)
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

    public override int CreateOpening()
    {
            int opening;
            int index_x, index_z;
            string side = RandomnessMaestro.OpenRandomAvailableSide(this);
            if (side == "Left")
            {
                index_z = this.Tiles_number_z / 2;
                index_x = 0;
                
            }
            else if(side == "Top")
            {
                index_z = 0;
                index_x = this.Tiles_number_x/2;
            }
            else if (side == "Right")
            {
                index_z = this.Tiles_number_z / 2;
                index_x = this.Tiles_number_x-1;
            }
            else
            {
                index_z = this.Tiles_number_z-1;
                index_x = this.Tiles_number_x /2;
            }
            opening=(index_x * Tiles_number_x + index_z);
            return opening;
        
    }
}
