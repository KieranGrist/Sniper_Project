using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultQuatToEuler : MonoBehaviour {

    public Quaternion q = new Quaternion();
    public Vector3 Axis;
    public float Angle = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Angle += Time.deltaTime * 40;
        q =  Quaternion.AngleAxis(Angle, Axis);
        transform.rotation = Quaternion.AngleAxis(Angle, Axis);
    }
}
