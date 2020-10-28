using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Modification_Factory
{
    public static IModification Construct_Modification(string modtype)
    {
        switch (modtype)
        {
            case "speed1":
                return new Speed_Modification_1();
            default:
                return new Speed_Modification_1();
        }
    }
}
