using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ViewFinalMachineScript : NetworkBehaviour
{
    public int id;

    public void ButtonClick() {
        goToNextServerRpc();
        OnPressClose();
    }

    [ServerRpc(RequireOwnership = false)]
    void goToNextServerRpc() {
        NetworkManager.Singleton.ConnectedClientsList[id].PlayerObject.GetComponent<NetworkPlayer>().LoadFinalScene();
    }

    public GameObject explanationCanvas;

    public void OnPressClose() {
        Destroy(explanationCanvas);
    }
}
