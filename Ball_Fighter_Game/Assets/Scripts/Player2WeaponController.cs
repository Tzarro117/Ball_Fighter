using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2WeaponController : MonoBehaviour {

    //This script controls attack interactions attached to the player's weapon

    public Material enemyColor;
    public Material enemyHit;
    public GameObject enemyObject;
    public GameObject player;
    public float attackPower;
    public Animator anim;
//    public float attackTimer;

    public bool attack;
    public bool attackWindow;

    float fadeTime = 0.0f;

    //public Renderer rend;
    void Awake()
    {
        anim = player.GetComponent<Animator>();
    }


    void Start()
    {
        //rend = enemyObject.GetComponent<Renderer>();
    }

    void OnEnable()
    {
        attack = false;
        attackWindow = false;
    }

    void Update()
    {
        if ((Input.GetButtonDown("Player2Fire1") && (attack == false)))
        {
            StartCoroutine("AttackAnimStart");
        }

        if (enemyObject.GetComponent<Renderer>().material || enemyColor)
        {
            fadeTime += Time.deltaTime * 1;
            enemyObject.GetComponent<Renderer>().material.Lerp(enemyObject.GetComponent<Renderer>().material, enemyColor, fadeTime);
        }

        //Part of Attack Method Version
/*
        attackTimer += Time.deltaTime;

        if (attackTimer > 0.5f)
        {
            attack = false;
        }
*/
    }


/*          //Attack Method Version
    void Attack()
    {
        attack = true;
        fadeTime = 0.0f;
        enemyObject.GetComponent<Renderer>().material = enemyHit;
        enemyObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * attackPower, ForceMode.Impulse);
        GameObject.Find("Player2WindSlider").GetComponent<WindScript>().player2WindValue -= 5;
        attackTimer = 0f;
    }
*/


    IEnumerator AttackCD()
    {
        attackWindow = true;
        anim.SetBool("stabAttackAnim", false);
        yield return new WaitForSeconds(.1f);
        attackWindow = false;
        yield return new WaitForSeconds(.1f);
        attack = false;
        StopCoroutine("AttackCD");
    }

    IEnumerator AttackAnimStart()
    {
        attack = true;
        anim.SetBool("stabAttackAnim", true);
        yield return new WaitForSeconds(.15f);
        StartCoroutine("AttackCD");
        // Important to stop this co-routine, otherwise AttackCD gets executed multple times (damage multiplied for some reason)
        StopCoroutine("AttackAnimStart");
    }



    void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Player1")) && (attackWindow == true))
        {
            enemyObject = other.gameObject;
            enemyColor = enemyObject.GetComponent<Renderer>().material;
            fadeTime = 0.0f;
            enemyObject.GetComponent<Renderer>().material = enemyHit;
            enemyObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * attackPower, ForceMode.Impulse);
            GameObject.Find("Player2WindSlider").GetComponent<WindScript>().player2WindValue -= 5;
            attackWindow = false;
        }
    }
}
