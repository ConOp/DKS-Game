using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Modification_Prefab_Manager
{
    static List<GameObject> Movement_Speed_Modifications;
    static List<GameObject> Attack_Damage_Modifications;
    static List<GameObject> Attack_Range_Modifications;
    static List<GameObject> Attack_Speed_Modifications;
    public static bool LoadModificationPrefabs()
    {
        try
        {
            Movement_Speed_Modifications = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Modifications/Movement_Speed_Modifications"))); // Load all speed modification prefabs from the project folder.
            Attack_Damage_Modifications = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Modifications/Attack_Damage_Modifications"))); // Load all attack damage modification prefabs from the project folder.
            Attack_Range_Modifications = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Modifications/Attack_Range_Modifications"))); // Load all attack range modification prefabs from the project folder.
            Attack_Speed_Modifications = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Modifications/Attack_Speed_Modifications"))); // Load all attack speed modification prefabs from the project folder.
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static List<GameObject> GetAllSpeedModifications()
    {
        return Movement_Speed_Modifications;
    }
    public static GameObject SearchModification(string mod_name)
    {
        if (mod_name.Contains("Movement_Speed"))
        {
            for(int i = 0; i < Movement_Speed_Modifications.Count; i++)
            {
                if (Movement_Speed_Modifications[i].name == mod_name)
                {
                    return Movement_Speed_Modifications[i];
                }
            }
        }
        else if (mod_name.Contains("Attack_Damage"))
        {
            for (int i = 0; i < Attack_Damage_Modifications.Count; i++)
            {
                if (Attack_Damage_Modifications[i].name == mod_name)
                {
                    return Attack_Damage_Modifications[i];
                }
            }
        }
        else if (mod_name.Contains("Attack_Range"))
        {
            for (int i = 0; i < Attack_Range_Modifications.Count; i++)
            {
                if (Attack_Range_Modifications[i].name == mod_name)
                {
                    return Attack_Range_Modifications[i];
                }
            }
        }
        else if (mod_name.Contains("Attack_Speed"))
        {
            for (int i = 0; i < Attack_Speed_Modifications.Count; i++)
            {
                if (Attack_Speed_Modifications[i].name == mod_name)
                {
                    return Attack_Speed_Modifications[i];
                }
            }
        }
        return null;
    }
}
