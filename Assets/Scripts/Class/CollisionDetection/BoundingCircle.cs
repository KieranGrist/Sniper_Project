using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoundingCircle :BoundingObject
{
    public MyVector3 CenterPoint;
    public MyVector3 Half;
    public float Radius;
    public BoundingCircle(MyVector3 CenterPoint, float Radius)
    {
        this.CenterPoint = CenterPoint;
        this.Radius = Radius;
    }

    public static bool Collide(BoundingCircle circle,BoundingCircle otherCircle)
    {
        MyVector3 VectorToOther = otherCircle.CenterPoint - circle.CenterPoint;
        float CombinedRadiusSq = (otherCircle.Radius + circle.Radius);
        CombinedRadiusSq *= CombinedRadiusSq;
        return MyVector3.Length(VectorToOther) <= CombinedRadiusSq;
    }

    public static bool Collide(BoundingCircle circle,BoundingCapsule OtherCapsule)
    {
        float CombinedRadiusSq = (circle.Radius + OtherCapsule.Radius) * (circle.Radius + OtherCapsule.Radius);
        return VectorMaths.SqDistanceFromFloat(OtherCapsule.A, OtherCapsule.B, circle.CenterPoint) <= CombinedRadiusSq;
    }
    public static bool Collide(BoundingCircle circle, AABB box)
    {
        box.Half = (box.MaxExtent - box.MinExtent) * 0.5f;
        box.Center = box.Half + box.MinExtent;
        MyVector3 VectorToOther = box.Center - circle.CenterPoint;
        return VectorToOther.Length() <= circle.Radius;
    }
    public static void Resolve (BoundingCircle Circle1, BoundingCircle Circle2,out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
    }
    public static void Resolve(BoundingCircle Circle1, AABB Box1, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;

        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box1.Center = Box1.Half + Box1.MinExtent;



        MyVector3 MinExtent, MaxExtent;
        MinExtent = Circle1.CenterPoint - Circle1.Radius;
        MaxExtent = Circle1.CenterPoint + Circle1.Radius;
        Circle1.Half = (MaxExtent - MinExtent) * 0.5f;
        Circle1.CenterPoint = Circle1.Half + MinExtent;

        float CurPenetration = (Box1.Half.x + Circle1.Half.x) - Mathf.Abs(Box1.Center.x - Circle1.CenterPoint.x);
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;
            if (Box1.Center.x > Circle1.CenterPoint.x)
            {
                Normal = new MyVector3(-1, 0, 0);
            }

            if (Box1.Center.x < Circle1.CenterPoint.x)
            {
                Normal = new MyVector3(1, 0, 0);
            }
        }
        CurPenetration = (Box1.Half.y + Circle1.Half.y) - Mathf.Abs(Box1.Center.y - Circle1.CenterPoint.y);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.y > Circle1.CenterPoint.y)
                Normal = new MyVector3(0, -1, 0);
            if (Box1.Center.y < Circle1.CenterPoint.y)
                Normal = new MyVector3(0, 1, 0);
        }

        CurPenetration = (Box1.Half.z + Circle1.Half.z) - Mathf.Abs(Box1.Center.z - Circle1.CenterPoint.z);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.z > Circle1.CenterPoint.z)
                Normal = new MyVector3(0, 0, -1);
            if (Box1.Center.z < Circle1.CenterPoint.z)
                Normal = new MyVector3(0, 0, 1);
        }
    }


    public static void Resolve(BoundingCircle Circle1, BoundingCapsule Capsule2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
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
            return Collide(this,Box1);
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
}