using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnExplanation : MonoBehaviour
{
    public GameObject explanationPrefab;
    public GameObject spawnLocation;

    private GameObject instance;

    public void SpawnExplanationPopup(Vector3 position, Quaternion rotation) {
        instance = Instantiate(explanationPrefab, position, rotation);
        // explanationGo.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void ClickOpen() {
        SpawnExplanationPopup(spawnLocation.transform.position, spawnLocation.transform.rotation);
    }

    public void CloseOnRelease() {
        Destroy(instance);
    }
}
