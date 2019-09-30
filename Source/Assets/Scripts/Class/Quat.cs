using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
[System.Serializable]

public class Quat
{
    public float x;  
    public float y;  
    public float z;  
    public float w;  
    public Quat(float Angle, MyVector3 Axis)
    {
        float halfAngle = Angle / 2;  
        w = Mathf.Cos(halfAngle);  
        x = Axis.x * Mathf.Sin(halfAngle);  
        y = Axis.y * Mathf.Sin(halfAngle);  
        z = Axis.z * Mathf.Sin(halfAngle);  
    }
    public Quat(MyVector3 Axis)
    {
        Quat t =  EulerToQuat(Axis);  
        x = t.x;  
        y = t.y;  
        z = t.z;  
        w = t.z;  
    }
    public Quat(float x, float y, float z, float w)
    {
        this.x = x;  
        this.y = y;  
        this.z = z;  
        this.w = w;  
    }
    public Quat(Vector4 Axis)
    {
        x = Axis.x;  
        y = Axis.y;  
        z = Axis.z;  
        w = Axis.w;  
    }
    public static void DebugMyQuat(Quat quat)
    {
        Debug.Log("Quat Debug");  
        Vector4 debug;  
        debug.x = quat.x;  
        debug.y = quat.y;  
        debug.z = quat.z;  
        debug.w = quat.w;  
        Debug.Log(debug);  
    }
    public static bool operator !=(Quat lhs, Vector4 rhs)
    {
        if (lhs.w != rhs.w)
        {
            return true;  
        }
        if (lhs.x != rhs.x)
        {
            return true;  
        }
        if (lhs.y != rhs.y)
        {
            return true;  
        }
        if (lhs.z != rhs.z)
        {
            return true;  
        }
        return false;  
    }
    public static bool operator ==(Quat lhs, Vector4 rhs)
    {
        if (lhs.w == rhs.w)
        {


            if (lhs.x == rhs.x)
            {


                if (lhs.y == rhs.y)
                {


                    if (lhs.z == rhs.z)
                    {
                        return true;  
                    }
                }
            }
        }
        return false;  
    }
    public static bool operator !=(Vector4 lhs, Quat rhs)
    {
        if (lhs.w != rhs.w)
        {
            return true;  
        }
        if (lhs.x != rhs.x)
        {
            return true;  
        }
        if (lhs.y != rhs.y)
        {
            return true;  
        }
        if (lhs.z != rhs.z)
        {
            return true;  
        }
        return false;  
    }
    public static bool operator ==(Vector4 lhs, Quat rhs)
    {
        if (lhs.w == rhs.w)
        {


            if (lhs.x == rhs.x)
            {


                if (lhs.y == rhs.y)
                {


                    if (lhs.z == rhs.z)
                    {
                        return true;  
                    }
                }
            }
        }
        return false;  
    }
    public Quat()
    {
        w = 0;  
        x = 0;  
        y = 0;  
        z = 0;  

    }

    //GET AND SET AXIS

