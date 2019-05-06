using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WindSpeedtext : MonoBehaviour {
    public Slider Windslider;
	// Use this for initialization
	void Start () {
        Windslider.value = Random.Range(Windslider.minValue, Windslider.maxValue);
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "WindSpeed: " + Windslider.value;
	}
}
