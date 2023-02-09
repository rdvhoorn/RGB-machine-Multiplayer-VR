using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ViewFinalMachineScript : NetworkBehaviour
{
    public int id;

    public void ButtonClick() {
        goToNext();
        OnPressClose();
    }

    void goToNext() {
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<NetworkPlayer>().LoadFinalScene();
    }

    public GameObject explanationCanvas;

    public void OnPressClose() {
        Destroy(explanationCanvas);
    }
}