    public MyVector3 GetVector()
    {
        return new MyVector3(x, y, z);  
    }
    public void SetVector(MyVector3 lhs)
    {
        x = lhs.x;  
        y = lhs.y;  
        z = lhs.z;  
    }
    public static Quat operator +(Quat lhs, Quat rhs)
    {
        Quat RET = new Quat
        {
            x = lhs.x + rhs.z,
            y = lhs.y + rhs.z,
            z = lhs.z + rhs.z,
            w = lhs.w + rhs.w
        };  
        return RET; //Create RET  
    }
    public static Quat operator *(Quat lhs, Quat rhs)
    {
        Quat RET = new Quat(0, new MyVector3(0, 0, 0));  
        //RS = (SwRw – Sv· Rv,  Sw * Rv +Rw * Sv + Rv X Sv)
        // s = right
        /// R = left 
        /// 

        RET.x = lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y + lhs.w * rhs.x;  
        RET.y = -lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x + lhs.w * rhs.y;  
        RET.z = lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w + lhs.w * rhs.z;  
        RET.w = -lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z + lhs.w * rhs.w;  


        //RET.w = lhs.w * rhs.w + VectorMaths.DotProduct(new MyVector3(lhs.x, lhs.y, lhs.z), new MyVector3(rhs.x, rhs.y, rhs.z),false);  
        //MyVector3 Vec = rhs.w * lhs.GetVector() + lhs.w * rhs.GetVector() + VectorMaths.VectorCrossProduct(lhs.GetVector(), rhs.GetVector());  
        //RET.SetVector(Vec);  
        return RET; //Create RET  
    }
    public Quat Inverse()
    {
        Quat rv = new Quat();  
        rv.w = w;  
        rv.SetVector(-GetVector());  
        return rv;  
    }
    public Vector4 GetAxisAngle()
    {
        Vector4 rv = new Vector4();  
        float halfAngle = Mathf.Acos(w);  
        rv.w = halfAngle * 2;  
        rv.x = x / Mathf.Sin(halfAngle);  
        rv.y = y / Mathf.Sin(halfAngle);  
        rv.z = z / Mathf.Sin(halfAngle);  
        return rv;  
    }
    public static Quat Slerping(Quat lhs, Quat rhs, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);  
        Quat d = rhs * lhs.Inverse();  
        Vector4 AxisAngle = d.GetAxisAngle();  
        Quat dt = new Quat(AxisAngle.w * t, new MyVector3(AxisAngle.x, AxisAngle.y, AxisAngle.z));  
        return dt * lhs;  
    }
    public static Quat ConvertToRad(Quat lhs)
    {
        Quat RET = new Quat();  
        RET.x = VectorMaths.Deg2Rad(lhs.x);  
        RET.y = VectorMaths.Deg2Rad(lhs.y);  
        RET.z = VectorMaths.Deg2Rad(lhs.z);  
        RET.w = VectorMaths.Deg2Rad(lhs.z);  
        return RET; //Create RET  
    }
    public Quat MatToQuat(Matrix4B4 m)
    {
        Quat RET = new Quat();  
        //Rotating Objects Using Quaternions (Bobic, 1998)
        //Moefied to work with my maths
        float tr, s;  
        float[] q = new float[4];  
        int i, j, k;  
        int[] nxt = new int[3];  
        nxt[0] = 1;  
        nxt[1] = 2;  
        nxt[2] = 0;  
        tr = m.values[0, 0] + m.values[1, 1] + m.values[2, 2];  
        // check the diagonal
        if (tr > 0.0)
        {
            s = Mathf.Sqrt(tr + 1.0f);  
            RET.w = s / 2.0f;  
            s = 0.5f / s;  
            RET.x = (m.values[1, 2] - m.values[2, 1]) * s;  
            RET.y = (m.values[2, 0] - m.values[0, 2]) * s;  
            RET.z = (m.values[0, 1] - m.values[1, 0]) * s;  
            return RET; //Create RET  
        }
        else
        {
            // diagonal is negative
            i = 0;  
            if (m.values[1, 1] > m.values[0, 0]) i = 1;  
            if (m.values[2, 2] > m.values[i, i]) i = 2;  
            j = nxt[i];  
            k = nxt[j];  
            s = Mathf.Sqrt((m.values[i, i] - (m.values[j, j] + m.values[k, k])) + 1.0f);  
            q[i] = s * 0.5f;  
            if (s != 0.0) s = 0.5f / s;  
            q[3] = (m.values[j, k] - m.values[k, j]) * s;  
            q[j] = (m.values[i, j] + m.values[j, i]) * s;  
            q[k] = (m.values[i, k] + m.values[k, i]) * s;  
            RET.x = q[0];  
            RET.y = q[1];  
            RET.z = q[2];  
            RET.w = q[3];  
            return RET; //Create RET  
        }
    }
    public static Quat CreateFromYawPitchRoll(float yaw, float pitch, float roll)
    {
       // Quaternion to Euler angles algorithm - How to convert to 'Y = Up' and between handedness ? (user1423893, 2012)
  
          float num = roll * 0.5f; //set num to be roll halfed
        float num2 =   Mathf.Sin(num); //Set 2 to be sin of num
        float num3 = Mathf.Cos(num); //Set 3 to be cos of num
        float num4 = pitch * 0.5f; //set 4 to be pitch halfed
        float num5 = Mathf.Sin(num4); //Set number 5 to be sin of 4
        float num6 =Mathf.Cos(num4); //Set number 6 to be cos of 4
        float num7 = yaw * 0.5f; //Set number 7 to be yaw halfed
        float num8 =Mathf.Sin(num7); //Set number 8 to be sin of 7
        float num9 =Mathf.Cos(num7); //set num 9 to be cos of 7 
        Quat result = new Quat(); //Create result
        result.x = num9 * num5 * num3 + num8 * num6 * num2; //Set x
        result.y = num8 * num6 * num3 - num9 * num5 * num2; //Set Y 
        result.z = num9 * num6 * num2 - num8 * num5 * num3; //Set z
        result.w = num9 * num6 * num3 + num8 * num5 * num2; //Set w 
        return result; //Return Result
    }


    public static MyVector3 QuatToEuler(Quat q1)
    {
      //  Rotation Matrix To Euler Angles(Mallick, 2016)

        if (float.IsNaN(q1.w))
        {
            q1.w = 0; //set w to be 0
        }

        if (float.IsNaN(q1.x))
        {
            q1.x = 0; //set x to be 0
        }

        if (float.IsNaN(q1.y))
        {
            q1.y = 0; //set y to be 0 
        }
   

        if (float.IsNaN(q1.z))
        {
            q1.z = 0; //set z to be 0
        }
        Matrix4B4 m = Matrix4B4.QuatToMatrix(q1); //Convert quat to matrix
        m = m.Transpose;  
        float sy = Mathf.Sqrt(m.values[0, 0] * m.values[0, 0] + m.values[1, 0] * m.values[1, 0]); //Sqrt of 0,0 * 0,0 + 1,0 * 1,0

        bool singular = sy < 1e-6; //Check if sy is less then 1e-6

        float x, y, z; //Create x y and z
        if (!singular)
        {
            x = Mathf.Atan2(m.values[2, 1],m.values[2, 2]); //Set x to be atan2 of 2,1 and 2,2
            y = Mathf.Atan2(-m.values[2, 0], sy); //Set y to be atan2 of -2,0 and sy
            z = Mathf.Atan2(m.values[1, 0],m.values[0, 0]); //Set z to be atan2 of 1,0 and 0,0
        }
        else
        {
            x = Mathf.Atan2(-m.values[1, 2],m.values[1, 1]); //Set x to be atan2 of 1,2 and 1,1
            y = Mathf.Atan2(-m.values[2, 0], sy); //SEt y to be atan2 of - 2,0 and sy 
            z = 0; //Set z to be 0
        }
        MyVector3 RET = new MyVector3(x,y,z); //Set ret to be x y z
        RET *= Mathf.Rad2Deg; //Convert RET to degres
        return RET; //Create RET 
    }
    public static Quat EulerToQuat(MyVector3 EulerAngle)
    {
        //Rotating Objects Using Quaternions (Bobic, 1998)
        //Moefied to fit my maths
   
        float Roll = EulerAngle.x; //SEt roll to be x
        float Pitch = EulerAngle.y; //Set pitch to be y
        float Yaw = EulerAngle.z; //Yaw is equal to angle z 
        Quat RET = new Quat(); //Create RET
        float CosRoll; //Cos of roll 
        float CosPitch;//Cos of pitch
        float CosYaw; //Cos of yaw
        float SinRoll; // Sin of roll
        float SinPitch; //Sin of pitch 
        float SinYaw; //Sin of yaw
        float CosPitchCosYaw; //Combintation of Cos Pitch and Cos yaw
        float SinPitchSinYaw; //Combintation of Sin Pitch and Sin Yaw

        // calculate trig identities
        CosRoll = Mathf.Cos(Roll / 2); //Set CosRoll to be the sin of Roll / 2
        CosPitch = Mathf.Cos(Pitch / 2); //Set CosPitch to be the sin of Pitch / 2
        CosYaw = Mathf.Cos(Yaw / 2); //Set CosYaw to be the sin of Yaw / 2
        SinRoll = Mathf.Sin(Roll / 2); //Set SinRoll to be the sin of Roll / 2
        SinPitch = Mathf.Sin(Pitch / 2); //Set SinPitch to be the sin of Pitch / 2
        SinYaw = Mathf.Sin(Yaw / 2); //Set SinYaw to be the sin of Yaw / 2
        CosPitchCosYaw = CosPitch * CosYaw; //Set CosPitchCosYaw to be CosPitch * CosYaw
        SinPitchSinYaw = SinPitch * SinYaw; //Set SinPitchSinYaw to be SinPitch * SinYaw
        RET.w = CosRoll * CosPitchCosYaw + SinRoll * SinPitchSinYaw; //Set w 
        RET.x = SinRoll * CosPitchCosYaw - CosRoll * SinPitchSinYaw; //Set x  
        RET.y = CosRoll * SinPitch * CosYaw + SinRoll * CosPitch * SinYaw; //Set y
        RET.z = CosRoll * CosPitch * SinYaw - SinRoll * SinPitch * CosYaw; //Set z 
        return RET; //Return RET 
    }
}