using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Modification_Prefab_Manager
{
    static List<GameObject> Speed_Modifications;
    public static bool LoadModificationPrefabs()
    {
        try
        {
            Speed_Modifications = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Modifications/Speed_Modifications"))); // Load all speed modification prefabs from the project folder.
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static List<GameObject> GetAllSpeedModifications()
    {
        return Speed_Modifications;
    }
    public static GameObject SearchModification(string mod_name)
    {
        if (mod_name.Contains("Speed"))
        {
            for(int i = 0; i < Speed_Modifications.Count; i++)
            {
                if (Speed_Modifications[i].name == mod_name)
                {
                    return Speed_Modifications[i];
                }
            }
        }
        return null;
    }
}
