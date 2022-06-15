using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class UIManager : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI playerCountText;
    [SerializeField] TextMeshProUGUI serverCount;
    [SerializeField] TextMeshProUGUI clientCount;

    [Command]
    public void UpdateServerwithClientCubeNumber(int index, int count)
    {
        Debug.Log("UpdatesererwithClient CubeNumber was called");
        switch (index)
        {
            case 0:
                serverCount.text = count.ToString();
                break;

            case 1:
                clientCount.text = count.ToString();
                break;
        }       
        UpdateClientWithCubeNumber(serverCount.text, clientCount.text);
    }

    [ClientRpc]
    private void UpdateClientWithCubeNumber(string countServer, string countClient)
    {
        serverCount.text = countServer;
        clientCount.text = countClient;
    }

    public void UpdatePlayerCountUI(int count)
    {
        playerCountText.text = count.ToString();
    }

    [ClientRpc]
    public void UpdateTimerClientRpc(int time)
    {
        timerText.text = time.ToString();
    }
}
