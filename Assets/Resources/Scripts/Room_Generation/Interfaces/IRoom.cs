using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoom
{
    Vector3 Position { get; set; }
    GameObject RoomObject { get; set; }
    int Tiles_number_x { get; set; }
    int Tiles_number_z { get; set; }
    List<Tile> RoomTiles { get; set; }
    string Type { get; set; }
    void CreateRoom(List<GameObject> tiles);


}