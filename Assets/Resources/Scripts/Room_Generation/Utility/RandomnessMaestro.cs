using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomnessMaestro
{

    public static bool endRoomPlaced = false;

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
        ("ChestRoom",1f),
        ("EndRoom",0.1f)
    };


    /// <summary>
    /// Shows appropriate error (debug perposes)
    /// </summary>
    /// <param name="error"></param>
    private static void ShowError(string error)
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
        List<(string, float)> appropriateProbabilities = CalculateProbabilites(available_rooms);
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
    /// <summary>
    /// Calculates the probabibilites of all the available rooms depending on some formulas.
    /// </summary>
    /// <param name="available_rooms"></param>
    /// <returns></returns>
    static List<(string, float)> CalculateProbabilites(List<string>available_rooms)
    {
        List<(string, float)> appropriateProbabilities = new List<(string, float)>();
        foreach (string room in available_rooms)
        {
            appropriateProbabilities.Add(room_type.Find(x => x.Item1 == room));//Get only the available rooms probabilities.
        }
        float endRoomProbability;
        float endRoomStartingProb = room_type.Find(x => x.Item1 == "EndRoom").Item2;
        float t;
        float probabilityNotEndroom;
        if (!endRoomPlaced)
        {
            /*Variable to decide if the room has more probabilities to spawn the start, middle or the end of the dungeon.
             * Low --------> High Power = Start --------> End.
            */
            t = (float)System.Math.Pow((float)(NewRoomGen.roomsPlaced + 1) / NewRoomGen.RoomNumber, 10);
            endRoomProbability = endRoomStartingProb * (1 - t) + 100 * t;
            probabilityNotEndroom = 100 - endRoomProbability;
        }
        else
        {
            endRoomProbability = 0;
            probabilityNotEndroom = 100;
        }
        for (int i = 0; i < appropriateProbabilities.Count; i++)
        {
            if (appropriateProbabilities[i].Item1.Equals("EndRoom"))
            {
                appropriateProbabilities[i] = (appropriateProbabilities[i].Item1, endRoomProbability);
            }
            else
            {
                appropriateProbabilities[i] = (appropriateProbabilities[i].Item1, appropriateProbabilities[i].Item2 * probabilityNotEndroom / (100 - endRoomStartingProb));
            }
        }
        return appropriateProbabilities;
    }
}
