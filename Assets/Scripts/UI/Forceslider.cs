using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Forceslider : MonoBehaviour {
    public Slider massslider;
    public float Maxvalue, Minvalue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float value = massslider.value;
        Minvalue = value * 100;
        Maxvalue = Minvalue * 1000;
        float Mid = (Minvalue + Maxvalue) / 2;
        GetComponent<Slider>().value = Mid;
        GetComponent<Slider>().maxValue = Maxvalue;
        GetComponent<Slider>().minValue = Minvalue;
	}
}
