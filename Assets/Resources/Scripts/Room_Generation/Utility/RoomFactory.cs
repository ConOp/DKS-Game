using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory 
{
    /// <summary>
    /// Room Factory constructs room given the type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="position"></param>
    /// <param name="tiles"></param>
    /// <param name="tiles_x"></param>
    /// <param name="tiles_z"></param>
    /// <returns></returns>
    public static IRoom Build(string type,Vector3 position,List<GameObject> tiles,int tiles_x,int tiles_z)
    {
        switch (type)
        {
            case "SpawningRoom":
                return new SpawningRoom(position,tiles,type);
            case "FightingRoom":
                return new FightingRoom(position, tiles, type,tiles_x,tiles_z);
            case "Horizontal_Corridor":
                return new Horizontal_Corridor(position, tiles, type, tiles_x, tiles_z);
            case "Horizontal_End_Corridor":
                return new Horizontal_End_Corridor(position, tiles, type, tiles_x, tiles_z);
            case "Vertical_Corridor":
                return new Vertical_Corridor(position, tiles, type, tiles_x, tiles_z);
            case "Vertical_End_Corridor":
                return new Vertical_End_Corridor(position, tiles, type, tiles_x, tiles_z);
            default:
                return new SpawningRoom(position, tiles,type);
        }
    }
}
