using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum geartypes {oneone, onetwo, onethree, onefour};

public class GeneralMechenicalScript : MonoBehaviour
{
    public List<GameObject> wheels = new List<GameObject>();
    public GameObject startingBin;
    public GameObject[] prefabs;

    public GameObject ballPrefab;
    public GameObject ballSpawnPosition;
    private GameObject ballInstance;
    public GameObject finalInstallationPrefab;
    private GameObject finalInstallationInstance = null;


    public float rotationSpeed;

    private bool startRotation = false;
    private List<float> rotationSpeeds = new List<float>();

    private Vector3 nextSpawnPosition;

    public GameObject finalPopupGo;
    public GameObject finalPopup;


    void Start() {
        nextSpawnPosition = wheels[0].transform.position;
        nextSpawnPosition.x -= 1;
        nextSpawnPosition.z -= 0.2f;
    }

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

            if (rotationSpeeds[wheels.Count] == 720) {
                finalPopup.transform.position = finalPopupGo.transform.position;
            }
        }

        for (int i = 0; i < wheels.Count; i++) {
            bool rotation = i%2==1;
            wheels[i].GetComponent<wheel>().rotateWheel(rotationSpeeds[i], rotation);
        }

        if (finalInstallationInstance != null) {
            finalInstallationInstance.GetComponent<finalinstallation>().Rotate(rotationSpeeds[wheels.Count]);
        }
    }

    public void addGear(geartypes type) {
        if (wheels.Count >= 5) return;

        if (startRotation) return;

        int index = -1;

        if (type == geartypes.oneone) {
            index = 0;
        } else if (type == geartypes.onetwo) {
            index = 1;
        } else if (type == geartypes.onethree) {
            index = 2;
        } else if (type == geartypes.onefour) {
            index = 3;
        }

        GameObject newGear = Instantiate(prefabs[index], nextSpawnPosition, Quaternion.identity);
        wheels.Add(newGear);

        
        updateNextSpawnPosition();
        

        if (wheels.Count == 5) {
            finalInstallationInstance = Instantiate(finalInstallationPrefab, nextSpawnPosition, Quaternion.identity);
        }
    }

    private void updateNextSpawnPosition() {
        int ratio = wheels[wheels.Count-1].GetComponent<wheel>().ratio;

        if (wheels.Count < 5) {
            nextSpawnPosition.z -= 0.2f;
        } else {
            nextSpawnPosition.z += 0.14f;
        }

        nextSpawnPosition.x -= (1f + (ratio-1)*0.5f);
    }

    public void StartTesting() {
        ballInstance = Instantiate(ballPrefab, ballSpawnPosition.transform.position, ballSpawnPosition.transform.rotation);
    }

    public void ResetAll() {
        List<GameObject> newWheels = new List<GameObject>();
        newWheels.Add(wheels[0]);

        for (int i = 1; i < wheels.Count; i++) {
            Destroy(wheels[i]);
        }

        wheels = newWheels;
        startingBin.transform.rotation = Quaternion.Euler(0, 0, 45);
        
        nextSpawnPosition = wheels[0].transform.position;
        nextSpawnPosition.x -= 1;
        nextSpawnPosition.z -= 0.2f;

        Destroy(ballInstance);
        ballInstance = null;
        Destroy(finalInstallationInstance);
        finalInstallationInstance = null;
    }
}
