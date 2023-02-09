using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalmachinebinscript : MonoBehaviour
{
    public GameObject msgo;

    private finalmachinemechscript ms;

    void Start() {
        ms = msgo.GetComponent<finalmachinemechscript>();
    }

    void OnCollisionEnter(Collision other) {
        ms.StartRotation();
    }
}
