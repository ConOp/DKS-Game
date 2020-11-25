using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomnessMaestro 
{

    public static bool endRoomPlaced=false;

    //Room or Corridor chances.
    static List<(string, float)> room_corridor_chance = new List<(string, float)>{
        ("Corridor",60f),
        ("Room",40f) };
    //
    static List<(string, float)> corridor_type = new List<(string, float)>{
        ("Corner",30f),
        ("Straight",70f) };
    //Room size chances.
    static List<(string, float)> room_size = new List<(string, float)>{
        ("Small",20f),
        ("Medium",50f),
        ("Large",30f)
    };

    //Room type chances.
    static List<(string, float)> room_type = new List<(string, float)>{
        ("FightingRoom",98.9f),
        ("TreasureRoom",1f),
        ("EndRoom",0.1f)
    };


    /// <summary>
    /// Shows appropriate error (debug perposes)
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

    /// <summary>
    /// Decides the next room type depending on the list of the available rooms.
    /// </summary>
    /// <param name="available_rooms"></param>
    /// <returns></returns>
    public static string Choose_Room_Type(List<string> available_rooms)
    {
        List<(string, float)> appropriateProbabilities = new List<(string, float)>();
        float endRoomAddedProbability = 0;
        bool endRoomFound = false;
        foreach (string room in available_rooms)
        {
            for (int i = 0; i < room_type.Count; i++)
            {
                if (room.Equals(room_type[i].Item1))
                {
                    if (room.Equals("EndRoom")&&!endRoomPlaced) {
                        endRoomAddedProbability = ((100 - room_type[i].Item2) / NewRoomGen.RoomNumber) * (NewRoomGen.roomsPlaced+1);
                        endRoomFound = true;
                    }
                    appropriateProbabilities.Add(room_type[i]);//Get only the available rooms probabilities.
                    break;
                }
            }
        }
        if (!endRoomPlaced&&endRoomFound)
        {
            for (int i = 0; i < appropriateProbabilities.Count; i++)
            {
                if (appropriateProbabilities[i].Item1.Equals("EndRoom"))
                {
                    appropriateProbabilities[i]= (appropriateProbabilities[i].Item1,appropriateProbabilities[i].Item2+endRoomAddedProbability);
                    Debug.Log(appropriateProbabilities[i].Item1 + " " + appropriateProbabilities[i].Item2);
                }
                else
                {
                    appropriateProbabilities[i] = (appropriateProbabilities[i].Item1, appropriateProbabilities[i].Item2 - (endRoomAddedProbability/(appropriateProbabilities.Count-1)));
                }

            }
        }
        return RandomProbability.Choose(appropriateProbabilities);
    }
    /// <summary>
    /// Decides the corridor type.
    /// </summary>
    /// <returns></returns>
    public static string Choose_Corridor_Type()
    {
        return RandomProbability.Choose(corridor_type);
    }
}
