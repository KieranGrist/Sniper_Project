using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ResolveDebuger : MonoBehaviour {
    public myTransformation Object1;
    public myTransformation Object2;
    public MyVector3 Norm;
    public float Push;
    int i;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
         if (Object1.BoundObject.Intersects(Object2.BoundObject))
        {
            i++;
            Debug.Log("Collision " + i);
            Object1.BoundObject.CollisionResolution(Object2.BoundObject, out Norm, out Push);
            Object1.Translation += Norm * (Push + 0.0000001639f);
        }
        else
        {
            i = 0;
        }
        
	}
}
