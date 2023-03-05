using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireworksExploder : MonoBehaviour
{
    public GameObject fireworks;
    public int secondsDelayUntilMainMenu;

    void Start() {
        fireworks.SetActive(false);
    }

    void OnCollisionEnter(Collision other) {
        fireworks.SetActive(true);
        StartCoroutine(backToMainMenu(secondsDelayUntilMainMenu));
    }

    IEnumerator backToMainMenu(int seconds) {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("StartMenu");
    }

    public void OnMouseDown() {
        StartCoroutine(backToMainMenu(1));
    }
}
