using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSlot : MonoBehaviour
{

    public Color baseColor = new Color(0, 0.4941176f, 0.654902f, 1);
    public Color selectedColor = new Color(0.5019608f, 0.8078431f, 0.8431372f, 1);
    [Range(0, 4)]
    public int ID;

    public List<GameObject> inputWires;
    public List<GameObject> outputWires;

    private Renderer blockRenderer;

    private bool selected = false;


    // Start is called before the first frame update
    void Start()
    {
        blockRenderer = GetComponentInChildren<Renderer>();
        
        updateColor();
    }

    public bool getSelected() {
        return selected;
    }

    public void toggle() {
        selected = !selected;

        updateColor();
    }

    public void activate() {
        selected = true;
        updateColor();
    }

    public void deactivate() {
        selected = false;
        updateColor();
    }

    public void updateColor() {
        if (selected) {
            // blockRenderer.material.SetColor("_Color", selectedColor);
            blockRenderer.material.color = selectedColor;
        } else {
            // blockRenderer.material.SetColor("_Color", baseColor);
            blockRenderer.material.color = baseColor;
        }
    }   
}
