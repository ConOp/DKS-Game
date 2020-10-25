using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabManager
{
    static List<GameObject> allroomtiles;
     static List<GameObject> allcorridortiles;

    /// <summary>
    /// Returns all loaded tiles from project.
    /// </summary>
    /// <returns></returns>
    public static List<GameObject> GetAllRoomTiles()
    {
        return allroomtiles;
    }

    /// <summary>
    /// Returns all loaded corridors from project.
    /// </summary>
    /// <returns></returns>
    public static List<GameObject> GetAllCorridorTiles()
    {
        return allcorridortiles;
    }
    /// <summary>
    /// Loads all prefabs from project.
    /// </summary>
    /// <returns></returns>
    public static bool LoadPrefabs()
    {
        try
        {
            allroomtiles = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Tiles/Room_Tiles"))); // Load all tile prefabs from the project folder.
            allcorridortiles = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Tiles/Corridor_Tiles"))); // Load all corridor prefabs from the project folder.
            return true;
        }
        catch
        {
            return false;
        }
    }
}
