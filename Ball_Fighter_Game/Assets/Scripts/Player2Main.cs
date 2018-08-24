using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Main : MonoBehaviour {

    public float speed;
    public float maxSpeed;
    public float drag;
    public Vector3 grav = new Vector3(0.0f, -9.81f, 0.0f);
    public bool stabAttackAnim;

    public GameObject basicSword;
    public Avatar swordAvatar;
    public GameObject spear;
    public Avatar spearAvatar;
    public Material playerMaterial;

    public Vector3 playerMoveDirection;
    Vector3 movement;
    Rigidbody rb;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    // Use this for initialization
    void Start () {
		
	}

    private void Update()

    {
        /*
        if ((!Input.GetKey("a") && (rb.velocity.x < 0)) || (Input.GetKey("a") && Input.GetKey("d")))
        {
            Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(decel1 * drag, ForceMode.Acceleration);
        }

        if (!Input.GetKey("d") && (rb.velocity.x > 0))
        {
            Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(decel1 * drag, ForceMode.Acceleration);
        }

        if ((!Input.GetKey("w") && (rb.velocity.z > 0)) || (Input.GetKey("w") && Input.GetKey("s")))
        {
            Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
            rb.AddForce(decel2 * drag, ForceMode.Acceleration);
        }

        if (!Input.GetKey("s") && (rb.velocity.z < 0))
        {
            Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
            rb.AddForce(decel2 * drag, ForceMode.Acceleration);
        }
        */
        Rotation();

    }

    void FixedUpdate()
    {

        {
            if (Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s"))
            {
                Move();
            }

            rb.AddForce(grav * 2, ForceMode.Acceleration);

            Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(decel1 * drag, ForceMode.Acceleration);

            Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
            rb.AddForce(decel2 * drag, ForceMode.Acceleration);
        }
        /*
        float h = Input.GetAxis("Player2Horizontal");
        float v = Input.GetAxis("Player2Vertical");

        if ((Input.GetKey("a") && Input.GetKey("d")) || (((rb.velocity.x < -maxSpeed) && (h < 0)) || ((rb.velocity.x > maxSpeed) && (h > 0))))
        {
            h = 0.0f;
        }

        if ((Input.GetKey("w") && Input.GetKey("s")) || (((rb.velocity.z < -maxSpeed) && (v < 0)) || ((rb.velocity.z > maxSpeed) && (v > 0))))
        {
            v = 0.0f;
        }
        */

        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, 8);

        /* This is the original system to cap move speed

                var rbv = rb.velocity;
                var f = rbv.y;
                rbv.y = 0.0f;
                rbv = Vector3.ClampMagnitude(rbv, maxSpeed);
                rbv.y = f;
                rb.velocity = rbv;
        */

        /*
        Move(h, v);

        rb.AddForce(grav * 2, ForceMode.Acceleration);
        */
    }

    void Move()
    {
        //if (rb.rotation.eulerAngles.y - playerMoveDirection.y < 45)
        {
            rb.AddForce(playerMoveDirection.normalized * speed, ForceMode.Acceleration);
        }
    }

    /*
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        rb.AddForce(movement.normalized * speed, ForceMode.Acceleration);
    }
    */

    void Rotation()
    {

        //Player object movement
        float horMovement = Input.GetAxisRaw("Player2Horizontal");
        float vertMovement = Input.GetAxisRaw("Player2Vertical");

        //Player graphic rotation
        playerMoveDirection = new Vector3(horMovement, 0, vertMovement);
        if (playerMoveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(playerMoveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, Time.deltaTime * 8);
        }

    }

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
