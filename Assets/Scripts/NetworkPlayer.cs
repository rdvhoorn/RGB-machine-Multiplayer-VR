using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;


public class NetworkPlayer : NetworkBehaviour
{
    private Vector3[] sps = new Vector3[3]{
        new Vector3(-26.2700005f,-0.0396533012f,16.8500004f), // electrical,
        new Vector3(-62.2299995f,-0.0396533012f,19.9300003f), // software
        new Vector3(-40.6399994f,-0.0396533012f,-0.389999866f), // mech
    };

    private Vector3[] sps_start = new Vector3[3]{
        new Vector3(-26.2700005f,-0.0396533012f,16.8500004f), // electrical,
        new Vector3(-62.2299995f,-0.0396533012f,19.9300003f), // software
        new Vector3(-40.6399994f,-0.0396533012f,-0.389999866f), // mech
    };

    private Vector3[] finalLocations = new Vector3[3]{
        new Vector3(230.74527f,510,-311.751129f),
        new Vector3(230.74527f,694,-311.751129f),
        new Vector3(230.74527f,318,-311.751129f),
    };

    private int numberConnectedClientsStart = 3;

    public override void OnNetworkSpawn()
    {
        DisableClientInput();

        if (IsOwner) {
            transform.position = sps[NetworkManager.Singleton.LocalClientId % 3];
            
            CheckGameStartServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void CheckGameStartServerRpc() {
        Debug.Log(NetworkManager.Singleton.ConnectedClientsList.Count);
        for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsIds.Count; i++) {
            Debug.Log(NetworkManager.Singleton.ConnectedClientsIds[i]);
        }
        // if (NetworkManager.Singleton.ConnectedClientsList.Count >= numberConnectedClientsStart) {
        //     for (int i = 0; i < NetworkManager.Singleton.ConnectedClients.Count; i++) {
        //         NetworkManager.Singleton.ConnectedClientsList[i].PlayerObject.GetComponent<NetworkPlayer>().StartGameServerRpc();
        //     }
        //     StartGameServerRpc();
        // }
    }

    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc() {
        Debug.Log("StartGame!");
        StartGameClientRpc();
    }

    [ClientRpc]
    private void StartGameClientRpc() {
        transform.position = sps_start[NetworkManager.Singleton.LocalClientId % 3];
    }

    public void DisableClientInput() {
        if (IsClient && !IsOwner) {
            NetworkMoveProvider clientMoveProvider = GetComponent<NetworkMoveProvider>();
            var clientControllers = GetComponentsInChildren<ActionBasedController>();
            var clientTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
            var clientHead = GetComponentInChildren<TrackedPoseDriver>();
            var clientCamera = GetComponentInChildren<Camera>();

            clientCamera.enabled = false;
            clientMoveProvider.enableInputActions = false;
            clientTurnProvider.enableTurnLeftRight = false;
            clientTurnProvider.enableTurnAround = false;
            clientHead.enabled = false;

            foreach (var controller in clientControllers) {
                controller.enableInputActions = false;
                controller.enableInputTracking = false;
            }
        }

    }

    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs) {
        LogServerRpc("This happened");
        if (IsClient && IsOwner) {
            NetworkObject networkObjectSelected = eventArgs.interactableObject.transform
                .GetComponent<NetworkObject>();
            if (networkObjectSelected != null) {
                RequestGrabbableOwnershipServerRpc(OwnerClientId, networkObjectSelected);
            }
        }
    }

    [ServerRpc]
    public void RequestGrabbableOwnershipServerRpc(ulong newOwnerClientId, NetworkObjectReference networkObjectReference) {
        if (networkObjectReference.TryGet(out NetworkObject networkObject)) {
            networkObject.ChangeOwnership(newOwnerClientId);
        } else {
            Debug.LogWarning($"Unable to change ownership for clientId {newOwnerClientId}");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void LogServerRpc(string message) {
        Debug.Log(message);
    }

    public void LoadFinalScene() {
        TransportfinalsceneServerRpc(NetworkManager.Singleton.LocalClientId);

        GetComponent<NetworkMoveProvider>().enableInputActions = false;
    }

    [ServerRpc(RequireOwnership = false)]
    private void TransportfinalsceneServerRpc(ulong id) {
        TransportfinalsceneClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong>() { id }}});
    }

    [ClientRpc]
    private void TransportfinalsceneClientRpc(ClientRpcParams crp) {
        transform.position = finalLocations[NetworkManager.Singleton.LocalClientId % 3];
    }

    public void RestartGame() {
        SceneManager.LoadScene("VRTestScene");
    }
}
