using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TargetText : MonoBehaviour {

 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Text text = GetComponent<Text>();
        TargetSpawn TargetSpawner = FindObjectOfType<TargetSpawn>();
        text.text = "Target Ammount : " + TargetSpawner.TargetMax;


    }
}
