using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory 
{
    //Room Factory building room depending of the type.
 public static IRoom Build(string type,Vector3 position,List<GameObject> tiles,int tiles_x,int tiles_z)
    {
        switch (type)
        {
            case "SpawningRoom":
                return new SpawningRoom(position,tiles,type);
            case "FightingRoom":
               // return new FightingRoom(position, tiles, type,tiles_x,tiles_z);
            default:
                return new SpawningRoom(position, tiles,type);
        }
    }
}
