using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        DisableClientInput();
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

    private void Start() {
        if (IsClient && IsOwner) {
            LogServerRpc("StartnetworkPlayer!");
            transform.position = new Vector3(0, transform.position.y, 0);
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

    [ServerRpc]
    public void LogServerRpc(string message) {
        Debug.Log(message);
    }
}
