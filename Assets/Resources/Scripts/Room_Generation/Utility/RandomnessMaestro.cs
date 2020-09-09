using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomnessMaestro 
{
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
}
