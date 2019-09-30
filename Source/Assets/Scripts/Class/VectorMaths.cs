using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
[System.Serializable]

public class VectorMaths
{
    public static MyVector3 VectorNormalized(MyVector3 LHS)
    {
     if (LHS.Length() == 0)
        {
            return LHS; //Return LH
        }

        MyVector3 RV = new MyVector3(LHS.x, LHS.y, LHS.z); //set RV to be lhs
        RV = RV / RV.Length(); //Rv = rv/rv length
        return RV; //Return RV
    }

    public static float DotProduct(MyVector3 A, MyVector3 B,bool normalise = true)
    {
        float RV; //Create RV
        MyVector3 Vec1 = new MyVector3(A.x, A.y, A.z); //Sets vec 1 to be a
        MyVector3 Vec2 = new MyVector3(B.x, B.y, B.z); //Sets vec 2 to be b
        if (normalise == true)
        {
            Vec1 = VectorNormalized(Vec1); //Normalise Vec1
            Vec2 = VectorNormalized(Vec2); //Normalise Vec2
        }
    
        RV = Vec1.x * Vec2.x + Vec1.y * Vec2.y+ Vec1.z * Vec2.z; //pythagoras theroem
        return RV; //Returns RV
    }

    public static float VectorToRadians(MyVector2 a)
    {
        float RV = 0.0f; //Creates RV
        RV = Mathf.Atan2(a.y, a.x); //Returns the arc-tangent of f - the angle in radians whose tangent is f.
        return RV; // Return RV
    }
    public static MyVector2 RadianToVector(float RV)
    {
        //SIN = OPPOSITE
        //COS = ADJACENT
        MyVector2 A = new MyVector2(Mathf.Cos(RV), Mathf.Sin(RV)); //Sets x to be cos of rv and y to be the sin of rv 
        return A; //Return A
    }
    public static float Deg2Rad(float RV)
    {
        return RV * Mathf.PI / 180; //Times RV by pie / 180
    }
    public static float Rad2Deg(float RV)
    {
        return RV * 180 / Mathf.PI; //Times RV by 180 / pi
    }
    public static MyVector3 EulerAnglesToDirection(MyVector2 EulerAngle)
    {
        //takes a 2D vector converts it to a direction then returns
        //Warning this edits X and Z and does not touch Y
        float Pitch = Deg2Rad(EulerAngle.x); //Set pitch to be euler angle x in radians
        float Yaw = Deg2Rad(EulerAngle.y); //Set yaw to be euler angle y in radians
        MyVector3 RV = new MyVector3
        {
            z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch), //Set z to be cos of yaw * cos of pitch
            y = 0,
            x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw) //Set x to be cos of pitch * sin of yaw
        }; 
        return RV; //Set RV
    }
    public static MyVector3 EulerAnglesToDirection(MyVector3 EulerAngle)
    {
        float Pitch = Deg2Rad(EulerAngle.x); //Set pitch to be euler angle x in radians
        float Yaw = Deg2Rad(EulerAngle.y); //Set yaw to be euler angle y in radians
        MyVector3 RV = new MyVector3
        {

            z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch), //z = cos of yaw * cos of pitch
            y = Mathf.Sin(Pitch), //Y = sin of pitch
            x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw) //x = cos of pitch * sin of yaw
        }; 
        return RV; //Return RV
    }
    public static MyVector3 EulerAnglesToDirectionRoll(MyVector3 EulerAngle)
    {
        MyVector3 RV = new MyVector3(); //Create RV
        RV.x = Mathf.Cos(EulerAngle.z); //x = cos of z 
        RV.y = Mathf.Sin(EulerAngle.z); //y = sin of angle z 
        RV.z = 0; //z = 0 

        return RV; //Return RV
    }
    public static MyVector3 VectorCrossProduct(MyVector3 a, MyVector3 b)
    {
        MyVector3 C = new MyVector3(); //Create C
        C.x = a.y * b.z - a.z * b.y; //x = A y * B z - a z * b y
        C.y = a.z * b.x - a.x * b.z; //y = A z * B x - a x * b z
        C.z = a.x * b.y - a.y * b.x; //z = A x * B y - a y * b x

        return C; //Return C
    }
    public static MyVector3 VecLerp(MyVector3 A, MyVector3 B, float t)
    {
        return A * (1.0f - t) + B * t; // Return A * (1 - t(Time) + b * t(time)
    }
    public static MyVector3 RotateVertexAroundAxis(float Angle, MyVector3 Axis, MyVector3 Vertex)
    {

        Angle = Deg2Rad(Angle); //Convert angle to radians
        MyVector3 RET = (Vertex * Mathf.Cos(Angle)) +
            DotProduct(Vertex, Axis) * Axis * (1 - Mathf.Cos(Angle)) +
            VectorCrossProduct(Axis, Vertex) * Mathf.Sin(Angle);  //The rodriguess rotation formula
        return RET; //Create RET //Return RET
    }


    public static MyVector3 DirectionToEuler(MyVector3 Direction)
    {
      //From UE4 Source Code Formula Adapated for Unity Maths System Unreal 4 (Epic Games, 2019)
        MyVector3 RET = new MyVector3(); //Creates RET and intialises it with a black vector 3
        float X, Y, Z; //Creates X Y Z
        X = Direction.z; //Sets X to Z
        Z = Direction.y; //Sets Z to Y
        Y = Direction.x; //Sets Y to X
        // Find yaw.
        RET.y = Mathf.Atan2(Y, X); //Sets y to be atan of y and x

        // Find pitch.
        RET.x = -Mathf.Atan2(Z, Mathf.Sqrt(X * X + Y * Y)); //Sets x to be -Atan2(Z, sqrt of x* x + y*y

        // Find roll.
        RET.z = 0; //Sets Ret to be 0

        return RET; //Create RET //Returns RET
    }
    public static float SqDistanceFromFloat(MyVector3 A, MyVector3 B, MyVector3 C)
    {
        float RET = 0; //Creates RET and sets it to 1
        MyVector3 AB = B - A; //Creates AB and sets it to be B - A
        MyVector3 BC = C - B; //Creates BC and sets it to be C - B
        MyVector3 AC = C - A; //Creates AC and sets it to be C - A
        MyVector3 BA = A - B; //Creates BA and sets it to be A - B
        if (DotProduct(AC, AB,false) < 0)
        {
            return AC.Length(); //Return length of AC
        }
        if (DotProduct(BA, BC, false) < 0)
        {
            return MyVector3.Length(BC); //Return length of bc
        }
      else
        {
            RET = MyVector3.Length(AC) - DotProduct(AC, AB, false) * DotProduct(AC, AB, false) / MyVector3.Length(AB); //Set RET by getting the length of AC - dotproduct of ac ab * dotproduct ac ab / length ab
            return RET; //Create RET //Return RET
        }
          
    }
}