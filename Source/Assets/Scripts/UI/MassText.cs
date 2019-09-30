using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI;
[System.Serializable]
public class MassText : MonoBehaviour
{
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Sniper Player = FindObjectOfType<Sniper>(); //Player Variable which is set by finding the player in the world
        Text text = GetComponent<Text>(); //Get the text component of the object
        text.text = "Mass : " + Player.Mass + " G"; //Text is equal to Mass + Player mass + G 
    }
}