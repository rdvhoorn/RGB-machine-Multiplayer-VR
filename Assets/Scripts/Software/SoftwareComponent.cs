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
            {21, "current_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6,7,8,9"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, "current_height"}, {62, "<=,<"}, {63, "1,2,3,4,5,6,7,8,9"},
            {7, "rotate_arm_up"},
            {81, "current_height"}, {82, "="}, {83, "get_arm_height"}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "get_arm_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6,7,8,9"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, "get_arm_height"}, {62, "<=,<"}, {63, "1,2,3,4,5,6,7,8,9"},
            {7, "rotate_arm_up"},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "current_height"}, {22, "=>,>"}, {23, "1,2,3,4,5,6,7,8,9"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, "current_height"}, {62, "=>,>"}, {63, "1,2,3,4,5,6,7,8,9"},
            {7, "rotate_arm_up"},
            {81, "current_height"}, {82, "="}, {83, "get_arm_height"}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "1,2,3,4,5,6,7,8,9"}, {22, "=>,>"}, {23, "get_arm_height"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, "1,2,3,4,5,6,7,8,9"}, {62, "=>,>"}, {63, "get_arm_height"},
            {7, "rotate_arm_up"},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "current_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6,7,8,9"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, null}, {62, null}, {63, null},
            {7, null},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "get_arm_height"}, {22, "<=,<"}, {23, "1,2,3,4,5,6,7,8,9"},
            {3, "rotate_arm_up"},
            {41, null}, {42, null}, {43, null},

            {61, null}, {62, null}, {63, null},
            {7, null},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, "get_arm_height"},
            {21, "1,2,3,4,5,6,7,8,9"}, {22, "=>,>"}, {23, "current_height"},
            {3, "rotate_arm_up"},
            {41, "current_height"}, {42, "="}, {43, "get_arm_height"},

            {61, null}, {62, null}, {63, null},
            {7, null},
            {81, null}, {82, null}, {83, null}
        },
        new Dictionary<int, string>{
            {1, null},
            {21, "1,2,3,4,5,6,7,8,9"}, {22, "=>,>"}, {23, "get_current_height"},
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

            int[] parameters = new int[2];
            int parameters_parsed = 0;
            
            for (int i = 0; i < inputFieldScripts.Length; i++) {
                bool isCorrect = isConfigSettingCorrect(inputFieldScripts[i], temp_configs[inputFieldScripts[i].id]);

                if (inputFieldScripts[i].codeBlock != null) {
                    string entered_value = inputFieldScripts[i].codeBlock.GetComponent<CodeClick>().value;
                    if (entered_value.Length == 1) {
                        int out_number = 0;
                        bool canConvert = int.TryParse(entered_value, out out_number);
                        if (canConvert && parameters_parsed < 2) {
                            parameters[parameters_parsed] = out_number;
                            parameters_parsed += 1;
                        }
                    }
                }

                if (!isCorrect) {
                    current_correct = false;
                    break;
                }
            }

            if (current_correct) {
                current_parameters = parameters;
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

    public string getDebuggerText() {
        CodeBlockSelect[] ordered = getInOrder();
        if (ordered[0].codeBlock != null && "123456789".Contains(ordered[0].codeBlock.GetComponent<CodeClick>().value)) {
            return "It seems like you made a tiny mistake in line 1! Instead of assigning a number to the height, try to obtain the current height through a different method.";
        }

        string emptyfieldstring = fieldsFilledInInvalidly(ordered);
        if (!emptyfieldstring.Contains("good")) {
            return emptyfieldstring;
        }
        
        string invalidvariablestirng = invalidVariable(ordered);
        if (!invalidvariablestirng.Contains("good")) {
            return invalidvariablestirng;
        }

        return "Your code does not compile! However, the compiler does not understand exactly what you did wrong! Please have a look at your code to see if you can spot any mistakes. If you cannot spot any mistakes, ask for help.";
    }

    private CodeBlockSelect[] getInOrder() {
        CodeBlockSelect[] code_blocks_in_order = new CodeBlockSelect[15];

        Dictionary<int, int> mapping = new Dictionary<int, int>{
            {1,0},
            {21,1},
            {22,2},
            {23,3},
            {3,4},
            {41,5},
            {42,6},
            {43,7},
            {61,8},
            {62,9},
            {63,10},
            {7,11},
            {81,12},
            {82,13},
            {83,14},
        };

        for (int i = 0; i < inputFieldScripts.Length; i++) {
            code_blocks_in_order[mapping[inputFieldScripts[i].id]] = inputFieldScripts[i];
        }

        return code_blocks_in_order;
    }

    private string fieldsFilledInInvalidly(CodeBlockSelect[] ordered) {
        if (ordered[1].codeBlock == null || ordered[2].codeBlock == null || ordered[3].codeBlock == null) {
            return "Make sure that you use the first while loop appropriately. It seems that there is some code missing there!";
        }

        if (ordered[8].codeBlock == null || ordered[9].codeBlock == null || ordered[10].codeBlock == null) {
            return "Make sure that you urse the second while loop appropriately. It seems that there is some code missing there!";
        }

        if (ordered[4].codeBlock == null || ordered[11].codeBlock == null) {
            return "The first line within the contents of the while loops are for statements that make the grabber go up. Make sure you use them for that!";
        }

        if ((ordered[5].codeBlock == null || ordered[6].codeBlock == null || ordered[7].codeBlock == null) && (ordered[5].codeBlock != null || ordered[6].codeBlock != null || ordered[7].codeBlock != null)) {
            return "The second line of the first while loop is only partially filled in. Hence your code is not compiling!";
        }

        if ((ordered[12].codeBlock == null || ordered[13].codeBlock == null || ordered[14].codeBlock == null) && (ordered[12].codeBlock != null || ordered[13].codeBlock != null || ordered[14].codeBlock != null)) {
            return "The second line of the second while loop is only partially filled in. Hence your code is not compiling!";
        }

        return "good";
    }

    private string invalidVariable(CodeBlockSelect[] ordered) { 
        if (ordered[5].codeBlock != null) {
            if ("123456789".Contains(ordered[5].codeBlock.GetComponent<CodeClick>().value)) {
                return "Syntax error in line 4: cannot assign value to an integer.";
            } else if ("rotate_arm_upget_arm_height".Contains(ordered[5].codeBlock.GetComponent<CodeClick>().value)) {
                return "Syntax error in line 4: cannot assign value to function.";
            }
        }

        if (ordered[12].codeBlock != null) {
            if ("123456789".Contains(ordered[12].codeBlock.GetComponent<CodeClick>().value)) {
                return "Syntax error in line 8: cannot assign value to an integer.";
            } else if ("rotate_arm_upget_arm_height".Contains(ordered[12].codeBlock.GetComponent<CodeClick>().value)) {
                return "Syntax error in line 8: cannot assign value to function.";
            }
        }
        

        return "good";
    }
}
