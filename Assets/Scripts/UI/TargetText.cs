using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetText : MonoBehaviour {
    public Text text;
    public TargetSpawn TargetSpawner;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Target Ammount : " + TargetSpawner.TargetMax;
    }
}
