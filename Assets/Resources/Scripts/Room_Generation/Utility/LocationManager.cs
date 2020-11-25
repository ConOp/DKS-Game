using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocationManager
{
    public  static Vector3 GetApropriateLocationForRoom(string connectionSide,Vector3 opening_location,int new_opening_index,int TilesX,int TilesZ)
    {
        Vector3 new_placed_location = new Vector3(0, 0, 0);
        if (connectionSide == "Left")
        {
            new_placed_location.x = opening_location.x - (Tile.X_length * TilesX);
            new_placed_location.z = opening_location.z + (Tile.Z_length * (new_opening_index / TilesX));
        }
        else if (connectionSide == "Right")
        {
            new_placed_location.x = opening_location.x + Tile.X_length;
            new_placed_location.z = opening_location.z + (Tile.Z_length * (new_opening_index / TilesX));
        }
        else if (connectionSide == "Top")
        {
            new_placed_location.x = opening_location.x - (Tile.X_length * (new_opening_index % TilesX));
            new_placed_location.z = opening_location.z + (Tile.Z_length * TilesZ);
        }
        else
        {
            new_placed_location.x = opening_location.x - (Tile.X_length * (new_opening_index % TilesX));
            new_placed_location.z = opening_location.z - Tile.Z_length;
        }
        return new_placed_location;
    }
}
