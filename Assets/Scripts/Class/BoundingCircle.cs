using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoundingCircle :BoundingObject
{
    public MyVector3 CentrePoint, Half;
    public float Radius;


    public BoundingCircle(MyVector3 CentrePoint, float Radius)
    {
        this.CentrePoint = CentrePoint;
        this.Radius = Radius;
    }

    public static bool Collide(BoundingCircle circle,BoundingCircle otherCircle)
    {
        MyVector3 VectorToOther = otherCircle.CentrePoint - circle.CentrePoint;
        float CombinedRadiusSq = (otherCircle.Radius + circle.Radius);
        CombinedRadiusSq *= CombinedRadiusSq;
        return MyVector3.Length(VectorToOther) <= CombinedRadiusSq;
    }

    public static bool Collide(BoundingCircle circle,BoundingCapsule OtherCapsule)
    {
        float CombinedRadiusSq = (circle.Radius + OtherCapsule.Radius) * (circle.Radius + OtherCapsule.Radius);
        return VectorMaths.SqDistanceFromFloat(OtherCapsule.A, OtherCapsule.B, circle.CentrePoint) <= CombinedRadiusSq;
    }
    public static bool Collide(BoundingCircle circle, AABB box)
    {
        MyVector3 Mid = (box.MaxExtent - box.MinExtent) * 0.5f;
        MyVector3 VectorToOther = Mid - circle.CentrePoint;

        return VectorToOther.Length() <= circle.Radius;
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