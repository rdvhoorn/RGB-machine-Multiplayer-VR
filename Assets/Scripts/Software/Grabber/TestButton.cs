using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestButton : NetworkBehaviour
{
    public GameObject Grabber;

    public void OnMouseDown() {
        Grabber.GetComponent<MoveGrabber>().StartGrabberMovement();
    }
}
