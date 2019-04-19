using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRotationPhysics : MonoBehaviour {
    public Vector3 toreque, AngularAcceleration, AngularVelocity;
    public float Intertia = 1;
    // Use this for initialization
    public float avMag;
    public Quaternion q;
    public Quaternion Rotation;
    public Quaternion TargetOrientation;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        AngularAcceleration = toreque * Intertia;
        AngularVelocity += AngularAcceleration * Time.fixedDeltaTime;

        q = new Quaternion();
        avMag = (AngularVelocity * Time.fixedDeltaTime).magnitude;
        q.w = Mathf.Cos(avMag / 2);
        q.x = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).x / avMag;
        q.y = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).y / avMag;
        q.z = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).z / avMag;
        Rotation = transform.rotation;
        TargetOrientation = q * Rotation;
        transform.rotation = TargetOrientation;
    }
}
