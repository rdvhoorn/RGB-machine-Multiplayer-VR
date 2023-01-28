using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ChangeColorTest: NetworkBehaviour
{
    public ServerColorToggle colorToggle;

    public void Select() {
        colorToggle.ToggleColorServerRpc(Color.green, Color.red);
    }
}
