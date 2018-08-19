using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindScript : MonoBehaviour {

    public int player2WindValue = 100;
    public Slider player2WindSlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        player2WindSlider.value = player2WindValue;
    }
}
