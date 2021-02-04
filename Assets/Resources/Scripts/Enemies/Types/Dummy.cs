using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy:Basic_Enemy
{
    public override void InitializeModificationBases()
    {
        Modification_Bases = new Modification_Base_Collection();
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Base"))
            {
                Modification_Bases.AddModificationBase(child.GetComponent<Modification_Base>());
            }
        }
    }


}
