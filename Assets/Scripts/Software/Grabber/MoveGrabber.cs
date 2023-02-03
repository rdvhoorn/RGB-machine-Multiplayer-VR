using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveGrabber : NetworkBehaviour
{

    public float upwardsSpeed = 0.2f;
    public float rotationalSpeed = 20f;
    public GameObject software;

    public GameObject ballSpawnLocation;

    public GameObject leftHandle;
    public GameObject rightHanlde;
    public GameObject PopupLocation;


    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool started = false;
    private bool GrabberOpen = true;
    private bool GrabberStatic = true;
    private float GrabberSpeed = 5f;
    private int[] parameters;
    private int stage = 0;
    private SpawnExplanation se;

    private SoftwareComponent softwareComponent;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        softwareComponent = software.GetComponent<SoftwareComponent>();
        parameters = new int[2]{3, 5};
        se = GetComponent<SpawnExplanation>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrabberServerRpc();
    }

    // Start grabber movement
    public void StartGrabberMovement() {
        ResetGrabberMovement();

        // started = true;
        // TestServerRpc(new int[2]{3, 6});

        SoftwareState state = softwareComponent.CalculateSoftwareState();

        if (state == SoftwareState.CORRECT || state == SoftwareState.PLAUSIBLE) {
            int[] parameters = softwareComponent.GetCurrentSoftwareParameters();

            TestServerRpc(parameters);
        } else {
            se.SpawnExplanationPopup(PopupLocation.transform.position, PopupLocation.transform.rotation);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void TestServerRpc(int[] param) {
        ballSpawnLocation.GetComponent<BallSpawn>().DeleteBall();
        started = true;
        parameters = param;

    }

    [ServerRpc(RequireOwnership = false)]
    void ToggleGrabberHandleServerRpc() {
        GrabberOpen = !GrabberOpen;
        GrabberStatic = false;
    }

    // Reset Grabber
    public void ResetGrabberMovement() {
        ResetGrabberMovementServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ResetGrabberMovementServerRpc() {
        transform.position = startPosition;
        transform.rotation = startRotation;
        stage = 0;

        ToggleGrabberHandleServerRpc();
        started = false;
    }

    private double startTime = 0;
    private bool saved = false;

    // update Grabber Movement (server side )
    [ServerRpc(RequireOwnership = false)]
    void UpdateGrabberServerRpc() {
        if (!started) return;

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
            Debug.Log(leftHandle.transform.localRotation.y);

            if (leftHandle.transform.localRotation.y * 90 > 12) {
                ballSpawnLocation.GetComponent<BallSpawn>().SpawnBall();
                Debug.Log(Time.realtimeSinceStartupAsDouble);
            }
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
            } else {
                started = false;
            }
        }

        ResetGrabberMovement();
    }
}
