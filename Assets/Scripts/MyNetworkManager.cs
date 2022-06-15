using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkManager : NetworkManager
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] UIManager UIManagerScript;
    int text = 0;
    public override void OnStartServer()
    {
        Debug.Log("Server stared");
        StartCoroutine(StartTimer());
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

    IEnumerator StartTimer()
    {
        while (true)
        {
            timerText.text = text++.ToString();
            //UIManagerScript.UpdateTimerClientRpc(text);
            yield return new WaitForSeconds(1);
        }
    }

}
