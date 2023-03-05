using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadElectrical() {
        LoadScene("ElectricalScene");
    }

    public void LoadMechenical() {
        LoadScene("MechenicalScene");
    }

    public void LoadSoftware() {
        LoadScene("SoftwareScene");
    }
}
