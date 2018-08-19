using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Vector3 grav = new Vector3(0.0f, -9.81f, 0.0f);
    Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {

    }
	

	void Update () {
        rb.AddForce(-rb.velocity.x / 2, 0.0f, -rb.velocity.z / 2);
    }
 

    void FixedUpdate ()
    {
        rb.AddForce(grav * 2, ForceMode.Acceleration);
    }
}
