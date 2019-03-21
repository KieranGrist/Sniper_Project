using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float X = 0;
    public BoundingObject BoundObject, BoundObject2;
    public float Push;
    public MyVector3 Pos1, Pos2, Normal;
    // Use this for initialization
    void Start()
    {

    }
    void Resoloution(AABB Box1, AABB Box2, out MyVector3 Norms, out float Penetration)
    {
        Norms = new MyVector3(0, 0, 0);
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
                Norms = new MyVector3(-1, 0, 0);
            }

            if (Box1.Center.x < Box2.Center.x)
            { 
            Norms = new MyVector3(1, 0, 0);
            }
        }
        CurPenetration = (Box1.Half.y + Box2.Half.y) - Mathf.Abs(Box1.Center.y - Box2.Center.y);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.y > Box2.Center.y)
            {
                Norms = new MyVector3(0, -1, 0);
            }

            if (Box1.Center.y < Box2.Center.y)
            {
                Norms = new MyVector3(0, 1, 0);
            }
        }

        CurPenetration = (Box1.Half.z + Box2.Half.z) - Mathf.Abs(Box1.Center.z - Box2.Center.z);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.z > Box2.Center.z)
            {
                Norms = new MyVector3(0, 0, -1);
            }

            if (Box1.Center.z < Box2.Center.z)
            {
                Norms = new MyVector3(0, 0, 1);
            }
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
        {
            Norms = new MyVector3(1, 0, 0);
        }

        if (Box1.Center.x < Circle2.CentrePoint.x)
        {
            Norms = new MyVector3(-1, 0, 0);
        }

        if (Box1.Center.y > Circle2.CentrePoint.y)
        {
            Norms = new MyVector3(0, 1, 0);
        }

        if (Box1.Center.y < Circle2.CentrePoint.y)
        {
            Norms = new MyVector3(0, -1, 0);
        }

        if (Box1.Center.z > Circle2.CentrePoint.z)
        {
            Norms = new MyVector3(0, 0, 1);
        }

        if (Box1.Center.z < Circle2.CentrePoint.z)
        {
            Norms = new MyVector3(0, 0, -1);
        }
    }
    void CollisionResoloution(BoundingObject LHS, BoundingObject RHS, out MyVector3 Norm, out float Penetration)
    {

        Norm = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
        if (LHS is AABB && RHS is AABB)
        {

            AABB Box1 = RHS as AABB;
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

    // Update is called once per frame
    void Update()
    {
        BoundObject = new AABB(
       Pos1 - new MyVector3(1, 1, 1),
         Pos1 + new MyVector3(1, 1, 1));

        BoundObject2 = new AABB(
     Pos2 - new MyVector3(1, 1, 1),
       Pos2 + new MyVector3(1, 1, 1));

        if (BoundObject.Intersects(BoundObject2))
        {
             Normal = new MyVector3(0, 0, 0);
            CollisionResoloution(BoundObject, BoundObject2, out Normal, out Push);
            Pos1 += Normal * (Push + 0.5f);
        }
    }
}
