using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Color onColor = Color.green;
    public Color offColor = Color.red;
    public Color disabledColor = Color.gray;

    public bool wire_is_enabled = true;
    

    private bool state = false;
    private Renderer[] meshRenderers;

    void Start() {
        meshRenderers = gameObject.GetComponentsInChildren<Renderer>();

        updateColor();
    }

    public bool getState() {
        return state;
    }

    public void setState(bool newState) {
        state = newState;

        updateColor();
    }

    private void updateColor() {
        if (wire_is_enabled == false) {
            foreach (Renderer r in meshRenderers) {
                r.material.color = disabledColor;
            }
        } else {
            if (state) {
                foreach (Renderer r in meshRenderers) {
                    r.material.color = onColor;
                }
            } else {
                foreach (Renderer r in meshRenderers) {
                    r.material.color = offColor;
                }
            }
        }
    }

    public void disableWire() {
        wire_is_enabled = false;

        updateColor();
    }

    public void enableWire() {
        wire_is_enabled = true;

        updateColor();
    }
}
