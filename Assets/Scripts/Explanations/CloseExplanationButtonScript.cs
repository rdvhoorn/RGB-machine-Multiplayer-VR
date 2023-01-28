using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseExplanationButtonScript : MonoBehaviour
{
    public GameObject explanationCanvas;

    public void OnPressClose() {
        Destroy(explanationCanvas);
    }
}
