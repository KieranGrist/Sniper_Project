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
            return LHS;
        }

        MyVector3 RV = new MyVector3(LHS.x, LHS.y, LHS.z);
        RV = RV / RV.Length();
        return RV;
    }

    public static float DotProduct(MyVector3 A, MyVector3 B,bool normalise = true)
    {
        float RV;
        MyVector3 Vec1 = new MyVector3(A.x, A.y, A.z);
        MyVector3 Vec2 = new MyVector3(B.x, B.y, A.z);
        if (normalise == true)
        {
            Vec1 = VectorNormalized(Vec1);
            Vec2 = VectorNormalized(Vec2);
        }
    
        RV = Vec1.x * Vec2.x + Vec1.y * Vec2.y+ Vec1.z * Vec2.z;
        return RV;
    }

    public static float VectorToRadians(MyVector2 a)
    {
        float RV = 0.0f;
        RV = Mathf.Atan2(a.y, a.x);  // Summary: Returns the arc-tangent of f - the angle in radians whose tangent is f.
        return RV;
    }
    public static MyVector2 RadianToVector(float RV)
    {
        //SIN = OPPOSITE
        //COS = ADJACENT
        MyVector2 A = new MyVector2(Mathf.Cos(RV), Mathf.Sin(RV));
        return A;
    }
    public static float Deg2Rad(float RV)
    {
        return RV * Mathf.PI / 180;
    }
    public static float Rad2Deg(float RV)
    {
        return RV * 180 / Mathf.PI;
    }
    public static MyVector3 EulerAnglesToDirection(MyVector2 EulerAngle)
    {
        //takes a 2D vector converts it to a direction then returns
        //Warning this edits X and Z and does not touch Y
        float Pitch = Deg2Rad(EulerAngle.x);
        float Yaw = Deg2Rad(EulerAngle.y);
        MyVector3 RV = new MyVector3
        {
            z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch),
            y = 0,
            x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw)
        };
        return RV;
}
        public static MyVector3 EulerAnglesToDirection(MyVector3 EulerAngle)
    {
        float Pitch = Deg2Rad(EulerAngle.x);
        float Yaw = Deg2Rad(EulerAngle.y);

        MyVector3 RV = new MyVector3
        { 
            //x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw),
            //y = Mathf.Sin(Yaw) * Mathf.Cos(Pitch),
            //z = Mathf.Sin(Yaw)

            //x = Mathf.Cos(EulerAngle.x) * Mathf.Sin(EulerAngle.y),
            //y = Mathf.Cos(EulerAngle.y) * Mathf.Cos(EulerAngle.x),
            //z = Mathf.Sin(EulerAngle.x)

            z = Mathf.Cos(Yaw) * Mathf.Cos(Pitch),
            y = Mathf.Sin(Pitch),
            x = Mathf.Cos(Pitch) * Mathf.Sin(Yaw)
        };
        return RV;
    }
    public static MyVector3 EulerAnglesToDirectionRoll(MyVector3 EulerAngle)
    {
        MyVector3 RV = new MyVector3();
        RV.x = Mathf.Cos(EulerAngle.z);
        RV.y = Mathf.Sin(EulerAngle.z);
        RV.z = 0;

        return RV;
    }
    public static MyVector3 VectorCrossProduct(MyVector3 a, MyVector3 b)
    {
        MyVector3 C = new MyVector3();
        C.x = a.y * b.z - a.z * b.y;
        C.y = a.z * b.x - a.x * b.z;
        C.z = a.x * b.y - a.y * b.x;

        return C;
    }
    public static MyVector3 VecLerp(MyVector3 A, MyVector3 B, float t)
    {
        return A * (1.0f - t) + B * t;
    }
    public static MyVector3 RotateVertexAroundAxis(float Angle, MyVector3 Axis, MyVector3 Vertex)
    {
        //The rodriguess rotation formula
        Angle = Deg2Rad(Angle);
        MyVector3 rv = (Vertex * Mathf.Cos(Angle)) +
            DotProduct(Vertex, Axis) * Axis * (1 - Mathf.Cos(Angle)) +
            VectorCrossProduct(Axis, Vertex) * Mathf.Sin(Angle);
        return rv;
    }


    public static MyVector3 DirectionToEuler(MyVector3 Direction)
    {
      //From UE4 Source Code Formula Adapated for Unity Maths System
        MyVector3 RET = new MyVector3();
        float X, Y, Z;
        X = Direction.z;
        Z = Direction.y;
        Y = Direction.x;
        // Find yaw.
        RET.y = Mathf.Atan2(Y, X); //* (180.0f / Mathf.PI);

        // Find pitch.
        RET.x = -Mathf.Atan2(Z, Mathf.Sqrt(X * X + Y * Y));// * (180.0f / Mathf.PI);

        // Find roll.
        RET.z = 0;

        return RET;
    }
    public static float SqDistanceFromFloat(MyVector3 A, MyVector3 B, MyVector3 C)
    {
        float RT = 0;
        MyVector3 AB = B - A;
        MyVector3 BC = C - B;
        MyVector3 AC = C - A;
        MyVector3 BA = A - B;
        /*
        AB = VectorNormalized(AB);
        BC = VectorNormalized(BC);
        AC = VectorNormalized(AC);
        BA = VectorNormalized(BA);
        */
        if (DotProduct(AC, AB,false) < 0)
        {
            return AC.Length();
        }
        if (DotProduct(BA, BC, false) < 0)
        {
            return MyVector3.Length(BC);
        }
      else
        {
            RT = MyVector3.Length(AC) - DotProduct(AC, AB, false) * DotProduct(AC, AB, false) / MyVector3.Length(AB);
            return RT;
        }
          
    }
}