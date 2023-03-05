using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Net;
using System.Net.Sockets;

public enum NetworkType {Server, Host, Client};

public class NetworkStartup : MonoBehaviour
{
    [SerializeField]
    private NetworkManager NetworkManager;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneSwapAndNetworkLoad.playerType == Type.Server) {
            NetworkManager.StartServer();
            Debug.Log(GetLocalIPAddress());
        } if (SceneSwapAndNetworkLoad.playerType == Type.Host) {
            NetworkManager.StartHost();
        } else if (SceneSwapAndNetworkLoad.playerType == Type.Client) {
            NetworkManager.StartClient();
        }
    }

    public string GetLocalIPAddress() {
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList) {
			if (ip.AddressFamily == AddressFamily.InterNetwork) {
				return ip.ToString();
			}
		}
		throw new System.Exception("No network adapters with an IPv4 address in the system!");
	}
}
