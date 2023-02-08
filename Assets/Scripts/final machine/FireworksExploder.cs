using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireworksExploder : MonoBehaviour
{
    public GameObject fireworks;

    void Start() {
        fireworks.SetActive(false);
    }

    void OnCollisionEnter(Collision other) {
        fireworks.SetActive(true);

        StartCoroutine(switchToFinalScene());
    }

    IEnumerator switchToFinalScene() {
        yield return new WaitForSeconds(15);

        SceneManager.LoadScene("VRTestScene");
    }
}
