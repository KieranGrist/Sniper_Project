using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
  public  float Angle;
    public MyVector3 Axis;
    public MyVector3 Vertex;
    public MyVector3 Result;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Result = VectorMaths.RotateVertexAroundAxis(Angle, Axis, Vertex);
    
    }
}
