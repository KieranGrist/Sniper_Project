using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JayRigidBody : MonoBehaviour {
    public float Mass = 1.01f;
    public int ID;
    public MyVector3 Gravity = new MyVector3(0, -9.81f, 0);
    public MyVector3 Force = new MyVector3(0, 0, 0);
    public MyVector3 Acceleration = new MyVector3(0, 0, 0);
    public MyVector3 Velocity = new MyVector3(0, 0, 0);
    public MyVector3 OriginalPostion;
    public MyVector3 Postion;
    // Use this for initialization
    void Start () {
		
	}
    public void Restart()
    {
         Gravity = new MyVector3(0, -9.81f, 0);
         Force = new MyVector3(0, 0, 0);
         Acceleration = new MyVector3(0, 0, 0);
         Velocity = new MyVector3(0, 0, 0);
         Postion = new MyVector3(0, 0, 0);
        GetComponent<myTransformation>().Translation = OriginalPostion;
    }
    // Update is called once per frame
    void FixedUpdate() {
        Force += Gravity;
        Acceleration = Force / Mass;
        Velocity += Acceleration * Time.fixedDeltaTime;
        Force = new MyVector3(0, 0, 0);
        GetComponent<myTransformation>().Translation += Velocity * Time.fixedDeltaTime;
        Velocity /= 1.01f;
        Postion = GetComponent<myTransformation>().Translation;
    }
}
