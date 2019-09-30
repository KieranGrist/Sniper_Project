using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
public class ClutterText : MonoBehaviour {
    public Slider Cslider; //Cluter Slider Reference
	// Use this for initialization
	void Start () {
        Cslider.value = Random.Range(Cslider.minValue, Cslider.maxValue); //Slider value = to random range between slider min and max value
    }
	
	// Update is called once per frame
	void Update () {
        Cluter clutter = FindObjectOfType<Cluter>(); //Get the clutter object from world
        Text text = GetComponent<Text>(); //Get text component of object
        text.text = "Clutter Ammount : " + clutter.CluterAmmount; //text = Cluter ammount: + slider value
        clutter.CluterAmmount = Cslider.value; //Set cluter ammount to be slider value
    }
}
