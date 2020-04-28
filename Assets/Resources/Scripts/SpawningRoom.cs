using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawningRoom : IRoom
{
    public GameObject RoomObject { get; set; }
    public Vector3 Position { get; set; }
    public string Type { get; set; }
    public int Tiles_number_x { get; set; }
    public int Tiles_number_z { get; set; }
    public List<Tile> RoomTiles { get; set; }
    public List<IRoom> Neighbors { get; set; }

    public List<(string,Vector3)> AvailableSides { get; set; }
    
    public SpawningRoom( Vector3 position, List<GameObject> tiles,string type)
    {
        this.Neighbors = new List<IRoom>();
        this.RoomTiles= new List<Tile>();
        this.Type = type;
        this.Tiles_number_x = 5;
        this.Tiles_number_z = 5;
        this.Position = position;
        this.AvailableSides = CreateNeighborComponents();
        CreateRoom(tiles);
    }

    public void  CreateRoom(List<GameObject> tiles)
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

    public List<(string, Vector3)> CreateNeighborComponents()
    {

        //Create the available side components.
        List<(string, Vector3)> comp = new List<(string, Vector3)>();
        comp.Add(("N", new Vector3((Position.x + Tiles_number_x * Tile.X_length) / 2, Position.y, Position.z)));
        comp.Add(("S", new Vector3((Position.x + Tiles_number_x * Tile.X_length) / 2, Position.y, (Position.z + Tiles_number_z * Tile.Z_length))));
        comp.Add(("E", new Vector3((Position.x + Tiles_number_x * Tile.X_length), Position.y, (Position.z + Tiles_number_z * Tile.Z_length) / 2)));
        comp.Add(("W", new Vector3(Position.x, Position.y, (Position.z + Tiles_number_z * Tile.Z_length) / 2)));
        return comp;
    }
}
