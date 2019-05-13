using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Forceslider : MonoBehaviour {
    public Slider MassSlider;
    public float Maxvalue, Minvalue;
	// Use this for initialization
	void Start () {
      
        float value = MassSlider.value;
        Minvalue = value * 10;
        Maxvalue = Minvalue * 10000;
        float Mid = (Minvalue + Maxvalue) / 2;

        GetComponent<Slider>().maxValue = Maxvalue;
        GetComponent<Slider>().minValue = Minvalue;


        GetComponent<Slider>().value = 6907033;

    }
	
	// Update is called once per frame
	void Update () {
        float value = MassSlider.value;
        Minvalue = value * 10;
        Maxvalue = Minvalue * 10000;
        float Mid = (Minvalue + Maxvalue) / 2;

        GetComponent<Slider>().maxValue = Maxvalue;
        GetComponent<Slider>().minValue = Minvalue;
	}
}
