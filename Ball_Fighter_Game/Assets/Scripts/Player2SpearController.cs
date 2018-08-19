using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2SpearController : MonoBehaviour {

    //This script controls attack interactions attached to the player's weapon

    public Material enemyColor;
    public Material enemyHit;
    public GameObject enemyObject;
    public GameObject player;
    public float attackPower;
    public Animator anim;

    public bool attack;
    public bool attackWindow;

    float fadeTime = 0.0f;

    void Awake()
    {
        anim = player.GetComponent<Animator>();
    }


    void Start()
    {

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
    }

    //Attack Coroutine Version

    IEnumerator AttackCD()
    {
        attackWindow = true;
        yield return new WaitForSeconds(.1f);
        attackWindow = false;
        anim.SetBool("spearAttackAnim", false);
        yield return new WaitForSeconds(.1f);
        attack = false;
        StopCoroutine("AttackCD");
    }

    IEnumerator AttackAnimStart()
    {
        attack = true;
        anim.SetBool("spearAttackAnim", true);
        yield return new WaitForSeconds(.05f);
        StartCoroutine("AttackCD");
        // Important to stop this co-routine, otherwise AttackCD gets executed multiple times (damage multiplied for some reason)
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
