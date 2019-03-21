using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyPhysics : MonoBehaviour
{
    public MyVector3 Gravity, Force, Acceleration, Velocity, WindVelocity;
    //1 Box, 2 Sphere, 3 Cylander;
    public int ObjectType = 1;
    public float Mass = 1, AirResitance = 1, Push, ObjectId;
    public bool Dynamic, Bouncy = false; //Object that has motion and is effected by physics, if false object is static at all times, Bouncy will effect what it does when colliding, Static does not move the object at all
    public myTransformation Transformation;
    public GridHandle GlobalGridList;
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
        MyVector3 Mid = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
            MyVector3 VectorToOther = Mid - Circle2.CentrePoint;

        Penetration = VectorToOther.Length() - Circle2.Radius;


        if (Box1.Center.x > Circle2.CentrePoint.x)
            Norms = new MyVector3(1, 0, 0);
        if (Box1.Center.x < Circle2.CentrePoint.x)
            Norms = new MyVector3(-1, 0, 0);


        if (Box1.Center.y > Circle2.CentrePoint.y)
            Norms = new MyVector3(0, 1, 0);
        if (Box1.Center.y < Circle2.CentrePoint.y)
            Norms = new MyVector3(0, -1, 0);


        if (Box1.Center.z > Circle2.CentrePoint.z)
            Norms = new MyVector3(0, 0, 1);
        if (Box1.Center.z < Circle2.CentrePoint.z)
            Norms = new MyVector3(0, 0, -1);


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

    }


    void FixedUpdate()
    {
        Transformation = GetComponent<myTransformation>();

            Gravity = GlobalGridList.Gravity;
 

        if (Dynamic == true|| Bouncy == true)
        {
            Transformation = GetComponent<myTransformation>();
            Acceleration = Force / Mass;
            Velocity += ((Acceleration + Gravity) / AirResitance) * Time.deltaTime;

            Force = new MyVector3(0, 0, 0);
            //WindVelocity = CurrentGrid.WindVelocity;
            //AirResitance = CurrentGrid.AirResitance;
        }
        //Object type does not matter for grid checks


        if (GlobalGridList != null)
        {
            if (GlobalGridList.Grids != null)
            {
                if (GlobalGridList.Grids.Count != 0)
                {
                    for (int i = 0; i < GlobalGridList.Grids.Count; i++)
                    {
                        //                GlobalGridList.Grids[i].PhysicsObjectsInGrid.Clear();
                        if (Transformation.BoundObject.Intersects(GlobalGridList.Grids[i].Transformation.BoundObject))
                        {
                            CurrentGrid = GlobalGridList.Grids[i];
                        }
                    }
                }
            }
        }
        if (ObjectType == 1)
        {
            Transformation.BoundObject = new AABB(
        Transformation.Translation - new MyVector3(1, 1, 1),
         Transformation.Translation + new MyVector3(1, 1, 1));
        }
        for (int i = 0; i < GlobalGridList.myPhysics.Count; i++)
        {
            if (ObjectId != GlobalGridList.myPhysics[i].ObjectId)
            {
                if (Transformation.BoundObject.Intersects(GlobalGridList.myPhysics[i].Transformation.BoundObject))
                {
                    MyVector3 Normal = new MyVector3(0, 0, 0);
                    CollisionResoloution(Transformation.BoundObject, GlobalGridList.myPhysics[i].Transformation.BoundObject, out Normal, out Push);
    
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