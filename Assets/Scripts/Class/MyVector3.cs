using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MyVector3
{
    public float x;
    public float y;
    public float z;




    public static MyVector3 right = new MyVector3(1, 0, 0);
    public static MyVector3 left = new MyVector3(-1, 0, 0);
    public static MyVector3 up = new MyVector3(0, 1, 0);
    public static MyVector3 down = new MyVector3(0, -1, 0);
    public static MyVector3 forward = new MyVector3(0, 0, 1);
    public static MyVector3 backWard = new MyVector3(0, 0, -1);
    public static MyVector3 zero = new MyVector3(0, 0, 0);


    Vector3 Test;
    public void MyVec3Debug()
    {
        Debug.Log("X = " + x);
        Debug.Log("Y = " + y);
        Debug.Log("Z = " + z);

    }
    public MyVector3()
    {
        x = 0;
        x = 0;
        x = 0;
    }
    public MyVector3(Vector3 LHS)
    {
        x = LHS.x;
        y = LHS.y;
        z = LHS.z;

    }
        public MyVector3(float x, float y,float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public MyVector3(float x, float y)
    {
        this.x = x;
        this.y = y;
        this.z = 0;
    }

    public static MyVector3 operator -(MyVector3 RHS)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = 0 - RHS.z;
        rv.y = 0 - RHS.y;
        rv.y = 0 - RHS.z;
        return rv;
    }

    public static MyVector3 AddVectors(MyVector3 LHS, float RHS)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = LHS.x + RHS;
        rv.y = LHS.y + RHS;
        rv.z = LHS.z + RHS;
        return rv;
    }
    public static MyVector3 AddVectors(MyVector3 LHS, MyVector3 RHS)
    {
        MyVector3 rv = new MyVector3(0, 0,0);
        rv.x = LHS.x + RHS.x;
        rv.y = LHS.y + RHS.y;
        rv.z = LHS.z + RHS.z;
        return rv;
    }

    public static MyVector3 operator +(MyVector3 LHS, MyVector3 RHS)
    {
        return AddVectors(LHS, RHS);
    }
    public static MyVector3 operator +(MyVector3 LHS, float RHS)
    {
        return AddVectors(LHS, RHS);
    }

    public static MyVector3 SubtractVectors(MyVector3 LHS, float RHS)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = LHS.x - RHS;
        rv.y = LHS.y - RHS;
        rv.z = LHS.z - RHS;
        return rv;
    }
    public static MyVector3 SubtractVectors(MyVector3 LHS, MyVector3 RHS)
    {
        MyVector3 RET = new MyVector3();
        RET.x = LHS.x - RHS.x;
        RET.y = LHS.y - RHS.y;
        RET.z = LHS.z - RHS.z;
        return RET;
    }
    public static MyVector3 operator -(MyVector3 LHS, MyVector3 RHS)
    {
        return SubtractVectors(LHS, RHS);
    }
    public static MyVector3 operator -(MyVector3 LHS, float RHS)
    {
        return SubtractVectors(LHS, RHS);
    }

    public static MyVector3 MultiplyVectors(float LHS, MyVector3 RHS)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = LHS * RHS.x;
        rv.y = LHS * RHS.y;
        rv.z = LHS * RHS.z;
        return rv;
    }
    public static MyVector3 MultiplyVectors(MyVector3 LHS, float RHS)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = LHS.x * RHS;
        rv.y = LHS.y * RHS;
        rv.z = LHS.z * RHS;
        return rv;
    }
    public static MyVector3 MultiplyVectors(MyVector3 LHS, MyVector3 RHS)
    {
        MyVector3 RET = new MyVector3();
        RET.x = LHS.x * RHS.x;
        RET.y = LHS.y * RHS.y;
        RET.z = LHS.z * RHS.z;
        return RET;
    }
    public static MyVector3 operator *(MyVector3 LHS, MyVector3 RHS)
    {
        return MultiplyVectors(LHS, RHS);
    }
    public static MyVector3 operator *(MyVector3 LHS, float RHS)
    {
        return MultiplyVectors(LHS, RHS);
    }
    public static MyVector3 operator *(float LHS, MyVector3 RHS)
    {
        return MultiplyVectors(LHS, RHS);
    }
    public static MyVector3 DivideVectors(MyVector3 LHS, float RHS)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = LHS.x / RHS;
        rv.y = LHS.y / RHS;
        rv.z = LHS.z / RHS;
        return rv;
    }
    public static MyVector3 DivideVectors(MyVector3 LHS, MyVector3 RHS)
    {
        MyVector3 RET = new MyVector3();
        RET.x = LHS.x / RHS.x;
        RET.y = LHS.y / RHS.y;
        RET.z = LHS.y / RHS.z; 
        return RET;
    }
    public static MyVector3 operator /(MyVector3 LHS, MyVector3 RHS)
    {
        return DivideVectors(LHS, RHS);
    }
    public static MyVector3 operator /(MyVector3 LHS, float RHS)
    {
        return DivideVectors(LHS, RHS);
    }

    public static bool operator != (MyVector3 LHS, MyVector3 RHS)
    {
        if (LHS.x != RHS.x || LHS.y != RHS.y|| LHS.z != RHS.z)
        {
            return true;
        }
        return false;
    }
    public static bool operator ==(MyVector3 LHS, MyVector3 RHS)
    {
        if (LHS.x == RHS.x && LHS.y == RHS.y && LHS.z == RHS.z)
        {
            return true;
        }
        return false;
    }
    public static bool operator <(MyVector3 LHS, MyVector3 RHS)
    {
        if (LHS.x < RHS.x || LHS.y < RHS.y||LHS.z < RHS.z)
        {
            return true;
        }
        return false;
    }

    public static bool operator >(MyVector3 LHS, MyVector3 RHS)
    {
        if (LHS.x > RHS.x || LHS.y > RHS.y|| LHS.z > RHS.z)
        {
            return true;
        }
        return false;
    }

    public static bool operator <=(MyVector3 LHS, MyVector3 RHS)
    {
        if (LHS.x <= RHS.x && LHS.y <= RHS.y && LHS.z <= RHS.z)
        {
            return true;
        }
        return false;
    }

    public static bool operator >=(MyVector3 LHS, MyVector3 RHS)
    {
        if (LHS.x >= RHS.x  && LHS.y >= RHS.y && LHS.z >= RHS.z)
        {
            return true;
        }
        return false;
    }

    public float Length()
    {
        float rv;
        rv = Mathf.Sqrt(x * x + y * y + z*z);
        return rv;
    }

    public float LengthSquared()
    {
        float rv;
        rv = (x * x + y * y +z*z);
        return rv;
    }

    public static float Length(MyVector3 LHS)
    {
        float rv;
        rv = Mathf.Sqrt(LHS.x * LHS.x + LHS.y * LHS.y + LHS.z * LHS.z);
        return rv;
    }

    public static float LengthSquared(MyVector3 LHS)
    {
        float rv;
        rv = (LHS.x * LHS.x + LHS.y * LHS.y + LHS.z * LHS.z);
        return rv;
    }


    public override bool Equals(object obj)
    {
        var vector = obj as MyVector3;
        return vector != null &&
               x == vector.x &&
               y == vector.y;
    }

    public override int GetHashCode()
    {
        var hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        return hashCode;
    }
}
