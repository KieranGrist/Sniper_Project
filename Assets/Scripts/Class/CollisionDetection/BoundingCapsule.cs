using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoundingCapsule :BoundingObject
{
    public MyVector3 A;
    public MyVector3 B;
    public float Radius;
    public MyVector3 Center;
    public MyVector3 Half;


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
    public static bool Collide (BoundingCapsule Capsule2, AABB Box1)
    {
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        AABB Box2 = new AABB(Capsule2.A, Capsule2.B);
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

    public static void Resolve(BoundingCapsule Capsule2, AABB Box1, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;

        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        AABB Box2 = new AABB(new MyVector3(Capsule2.A.x,Capsule2.A.y, Capsule2.A.z), new MyVector3(Capsule2.B.x, Capsule2.B.y, Capsule2.B.z));
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

    public static void Resolve(BoundingCapsule Capsule2, BoundingCircle Circle1, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;

        Penetration = VectorMaths.SqDistanceFromFloat(Capsule2.A, Capsule2.B, Circle1.CenterPoint);
        Capsule2.Half = (Capsule2.B - Capsule2.A) * 0.5f;
        Capsule2.Center = Capsule2.Half + Capsule2.A;
         Penetration = Mathf.Sqrt(Penetration);
        if (Circle1.CenterPoint.x > Capsule2.Center.x)
        {
            Normal = new MyVector3(1, 0, 0);
        }

        if (Circle1.CenterPoint.x < Capsule2.Center.x)
        {

            Normal = new MyVector3(-1, 0, 0);
        }


    //     Penetration *= 0.001f;


        if (Circle1.CenterPoint.y > Capsule2.Center.y)
            Normal = new MyVector3(0, 1, 0);
        if (Circle1.CenterPoint.y < Capsule2.Center.y)
            Normal = new MyVector3(0, -1, 0);


        if (Circle1.CenterPoint.z > Capsule2.Center.z)
            Normal = new MyVector3(0, 0, 1);
        if (Circle1.CenterPoint.z < Capsule2.Center.z)
            Normal = new MyVector3(0, 0, -1);
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