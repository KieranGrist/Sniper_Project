using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directiontoeuler : MonoBehaviour {
    public MyVector3 Direciton,EulerAngle,d2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        EulerAngle = VectorMaths.DirectionToEuler(Direciton);
        d2 = VectorMaths.EulerAnglesToDirection(EulerAngle);
	}
}
