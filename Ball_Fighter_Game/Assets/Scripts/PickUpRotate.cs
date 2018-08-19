using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotate : MonoBehaviour {

    float originalY;

    public float floatStrength = 1;

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update () {
        transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime, Space.World);

        transform.position = new Vector3(transform.position.x,
        originalY + (Mathf.Sin(Time.time * 3) * floatStrength),
        transform.position.z);
    }
}
