using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB :BoundingObject
{
    public AABB(MyVector3 Min, MyVector3 Max)
    {
        MinExtent = Min;
        MaxExtent = Max;
    }

    public MyVector3 MinExtent;
    public MyVector3 MaxExtent, Center, Half;
    public float Top
    {
        get { return MaxExtent.y; }
    }
    public float Bottom
    {
        get { return MinExtent.y; }
    }
    public float Left
    {
        get { return MinExtent.x; }
    }
    public float Right
    {
        get { return MaxExtent.x; }
    }
    public float Front
    {
        get { return MaxExtent.z; }
    }
    public float Back
    {
        get { return MinExtent.z; }
    }
    public void BoxDebug()
    {
        //Debug.Log(MinExtent);
        //Debug.Log(MaxExtent);
    }

    public static bool Collide(AABB Box1, AABB Box2)
    {

        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        Box2.Center = Box2.Half + Box2.MinExtent;
        //Colliding to the Right


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
    public static bool Collide(AABB Box1, BoundingCapsule Capsule2)
    {
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        Capsule2.A.x -= Capsule2.Radius;
        Capsule2.A.z -= Capsule2.Radius;
        Capsule2.B.x += Capsule2.Radius;
        Capsule2.B.z += Capsule2.Radius;
        Capsule2.Half = (Capsule2.B - Capsule2.A) * 0.5f;
        Capsule2.Center = Capsule2.Half + Capsule2.A;

        if (Mathf.Abs(Box1.Center.x - Capsule2.Center.x) > (Box1.Half.x + Capsule2.Half.x))
        {

            return false;

        }



        if (Mathf.Abs(Box1.Center.y - Capsule2.Center.y) > (Box1.Half.y + Capsule2.Half.y))
        {
            return false;
        }

        if (Mathf.Abs(Box1.Center.z - Capsule2.Center.z) > (Box1.Half.z + Capsule2.Half.z))
        {
            return false;
        }
        return true;
    }
    public static bool Collide(AABB box, BoundingCircle circle)
    {
        box.Half = (box.MaxExtent - box.MinExtent) * 0.5f;
        box.Center = box.Half + box.MinExtent;
        MyVector3 VectorToOther = box.Center - circle.CenterPoint;
        return VectorToOther.Length() <= circle.Radius;
    }

        public static bool LineIntersection(AABB Box, MyVector3 StartPoint, MyVector3 EndPoint, out MyVector3 intersectionPoint)
    {
        float Lowest = 0.0f;
        float Highest = 1.0f;
        intersectionPoint = MyVector3.zero;
        if (!IntersectingAxis(MyVector3.right, Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        if (!IntersectingAxis(MyVector3.up, Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        if (!IntersectingAxis(MyVector3.forward, Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;

        intersectionPoint = VectorMaths.VecLerp(StartPoint, EndPoint, Lowest);
        return true;
    }
    public static bool IntersectingAxis(MyVector3 Axis, AABB Box, MyVector3 StartPoint, MyVector3 EndPoint, ref float Lowest, ref float Highest)
    {
        float Minimum = 0.0f, Maxium = 1.0f;
        if (Axis == MyVector3.right)
        {
            Minimum = (Box.Left - StartPoint.x) / (EndPoint.x - StartPoint.x);
            Maxium = (Box.Right - StartPoint.x) / (EndPoint.x - StartPoint.x);
        }
        else if (Axis == MyVector3.up)
        {
            Minimum = (Box.Bottom - StartPoint.y) / (EndPoint.y - StartPoint.y);
            Maxium = (Box.Top - StartPoint.y) / (EndPoint.y - StartPoint.y);
        }
        else if (Axis == MyVector3.forward)
        {
            Minimum = (Box.Back - StartPoint.z) / (EndPoint.z - StartPoint.z);
            Maxium = (Box.Front - StartPoint.z) / (EndPoint.z - StartPoint.z);
        }
        if (Maxium < Minimum)
        {
            float temp = Maxium;
            Maxium = Minimum;
            Minimum = temp;
        }
        if (Maxium < Lowest)
            return false;
        if (Minimum > Highest)
            return false;
        Lowest = Mathf.Max(Minimum, Lowest);
        Highest = Mathf.Min(Maxium, Highest);
        if (Lowest > Highest)
            return false;
        return true;

    }

    public static void Resolve(AABB Box1, AABB Box2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;

        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;


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

    public static void Resolve(AABB Box1, BoundingCircle Circle2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box1.Center = Box1.Half + Box1.MinExtent;


        MyVector3 MinExtent, MaxExtent;
        MinExtent = Circle2.CenterPoint - Circle2.Radius;
        MaxExtent = Circle2.CenterPoint + Circle2.Radius;
        Circle2.Half = (MaxExtent - MinExtent) * 0.5f;

        Circle2.CenterPoint = Circle2.Half + MinExtent;
        float CurPenetration = (Box1.Half.x + Circle2.Half.x) - Mathf.Abs(Box1.Center.x - Circle2.CenterPoint.x);
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;
            if (Box1.Center.x > Circle2.CenterPoint.x)
            {
                Normal = new MyVector3(-1, 0, 0);
            }

            if (Box1.Center.x < Circle2.CenterPoint.x)
            {
                Normal = new MyVector3(1, 0, 0);
            }
        }
        CurPenetration = (Box1.Half.y + Circle2.Half.y) - Mathf.Abs(Box1.Center.y - Circle2.CenterPoint.y);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.y > Circle2.CenterPoint.y)
                Normal = new MyVector3(0, -1, 0);
            if (Box1.Center.y < Circle2.CenterPoint.y)
                Normal = new MyVector3(0, 1, 0);
        }

        CurPenetration = (Box1.Half.z + Circle2.Half.z) - Mathf.Abs(Box1.Center.z - Circle2.CenterPoint.z);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.z > Circle2.CenterPoint.z)
                Normal = new MyVector3(0, 0, -1);
            if (Box1.Center.z < Circle2.CenterPoint.z)
                Normal = new MyVector3(0, 0, 1);
        }

    }
    public static void Resolve(AABB Box1, BoundingCapsule Capsule2, out MyVector3 Normal, out float Penetration)
    {
        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;

        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Capsule2.Half = (Capsule2.B - Capsule2.A) * 0.5f;


        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        Capsule2.Center = Capsule2.Half + Capsule2.A;


        float CurPenetration = (Box1.Half.x + Capsule2.Half.x) - Mathf.Abs(Box1.Center.x - Capsule2.Center.x);
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;
            if (Box1.Center.x > Capsule2.Center.x)
            {
                Normal = new MyVector3(-1, 0, 0);
            }

            if (Box1.Center.x < Capsule2.Center.x)
            {
                Normal = new MyVector3(1, 0, 0);
            }
        }
        CurPenetration = (Box1.Half.y + Capsule2.Half.y) - Mathf.Abs(Box1.Center.y - Capsule2.Center.y);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.y > Capsule2.Center.y)
                Normal = new MyVector3(0, -1, 0);
            if (Box1.Center.y < Capsule2.Center.y)
                Normal = new MyVector3(0, 1, 0);
        }

        CurPenetration = (Box1.Half.z + Capsule2.Half.z) - Mathf.Abs(Box1.Center.z - Capsule2.Center.z);

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;
            if (Box1.Center.z > Capsule2.Center.z)
                Normal = new MyVector3(0, 0, -1);
            if (Box1.Center.z < Capsule2.Center.z)
                Normal = new MyVector3(0, 0, 1);
        }
    }



    public override void CollisionResolution(BoundingObject RHS,out MyVector3 Norm, out float Penetration)
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