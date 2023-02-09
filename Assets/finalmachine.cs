using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalmachine : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballSpawnLocation;

    public GameObject sball;
    public GameObject sballSpawnLocation;
    public GameObject grabber;

    public void StartMachine() {
        Instantiate(ball, ballSpawnLocation.transform.position, ballSpawnLocation.transform.rotation);
        Instantiate(sball, sballSpawnLocation.transform.position, sballSpawnLocation.transform.rotation);

        grabber.GetComponent<Grabber>().StartGrabber();
    }
}
