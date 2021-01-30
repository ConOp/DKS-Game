using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionCanvas : MonoBehaviour
{
    public UnityEngine.UI.InputField nameField;
    public void ConnectToServer()
    {
        ConnectionManager.GetInstance().username = nameField;
        ConnectionManager.GetInstance().ConnectToServer();
    }
}
