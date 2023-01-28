using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeClick : MonoBehaviour
{
    private SoftwareComponent sc;
    public string showText;
    public string value;

    void Start() {
        sc = GetComponentInParent<SoftwareComponent>();
    }

    public void OnMouseDown() {
        sc.appendCodeToSelected(gameObject);
    }
}
