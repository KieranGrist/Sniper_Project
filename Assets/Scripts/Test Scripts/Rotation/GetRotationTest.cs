using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRotationTest : MonoBehaviour {

    public Vector3 EulerAngle;
    public Quaternion UnityRotation;
    public Quat Rotation;


    public Vector3 UnityConvEuler;
    public MyVector3 ConvEuler;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion R = Quaternion.Euler(EulerAngle);
        transform.rotation = R;
        GetComponent<myTransformation>().Rotation = new MyVector3(EulerAngle);
        UnityRotation = transform.rotation;
        Rotation = GetComponent<myTransformation>().GetRotation();
        UnityConvEuler = transform.rotation.eulerAngles;
        ConvEuler = Quat.QuatToEuler(Rotation);
    }
}
