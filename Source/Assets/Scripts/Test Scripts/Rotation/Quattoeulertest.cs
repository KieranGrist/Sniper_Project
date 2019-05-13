using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quattoeulertest : MonoBehaviour {
     
    public Quat q = new Quat();
    public MyVector3 Axis;
    public float Angle = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Angle += Time.deltaTime * 2;
        q = new Quat(Angle, Axis);
        myTransformation Transformation = GetComponent<myTransformation>();
        Transformation.SetRotation(q);
    }
}
