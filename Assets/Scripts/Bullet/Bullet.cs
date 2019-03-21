using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct BulletInit
{
    public MyVector3 GunPosition;
    public MyVector3 GunRotation;
    public float FireSpeed, mass;
    public myTransformation Floor;
   // public GridHandle Collison;
}
public class Bullet : MonoBehaviour {
     MyVector3 GunPosition;
     MyVector3 GunRotation;
    float FireSpeed,mass;
    public myTransformation Floor;

    public bool Alive = true;
    public float timeoutDestructor;
    public myTransformation Transformation;
    public myRigidBody Physics;
    public void Init(BulletInit Initiate)
    {
        GunPosition = Initiate.GunPosition;
        GunRotation = Initiate.GunRotation;
        FireSpeed = Initiate.FireSpeed;
        mass = Initiate.mass;
        Floor = Initiate.Floor;
        Physics = GetComponent<myRigidBody>();
        Transformation = GetComponent<myTransformation>();
    }
    void Start()
    {
        timeoutDestructor = 0;
        Physics = GetComponent<myRigidBody>();
        Transformation = GetComponent<myTransformation>();
        Transformation.Scale = new MyVector3(5.5f,5.5f, 5.5f);
        Physics.Mass = mass;
        Transformation.Translation = GunPosition;
        Transformation.Rotation = GunRotation;
        Physics.Force = VectorMaths.EulerAnglesToDirection(new MyVector3(-GunRotation.x, GunRotation.y, 0)) * FireSpeed;
    }

    // Update is called once per frame
    void Update()
    {
      
        timeoutDestructor += Time.deltaTime;
        //if (AABB.Intersects(Transformation.CollisionBox, Floor.CollisionBox,out MyVector3))
        //{
        //    Alive = false;
        //    //Destroy(Physics);
        //}

       MyVector3 Distance = GunPosition - Transformation.Translation;
     
        if (Distance.Length() >= 10000)
        {
            Alive = false;
        }
        if (Transformation.Translation.y <= 0)
        {
            Alive = false;
           // Destroy(Physics);
        }
    }
}
