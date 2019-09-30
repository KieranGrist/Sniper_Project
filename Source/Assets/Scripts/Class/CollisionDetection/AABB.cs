using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
[System.Serializable]
//Collision Box used in collision detection
public class AABB :BoundingObject
{
    public MyVector3 MinExtent;   //Minum Bounds of Boix
    public MyVector3 MaxExtent;   //Maxium Bounds of box
    public MyVector3 Centre;   //Centre Of BOx
    public MyVector3 Half;   //Half Extent Of Box

    //Initialise AABB with values 
    public AABB(MyVector3 Min, MyVector3 Max)
    {

        //Set Extents
        MinExtent = Min;   
        MaxExtent = Max;  

        //Create Half Extent
        Half = (MaxExtent - MinExtent) * 0.5f;  

        //Create Centre
        Centre = Half + MinExtent;  
    }

    //Default Constructor for AABB
    public AABB()
    {
        //Set Extents
        MinExtent = new MyVector3(-1,-1,-1);  
        MaxExtent = new MyVector3(1, 1, 1);  

        //Create Half Extent
        Half = (MaxExtent - MinExtent) * 0.5f;  

        //Create Centre
        Centre = Half + MinExtent;  
    }

    //Shortcut Values to get extents of box
    public float Top //Return Top of box
    {
        get { return MaxExtent.y;   }
    }
    public float Bottom //Return Bottom of box
    {
        get { return MinExtent.y;   }
    }
    public float Left //Return Left of box
    {
        get { return MinExtent.x;   }
    }
    public float Right //Return Right of box
    {
        get { return MaxExtent.x;   }
    }
    public float Front //Return front of box
    {
        get { return MaxExtent.z;   }
    }
    public float Back //Return back of box
    {
        get { return MinExtent.z;   }
    }

    //Collision Detection
    public static bool Collide(AABB Box1, AABB Box2) //Collision Detection between boxes
    {
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;   //Set half extent by subtracting max extent by min extent and halfing the value
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;   //Set half extent by subtracting max extent by min extent and halfing the value
        //Get Direction Between the two boxes + edges
        Box1.Centre = Box1.Half + Box1.MinExtent;   //Set the centre by  Adding Half to the min extent 
        Box2.Centre = Box2.Half + Box2.MinExtent;  //Set the centre by  Adding Half to the min extent 
        //Colliding to the Right

        //Real-Time Collision Detection (Ericson, 2004)
        if (Mathf.Abs(Box1.Centre.x - Box2.Centre.x) > (Box1.Half.x + Box2.Half.x)) //Gets the abs of Box 1 Centre x - Box 2 Centre x and checks if it is more then Box 1 Half x + Box 2 Half x
        {

            return false;  

        }

        if (Mathf.Abs(Box1.Centre.y - Box2.Centre.y) > (Box1.Half.y + Box2.Half.y)) //Gets the abs of Box 1 Centre y - Box 2 Centre y and checks if it is more then Box 1 Half y + Box 2 Half y
        {
            return false;  
        }

        if (Mathf.Abs(Box1.Centre.z - Box2.Centre.z) > (Box1.Half.z + Box2.Half.z))//Gets the abs of Box 1 Centre z - Box 2 Centre z and checks if it is more then Box 1 Half z + Box 2 Half z
        {
            return false;  
        }
        return true;  
    }

    
     public static bool Collide(AABB Box1, BoundingCircle circle) //Collision detection between box and circle
    {
        float Radius = circle.Radius;   //Set temp radius to be circle radius
        Radius *= 0.5f;   //Half it as this is the distance from centre to edge
        MyVector3 Extent = new MyVector3(Radius, Radius, Radius);   //Create vector 3 extent this is used as extents for a new box
        AABB Box2;   //Create Box 2 which is the circle
        Box2 = new AABB(circle.CentrePoint - Extent, circle.CentrePoint + Extent);   //Set box 2 extents, minum extent is circles postion - extent and maxium is circles postion + extent
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;   //Set half extent by subtracting max extent by min extent and halfing the value
        Box2.Centre = Box2.Half + Box2.MinExtent;    //Set the centre by  Adding Half to the min extent 

        if (Mathf.Abs(Box1.Centre.x - Box2.Centre.x) > (Box1.Half.x + Box2.Half.x)) //Gets the abs of Box 1 Centre x - Box 2 Centre x and checks if it is more then Box 1 Half x + Box 2 Half x
        {

            return false;  

        }

        if (Mathf.Abs(Box1.Centre.y - Box2.Centre.y) > (Box1.Half.y + Box2.Half.y)) //Gets the abs of Box 1 Centre y - Box 2 Centre y and checks if it is more then Box 1 Half y + Box 2 Half y
        {
            return false;  
        }

        if (Mathf.Abs(Box1.Centre.z - Box2.Centre.z) > (Box1.Half.z + Box2.Half.z))//Gets the abs of Box 1 Centre z - Box 2 Centre z and checks if it is more then Box 1 Half z + Box 2 Half z
        {
            return false;  
        }
        return true;  
    }


