using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quattoeulertest : MonoBehaviour {
    public myTransformation Transformation;
    public Quat q = new Quat();
    float t = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime *2;
        q = new Quat(t, new MyVector3(0, 1, 0));
        Transformation.SetRotation(q);
    }
}
