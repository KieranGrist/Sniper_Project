﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExplosionRadiusText : MonoBehaviour {
    public Slider slider;
	// Use this for initialization
	void Start () {
        slider.value = Random.Range(slider.minValue, slider.maxValue);
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Explosion Radius: " + slider.value;
	}
}