using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//Collision Box used in collision detection
public class AABB :BoundingObject
{
    public MyVector3 MinExtent; //Minum Bounds of Boix
    public MyVector3 MaxExtent; //Maxium Bounds of box
    public MyVector3 Center; //Center Of BOx
    public MyVector3 Half; //Half Extent Of Box

    //Initialise AABB with values 
    public AABB(MyVector3 Min, MyVector3 Max)
    {

        //Set Extents
        MinExtent = Min; 
        MaxExtent = Max;

        //Create Half Extent
        Half = (MaxExtent - MinExtent) * 0.5f;

        //Create Center
        Center = Half + MinExtent;
    }

    //Default Constructor for AABB
    public AABB()
    {
        //Set Extents
        MinExtent = new MyVector3(-1,-1,-1);
        MaxExtent = new MyVector3(1, 1, 1);

        //Create Half Extent
        Half = (MaxExtent - MinExtent) * 0.5f;

        //Create Center
        Center = Half + MinExtent;
    }

    //Shortcut Values to get extents of box
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

    //Collision Detection
    public static bool Collide(AABB Box1, AABB Box2)
    {

        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        Box2.Center = Box2.Half + Box2.MinExtent;
        //Colliding to the Right

        //Christer Ericson Formula 
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
    public static bool Collide(AABB Box1, BoundingCircle circle)
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
        float Radius = Circle2.Radius;
        Radius *= 0.5f;
        MyVector3 Extent = new MyVector3(Radius, Radius, Radius);
        AABB Box2;
        Box2 = new AABB(Circle2.CenterPoint - Extent, Circle2.CenterPoint + Extent);
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
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
    public static void Resolve(AABB Box1, BoundingCapsule Capsule2, out MyVector3 Normal, out float Penetration)
    {

        Normal = new MyVector3(0, 0, 0);
        Penetration = float.MaxValue;

        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;
        AABB Box2 = new AABB(new MyVector3(Capsule2.A.x , Capsule2.A.y, Capsule2.A.z), new MyVector3(Capsule2.B.x , Capsule2.B.y, Capsule2.B.z));
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;
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