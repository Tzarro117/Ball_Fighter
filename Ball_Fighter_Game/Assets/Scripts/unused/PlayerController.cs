using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text moveZ;
    public Text winText;
    public int count;
    public float maxSpeed = 10f;
    public Vector3 grav = new Vector3(0.0f, -9.81f, 0.0f);
   

    private Rigidbody rb;

    private void Awake()
    {
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText ();
        winText.text = "";
    }


    private void Update()
    {
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(decel1 * 2 * Time.deltaTime);
            //countText.text = "left/right";
        }
        else
        {
            //countText.text = "no left/right";
        }

        if (!Input.GetKey("up") && !Input.GetKey("down"))
        {
            Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
            rb.AddForce(decel2 * 2 * Time.deltaTime);
            //moveZ.text = "up/down";
        }
        else
        {
            //moveZ.text = "no up/down";
        }
 
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        /*
        if (rb.velocity.x + rb.velocity.z * Mathf.Sqrt(2) < 7.071)
        {
            rb.AddForce(movement.normalized * speed);
        }
        */

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        rb.AddForce (movement.normalized * speed);
        rb.AddForce (grav * 2, ForceMode.Acceleration);

        /*
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            Vector3 decel = new Vector3(-rb.velocity.x, 0.0f, -rb.velocity.z);
            rb.AddForce(decel * 2);
            moveZ.text = "stopping";
        }
        else
        {
            moveZ.text = "Speed Z: " + moveVertical.ToString();
        }
        */

        countText.text = "Speed X: " + rb.velocity.x.ToString();
        moveZ.text = "Speed Z: " + rb.velocity.z.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText ();
        }
    }
 
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}

