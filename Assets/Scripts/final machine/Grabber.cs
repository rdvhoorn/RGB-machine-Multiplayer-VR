using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Grabber : MonoBehaviour
{

    public float upwardsSpeed = 0.2f;
    public float rotationalSpeed = 20f;


    public GameObject leftHandle;
    public GameObject rightHanlde;


    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool GrabberOpen = true;
    private bool GrabberStatic = true;
    private float GrabberSpeed = 5f;
    private int[] parameters;
    private int stage = 0;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);;
        parameters = new int[2]{3, 6};
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrabberServerRpc();
    }

 

    private double startTime = 0;
    private bool saved = false;

    // update Grabber Movement (server side )
    [ServerRpc(RequireOwnership = false)]
    void UpdateGrabberServerRpc() {

        if (!saved) {
            startTime = Time.realtimeSinceStartupAsDouble;
            saved = true;
        }

        if (stage == 0)
            if (transform.position.y < parameters[0]) {
                transform.position += Vector3.up * upwardsSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime);   
                return;  
            } else {
                stage = 1;
            }

        if (stage == 1) {
            if (leftHandle.transform.localRotation.y * 90 < 16) {
                leftHandle.transform.Rotate(Vector3.up * GrabberSpeed * Time.deltaTime);
                rightHanlde.transform.Rotate(Vector3.down * GrabberSpeed * Time.deltaTime);
                return;
            } else {
                stage = 2;
            }
        }

        if (stage == 2) {
            if (transform.position.y < parameters[1]) {
                transform.position += Vector3.up * upwardsSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime);
                return;
            } else {
                stage = 3;
            }
        }

        if (stage == 3) {
            if (leftHandle.transform.localRotation.y > 0) {
                leftHandle.transform.Rotate(Vector3.down * GrabberSpeed * Time.deltaTime);
                rightHanlde.transform.Rotate(Vector3.up * GrabberSpeed * Time.deltaTime);
                return;
            }
        }
    }
}
