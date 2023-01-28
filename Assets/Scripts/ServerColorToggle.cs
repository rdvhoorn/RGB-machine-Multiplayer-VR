using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ServerColorToggle : NetworkBehaviour
{
    public bool toggle = false;

    [ServerRpc(RequireOwnership=false)]
    public void ToggleColorServerRpc(Color on, Color off) {
        Debug.Log("Toggle");
        if (toggle) {
            SetColorClientRpc(off);
        } else {
            SetColorClientRpc(on);
        }
        toggle = !toggle;
    }

    [ClientRpc]
    void SetColorClientRpc(Color c) {
        GetComponent<Renderer>().material.color = c;
    }
}
