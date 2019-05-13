                                                                                                                                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualTest : MonoBehaviour {
    public Quat q;


    public Vector3 angle;
    public Quaternion UnityRotation;
    public Quat Rotation;

    public Quaternion UnityTargetOrientation;
    public Quat TargetOrientation;

    public Vector3 UnityEulerAngle;
    public MyVector3 EulerAngle;

    public Quaternion UnityNewRotation;
    public Quat NewRotation;


    public Vector3 UnityNewEulerAngle;
    public MyVector3 NewEulerAngle;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion R = Quaternion.Euler(angle);
        transform.rotation=R;
        GetComponent<myTransformation>().Rotation = new MyVector3(angle);
        Quaternion t = new Quaternion(q.x, q.y, q.z, q.w);
        UnityRotation = transform.rotation;
        UnityTargetOrientation = t * UnityRotation;
        UnityEulerAngle = UnityTargetOrientation.eulerAngles;
        transform.rotation = UnityTargetOrientation;
        UnityNewRotation = transform.rotation;
        UnityNewEulerAngle = UnityNewRotation.eulerAngles;
        transform.rotation = UnityRotation;




        Rotation = GetComponent<myTransformation>().GetRotation();
        TargetOrientation = q * Rotation;
        myTransformation Temp = GetComponent<myTransformation>();
        EulerAngle = Quat.QuatToEuler(TargetOrientation);
        Temp.SetRotation(TargetOrientation);

        NewRotation = Temp.GetRotation();
        NewEulerAngle = Quat.QuatToEuler(NewRotation);
        GetComponent<myTransformation>().Rotation = new MyVector3(angle);
    }
}
