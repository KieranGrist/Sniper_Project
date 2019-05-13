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
          Canvas UI = GetComponent<Canvas>();
		if (Input.GetKey(KeyCode.LeftControl))
        {
            UI.enabled = true;
        }
        else
        {
            UI.enabled = false;
        }

    }
}
