using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomnessMaestro 
{

     static List<(string, float)> room_corridor_chance = new List<(string, float)>{
        ("Corridor",80f),
        ("Room",20f) };
    private static void ShowError( string error)
    {
        switch (error)
        {
            case "NO_SIDES":
                Debug.LogError("No available sides for the room");
                break;
        }
    }
    public static string OpenRandomAvailableSide(IRoom room)
    {
        if (room.Available_Sides.Count > 0)
        {
            return room.Available_Sides[Random.Range(0, room.Available_Sides.Count - 1)];
        }
        else
        {
            ShowError("NO_SIDES");
            return null;
        }
    }
    public static string Choose_Room_Or_Corridor()
    {
        return RandomProbability.Choose(room_corridor_chance);
    }
}
