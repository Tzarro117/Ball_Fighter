using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1WindScript : MonoBehaviour {

    public int player1WindValue = 100;
    public Slider player1WindSlider;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player1WindSlider.value = player1WindValue;
    }
}
