using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPhysics : MonoBehaviour {
    public MyVector3 toreque, AngularAcceleration, AngularVelocity;
    public float Intertia = 1;
  public float avMag;
    public Quat q;
    public Quat Rotation;
    public Quat TargetOrientation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()  {
        AngularAcceleration = toreque * Intertia;
        AngularVelocity += AngularAcceleration * Time.fixedDeltaTime;

         q = new Quat();
         avMag = (AngularVelocity * Time.fixedDeltaTime).Length();
        q.w = Mathf.Cos(avMag / 2);
        q.x = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).x / avMag;
        q.y = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).y / avMag;
        q.z = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).z / avMag;

        Rotation = GetComponent<myTransformation>().GetRotation(); 

        TargetOrientation = q * Rotation;

        GetComponent<myTransformation>().SetRotation(TargetOrientation);
    }
}
