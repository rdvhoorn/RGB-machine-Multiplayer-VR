using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleStringSpawnExplanation : MonoBehaviour
{
    public GameObject explanationPrefab;
    public string text; 

    public void SpawnExplanationPopup(Vector3 position, Quaternion rotation, string text) {
        GameObject explanationGo = Instantiate(explanationPrefab, position, rotation);
        explanationGo.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
