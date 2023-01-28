using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeClick : MonoBehaviour
{
    private SoftwareComponent sc;
    public string showText;

    void Start() {
        sc = GetComponentInParent<SoftwareComponent>();
    }

    void OnMouseDown() {
        sc.appendCodeToSelected(gameObject);
    }
}
