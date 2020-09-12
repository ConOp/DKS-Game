using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Basic_Room : IRoom
{
    public GameObject RoomObject { get; set; }
    public Vector3 Position { get; set; }
    public string Type { get; set; }
    public int Tiles_number_x { get; set; }
    public int Tiles_number_z { get; set; }
    public List<Tile> RoomTiles { get; set; }
    public List<GameObject> Instantiated_Tiles { get; set; }
    public List<string> Available_Sides { get; set; }
    public Basic_Room(List<GameObject> tiles, string type, int tiles_x, int tiles_z)
    {
        this.RoomTiles = new List<Tile>();
        this.Type = type;
        this.Tiles_number_x = tiles_x;
        this.Tiles_number_z = tiles_z;
        CreateRoom(tiles);
    }
    public abstract void CreateRoom(List<GameObject> tiles);
    /// <summary>
    /// Create new opening to connect rooms on selected side.
    /// </summary>
    /// <param name="side"></param>
    /// <returns></returns>
    public (int,Vector3,string) CreateOpening(string side)
    {
        int opening;
        int index_x, index_z;
        if (side == "Random")
        {
            side = RandomnessMaestro.OpenRandomAvailableSide(this);
        }
        if (side == "Left")
        {
            index_z = this.Tiles_number_z / 2;
            index_x = 0;


        }
        else if (side == "Top")
        {
            index_z = 0;
            index_x = this.Tiles_number_x / 2;
        }
        else if (side == "Right")
        {
            index_z = this.Tiles_number_z / 2;
            index_x = this.Tiles_number_x - 1;
        }
        else
        {
            index_z = this.Tiles_number_z - 1;
            index_x = this.Tiles_number_x / 2;
        }
        Available_Sides.Remove(side);
        opening = (index_z * Tiles_number_x + index_x);
        //Tile to be placed.
        Tile t = new Tile("Center", PrefabManager.GetAllTiles().Where(obj => obj.name == "Center").First(), RoomTiles[opening].Position_X, RoomTiles[opening].Position_Z);
        //Replace opening.
        RoomTiles[opening] = t;
        Vector3 oldtileloc = new Vector3(0, 0, 0);
        if (Instantiated_Tiles!=null)//If room is already instatiated.
        {
            oldtileloc = Instantiated_Tiles[opening].transform.position;
            //Destroy old opening.
            Object.Destroy(Instantiated_Tiles[opening]);
            //Instantiate new opening.
            Instantiated_Tiles[opening] = Object.Instantiate(t.Objtile, oldtileloc, new Quaternion(), RoomObject.transform);
        }
        return (opening, oldtileloc,side);

    }
    /// <summary>
    /// Create an adjacent room on selected side.
    /// </summary>
    /// <param name="side"></param>
    /// <param name="opening_location"></param>
    /// <returns></returns>
    public (IRoom,Vector3) CreateAdjacentRoom(string side,Vector3 opening_location)
    {
        string room_corridor = RandomnessMaestro.Choose_Room_Or_Corridor();
        List<string>available_rooms = ValidationMaestro.GetAppropriateRooms(room_corridor, side);
        //TODO Choose random room depending on probability from available_rooms.
        if (room_corridor == "Room")//TODO make corridors and room use same code.
        {
            (int sizex, int sizez) = DataManager.Search_Sizes_Dictionary(RandomnessMaestro.Choose_Room_Size());
            IRoom new_room = RoomFactory.Build(available_rooms[1], PrefabManager.GetAllTiles(), sizex, sizez);
            //Change side to the desired side so that the adjacent room can place the opening appropriately.
            string adjside;
            if (side == "Left")
                adjside = "Right";
            else if (side == "Right")
                adjside = "Left";
            else if (side == "Top")
                adjside = "Bottom";
            else
                adjside = "Top";
            (int new_opening_index,Vector3 newopenloc,string newside)=new_room.CreateOpening(adjside); //Create opening to the adjacent room.
            Vector3 new_placed_location=new Vector3(0,0,0);
            //Place new adjacent room appropriately.
            if (side == "Left")
            {
                new_placed_location.x = opening_location.x - (Tile.X_length*new_room.Tiles_number_x);
                new_placed_location.z = opening_location.z + (Tile.Z_length * (new_opening_index / new_room.Tiles_number_x));
            }
            else if (side == "Right")
            {
                new_placed_location.x = opening_location.x + Tile.X_length;
                new_placed_location.z = opening_location.z + (Tile.Z_length * (new_opening_index / new_room.Tiles_number_x));
            }
            else if (side == "Top")
            {
                new_placed_location.x = opening_location.x - (Tile.X_length * (new_opening_index % new_room.Tiles_number_x));
                new_placed_location.z = opening_location.z + (Tile.Z_length * new_room.Tiles_number_z);
            }
            else
            {
                new_placed_location.x = opening_location.x - (Tile.X_length * (new_opening_index % new_room.Tiles_number_x));
                new_placed_location.z = opening_location.z - Tile.Z_length;
            }
            return (new_room, new_placed_location);
        }
        return (null, new Vector3(0,0,0));

    }
}
