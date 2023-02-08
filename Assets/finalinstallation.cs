using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalinstallation : MonoBehaviour
{
    public GameObject finalwheel;
    public GameObject rack;

    public void Rotate(float rotationspeed) {
        finalwheel.transform.Rotate(Vector3.back * rotationspeed * Time.deltaTime);

        float ratio = rotationspeed / 20;

        // per ratio, move 2.5 tand up
        rack.transform.Translate(Vector3.up * ratio*0.175f * Time.deltaTime);


    }
}
