using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("woo");

        projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0.5f, 1)*4000, ForceMode.Force);
    }
}
