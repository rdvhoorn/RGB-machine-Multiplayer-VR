using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlank : MonoBehaviour
{
    public GameObject connectedGear;
    private Rigidbody connectedGearRb;
    public Rigidbody plankRb;

    private Quaternion previousRotation;


    // Start is called before the first frame update
    void Start()
    {
        connectedGearRb = connectedGear.GetComponent<Rigidbody>();
        previousRotation = plankRb.rotation;
        connectedGear.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion newRotation = plankRb.rotation;

        // connectedGearRb.MoveRotation(newRotation);

        // previousRotation = newRotation;
    }

    void FixedUpdate() {
        Quaternion newRotation = plankRb.rotation;

        connectedGearRb.transform.rotation = newRotation;
        // connectedGearRb.AddRelativeTorque(Vector3.up, ForceMode.VelocityChange);

        previousRotation = newRotation;
    }
}
