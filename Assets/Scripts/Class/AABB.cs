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
 
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;// + Box2.MinExtent;
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
    public static bool Collide(AABB Box1, BoundingCapsule capsule)
    {
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;// + Box1.MinExtent;
        //Get Direction Between the two boxes + edges
        Box1.Center = Box1.Half + Box1.MinExtent;

        BoundingCircle CircleBox = new BoundingCircle(Box1.Center,2);
      return BoundingCircle.Collide(CircleBox, capsule);
      
    }
    public static bool Collide(AABB box, BoundingCircle circle)
    {
        MyVector3 Mid = (box.MaxExtent - box.MinExtent) * 0.5f;
        MyVector3 VectorToOther = Mid - circle.CentrePoint;

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