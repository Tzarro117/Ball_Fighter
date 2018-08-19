using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Main : MonoBehaviour {

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
    void Start()
    {

    }

    private void Update()
    {
        Rotation();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("left") || Input.GetKey("up") || Input.GetKey("right") || Input.GetKey("down"))
        {
            Move();
        }

        rb.AddForce(grav * 2, ForceMode.Acceleration);

        Vector3 decel1 = new Vector3(-rb.velocity.x, 0.0f, 0.0f);
        rb.AddForce(decel1 * drag, ForceMode.Acceleration);
    
        Vector3 decel2 = new Vector3(0.0f, 0.0f, -rb.velocity.z);
        rb.AddForce(decel2 * drag, ForceMode.Acceleration);
    }

    void Move()
    {
        //if (rb.rotation.eulerAngles.y - playerMoveDirection.y < 45)
        {
            rb.AddForce(playerMoveDirection.normalized * speed, ForceMode.Acceleration);
        }
    }

    void Rotation()
    {

        //Player object movement
        float horMovement = Input.GetAxisRaw("Horizontal");
        float vertMovement = Input.GetAxisRaw("Vertical");

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
