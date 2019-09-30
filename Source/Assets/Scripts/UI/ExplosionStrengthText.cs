using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
public class ExplosionStrengthText : MonoBehaviour {
    public Slider slider; //Explosion Slider
    // Use this for initialization
    void Start()
    {
        slider.value = Random.Range(slider.minValue, slider.maxValue); //Set explosion strength value to be random between slider min and max value
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Explosion Strength: " + slider.value; //Text = Explosion strength: + explosion slider value
    }
}
