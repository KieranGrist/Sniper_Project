using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class WindSpeedtext : MonoBehaviour { 
    public Slider Windslider; //Slider Controling the wind speed
	// Use this for initialization
	void Start () {
        Windslider.value = Random.Range(Windslider.minValue, Windslider.maxValue); //Generates a random value for the wind between the min and max value of the slider
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "WindSpeed: " + Windslider.value; //Sets the text of the texxt box to be windspeed: + value of slider
	}
}
