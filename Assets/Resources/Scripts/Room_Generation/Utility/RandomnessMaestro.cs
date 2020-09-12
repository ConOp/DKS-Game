using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomnessMaestro 
{


    //Room or Corridor chances.
    static List<(string, float)> room_corridor_chance = new List<(string, float)>{
        ("Corridor",0f),
        ("Room",100f) };

    //Room size chances.
    static List<(string, float)> room_size = new List<(string, float)>{
        ("Small",20f),
        ("Medium",50f),
        ("Large",30f)
    };


    /// <summary>
    /// Shows appropriate error (debug perpuses)
    /// </summary>
    /// <param name="error"></param>
    private static void ShowError( string error)
    {
        switch (error)
        {
            case "NO_SIDES":
                Debug.LogError("No available sides for the room");
                break;
        }
    }
    /// <summary>
    /// Chooses an available open side.
    /// </summary>
    /// <param name="room"></param>
    /// <returns>Random available side</returns>
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
    /// <summary>
    /// Decides if next room is corridor or room.
    /// </summary>
    /// <returns></returns>
    public static string Choose_Room_Or_Corridor()
    {
        return RandomProbability.Choose(room_corridor_chance);
    }
    /// <summary>
    /// Decides the next room size.
    /// </summary>
    /// <returns></returns>
    public static string Choose_Room_Size()
    {
        return RandomProbability.Choose(room_size);
    }
}
