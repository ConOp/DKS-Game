using UnityEngine;

public class PlayerManager1 : MonoBehaviour
{
    //player's info
    public int id;                                          //local player's client id
    public string username;

    public void Initialize(int player_id, string uname)
    {
        id = player_id;                                     //assign id, username to player gameobject
        username = uname;
    }
}
