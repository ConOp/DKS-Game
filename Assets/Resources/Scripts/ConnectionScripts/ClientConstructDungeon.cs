using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClientConstructDungeon
{
    #region Singleton
    private static ClientConstructDungeon instance = null;
    public static ClientConstructDungeon GetInstance()
    {
        if (instance == null)
        {
            return new ClientConstructDungeon();
        }
        return instance;
    }
    
    private ClientConstructDungeon()
    {
        instance = this;
    }
    #endregion
    
    GameObject dungeon;
    // IRoom currentConstructionRoom;
    string category;
    List<GameObject> instantiated_tiles;
    List<GameObject> dungeonDoors;
    GameObject currRoom;
    bool initialized = false;
    public bool isInitialized()
    {
        return initialized;
    }
    public void InitializeDungeon()
    {
        dungeon = new GameObject("Dungeon");
        dungeonDoors = new List<GameObject>();
        initialized = true;
    }
    public void ReadNInitializeRoom(string roomName,Vector3 roomLocation,int tilesX,int tilesZ,string category, string type)
    {   
       // currentConstructionRoom = RoomFactory.Build()
        //currentConstructionRoom.RoomObject = new GameObject(roomName);
        currRoom = new GameObject(roomName);
        this.category = category;
        //currentConstructionRoom.Type = type;
        // currentConstructionRoom.Category = category;
        // currentConstructionRoom.RoomObject.transform.position = new Vector3(roomLocation.x + (Mathf.Abs((roomLocation.x + tilesX * Tile.X_length) - roomLocation.x) / 2), roomLocation.y + 2.5f, roomLocation.z - (Mathf.Abs((roomLocation.z + tilesZ * Tile.Z_length) - roomLocation.z) / 2));
        currRoom.transform.position = new Vector3(roomLocation.x + (Mathf.Abs((roomLocation.x + tilesX * Tile.X_length) - roomLocation.x) / 2), roomLocation.y + 2.5f, roomLocation.z - (Mathf.Abs((roomLocation.z + tilesZ * Tile.Z_length) - roomLocation.z) / 2));
        instantiated_tiles = new List<GameObject>();
        //currentConstructionRoom.RoomObject.transform.parent = dungeon.transform;
        //currentConstructionRoom.Position = roomLocation;
        currRoom.transform.parent = dungeon.transform;
        if (category.Equals("Room") && (type != "SpawningRoom" && type != "ChestRoom" && type != "EndRoom"))
        {
            currRoom.AddComponent<BoxCollider>();//Create new collider for the room.
            currRoom.GetComponent<BoxCollider>().size = new Vector3(tilesX * Tile.X_length - 2, 5, tilesZ * Tile.Z_length - 2);//Set the size of the collider to cover all the room.
            currRoom.GetComponent<BoxCollider>().isTrigger = true;//Set collider to trigger.
        }
    }
    public void ReadNConstructTile(string tileName, Vector3 tileLocation,Quaternion tileRotation)
    {
        Tile tile;
        if (category.Equals("Room"))//currentConstructionRoom.Category.Equals("Room"))
        {
             tile = new Tile(tileName, PrefabManager.GetInstance().GetAllRoomTiles().Where(obj => obj.name == tileName).First(), tileLocation);
        }
        else
        {
             tile = new Tile(tileName, PrefabManager.GetInstance().GetAllCorridorTiles().Where(obj => obj.name == tileName).First(), tileLocation);
        }
        GameObject ObjTile = Object.Instantiate(tile.Objtile, tileLocation, tileRotation, currRoom.transform);
        instantiated_tiles.Add(ObjTile);// currentConstructionRoom.RoomObject.transform));
        if (ObjTile.name.Contains("Door")){
            dungeonDoors.Add(ObjTile);
        }
    }

    public void DoorRemoteControl(Vector3 doorLoc, bool isopen)
    {
        foreach(GameObject door in dungeonDoors)
        {
            if (door.transform.position.Equals(doorLoc))
            {
                door.GetComponent<Animator>().SetBool("isOpen", isopen);
            }
        }
    }
    public void FinalizeRoom()
    {
      /*  if (currentConstructionRoom != null)
        {
            currentConstructionRoom.Instantiated_Tiles = instantiated_tiles;
            allrooms.Add(currentConstructionRoom);
        }
    */
    }
}
