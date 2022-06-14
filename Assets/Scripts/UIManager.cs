using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class UIManager : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI playerCountText;

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
