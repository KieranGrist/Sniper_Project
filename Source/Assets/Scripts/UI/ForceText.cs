using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
[System.Serializable]
public class ForceText : MonoBehaviour {


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Sniper Player = FindObjectOfType<Sniper>(); //Player variable set by finding the player object
    Text text = GetComponent<Text>(); //Get the text component of the object
             text.text = "Firing Speed : " + Player.FireSpeed; //Set text to be firing speed : + players firing speed
    }
}
