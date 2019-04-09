using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoundingCapsule :BoundingObject
{
    public MyVector3 A;
    public MyVector3 B;
    public float Radius;
    public MyVector3 Center, Half;
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
        return VectorMaths.SqDistanceFromFloat(capsule.A, capsule.B, otherCircle.CenterPoint) <= CombinedRadiusSq;
    }
    public static bool Collide (BoundingCapsule capsule, AABB Box1)
    {
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        capsule.A.x -= capsule.Radius;
        capsule.A.z -= capsule.Radius;
        capsule.B.x += capsule.Radius;
        capsule.B.z += capsule.Radius;
        capsule.Half = (capsule.B - capsule.A) * 0.5f;
        capsule.Center = capsule.Half + capsule.A;

        if (Mathf.Abs(Box1.Center.x - capsule.Center.x) > (Box1.Half.x + capsule.Half.x))
        {

            return false;

        }



        if (Mathf.Abs(Box1.Center.y - capsule.Center.y) > (Box1.Half.y + capsule.Half.y))
        {
            return false;
        }

        if (Mathf.Abs(Box1.Center.z - capsule.Center.z) > (Box1.Half.z + capsule.Half.z))
        {
            return false;
        }
        return true;

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

    public static void Resolve(BoundingCapsule Capsule1, BoundingCapsule Capsule2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
    }

    public static void Resolve(BoundingCapsule Capsule1, AABB Box2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;


        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
        Capsule1.Half = (Capsule1.B - Capsule1.A) * 0.5f;


        //Get Direction Between the two boxes + edges
        Box2.Center = Box2.Half + Box2.MinExtent;
        Capsule1.Center = Capsule1.Half + Capsule1.A;


        float CurPenetration = (Box2.Half.x + Capsule1.Half.x) - Mathf.Abs(Box2.Center.x - Capsule1.Center.x);
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;
            if (Box2.Center.x > Capsule1.Center.x)
            {
                Normal = new MyVector3(-1, 0, 0);
            }

            if (Box2.Center.x < Capsule1.Center.x)
            {
                Normal = new MyVector3(1, 0, 0);
            }
        }
        CurPenetration = (Box2.Half.y + Capsule1.Half.y) - Mathf.Abs(Box2.Center.y - Capsule1.Center.y);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box2.Center.y > Capsule1.Center.y)
                Normal = new MyVector3(0, -1, 0);
            if (Box2.Center.y < Capsule1.Center.y)
                Normal = new MyVector3(0, 1, 0);
        }

        CurPenetration = (Box2.Half.z + Capsule1.Half.z) - Mathf.Abs(Box2.Center.z - Capsule1.Center.z);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box2.Center.z > Capsule1.Center.z)
                Normal = new MyVector3(0, 0, -1);
            if (Box2.Center.z < Capsule1.Center.z)
                Normal = new MyVector3(0, 0, 1);
        }
    }

    public static void Resolve(BoundingCapsule Capsule1, BoundingCircle Circle2, out MyVector3 Normal, out float Penetration)
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
            AABB Box2 = RHS as AABB;
            Resolve(this, Box2, out Norm, out Penetration);
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
}