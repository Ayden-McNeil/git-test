using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class ClockStartScript : NetworkBehaviour
{
    [SerializeField] UIManager UIManagerScript;
    int text = 0;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            UIManagerScript.UpdateTimerClientRpc(text);
            yield return new WaitForSeconds(1);
        }
    }
}
