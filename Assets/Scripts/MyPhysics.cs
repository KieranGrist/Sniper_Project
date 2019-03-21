using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyPhysics : MonoBehaviour
{
    public MyVector3 Force, Acceleration, Velocity,torque, AngularAcceleration,AngularVelocity;
    public float Mass = 1, AirResitance = 1, Push, ObjectId,Inertia;

    //1 Box, 2 Sphere, 3 Cylander;
    public int ObjectType = 1;

    public bool Dynamic, Bouncy = false; //Object that has motion and is effected by physics, if false object is static at all times, Bouncy will effect what it does when colliding, Static does not move the object at all
    public myTransformation Transformation;
    public PhysicsControler PhyCont;
    CollisionGrid CurrentGrid;
    // Use this for initialization
    void Start()
    {
        ObjectId = Random.Range(0, 1000000);
    }
    void Resoloution(AABB Box1, AABB Box2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;// + Box2.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        Box2.Center = Box2.Half + Box2.MinExtent;
            float CurPenetration = (Box1.Half.x + Box2.Half.x) - Mathf.Abs(Box1.Center.x - Box2.Center.x);
            if (CurPenetration < Penetration)
            {

                Penetration = CurPenetration;
                if (Box1.Center.x > Box2.Center.x)
            {
                Normal = new MyVector3(1, 0, 0);
            }

            if (Box1.Center.x < Box2.Center.x)
            {
                Normal = new MyVector3(-1, 0, 0);
            }
        }
             CurPenetration = (Box1.Half.y + Box2.Half.y) - Mathf.Abs(Box1.Center.y - Box2.Center.y);

            if (CurPenetration < Penetration)
            {
                Penetration = CurPenetration;
                if (Box1.Center.y > Box2.Center.y)
                    Normal = new MyVector3(0, 1, 0);
                if (Box1.Center.y < Box2.Center.y)
                    Normal = new MyVector3(0, -1, 0);
            }

             CurPenetration = (Box1.Half.z + Box2.Half.z) - Mathf.Abs(Box1.Center.z - Box2.Center.z);

            if (CurPenetration < Penetration)
            {
                Penetration = CurPenetration;
                if (Box1.Center.z > Box2.Center.z)
                    Normal = new MyVector3(0, 0, 1);
                if (Box1.Center.z < Box2.Center.z)
                    Normal = new MyVector3(0, 0, -1);
            }
    }
    void Resoloution(AABB Box1, BoundingCircle Circle2, out MyVector3 Norms, out float Penetration)
    {
        Norms = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box1.Center = Box1.Half + Box1.MinExtent;
        Circle2.Half = ((Circle2.CentrePoint + Circle2.Radius) - (Circle2.CentrePoint - Circle2.Radius));
        float CurPenetration = (Box1.Half.x + Circle2.Half.x) - Mathf.Abs(Box1.Center.x - Circle2.CentrePoint.x);
        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.x > Circle2.CentrePoint.x)
                Norms = new MyVector3(1, 0, 0);
            if (Box1.Center.x < Circle2.CentrePoint.x)
                Norms = new MyVector3(-1, 0, 0);
        }
        CurPenetration = (Box1.Half.y + Circle2.Half.y) - Mathf.Abs(Box1.Center.y - Circle2.CentrePoint.y);
        if (CurPenetration < Penetration)
        {
            if (Box1.Center.y > Circle2.CentrePoint.y)
                Norms = new MyVector3(0, 1, 0);
            if (Box1.Center.y < Circle2.CentrePoint.y)
                Norms = new MyVector3(0, -1, 0);
        }
        CurPenetration = (Box1.Half.z + Circle2.Half.z) - Mathf.Abs(Box1.Center.z - Circle2.CentrePoint.z);
        if (CurPenetration < Penetration)
        {
            if (Box1.Center.z > Circle2.CentrePoint.z)
                Norms = new MyVector3(0, 0, 1);
            if (Box1.Center.z < Circle2.CentrePoint.z)
                Norms = new MyVector3(0, 0, -1);
        }

    }
    void Resoloution(AABB Box1, BoundingCapsule Capsule2, out MyVector3 Norms, out float Penetration)
    {
        Norms = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box1.Center = Box1.Half + Box1.MinExtent;

        Capsule2.Center = (Capsule2.B - Capsule2.A) * 0.5f;
        Capsule2.Half = Capsule2.Half + Capsule2.A;

        float CurPenetration = (Box1.Half.x + Capsule2.Half.x) - Mathf.Abs(Box1.Center.x - Capsule2.Center.x);
        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.x > Capsule2.Center.x)
                Norms = new MyVector3(1, 0, 0);
            if (Box1.Center.x < Capsule2.Center.x)
                Norms = new MyVector3(-1, 0, 0);
        }
        CurPenetration = (Box1.Half.y + Capsule2.Half.y) - Mathf.Abs(Box1.Center.y - Capsule2.Center.y);
        if (CurPenetration < Penetration)
        {
            if (Box1.Center.y > Capsule2.Center.y)
                Norms = new MyVector3(0, 1, 0);
            if (Box1.Center.y < Capsule2.Center.y)
                Norms = new MyVector3(0, -1, 0);
        }
        CurPenetration = (Box1.Half.z + Capsule2.Half.z) - Mathf.Abs(Box1.Center.z - Capsule2.Center.z);
        if (CurPenetration < Penetration)
        {
            if (Box1.Center.z > Capsule2.Center.z)
                Norms = new MyVector3(0, 0, 1);
            if (Box1.Center.z < Capsule2.Center.z)
                Norms = new MyVector3(0, 0, -1);
        }

    }



    void CollisionResoloution(BoundingObject LHS, BoundingObject RHS, out MyVector3 Norm, out float Penetration)
    {

        Norm = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
        if (LHS is AABB && RHS is AABB)
        {

            AABB Box1 = LHS as AABB;
            AABB Box2 = RHS as AABB;
            Resoloution(Box1, Box2, out Norm, out Penetration);

        }
        if (LHS is AABB && RHS is BoundingCircle)
        {
            AABB Box1 = LHS as AABB;
            BoundingCircle Circle2 = RHS as BoundingCircle;
            Resoloution(Box1, Circle2, out Norm, out Penetration);
        }
        if (LHS is AABB && RHS is BoundingCapsule)
        {
            AABB Box1 = LHS as AABB;
            BoundingCapsule Capsule2 = RHS as BoundingCapsule;
            Resoloution(Box1, Capsule2, out Norm, out Penetration);
        }

        if (LHS is BoundingCircle && RHS is AABB)
        {
            AABB Box1 = RHS as AABB;
            BoundingCircle Circle2 = LHS as BoundingCircle;
            Resoloution(Box1, Circle2, out Norm, out Penetration);
        }
        if (LHS is BoundingCapsule && RHS is AABB)
        {
            AABB Box1 = RHS as AABB;
            BoundingCapsule Capsule2 = LHS   as BoundingCapsule;
            Resoloution(Box1, Capsule2, out Norm, out Penetration);
        }


    }
    void FixedUpdate()
    {
        Transformation = GetComponent<myTransformation>();



     

   
        if (Dynamic == true|| Bouncy == true)
        {
            Transformation = GetComponent<myTransformation>();
            Acceleration = Force / Mass;
            Velocity += ((Acceleration + PhyCont.Gravity) / AirResitance) * Time.deltaTime;

            Force = new MyVector3(0, 0, 0);
            //WindVelocity = CurrentGrid.WindVelocity;
            //AirResitance = CurrentGrid.AirResitance;

            AngularAcceleration = torque / Inertia;
            AngularVelocity += AngularAcceleration * Time.fixedDeltaTime;

        }
        //Object type does not matter for grid checks



        if (ObjectType == 1)
        {
            Transformation.BoundObject = new AABB(
        Transformation.Translation - new MyVector3(1, 1, 1),
         Transformation.Translation + new MyVector3(1, 1, 1));
        }
        if (ObjectType == 2)
        {
            Transformation.BoundObject = new BoundingCircle(
Transformation.Translation, 2.0f);
        }
            for (int i = 0; i < PhyCont.myPhysics.Count; i++)
        {
            if (ObjectId != PhyCont.myPhysics[i].ObjectId)
            {
                if (Transformation.BoundObject.Intersects(PhyCont.myPhysics[i].Transformation.BoundObject))
                {
                    MyVector3 Normal = new MyVector3(0, 0, 0);
                    CollisionResoloution(Transformation.BoundObject, PhyCont.myPhysics[i].Transformation.BoundObject, out Normal, out Push);
    
                    //if (GlobalGridList.myPhysics[i].Dynamic == true)
                    //{
                    //    Transformation.Translation += Normall * Penetration;
                    //    Transformation.Forceupdatecollision();
                    //    Velocity = -Velocity / Mass;

                    //}
                    if (Bouncy == true)
                    {
                        Transformation.Translation += (Normal * (Push + 0.0000001639f)) ;
                        Velocity =new MyVector3(-Velocity.x,-Velocity.y,-Velocity.z) *0.7f;
                    }
                    if (Dynamic == true)
                    {

                        Transformation.Translation += (Normal * (Push+0.0000001639f));
     
                        Velocity = new MyVector3(0, 0, 0);
                    }
                }
            }
        }
        //This Physics component can be added to any object and will allow the person to apply a force in a direction and collide with other physic options 
        Transformation.Translation += Velocity * Time.deltaTime;
        // Velocity /= AirResitance;

        Quat q = new Quat();
        float avMag = (AngularVelocity * Time.fixedDeltaTime).Length();
        q.w = Mathf.Cos(avMag / 2);
        q.x = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).x / avMag;
        q.y = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).y / avMag;
        q.z = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).z / avMag;
        Quat TargetOrienation = q * Transformation.GetRotation();
        Transformation.SetRotation(TargetOrienation);
    }
    // Update is called once per frame
    void Update()
    {
        /*
         * 
         * 
         * Inbetween fixed steps find out time between steps which would be 0.02seconds if it is 50 fps then over the course of 0.02s lerp between A to B 
         * */


    }
}