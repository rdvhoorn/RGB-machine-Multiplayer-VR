using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startontouchbase : MonoBehaviour
{
    public GameObject fmgo;
    private finalmachine fm;

    private bool started = false;

    void Start() {
        fm = fmgo.GetComponent<finalmachine>();
    }

    void OnCollisionEnter(Collision c) {
        if (started) return;

        started = true;
        fm.StartMachine();
    }
}
