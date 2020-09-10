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
}
