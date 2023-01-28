using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BallSpawn : NetworkBehaviour
{
    public GameObject ballPrefab;
    private GameObject ballInstance = null;

    public void SpawnBall() {
        if (ballInstance == null) {
            ballInstance = Instantiate(ballPrefab, transform.position, transform.rotation, transform);
            ballInstance.GetComponent<NetworkObject>().Spawn();
        }
    }

    public void DeleteBall() {
        if (ballInstance != null) {
            Destroy(ballInstance);
            ballInstance = null;
        }
    }
}
