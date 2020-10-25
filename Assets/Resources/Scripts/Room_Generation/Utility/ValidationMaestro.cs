using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ValidationMaestro
{
    public static List<string> GetAppropriateRooms(string room_corridor,string connect_side)
    {
        if (room_corridor == "Room")
        {
          return DataManager.Search_Available_Sides(connect_side,"Room");
        }
        else
        {
            return DataManager.Search_Available_Sides(connect_side, "Corridor");
        }
    }
    /// <summary>
    /// Checks if the space is not taken by other object.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="xsize"></param>
    /// <param name="zsize"></param>
    /// <returns></returns>
    public static bool IsNotClaimed(Vector3 location,int xsize,int zsize)
    {
        Vector3 center = new Vector3((location.x +(location.x + xsize*Tile.X_length)) / 2f, 0, (location.z+(location.z - zsize*Tile.Z_length)) / 2f);
        if (Physics.OverlapBox(center, new Vector3((xsize*Tile.X_length / 2f)-0.01f, 0, (zsize*Tile.Z_length / 2f)-0.01f)).Length > 0)
        {
            return false;
        }
        return true;
    }
}
