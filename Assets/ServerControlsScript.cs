using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ServerControlsScript : NetworkBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (!IsServer) return;

        // Move players to start locations in the game
        if (Input.GetKeyDown(KeyCode.S)) {
            NetworkManager.Singleton.ConnectedClientsList[0].PlayerObject.GetComponent<NetworkPlayer>().StartGameServerRpc();
        }

        // Move all players to final scene
        if (Input.GetKeyDown(KeyCode.F)) {
            foreach (NetworkClient player in NetworkManager.Singleton.ConnectedClientsList) {
                player.PlayerObject.GetComponent<NetworkPlayer>().LoadFinalScene();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            foreach (NetworkClient player in NetworkManager.Singleton.ConnectedClientsList) {
                player.PlayerObject.GetComponent<NetworkPlayer>().RestartGame();
            }
        }
    }
}
