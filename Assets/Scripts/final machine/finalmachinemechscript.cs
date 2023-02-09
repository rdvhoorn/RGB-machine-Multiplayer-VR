using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalmachinemechscript : MonoBehaviour
{
    public List<GameObject> wheels = new List<GameObject>();
    public GameObject startingBin;
    public GameObject finalInstallationInstance;


    public float rotationSpeed;

    private bool startRotation = false;
    private List<float> rotationSpeeds = new List<float>();


    public void StartRotation() {
        calculateRotatingSpeeds();

        startRotation = true;
    }

    void calculateRotatingSpeeds() {
        rotationSpeeds = new List<float>();

        float current_speed = rotationSpeed;

        for (int i = 0; i < wheels.Count; i++) {
            rotationSpeeds.Add(current_speed);

            float ratio = wheels[i].GetComponent<wheel>().ratio;
            current_speed = ratio * current_speed;
        }

        rotationSpeeds.Add(current_speed);
    }

    void Update() {
        if (!startRotation) return;

        startingBin.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);

        if (startingBin.transform.eulerAngles.z < 25) {
            startRotation = false;
        }

        for (int i = 0; i < wheels.Count; i++) {
            bool rotation = i%2==1;
            wheels[i].GetComponent<wheel>().rotateWheel(rotationSpeeds[i], rotation);
        }

        if (finalInstallationInstance != null) {
            finalInstallationInstance.GetComponent<finalinstallation>().Rotate(rotationSpeeds[wheels.Count]);
        }
    }


}
