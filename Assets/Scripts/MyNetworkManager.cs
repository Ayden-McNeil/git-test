using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("Server stared");
    }
    public override void OnStopServer()
    {
        Debug.Log("Server stopped");
    }

    public override void OnStartClient()
    {
        Debug.Log("Connected to server");
    }
    public override void OnStopClient()
    {
        Debug.Log("Disconnected to server");
    }
}
