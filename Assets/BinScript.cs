using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinScript : MonoBehaviour
{
    public GameObject msgo;

    private GeneralMechenicalScript ms;

    void Start() {
        ms = msgo.GetComponent<GeneralMechenicalScript>();
    }

    void OnCollisionEnter(Collision other) {
        ms.StartRotation();
    }
}
