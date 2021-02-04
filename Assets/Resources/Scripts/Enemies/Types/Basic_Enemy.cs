using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic_Enemy:MonoBehaviour
{

  
    public Modification_Base_Collection Modification_Bases;




    public abstract void InitializeModificationBases();

    private void Awake()
    {
        InitializeModificationBases(); 
    }
   
    /// <summary>
    /// Adds a modification.
    /// </summary>
    /// <param name="mod_type"></param>
    public void Add_Modification(string mod_type)
    {
        Modification mod = Modification_Prefab_Manager.GetInstance().SearchModification(mod_type).GetComponent<Modification>();
        foreach (Modification_Base mBase in Modification_Bases.GetAllBases())
        {
            if (mBase.AttachModification(mod))
            {
                
            }
        }
      
        
        return;
    }
    /// <summary>
    /// Removes the selected modification and the modification boosts.
    /// </summary>
    /// <param name="mod_to_remove"></param>
    public void Remove_Modification(GameObject mod_to_remove)
    {
        foreach(Modification_Base mbase in Modification_Bases.GetAllBases())
        {
            if (mbase.mod != null)
            {
                if (mbase.mod.Equals(mod_to_remove))
                {
                    mbase.DetachModification();
                    foreach (GameObject player in Battle_Manager.GetInstance().GetBattle(gameObject).GetPlayers())
                    {
                        if (player.GetComponent<LockOnTarget>().targetedCreature.Equals(gameObject))
                        {
                            player.GetComponent<LockOnTarget>().RefreshTargets();
                        }
                    }
                    return;
                }
            }
        }
    }
   
}
