using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class UIManager : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI playerCountText;
    int time = 0;

    private void Start()
    {
        if (isServer)
        {
            StartCoroutine(StartTimer());
        }
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            UpdateTimerClientRpc(time++);
            yield return new WaitForSeconds(1);
        }
    }

    [ClientRpc]
    public void UpdateTimerClientRpc(int time)
    {
        timerText.text = time.ToString();
    }

    public void UpdatePlayerCountUI(int numberOfPlayers)
    {
        playerCountText.text = numberOfPlayers.ToString();
    }
}
