using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechenical : MonoBehaviour
{
    public GameObject startObject;
    public GameObject ball;
    public List<GameObject> gears;
    private List<Quaternion> gearsStartRotations = new List<Quaternion>();
    public GameObject ballDropLocationEmptyGameObject;

    private GameObject instantiatedBall = null;

    void Start() {
        foreach (GameObject gear in gears) {
            gearsStartRotations.Add(gear.transform.rotation);
        }
    }

    public void StartMechenical() {
        if (instantiatedBall != null) {
            Destroy(instantiatedBall);
        }

        instantiatedBall = Instantiate(ball, ballDropLocationEmptyGameObject.transform.position, ballDropLocationEmptyGameObject.transform.rotation);

        // startObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeRotationX;
        // startObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        // ball.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
    }


    public void ResetGearRotations() {
        for (int i = 0; i < gears.Count; i++) {
            gears[i].transform.rotation = gearsStartRotations[i];
            gears[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gears[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
