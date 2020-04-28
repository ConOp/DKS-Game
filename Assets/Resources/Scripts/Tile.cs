using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
   public GameObject Objtile;
    public static int X_length = 10;
    public static int Z_length = 10;
   public string Type { get; set; }
   public float Position_X { get; set; }
   public float Position_Z { get; set; }
        

   public Tile(string type,GameObject objtile,float positionx,float positionz)
    {
        this.Objtile = objtile;
        this.Type = type;
        this.Position_X = positionx;
        this.Position_Z = positionz;

    }
}
