using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylinderProperties : MonoBehaviour
{

    public GameObject player;
    public Text countText;
    public int count;

    void Start()
    {
        SetCountText();
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Alt Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