    public static bool LineIntersection(AABB Box, MyVector3 StartPoint, MyVector3 EndPoint, out MyVector3 intersectionPoint) //Line Intersection test 
    {
        float Lowest = 0.0f;   //Create Lowest to 0
        float Highest = 1.0f;   //Create Highest to 1
        intersectionPoint = MyVector3.zero;   //Set intersection point to 0
        if (!IntersectingAxis(MyVector3.right, Box, StartPoint, EndPoint, ref Lowest, ref Highest)) //Call intersection axis function with right axis being the axis the result of this is returned in a bool which is inverted (true to false, false to true)
            return false;   //Return false
        if (!IntersectingAxis(MyVector3.up, Box, StartPoint, EndPoint, ref Lowest, ref Highest)) //Call intersection axis function with up axis being the axis
            return false;   //Return false
        if (!IntersectingAxis(MyVector3.forward, Box, StartPoint, EndPoint, ref Lowest, ref Highest)) //Call intersection axis function with forward axis being the axis
            return false;   //Return false

        intersectionPoint = VectorMaths.VecLerp(StartPoint, EndPoint, Lowest);   //Set the intersection point by lerping between start point and end point with t being 0
        return true;  
    }
    public static bool IntersectingAxis(MyVector3 Axis, AABB Box, MyVector3 StartPoint, MyVector3 EndPoint, ref float Lowest, ref float Highest) //Intersecting axis
    {
        float Minimum = 0.0f;   //Create Minium to 0 
        float Maxium = 1.0f;   //Create Maxium to 1
        if (Axis == MyVector3.right) //If Axis == right 
        {
            Minimum = (Box.Left - StartPoint.x) / (EndPoint.x - StartPoint.x);    //Minum =Box Left - start point x / end point x - start point x
            Maxium = (Box.Right - StartPoint.x) / (EndPoint.x - StartPoint.x);    //Minum =Box Right - start point x / end point x - start point x
        }
        else if (Axis == MyVector3.up)
        {
            Minimum = (Box.Bottom - StartPoint.y) / (EndPoint.y - StartPoint.y);   //Minum =Box left - start point x / end point x - start point x
            Maxium = (Box.Top - StartPoint.y) / (EndPoint.y - StartPoint.y);   //Minum =Box left - start point x / end point x - start point x
        }
        else if (Axis == MyVector3.forward)
        {
            Minimum = (Box.Back - StartPoint.z) / (EndPoint.z - StartPoint.z);   //Minum =Box left - start point x / end point x - start point x
            Maxium = (Box.Front - StartPoint.z) / (EndPoint.z - StartPoint.z);   //Minum =Box left - start point x / end point x - start point x
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
        Box1.Centre = Box1.Half + Box1.MinExtent;  
        Box2.Centre = Box2.Half + Box2.MinExtent;  


        float CurPenetration = (Box1.Half.x + Box2.Half.x) - Mathf.Abs(Box1.Centre.x - Box2.Centre.x);  
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;  
            if (Box1.Centre.x > Box2.Centre.x)
            {
                Normal = new MyVector3(1, 0, 0);  
            }

            if (Box1.Centre.x < Box2.Centre.x)
            {
                Normal = new MyVector3(-1, 0, 0);  
            }
        }
        CurPenetration = (Box1.Half.y + Box2.Half.y) - Mathf.Abs(Box1.Centre.y - Box2.Centre.y);  

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;  
            if (Box1.Centre.y > Box2.Centre.y)
                Normal = new MyVector3(0, 1, 0);  
            if (Box1.Centre.y < Box2.Centre.y)
                Normal = new MyVector3(0, -1, 0);  
        }

