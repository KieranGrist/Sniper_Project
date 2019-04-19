using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClutterText : MonoBehaviour {
    public Slider Cslider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Cluter clutter = FindObjectOfType<Cluter>();
        Text text = GetComponent<Text>();
        text.text = "Clutter Ammount : " + clutter.CluterAmmount;
        clutter.CluterAmmount = Cslider.value;
    }
}
