using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel : MonoBehaviour
{
    public int ratio;

    public void rotateWheel(float speed, bool forward) {
        if (forward) {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        } else {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
