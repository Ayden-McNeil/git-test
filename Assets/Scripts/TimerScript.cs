using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class TimerScript : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    [ClientRpc]
    public void UpdateTimerClientRpc(int time)
    {
        timerText.text = time.ToString();
    }
}