        CurPenetration = (Box1.Half.z + Box2.Half.z) - Mathf.Abs(Box1.Centre.z - Box2.Centre.z);  

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;  
            if (Box1.Centre.z > Box2.Centre.z)
                Normal = new MyVector3(0, 0, 1);  
            if (Box1.Centre.z < Box2.Centre.z)
                Normal = new MyVector3(0, 0, -1);  
        }
    }

    public static void Resolve(AABB Box1, BoundingCircle Circle2, out MyVector3 Normal, out float Penetration)
    {

        Normal = new MyVector3(0, 0, 0);  
        Penetration = float.MaxValue;  
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;  
        Box1.Centre = Box1.Half + Box1.MinExtent;  
        float Radius = Circle2.Radius;  
        Radius *= 0.5f;  
        MyVector3 Extent = new MyVector3(Radius, Radius, Radius);  
        AABB Box2;  
        Box2 = new AABB(Circle2.CentrePoint - Extent, Circle2.CentrePoint + Extent);  
        Box2.Half = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;  
        Box2.Centre = Box2.Half + Box2.MinExtent;  


        float CurPenetration = (Box1.Half.x + Box2.Half.x) - Mathf.Abs(Box1.Centre.x - Box2.Centre.x);  
        if (CurPenetration < Penetration)
        {

            Penetration = CurPenetration;  
            if (Box1.Centre.x > Box2.Centre.x)
            {
                Normal = new MyVector3(1, 0, 0);  
            }

            if (Box1.Centre.x < Box2.Centre.x)
            {
                Normal = new MyVector3(-1, 0, 0);  
            }
        }
        CurPenetration = (Box1.Half.y + Box2.Half.y) - Mathf.Abs(Box1.Centre.y - Box2.Centre.y);  

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;  
            if (Box1.Centre.y > Box2.Centre.y)
                Normal = new MyVector3(0, 1, 0);  
            if (Box1.Centre.y < Box2.Centre.y)
                Normal = new MyVector3(0, -1, 0);  
        }

        CurPenetration = (Box1.Half.z + Box2.Half.z) - Mathf.Abs(Box1.Centre.z - Box2.Centre.z);  

        if (CurPenetration < Penetration)
        {
            Penetration = CurPenetration;  
            if (Box1.Centre.z > Box2.Centre.z)
                Normal = new MyVector3(0, 0, 1);  
            if (Box1.Centre.z < Box2.Centre.z)
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
  
    }

    public static void ContPoin(AABB LHS,AABB RHS, out MyVector3 Point)
    {
        Point = new MyVector3(0, 0, 0);  

        if (LHS.MaxExtent.x > RHS.MinExtent.x)
        {
            Point.x = LHS.MaxExtent.x - RHS.MinExtent.x;  
        }
   if(LHS.MaxExtent.x < RHS.MinExtent.x)
        {
Point.x = RHS.MaxExtent.x - LHS.MinExtent.x;  
        }
    }

    public override void ContactPoint(BoundingObject RHS, out MyVector3 Point)
    {
        Point = new MyVector3(0, 0, 0);  
        if (RHS is AABB)
        {
            AABB Box1 = RHS as AABB;  
            ContPoin(this, Box1,out Point);  
        }
        if (RHS is BoundingCircle)
        {

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

        return false;  
    }

}