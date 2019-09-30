using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
[System.Serializable]
public class Focus_Script : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
          Canvas UI = GetComponent<Canvas>(); //Get the UI canvas 
		if (Input.GetKey(KeyCode.LeftControl)) //If player is pressing LControl show UI
        {
            UI.enabled = true; //Show UI
        }
        else
        {
            UI.enabled = false; //Hide ui
        }

    }
}