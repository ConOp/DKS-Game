using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
public class RoomGenScript : MonoBehaviour
{/*
    public GameObject Root_Room;
    public int Number_Of_Rooms;
    public int room_size;
    TreeStructure<Room> Tree_Room;
    int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        Room Rm = new Room(Root_Room, true, true, true, true, Root_Room.transform.position, Root_Room.transform.localScale.x, Root_Room.transform.localScale.z);
        Tree_Room = new TreeStructure<Room>(Rm);
        Room Current_room = Rm;
        List<GameObject> Allrooms = new List<GameObject>((Resources.LoadAll<GameObject>("Prefabs/Rooms")));
        for (int i=1; i <= Number_Of_Rooms; i++)
        {
            List<string> available_sides=new List<string>();
            int search = 1;
            int returns = 0;
            do
            {
                
                if (Current_room.Left)
                {
                    available_sides.Add("left");
                }
                if (Current_room.Bottom)
                {
                    available_sides.Add("bottom");
                }
                if (Current_room.Right)
                {
                    available_sides.Add("right");
                }
                if (Current_room.Top)
                {
                    available_sides.Add("top");
                
                }
                if (search >= count)
                {
                    Current_room = Tree_Room.root;
                    returns++;
                }
                else if (!available_sides.Any())
                {
                   
                    Current_room = Tree_Room.GetChild(search).root;
                    search++;
                }
                
                Debug.Log(Current_room.Objroom.name);
                Debug.Log(available_sides.Count);
                Debug.Log("search: "+search);
                Debug.Log("count:"+ count);
                Debug.Log(Tree_Room.children.Count);
             
            } while (returns<4 && !available_sides.Any());

            if (returns >= 4)
            {
                break;
            }
            Room fitroom = findfitroom(Allrooms, Current_room, available_sides[Random.Range(0,available_sides.Count)], "noend");
            checkavailability(fitroom);
            Tree_Room.AddChild(fitroom);
            Instantiate(fitroom.Objroom, fitroom.Position, new Quaternion() );
            Tree_Room.Traverse(Tree_Room, new TreeVisitor<Room>(checkavailability));
            Current_room = fitroom;
            count++;
        }
      
    }

    //Returns Best fit GameObject room to place
    private Room findfitroom(List<GameObject> rooms, Room starting, string side, string type)
    {
        List<GameObject> neededroomtype = rooms.Where(obj => Regex.IsMatch(obj.tag.ToString(), createfitregex(starting, side, type))).ToList(); //Create list of all possible rooms
        GameObject selected_room = neededroomtype[Random.Range(0, neededroomtype.Count)]; //Random selection of room type 
        bool Left=false, Bottom=false, Right=false, Top = false;
        if (selected_room.tag.ToString()[0] == '1')
        {
            Left = true;
        }
        if(selected_room.tag.ToString()[2] == '1') // Find the available sides
        {
            Bottom = true;
        }
        if (selected_room.tag.ToString()[4] == '1')
        {
            Right = true;
        }
        if (selected_room.tag.ToString()[6] == '1')
        {
            Top = true;
        }
        Vector3 pos;
        if (side == "left")
        {
            pos = new Vector3(starting.Position.x-starting.X_Size*room_size, starting.Position.y, starting.Position.z);
        }
        else if (side == "bottom")
        {
            pos = new Vector3(starting.Position.x, starting.Position.y, starting.Position.z- starting.Z_Size * room_size);
        }
        else if (side == "right")
        {
            pos = new Vector3(starting.Position.x+ starting.X_Size*room_size, starting.Position.y, starting.Position.z);
        }
        else
        {
            pos = new Vector3(starting.Position.x, starting.Position.y, starting.Position.z + starting.Z_Size * room_size);
        }

        return new Room(selected_room,Left,Bottom,Right,Top,pos,selected_room.transform.localScale.x,selected_room.transform.localScale.y);
    }

    Given the current room, the side and if its an ending room or not
     *this function generates the regex of the needed room
     
    private string createfitregex(Room currentroom, string side, string roomtype)
    {
        string temp = currentroom.Objroom.tag.ToString();
        string regexready = "";
        if (roomtype == "end")
        {
            switch (side)
            {
                case "left":
                    regexready = "0,0,1,0";
                    break;
                case "bottom":
                    regexready = "0,0,0,1";
                    break;
                case "right":
                    regexready = "1,0,0,0";
                    break;
                case "top":
                    regexready = "0,1,0,0";
                    break;
            }

        }
        else
        {
            switch (side)
            {
                case "left":
                    regexready = "[0:1],[0:1],1,[0:1]";
                    break;
                case "bottom":
                    regexready = "[0:1],[0:1],[0:1],1";
                    break;
                case "right":
                    regexready = "1,[0:1],[0:1],[0:1]";
                    break;
                case "top":
                    regexready = "[0:1],1,[0:1],[0:1]";
                    break;
            }
        }
        return regexready;

    }

    private void checkavailability(Room tr)
    {
        float x=tr.Position.x, y=tr.Position.y, z=tr.Position.z;
        float radius = 3;
        Vector3 spawnPos = new Vector3(x-tr.X_Size*room_size, y, z);
         if (Physics.CheckSphere(spawnPos, radius))
        {
            tr.Left= false;
        }
        spawnPos = new Vector3(x, y, z-tr.Z_Size*room_size);
        if (Physics.CheckSphere(spawnPos, radius))
        {
            tr.Bottom = false;
        }
        spawnPos = new Vector3(x +tr.X_Size*room_size, y, z);
        if (Physics.CheckSphere(spawnPos, radius))
        {
            tr.Right = false;
        }
        spawnPos = new Vector3(x , y, z+tr.Z_Size*room_size);
        if (Physics.CheckSphere(spawnPos, radius))
        {
            tr.Top = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    */

    
}
