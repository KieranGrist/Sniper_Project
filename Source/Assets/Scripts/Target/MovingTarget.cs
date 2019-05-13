using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour {
     myTransformation Transformation;
     public MyPhysics Physics;
    public MyVector3 ForceApplied;
    public MyVector3 Origin;
    float DeltaTimer;
    bool firsttime = false;
    // Use this for initialization

    void Start () {
        firsttime = false;
    }

    // Update is called once per frame
    void Update () {
        if (firsttime == false)
        {
            Transformation = GetComponent<myTransformation>();
            Physics = GetComponent<MyPhysics>();
            Physics.Dynamic = true;
            Physics.Mass = 1.01f;
            Physics.Force = new MyVector3(0, 0, 0);
            Physics.Force += VectorMaths.EulerAnglesToDirection(new MyVector3(Transformation.Rotation.x, -Transformation.Rotation.y, 0)) * 15000;
            Physics.Force.y += 15000;
            ForceApplied = Physics.Force;
            firsttime = true;
            Origin = Transformation.Translation;
        }
        Transformation.Scale = new MyVector3(20, 20, 20);
        Transformation = GetComponent<myTransformation>();
        Physics = GetComponent<MyPhysics>();
        DeltaTimer += Time.deltaTime;
        float Distance = (Origin - Transformation.Translation).Length();
        if (Distance > 5000)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        if (DeltaTimer > 2)

            if (Physics.Collided == true)
            {
                Destroy(this.gameObject);
                Destroy(this);
            }
    }
}
