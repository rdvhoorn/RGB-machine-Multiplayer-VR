using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public enum SoftwareState {CORRECT, PLAUSIBLE, WRONG};

public class SoftwareComponent : NetworkBehaviour
{
    private GameObject selected = null;
    public GameObject[] buttonGroups;

    private CodeBlockSelect[] inputFieldScripts;
    private int[] current_parameters = null;

    void Start() {
        inputFieldScripts = GetComponentsInChildren<CodeBlockSelect>();
    }

    public void appendCodeToSelected(GameObject newCode) {
        if (selected == null) return;
        
        selected.GetComponent<CodeBlockSelect>().SetCode(newCode);
    }

    public void newSelected(GameObject newlySelectedObject) {
        foreach (GameObject obj in buttonGroups) {
                obj.GetComponent<ButtonGroup>().deactivate();
            }

        if (selected != null) {
            selected.GetComponent<CodeBlockSelect>().Deselect();

            foreach (GameObject obj in selected.GetComponent<CodeBlockSelect>().AllowedButtonGroups) {
                obj.GetComponent<ButtonGroup>().deactivate();
            }
        }

        if (newlySelectedObject == selected) {
            selected = null;
            newlySelectedObject.GetComponent<CodeBlockSelect>().Deselect();

            foreach (GameObject obj in buttonGroups) {
                obj.GetComponent<ButtonGroup>().activate();
            }

            return;
        }

        newlySelectedObject.GetComponent<CodeBlockSelect>().Select();
        foreach (GameObject obj in newlySelectedObject.GetComponent<CodeBlockSelect>().AllowedButtonGroups) {
                obj.GetComponent<ButtonGroup>().activate();
            }
        selected = newlySelectedObject;
    }


    private Dictionary<int, string>[] correct_configs = new Dictionary<int, string>[2] {
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "current_height"}, {22, "<="}, {23, "3"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, "current_height"}, {62, "<="}, {63, "6"},
            {7, "rotate_arm_up"},
            {81, "current_height"}, {82, "="}, {83, "get_arm_height"}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "get_arm_height"}, {22, "<="}, {23, "3"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, "get_arm_height"}, {62, "<="}, {63, "6"},
            {7, "rotate_arm_up"},
            {81, null}, {82, null}, {83, null}
        },
    };

    private Dictionary<int, string>[] plausable_configs = new Dictionary<int, string>[1] {
        new Dictionary<int, string>{

        }
    };

    public SoftwareState CalculateSoftwareState() {
        // Check correct configs
        for (int c = 0; c < correct_configs.Length; c++) {
            Dictionary<int, string> temp_config = correct_configs[c];
            bool current_correct = true;

            for (int i = 0; i < inputFieldScripts.Length; i++) {
                Debug.Log(temp_config[inputFieldScripts[i].id]);
                if (inputFieldScripts[i].codeBlock == null) {
                    Debug.Log("null");
                    if (temp_config[inputFieldScripts[i].id] != null) {
                        current_correct = false;
                        break;
                    }
                } else if (inputFieldScripts[i].codeBlock.GetComponent<CodeClick>().value != temp_config[inputFieldScripts[i].id]) {
                    Debug.Log("Not correct");
                    current_correct = false;
                    break;
                }
            }

            if (current_correct) {
                current_parameters = new int[2]{3,6};
                return SoftwareState.CORRECT;
            }
        }

        return SoftwareState.WRONG;
    }

    public int[] GetCurrentSoftwareParameters() {
        return current_parameters;
    }
}
