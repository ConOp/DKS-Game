using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    /// <summary>
    /// Room helping struct.
    /// </summary>
    struct Room_Type
    {
        public bool[] available_sides;
        public string room_type;
    }

    //Room Sizes Dictionary.
    static readonly Dictionary<string, (int, int)> room_sizes_data = new Dictionary<string, (int, int)>()
    {
        {"Small",(3,3) },
        {"Medium",(5,5) },
        {"Large",(7,7) }
    };
    
    //Room Available sides data.
    //HINT! Sides go in this order = [Left,Top,Right,Bottom] -> true if available, false if not available.
    static readonly Room_Type[] room_types_data = new Room_Type[]
    {
        new Room_Type(){available_sides=new bool[4]{true,true,true,true},room_type="SpawningRoom" },
        new Room_Type(){available_sides=new bool[4]{true,true,true,true},room_type="FightingRoom" }
    };

    //Corridor Available sides data.
    //HINT! Sides go in this order = [Left,Top,Right,Bottom] -> true if available, false if not available.
    static readonly Room_Type[] corridor_types_data = new Room_Type[]
    {
        new Room_Type(){available_sides=new bool[4]{true,false,true,false},room_type="Corridor_Horizontal" },
        new Room_Type(){available_sides=new bool[4]{false,true,false,true},room_type="Corridor_Vertical" },
        new Room_Type(){available_sides=new bool[4]{true,false,false,true},room_type="Corner_Bottom_Left" },
        new Room_Type(){available_sides=new bool[4]{false,false,true,true},room_type="Corner_Bottom_Right" },
        new Room_Type(){available_sides=new bool[4]{true,true,false,false},room_type="Corner_Top_Left" },
        new Room_Type(){available_sides=new bool[4]{false,true,true,false},room_type="Corner_Top_Right" },
    };

    /// <summary>
    /// Get room proportions depending on room size.
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static (int,int) Search_Sizes_Dictionary(string size)
    {
        return room_sizes_data[size];
    }

    /// <summary>
    /// Get correct adjacent rooms depending on the side.
    /// </summary>
    /// <param name="side"></param>
    /// <returns></returns>
    public static List<string> Search_Available_Sides(string side,string type)
    {
        int index;
        if (side == "Left")
        {
            index = 0;
        }
        else if (side == "Top")
        {
            index = 1;
        }
        else if (side == "Right")
        {
            index = 2;
        }
        else 
        {
            index = 3;
        }
        List<string> correct_rooms = new List<string>();
        if (type == "Room")
        {
            foreach (Room_Type rm in room_types_data)
            {
                if (rm.available_sides[index] == true)
                {

                    correct_rooms.Add(rm.room_type);
                }
            }
        }
        else
        {

            foreach (Room_Type rm in corridor_types_data)
            {
                if (rm.available_sides[index] == true)
                {

                    correct_rooms.Add(rm.room_type);
                }
            }
        }
        return correct_rooms;
    }
}
