using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplanation : MonoBehaviour
{
    public GameObject explanationPrefab;

    public void OnMouseDown() {
        Instantiate(explanationPrefab);
    }
}
