using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ViewFinalMachineScript : NetworkBehaviour
{
    public void ButtonClick() {
        goToNextServerRpc();
        OnPressClose();
    }

    [ServerRpc(RequireOwnership = false)]
    void goToNextServerRpc() {
        Debug.Log("hey!");
        NetworkManager.Singleton.ConnectedClientsList[0].PlayerObject.GetComponent<NetworkPlayer>().LoadFinalScene();
    }

    public GameObject explanationCanvas;

    public void OnPressClose() {
        Destroy(explanationCanvas);
    }
}
