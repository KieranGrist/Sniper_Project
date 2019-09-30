using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
public class ExplosionRadiusText : MonoBehaviour {
    public Slider slider; //Explosion radius slider
	// Use this for initialization
	void Start () {
        slider.value = Random.Range(slider.minValue, slider.maxValue); //Explosion radius is equal to a random value between slider min and max value
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Explosion Radius: " + slider.value; //Text = Explosion radius: + Explosion radius slider
	}
}
