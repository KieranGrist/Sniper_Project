using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
public class Forceslider : MonoBehaviour {
    public Slider MassSlider; //Mass slider reference
    public float Maxvalue; //Max value of slider
        public float Minvalue; //Min value of slider
	// Use this for initialization
	void Start () {
      
        float value = MassSlider.value; //Get the mass slider value
        Minvalue = value * 10; //Set minium value to be value times 10
        Maxvalue = Minvalue * 10000; //Set maxium value to be minium value times 10000
        GetComponent<Slider>().maxValue = Maxvalue; //Set slider maxium value to be max value
        GetComponent<Slider>().minValue = Minvalue; //Set slider minium value to be min value
    }
	
	// Update is called once per frame
	void Update () {
        float value = MassSlider.value; //Get the mass slider value
        Minvalue = value * 10; //Set minium value to be value times 10
        Maxvalue = Minvalue * 10000; //Set maxium value to be minium value times 10000
        GetComponent<Slider>().maxValue = Maxvalue; //Set slider maxium value to be max value
        GetComponent<Slider>().minValue = Minvalue; //Set slider minium value to be min value
    }
}
