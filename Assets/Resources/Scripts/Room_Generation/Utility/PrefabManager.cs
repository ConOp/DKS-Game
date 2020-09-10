using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabManager
{
    static List<GameObject> alltiles;
     static List<GameObject> allcorridors;

    /// <summary>
    /// Returns all loaded tiles from project.
    /// </summary>
    /// <returns></returns>
    public static List<GameObject> GetAllTiles()
    {
        return alltiles;
    }

    /// <summary>
    /// Returns all loaded corridors from project.
    /// </summary>
    /// <returns></returns>
    public static List<GameObject> GetAllCorridors()
    {
        return allcorridors;
    }
    /// <summary>
    /// Loads all prefabs from project.
    /// </summary>
    /// <returns></returns>
    public static bool LoadPrefabs()
    {
        try
        {
            alltiles = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Tiles"))); // Load all tile prefabs from the project folder.
            allcorridors = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Corridors"))); // Load all corridor prefabs from the project folder.
            return true;
        }
        catch
        {
            return false;
        }
    }
}
