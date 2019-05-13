using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour {
    public Quaternion q;
    public Quaternion UnityRotation;
    public Quat Rotation;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = q;
        Quat quat = new Quat(q.x, q.y, q.z, q.w);
        GetComponent<myTransformation>().SetRotation(quat);
        UnityRotation = transform.rotation;
        Rotation = GetComponent<myTransformation>().GetRotation();
    }
}
