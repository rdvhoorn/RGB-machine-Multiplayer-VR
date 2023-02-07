using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum GateTypes {AND, OR, XOR, NAND, NOT, NONE};

public class Electrical : NetworkBehaviour
{
    private bool initialized = false;
    private Gate[] standardGates;

    private GateSlot currentSelectedSlot = null;
    private GameObject[] instantiatedGates = new GameObject[6];
    private GateSlot[] allSlots;

    public GameObject AndGatePrefab;
    public GameObject OrGatePrefab;
    public GameObject XorGatePrefab;
    public GameObject NandGatePrefab;

    public GameObject lastwire;
    public GameObject finalExplanation;
    public GameObject spawnPosition;

    void Start() {
        standardGates = GetComponentsInChildren<Gate>();
        allSlots = GetComponentsInChildren<GateSlot>();

        for (int i = 0; i < instantiatedGates.Length; i++) {
            instantiatedGates[i] = null;
        }
    }

    void Update() {
        if (initialized) return;

        update_gates();
        initialized = true;
    }

    public void updateWires() {
        foreach (GateSlot slot in allSlots) {
            if (slot.inputWires[0].GetComponent<Wire>().wire_is_enabled && slot.inputWires[1].GetComponent<Wire>().wire_is_enabled && instantiatedGates[slot.ID] != null) {
                slot.outputWires[0].GetComponent<Wire>().enableWire();
            }
        }

        foreach (Gate gate in standardGates) {
            if (gate.InputWires.Count == 1) {
                if (gate.InputWires[0].GetComponent<Wire>().wire_is_enabled) {
                    gate.OutputWires[0].GetComponent<Wire>().enableWire();
                }
            } else {
                if (gate.InputWires[0].GetComponent<Wire>().wire_is_enabled && gate.InputWires[1].GetComponent<Wire>().wire_is_enabled) {
                    gate.OutputWires[0].GetComponent<Wire>().enableWire();
                }
            }
        }
    }

    public void update_gates() {
        updateWires();

        for (int i = 0; i < 3; i++) {
            foreach (Gate gate in standardGates) {
                gate.gate_update();
            }

            foreach (GameObject gateGameObject in instantiatedGates) {
                if (gateGameObject != null) {
                    gateGameObject.GetComponentInChildren<Gate>().gate_update();
                }
            }
        }

        if (lastwire.GetComponent<Wire>().getState() && lastwire.GetComponent<Wire>().wire_is_enabled) {
            finalExplanation.transform.position = spawnPosition.transform.position;
        }
    }

    public void clickedSlot(GateSlot clickedSlot) {
        bool currentlySelected = clickedSlot.getSelected();

        foreach (GateSlot slot in allSlots) {
            slot.deactivate();
            
        }

        for (int i = 0; i < allSlots.Length; i++) {
            allSlots[i].deactivate();
            if (instantiatedGates[i] != null) {
                instantiatedGates[i].GetComponentInChildren<Gate>().deactivate();
            }
        }

        if (!currentlySelected) {
            clickedSlot.activate();
            currentSelectedSlot = clickedSlot;
            if (instantiatedGates[clickedSlot.ID] != null) {
                instantiatedGates[clickedSlot.ID].GetComponentInChildren<Gate>().activate();
            }

        } else {
            currentSelectedSlot = null;
        }
    }

    public void clickedSelectableGate(GateTypes type) {
        if (currentSelectedSlot == null) return;

        GameObject currentSpawnedObject = instantiatedGates[currentSelectedSlot.ID];
        Destroy(currentSpawnedObject);

        GameObject gateGameObject = Instantiate(GetPrefab(type), currentSelectedSlot.transform.position + new Vector3(0, 0, -0.3f), currentSelectedSlot.transform.rotation, gameObject.transform);

        Gate gate = gateGameObject.GetComponentInChildren<Gate>();
        gate.SetElectrical(gameObject.GetComponent<Electrical>());
        gate.selectable = true;
        gate.SetWires(currentSelectedSlot.inputWires, currentSelectedSlot.outputWires);
        gate.SetGateSlot(currentSelectedSlot);
        instantiatedGates[currentSelectedSlot.ID] = gateGameObject;

        update_gates();
    }

    private GameObject GetPrefab(GateTypes type) {
        if (type == GateTypes.AND) {
            return AndGatePrefab;
        } else if (type == GateTypes.OR) {
            return OrGatePrefab;
        } else if (type == GateTypes.XOR) {
            return XorGatePrefab;
        } else {
            return NandGatePrefab;
        }
    } 
}
