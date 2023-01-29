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


    private Dictionary<int, string>[] correct_configs = new Dictionary<int, string>[4] {
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "current_height"}, {22, "<=,<"}, {23, "3"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, "current_height"}, {62, "<=,<"}, {63, "6"},
            {7, "rotate_arm_up"},
            {81, "current_height"}, {82, "="}, {83, "get_arm_height"}
        },
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "3"}, {22, "=>,>"}, {23, "current_height"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, "6"}, {62, "=>,>"}, {63, "current_height"},
            {7, "rotate_arm_up"},
            {81, "current_height"}, {82, "="}, {83, "get_arm_height"}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "get_arm_height"}, {22, "<=,<"}, {23, "3"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, "get_arm_height"}, {62, "<=,<"}, {63, "6"},
            {7, "rotate_arm_up"},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "3"}, {22, "=>,>"}, {23, "get_arm_height"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, "6"}, {62, "=>,>"}, {63, "get_arm_height"},
            {7, "rotate_arm_up"},
            {81, null}, {82, null}, {83, null}
        },
    };

    private Dictionary<int, string>[] plausable_configs = new Dictionary<int, string>[8] {
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "current_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, "current_height"}, {62, "<=,<"}, {63, "1,2,3,4,5,6"},
            {7, "rotate_arm_up"},
            {81, "current_height"}, {82, "="}, {83, "get_arm_height"}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "get_arm_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, "get_arm_height"}, {62, "<=,<"}, {63, "1,2,3,4,5,6"},
            {7, "rotate_arm_up"},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "current_height"}, {22, "=>,>"}, {23, "1,2,3,4,5,6"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, "current_height"}, {62, "=>,>"}, {63, "1,2,3,4,5,6"},
            {7, "rotate_arm_up"},
            {81, "current_height"}, {82, "="}, {83, "get_arm_height"}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "1,2,3,4,5,6"}, {22, "=>,>"}, {23, "get_arm_height"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, "1,2,3,4,5,6"}, {62, "=>,>"}, {63, "get_arm_height"},
            {7, "rotate_arm_up"},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "current_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, null}, {62, null}, {63, null},
            {7, null},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "get_arm_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, null}, {62, null}, {63, null},
            {7, null},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "1,2,3,4,5,6"}, {22, "=>,>"}, {23, "current_height"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, null}, {62, null}, {63, null},
            {7, null},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "1,2,3,4,5,6"}, {22, "=>,>"}, {23, "get_current_height"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, null}, {62, null}, {63, null},
            {7, null},
            {81, null}, {82, null}, {83, null}
        },
    };

    public SoftwareState CalculateSoftwareState() {
        // Check correct configs
        for (int c = 0; c < correct_configs.Length; c++) {
            Dictionary<int, string> temp_config = correct_configs[c];
            bool current_correct = true;

            for (int i = 0; i < inputFieldScripts.Length; i++) {
                bool isCorrect = isConfigSettingCorrect(inputFieldScripts[i], temp_config[inputFieldScripts[i].id]);

                if (!isCorrect) {
                    current_correct = false;
                    break;
                }
            }

            if (current_correct) {
                current_parameters = new int[2]{3,6};
                return SoftwareState.CORRECT;
            }
        }

        for (int c = 0; c < plausable_configs.Length; c++) {
            Dictionary<int, string> temp_configs = plausable_configs[c];
            bool current_correct = true;
            
            for (int i = 0; i < inputFieldScripts.Length; i++) {
                bool isCorrect = isConfigSettingCorrect(inputFieldScripts[i], temp_configs[inputFieldScripts[i].id]);

                if (!isCorrect) {
                    current_correct = false;
                    break;
                }
            }

            if (current_correct) {
                current_parameters = new int[2]{-1, -1};
                return SoftwareState.PLAUSIBLE;
            }
        }

        return SoftwareState.WRONG;
    }

    public int[] GetCurrentSoftwareParameters() {
        return current_parameters;
    }

    private bool isConfigSettingCorrect(CodeBlockSelect codeblockselect, string config_value) {
        if (codeblockselect.codeBlock == null) {
            return config_value == null;
        }

        if (config_value == null) {
            return false;
        }

        // Both config value and codeblock have values.
        // First split the config value into all possible values, and get the entered value.
        string[] possible_values = config_value.Split(char.Parse(","));
        string entered_value = codeblockselect.codeBlock.GetComponent<CodeClick>().value;

        // Check if any of the config values match with the entered value.
        for (int i = 0; i < possible_values.Length; i++) {
            if (possible_values[i] == entered_value) {
                return true;
            }
        }

        return false;
    }
}
