using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Type {Server, Host, Client};

public class SceneSwapAndNetworkLoad : MonoBehaviour
{
    public static Type playerType;

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadClient() {
        playerType = Type.Client;
        LoadScene("VRMultiPlayerTestScene");
    }

    public void LoadHost() {
        playerType = Type.Host;
        LoadScene("VRMultiPlayerTestScene");
    }

    public void LoadServer() {
        playerType = Type.Server;
        LoadScene("VRMultiPlayerTestScene");
    }
}
