using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelButton : MonoBehaviour
{
    public geartypes type;
    public GameObject msgo;
    private GeneralMechenicalScript ms;

    void Start() {
        ms = msgo.GetComponent<GeneralMechenicalScript>();
    }

    public void OnMouseDown() {
        ms.addGear(type);
    }
}
