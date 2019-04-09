using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushx : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        myTransformation TempoaryTransformation = GetComponent<myTransformation>();
        TempoaryTransformation.Translation.x += 0.5f * Time.deltaTime;
    }
}
