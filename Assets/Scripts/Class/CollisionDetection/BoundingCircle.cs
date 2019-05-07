using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class BoundingCircle : BoundingObject
{
    public MyVector3 CenterPoint;
    public MyVector3 Half;
    public float Radius;
    public BoundingCircle(MyVector3 CenterPoint, float Radius)
    {
        this.CenterPoint = CenterPoint;
        this.Radius = Radius;
    }

    public static bool Collide(BoundingCircle circle, BoundingCircle otherCircle)
    {
        MyVector3 VectorToOther = otherCircle.CenterPoint - circle.CenterPoint;
        float CombinedRadiusSq = (otherCircle.Radius + circle.Radius);
        CombinedRadiusSq *= CombinedRadiusSq;
        return MyVector3.LengthSquared(VectorToOther) <= CombinedRadiusSq;
    }

    public static bool Collide(BoundingCircle circle, BoundingCapsule OtherCapsule)
    {
        float CombinedRadius = circle.Radius + OtherCapsule.Radius;
        float DistanceFromFloat = Mathf.Sqrt(VectorMaths.SqDistanceFromFloat(OtherCapsule.A, OtherCapsule.B, circle.CenterPoint));
        return DistanceFromFloat <= CombinedRadius;
    }
    public static bool Collide(BoundingCircle circle, AABB Box1)
    {
        float Radius = circle.Radius;
        Radius *= 0.5f;
        MyVector3 Extent = new MyVector3(Radius, Radius, Radius);
        AABB Box2;
        Box2 = new AABB(circle.CenterPoint - Extent, circle.CenterPoint + Extent);
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
        Box2.Center = Box2.Half + Box2.MinExtent;


        if (Mathf.Abs(Box1.Center.x - Box2.Center.x) > (Box1.Half.x + Box2.Half.x))
        {

            return false;

        }

        if (Mathf.Abs(Box1.Center.y - Box2.Center.y) > (Box1.Half.y + Box2.Half.y))
        {
            return false;
        }

        if (Mathf.Abs(Box1.Center.z - Box2.Center.z) > (Box1.Half.z + Box2.Half.z))
        {
            return false;
        }
        return true;

    }
    public static void Resolve(BoundingCircle Circle1, BoundingCircle Circle2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;




        MyVector3 VectorToOther = Circle2.CenterPoint - Circle1.CenterPoint;

        Penetration = VectorToOther.Length();

        if (Circle1.CenterPoint.x > Circle2.CenterPoint.x)
        {
            VectorToOther = Circle2.CenterPoint - Circle1.CenterPoint;

            Penetration = VectorToOther.Length();
            Normal = new MyVector3(1, 0, 0);
        }

        if (Circle1.CenterPoint.x < Circle2.CenterPoint.x)
        {
            VectorToOther = Circle1.CenterPoint - Circle2.CenterPoint;
            Penetration = VectorToOther.Length();
            Normal = new MyVector3(-1, 0, 0);
        }


        Penetration *= 0.001f;



        if (Circle1.CenterPoint.y > Circle2.CenterPoint.y)
            Normal = new MyVector3(0, 1, 0);
        if (Circle1.CenterPoint.y < Circle2.CenterPoint.y)
            Normal = new MyVector3(0, -1, 0);


        if (Circle1.CenterPoint.z > Circle2.CenterPoint.z)
            Normal = new MyVector3(0, 0, 1);
        if (Circle1.CenterPoint.z < Circle2.CenterPoint.z)
            Normal = new MyVector3(0, 0, -1);


    }
    public static void Resolve(BoundingCircle Circle1, AABB Box1, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box1.Center = Box1.Half + Box1.MinExtent;
        float Radius = Circle1.Radius;
        Radius *= 0.5f;
        MyVector3 Extent = new MyVector3(Radius, Radius, Radius);
        AABB Box2;
        Box2 = new AABB(Circle1.CenterPoint - Extent, Circle1.CenterPoint + Extent);
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
        Box2.Center = Box2.Half + Box2.MinExtent;


        float CurPenetration = (Box1.Half.x + Box2.Half.x) - Mathf.Abs(Box1.Center.x - Box2.Center.x);
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;
            if (Box1.Center.x > Box2.Center.x)
            {
                Normal = new MyVector3(-1, 0, 0);
            }

            if (Box1.Center.x < Box2.Center.x)
            {
                Normal = new MyVector3(1, 0, 0);
            }
        }
        CurPenetration = (Box1.Half.y + Box2.Half.y) - Mathf.Abs(Box1.Center.y - Box2.Center.y);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.y > Box2.Center.y)
                Normal = new MyVector3(0, -1, 0);
            if (Box1.Center.y < Box2.Center.y)
                Normal = new MyVector3(0, 1, 0);
        }

        CurPenetration = (Box1.Half.z + Box2.Half.z) - Mathf.Abs(Box1.Center.z - Box2.Center.z);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.z > Box2.Center.z)
                Normal = new MyVector3(0, 0, -1);
            if (Box1.Center.z < Box2.Center.z)
                Normal = new MyVector3(0, 0, 1);
        }
    }


    public static void Resolve(BoundingCircle Circle1, BoundingCapsule Capsule2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;

        AABB Box1 = new AABB(new MyVector3(Capsule2.A.x, Capsule2.A.y, Capsule2.A.z), new MyVector3(Capsule2.B.x, Capsule2.B.y, Capsule2.B.z));
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box1.Center = Box1.Half + Box1.MinExtent;


        float Radius = Circle1.Radius;
        Radius *= 0.5f;
        MyVector3 Extent = new MyVector3(Radius, Radius, Radius);
        AABB Box2;
        Box2 = new AABB(Circle1.CenterPoint - Extent, Circle1.CenterPoint + Extent);
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
        Box2.Center = Box2.Half + Box2.MinExtent;


        float CurPenetration = (Box1.Half.x + Box2.Half.x) - Mathf.Abs(Box1.Center.x - Box2.Center.x);
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;
            if (Box1.Center.x > Box2.Center.x)
            {
                Normal = new MyVector3(-1, 0, 0);
            }

            if (Box1.Center.x < Box2.Center.x)
            {
                Normal = new MyVector3(1, 0, 0);
            }
        }
        CurPenetration = (Box1.Half.y + Box2.Half.y) - Mathf.Abs(Box1.Center.y - Box2.Center.y);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.y > Box2.Center.y)
                Normal = new MyVector3(0, -1, 0);
            if (Box1.Center.y < Box2.Center.y)
                Normal = new MyVector3(0, 1, 0);
        }

        CurPenetration = (Box1.Half.z + Box2.Half.z) - Mathf.Abs(Box1.Center.z - Box2.Center.z);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.z > Box2.Center.z)
                Normal = new MyVector3(0, 0, -1);
            if (Box1.Center.z < Box2.Center.z)
                Normal = new MyVector3(0, 0, 1);
        }

        Penetration *= 0.001f;



    }

    public override void CollisionResolution(BoundingObject RHS, out MyVector3 Norm, out float Penetration)
    {
        Norm = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;


        if (RHS is AABB)
        {
            AABB Circle1 = RHS as AABB;
            Resolve(this, Circle1, out Norm, out Penetration);
        }
        if (RHS is BoundingCircle)
        {
            BoundingCircle Circle2 = RHS as BoundingCircle;
            Resolve(this, Circle2, out Norm, out Penetration);
        }
        if (RHS is BoundingCapsule)
        {
            BoundingCapsule Capsule2 = RHS as BoundingCapsule;
            Resolve(this, Capsule2, out Norm, out Penetration);
        }
    }

    public override bool Intersects(BoundingObject RHS)
    {
        if (RHS is AABB)
        {
            AABB Box1 = RHS as AABB;
            return Collide(this, Box1);
        }
        if (RHS is BoundingCircle)
        {
            BoundingCircle Sphere1 = RHS as BoundingCircle;
            return Collide(this, Sphere1);
        }
        if (RHS is BoundingCapsule)
        {
            BoundingCapsule Capsule1 = RHS as BoundingCapsule;
            return Collide(this, Capsule1);

        }
        return false;
    }
    public override void ContactPoint(BoundingObject RHS, out MyVector3 Point)
    {
        Point = new MyVector3(0, 0, 0);
    }
}