using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoundingCapsule :BoundingObject
{
    public MyVector3 A,B, Center, Half;

    public float Radius;
    public BoundingCapsule(MyVector3 A, MyVector3 B, float Radius)
    {
        this.A = A;
        this.B = B;
        this.Radius = Radius;
    }
    public static bool Collide(BoundingCapsule capsule, BoundingCapsule otherCapsule)
    {
        float CombinedRadiusSq = (capsule.Radius + otherCapsule.Radius) * (capsule.Radius + otherCapsule.Radius);
        MyVector3 LengthA = capsule.A - otherCapsule.A;

        float DistanceA = LengthA.Length();
        if (DistanceA <= CombinedRadiusSq)
        {
            return true;
        }
        MyVector3 LengthB= capsule.B - otherCapsule.B;
        float DistanceB = LengthB.Length();
        if (DistanceB <= CombinedRadiusSq)
        {
            return true;
        }

        return false;
    }
    public static bool Collide(BoundingCapsule capsule,BoundingCircle otherCircle)
    {
        float CombinedRadiusSq = (capsule.Radius + otherCircle.Radius) * (capsule.Radius + otherCircle.Radius);
        return VectorMaths.SqDistanceFromFloat(capsule.A, capsule.B, otherCircle.CentrePoint) <= CombinedRadiusSq;
    }
    public static bool Collide (BoundingCapsule capsule, AABB Box1)
    {
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;

        BoundingCircle CircleBox = new BoundingCircle(Box1.Center, 2);
        return BoundingCircle.Collide(CircleBox, capsule);

    }

    public override bool Equals(object obj)
    {
        var capsule = obj as BoundingCapsule;
        return capsule != null &&
               base.Equals(obj) &&
               EqualityComparer<MyVector3>.Default.Equals(A, capsule.A) &&
               EqualityComparer<MyVector3>.Default.Equals(B, capsule.B) &&
               Radius == capsule.Radius;
    }

    public override int GetHashCode()
    {
        var hashCode = -1161030750;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<MyVector3>.Default.GetHashCode(A);
        hashCode = hashCode * -1521134295 + EqualityComparer<MyVector3>.Default.GetHashCode(B);
        hashCode = hashCode * -1521134295 + Radius.GetHashCode();
        return hashCode;
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
}