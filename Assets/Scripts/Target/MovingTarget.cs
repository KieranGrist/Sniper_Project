using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour {
     myTransformation Transformation;
     MyPhysics Physics;
    float DeltaTimer;

    // Use this for initialization

    void Start () { 
        Transformation = GetComponent<myTransformation>();
        Physics = GetComponent<MyPhysics>();
        Physics.Mass = 1.01f;
        Physics.Force += VectorMaths.EulerAnglesToDirection(new MyVector3(Transformation.Rotation.x, -Transformation.Rotation.y, 0)) * 50000;
        Physics.Force.y += 20000;
        Physics.Dynamic = true;
    }

    // Update is called once per frame
    void Update () {
    
        Transformation = GetComponent<myTransformation>();
        Physics = GetComponent<MyPhysics>();
        DeltaTimer += Time.deltaTime;
        if (DeltaTimer >0.5f)
     
        if (Physics.Collided == true)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
   
    }
}
