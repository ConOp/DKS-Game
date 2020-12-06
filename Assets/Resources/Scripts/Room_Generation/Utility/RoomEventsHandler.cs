using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEventsHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            foreach(IRoom room in GameObject.FindGameObjectWithTag("DungeonMaster").GetComponent<NewRoomGen>().allrooms)
            {
                if (room.RoomObject.Equals(gameObject))
                {
                    ((Basic_Room)room).CloseDoors();
                }
            }
        }
    }
}
