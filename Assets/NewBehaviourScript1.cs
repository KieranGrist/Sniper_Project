using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        myTransformation transformation = GetComponent<myTransformation>();
        transformation.Rotation.x += Time.deltaTime *5;
        transformation.Rotation.y += Time.deltaTime *5;
        transformation.Rotation.z += Time.deltaTime *5;
        transformation.rota
    }
}
