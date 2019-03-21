using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector2 {
    public float y;
    public float x;

    public float X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
        }
    }

    public float Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    public MyVector2()
    {
        X = 0;
        Y = 0;
    }
    public MyVector2(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }
    public static MyVector2 AddVectors(MyVector2 LHS, MyVector2 RHS)
    {
        MyVector2 rv = new MyVector2(0, 0);
        rv.X = LHS.X + RHS.X;
        rv.Y = LHS.Y + RHS.Y;
        return rv;
    }

    public static MyVector2 operator +(MyVector2 LHS, MyVector2 RHS)
    {
        return AddVectors(LHS, RHS);
    }


    public static MyVector2 SubtractVectors(MyVector2 LHS, MyVector2 RHS)
    {
        MyVector2 RET = new MyVector2();
        RET.X = LHS.X - RHS.X;
        RET.Y = LHS.Y - RHS.Y;
        return RET;
    }
    public static MyVector2 operator -(MyVector2 LHS, MyVector2 RHS)
    {
        return SubtractVectors(LHS, RHS);
    }


    public static MyVector2 MultiplyVectors(MyVector2 LHS, MyVector2 RHS)
    {
        MyVector2 RET = new MyVector2();
        RET.X = LHS.X * RHS.X;
        RET.Y = LHS.Y * RHS.Y;
        return RET;
    }
    public static MyVector2 operator *(MyVector2 LHS, MyVector2 RHS)
    {
        return MultiplyVectors(LHS, RHS);
    }


    public static MyVector2 DivideVectors(MyVector2 LHS, MyVector2 RHS)
    {
        MyVector2 RET = new MyVector2();
        RET.X = LHS.X / RHS.X;
        RET.Y = LHS.Y / RHS.Y;
        return RET;
    }
    public static MyVector2 operator /(MyVector2 LHS, MyVector2 RHS)
    {
        return DivideVectors(LHS, RHS);
    }


    public static bool operator != (MyVector2 LHS, MyVector2 RHS)
        {
        if (LHS.X != RHS.X||LHS.Y !=RHS.Y)
        {
            return true;
        }
        return false;
        }
    public static bool operator ==(MyVector2 LHS, MyVector2 RHS)
    {
        if (LHS.X == RHS.X && LHS.Y == RHS.Y)
        {
            return true;
        }
        return false;
    }
    public static bool operator < (MyVector2 LHS, MyVector2 RHS)
    {
        if (LHS.X < RHS.X || LHS.Y < RHS.Y)
        {
            return true;
        }
        return false;
    }

    public static bool operator >(MyVector2 LHS, MyVector2 RHS)
    {
        if (LHS.X > RHS.X || LHS.Y > RHS.Y)
        {
            return true;
        }
        return false;
    }

    public static bool operator <=(MyVector2 LHS, MyVector2 RHS)
    {
        if (LHS.X <= RHS.X && LHS.Y <= RHS.Y)
        {
            return true;
        }
        return false;
    }

    public static bool operator >=(MyVector2 LHS, MyVector2 RHS)
    {
        if (LHS.X >= RHS.X || LHS.Y >= RHS.Y)
        {
            return true;
        }
        return false;
    }

    public float Length()
    {
        float rv;
        rv = Mathf.Sqrt(X * X + Y * Y);
        return rv;
    }

    public float VectorLengthSq()
    {
        float rv;
        rv = (X * X + Y * Y);
        return rv;
    }

    public override bool Equals(object obj)
    {
        var vector = obj as MyVector2;
        return vector != null &&
               X == vector.X &&
               Y == vector.Y;
    }

    public override int GetHashCode()
    {
        var hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Y.GetHashCode();
        return hashCode;
    }
}
