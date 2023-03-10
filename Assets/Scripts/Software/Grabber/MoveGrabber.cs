using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrabber : MonoBehaviour
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
    private float GrabberSpeed = 5f;
    private int[] parameters;
    private int stage = 0;
    private SpawnExplanation se;

    private SoftwareComponent softwareComponent;
    public GameObject lastexplanation;
    


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
        UpdateGrabber();
    }

    // Start grabber movement
    public void StartGrabberMovement() {
        ResetGrabberMovement();

        SoftwareState state = softwareComponent.CalculateSoftwareState();

        if (state == SoftwareState.CORRECT || state == SoftwareState.PLAUSIBLE) {
            parameters = softwareComponent.GetCurrentSoftwareParameters();

            Test(parameters);
        } else {
            string explanation = softwareComponent.getDebuggerText();
            se.SpawnExplanationWithCustomText(PopupLocation.transform.position, PopupLocation.transform.rotation, explanation);
        }
    }

    void Test(int[] param) {
        ballSpawnLocation.GetComponent<BallSpawn>().DeleteBall();
        started = true;
        parameters = param;

    }

    // Reset Grabber
    public void ResetGrabberMovement() {
        transform.position = startPosition;
        transform.rotation = startRotation;
        stage = 0;

        started = false;

        spawnFinalExplanation();
    }

    private void spawnFinalExplanation() {
        if (parameters[0] == 3 && parameters[1] == 6) {
            // GO TO FINAL MACHINE
            lastexplanation.transform.position = PopupLocation.transform.position;
            lastexplanation.transform.rotation = PopupLocation.transform.rotation;
        }
    }

    private double startTime = 0;
    private bool saved = false;

    // update Grabber Movement (server side )
    void UpdateGrabber() {
        if (!started) return;

        if (!saved) {
            startTime = Time.realtimeSinceStartupAsDouble;
            saved = true;
        }

        if (stage == 0)
            if (transform.localPosition.y < parameters[0]) {
                transform.localPosition += Vector3.up * upwardsSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime);   
                return;  
            } else {
                stage = 1;
            }

        if (stage == 1) {
            if (leftHandle.transform.localRotation.y * 90 > 12) {
                ballSpawnLocation.GetComponent<BallSpawn>().SpawnBall();
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
            if (transform.localPosition.y < parameters[1]) {
                transform.localPosition += Vector3.up * upwardsSpeed * Time.deltaTime;
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
