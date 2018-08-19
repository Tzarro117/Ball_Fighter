using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltMovement : MonoBehaviour {

    //This was the original Player 1 movement script and has nice been replaced by Player1Main.

    public Text countText;
    public Text moveZ;
    public Text winText;
    public int count;
    public float speed;
    public float speedConstant;
    public float speedMultiplier;
    public float maxSpeed;
    public float drag;
    public Vector3 grav = new Vector3(0.0f, -9.81f, 0.0f);
    public bool stabAttackAnim;
    //private int weaponSelect;

    public GameObject basicSword;
    public Avatar swordAvatar;
    public GameObject spear;
    public Avatar spearAvatar;

    public Vector3 playerMoveDirection;

    //private float speedRotate = 1.5f;

    Vector3 movement;
    Rigidbody rb;
    Animator animator;

    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
 /* 
        if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(decel1 * drag, ForceMode.Acceleration);
        }

        if (!Input.GetKey("up") && !Input.GetKey("down"))
        {
            Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
            rb.AddForce(decel2 * drag, ForceMode.Acceleration);
        }
*/
        if ((!Input.GetKey("left") && (rb.velocity.x < 0)) || (Input.GetKey("left") && Input.GetKey("right")))
        {
            Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(decel1 * drag, ForceMode.Acceleration);
        }

        if (!Input.GetKey("right") && (rb.velocity.x > 0))
        {
            Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(decel1 * drag, ForceMode.Acceleration);
        }

        if ((!Input.GetKey("up") && (rb.velocity.z > 0)) || (Input.GetKey("up") && Input.GetKey("down")))
        {
            Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
            rb.AddForce(decel2 * drag, ForceMode.Acceleration);
        }

        if (!Input.GetKey("down") && (rb.velocity.z < 0))
        {
            Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
            rb.AddForce(decel2 * drag, ForceMode.Acceleration);
        }

        Rotation();

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if ((Input.GetKey("left") && Input.GetKey("right")) || (((rb.velocity.x < -maxSpeed) && (h < 0)) || ((rb.velocity.x > maxSpeed) && (h > 0))))
        {
            h = 0.0f;
        }

        if ((Input.GetKey("up") && Input.GetKey("down")) || (((rb.velocity.z < -maxSpeed) && (v < 0)) || ((rb.velocity.z > maxSpeed) && (v > 0))))
        {
            v = 0.0f;
        }

        if ((Input.GetKey("left")) && (Input.GetKey("up")))
        {
            if (rb.velocity.x < (-maxSpeed * 0.7071))
            {
                h = 0;
                Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
                rb.AddForce(decel1 * (drag), ForceMode.Acceleration);
            }
            else
            {
                h = -0.7071f;
            }

            if (rb.velocity.z > (maxSpeed * 0.7071))
            {
                v = 0;
                Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
                rb.AddForce(decel2 * (drag), ForceMode.Acceleration);
            }
            else
            {
                v = 0.7071f;
            }

            if (rb.velocity.x < (-(maxSpeed / 2) / (0.7071)) && (rb.velocity.z > ((maxSpeed / 2) / (0.7071))))
            {
                h = 0;
                v = 0;
            }
        }

        if ((Input.GetKey("left")) && (Input.GetKey("down")))
        {
            if (rb.velocity.x < (-maxSpeed * 0.7071))
            {
                h = 0;
                Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
                rb.AddForce(decel1 * (drag), ForceMode.Acceleration);
            }
            else
            {
                h = -0.7071f;
            }

            if (rb.velocity.z < (-maxSpeed * 0.7071))
            {
                v = 0;
                Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
                rb.AddForce(decel2 * (drag), ForceMode.Acceleration);
            }
            else
            {
                v = -0.7071f;
            }

            if (rb.velocity.x < (-(maxSpeed / 2) / (0.7071)) && (rb.velocity.z < (-(maxSpeed / 2) / (0.7071))))
            {
                h = 0;
                v = 0;
            }
        }

        if ((Input.GetKey("right")) && (Input.GetKey("up")))
        {
            if (rb.velocity.x > (maxSpeed * 0.7071))
            {
                h = 0;
                Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
                rb.AddForce(decel1 * (drag), ForceMode.Acceleration);
            }
            else
            {
                h = 0.7071f;
            }

            if (rb.velocity.z > (maxSpeed * 0.7071))
            {
                v = 0;
                Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
                rb.AddForce(decel2 * (drag), ForceMode.Acceleration);
            }
            else
            {
                v = 0.7071f;
            }

            if (rb.velocity.x > ((maxSpeed / 2) / (0.7071)) && (rb.velocity.z > ((maxSpeed / 2) / (0.7071))))
            {
                h = 0;
                v = 0;
            }
        }

        if ((Input.GetKey("right")) && (Input.GetKey("down")))
        {
            if (rb.velocity.x > (maxSpeed * 0.7071))
            {
                h = 0;
                Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
                rb.AddForce(decel1 * (drag), ForceMode.Acceleration);
            }
            else
            {
                h = 0.7071f;
            }

            if (rb.velocity.z < (-maxSpeed * 0.7071))
            {
                v = 0;
                Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
                rb.AddForce(decel2 * (drag), ForceMode.Acceleration);
            }
            else
            {
                v = -0.7071f;
            }

            if (rb.velocity.x > ((maxSpeed / 2) / (0.7071)) && (rb.velocity.z < (-(maxSpeed / 2) / (0.7071))))
            {
                h = 0;
                v = 0;
            }
        }


        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 8);

        /* This is the original system to cap move speed

                var rbv = rb.velocity;
                var f = rbv.y;
                rbv.y = 0.0f;
                rbv = Vector3.ClampMagnitude(rbv, maxSpeed);
                rbv.y = f;
                rb.velocity = rbv;
        */
        Move(h, v);

        rb.AddForce(grav * 2, ForceMode.Acceleration);

        countText.text = GameObject.Find("Main Camera").GetComponent<CameraController>().cameraTest.ToString();
        moveZ.text = rb.velocity.magnitude.ToString();
    }

    //old movement system (don't forget to change input gravity and physics material!)
    /*
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }
    */

    //failed new physics movement system
    
    void Move(float h, float v)
    {
//        if (((Mathf.Abs(rb.velocity.x) <= maxSpeed) && (Mathf.Abs(rb.velocity.z) <= maxSpeed)))
        {



        movement.Set(h, 0f, v);

            if ((rb.velocity.x < 4.2426 && rb.velocity.z > 4.2426) && (Input.GetKey("right") && Input.GetKey("up")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }

            if ((rb.velocity.x > 4.2426 && rb.velocity.z < 4.2426) && (Input.GetKey("right") && Input.GetKey("up")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }

            if ((rb.velocity.x > 4.2426 && rb.velocity.z > -4.2426) && (Input.GetKey("right") && Input.GetKey("down")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }

            if ((rb.velocity.x < 4.2426 && rb.velocity.z > -4.2426) && (Input.GetKey("right") && Input.GetKey("down")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }
            //// ^right^ - vleftv
            if ((rb.velocity.x > -4.2426 && rb.velocity.z > 4.2426) && (Input.GetKey("left") && Input.GetKey("up")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }

            if ((rb.velocity.x < -4.2426 && rb.velocity.z < 4.2426) && (Input.GetKey("left") && Input.GetKey("up")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }

            if ((rb.velocity.x < -4.2426 && rb.velocity.z > -4.2426) && (Input.GetKey("left") && Input.GetKey("down")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }

            if ((rb.velocity.x > -4.2426 && rb.velocity.z < -4.2426) && (Input.GetKey("left") && Input.GetKey("down")))
            {
                speed *= speedMultiplier;
            }
            else
            {
                speed = speedConstant;
            }


            rb.AddForce(movement * speed, ForceMode.Acceleration);


            //if(rb.velocity.magnitude > maxSpeed)
            {
                //rb.AddForce(-movement.normalized * speed, ForceMode.Acceleration);
            }
        }

    }
    

    /*
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
    */

    void Rotation()
    {

        //Player object movement
        float horMovement = Input.GetAxisRaw("Horizontal");
        float vertMovement = Input.GetAxisRaw("Vertical");
        /*
        if (horMovement != 0 && vertMovement != 0)
        {
            speedRotate = 1.0f;
        }
        else
        {
            speedRotate = 1.5f;
        }
        
        transform.Translate(transform.right * horMovement * Time.deltaTime * speedRotate);
        transform.Translate(transform.forward * vertMovement * Time.deltaTime * speedRotate);
        */

        //Player graphic rotation
        playerMoveDirection = new Vector3(horMovement, 0, vertMovement);
        if (playerMoveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(playerMoveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, Time.deltaTime * 8);
        }      
    }

    // Weapon pickup/respawn code
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            rb.transform.position = new Vector3(-3f, 0.5f, -2f);
        }

        if (other.gameObject.CompareTag("Basic Sword"))
        {
            basicSword.SetActive(true);
            spear.SetActive(false);
            animator.runtimeAnimatorController = Resources.Load("Player1SwordSwingAnimController") as RuntimeAnimatorController;
            animator.avatar = swordAvatar;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Spear"))
        {
            spear.SetActive(true);
            basicSword.SetActive(false);
            animator.runtimeAnimatorController = Resources.Load("Player1SpearStabAnimController") as RuntimeAnimatorController;
            animator.avatar = spearAvatar;
            other.gameObject.SetActive(false);
        }
    }

}
