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
        Quat Ret = new Quat
        {
            x = lhs.x + rhs.z,
            y = lhs.y + rhs.z,
            z = lhs.z + rhs.z,
            w = lhs.w + rhs.w
        };
        return Ret;
    }
    public static Quat operator *(Quat lhs, Quat rhs)
    {
        Quat ret = new Quat(0, new MyVector3(0, 0, 0));
        //RS = (SwRw – Sv· Rv,  Sw * Rv +Rw * Sv + Rv X Sv)
        // s = right
        /// R = left 
        /// 

        ret.x = lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y + lhs.w * rhs.x;
        ret.y = -lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x + lhs.w * rhs.y;
        ret.z = lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w + lhs.w * rhs.z;
        ret.w = -lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z + lhs.w * rhs.w;


        //ret.w = lhs.w * rhs.w + VectorMaths.DotProduct(new MyVector3(lhs.x, lhs.y, lhs.z), new MyVector3(rhs.x, rhs.y, rhs.z),false);
        //MyVector3 Vec = rhs.w * lhs.GetVector() + lhs.w * rhs.GetVector() + VectorMaths.VectorCrossProduct(lhs.GetVector(), rhs.GetVector());
        //ret.SetVector(Vec);
        return ret;
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
        return RET;
    }
    public Quat MatToQuat(Matrix4B4 m)
    {
        Quat ret = new Quat();
        //Code retrieved from https://www.gamasutra.com/view/feature/131686/rotating_objects_using_quaternions.php
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
            ret.w = s / 2.0f;
            s = 0.5f / s;
            ret.x = (m.values[1, 2] - m.values[2, 1]) * s;
            ret.y = (m.values[2, 0] - m.values[0, 2]) * s;
            ret.z = (m.values[0, 1] - m.values[1, 0]) * s;
            return ret;
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
            ret.x = q[0];
            ret.y = q[1];
            ret.z = q[2];
            ret.w = q[3];
            return ret;
        }
    }
    public static Quat CreateFromYawPitchRoll(float yaw, float pitch, float roll)
    {
        //https://stackoverflow.com/questions/11492299/quaternion-to-euler-angles-algorithm-how-to-convert-to-y-up-and-between-ha/11505219
        float num = roll * 0.5f;
        float num2 =   Mathf.Sin(num);
        float num3 = Mathf.Cos(num);
        float num4 = pitch * 0.5f;
        float num5 = Mathf.Sin(num4);
        float num6 =Mathf.Cos(num4);
        float num7 = yaw * 0.5f;
        float num8 =Mathf.Sin(num7);
        float num9 =Mathf.Cos(num7);
        Quat result = new Quat();
        result.x = num9 * num5 * num3 + num8 * num6 * num2;
        result.y = num8 * num6 * num3 - num9 * num5 * num2;
        result.z = num9 * num6 * num2 - num8 * num5 * num3;
        result.w = num9 * num6 * num3 + num8 * num5 * num2;
        return result;
    }


    public static MyVector3 QuatToEuler(Quat q1)
    {
        //https://www.learnopencv.com/rotation-matrix-to-euler-angles/

        if (float.IsNaN(q1.w))
        {
            q1.w = 0;
        }

        if (float.IsNaN(q1.x))
        {
            q1.x = 0;
        }

        if (float.IsNaN(q1.y))
        {
            q1.y = 0;
        }
   

        if (float.IsNaN(q1.z))
        {
            q1.z = 0;
        }
        Matrix4B4 m = Matrix4B4.QuatToMatrix(q1);
        m = m.Transpose;
        float sy = Mathf.Sqrt(m.values[0, 0] * m.values[0, 0] + m.values[1, 0] * m.values[1, 0]);

        bool singular = sy < 1e-6; // If

        float x, y, z;
        if (!singular)
        {
            x = Mathf.Atan2(m.values[2, 1],m.values[2, 2]);
            y = Mathf.Atan2(-m.values[2, 0], sy);
            z = Mathf.Atan2(m.values[1, 0],m.values[0, 0]);
        }
        else
        {
            x = Mathf.Atan2(-m.values[1, 2],m.values[1, 1]);
            y = Mathf.Atan2(-m.values[2, 0], sy);
            z = 0;
        }
        MyVector3 RET = new MyVector3(x,y,z);
        RET *= Mathf.Rad2Deg;
        return RET;
    }
    public static Quat EulerToQuat(MyVector3 EulerAngle)
    {
        //    Sample Retrevied from https://www.gamasutra.com/view/feature/131686/rotating_objects_using_quaternions.php?page=2
        //Moefied to fit my maths
        /*
                EulerAngle.x = PITCH;
                EulerAngle.y = YAW;
                EulerAngle.z = ROLL;
                  EulerAngle.x = Roll;
                EulerAngle.y = Pitch;
                EulerAngle.z = Yaw;
        yaw = z;
                 */

        float Roll = EulerAngle.x;
        float Pitch = EulerAngle.y;
        float Yaw = EulerAngle.z;
        Quat ret = new Quat();
        float cr, cp, cy, sr, sp, sy, cpcy, spsy;
        // calculate trig identities
        cr = Mathf.Cos(Roll / 2);
        cp = Mathf.Cos(Pitch / 2);
        cy = Mathf.Cos(Yaw / 2);
        sr = Mathf.Sin(Roll / 2);
        sp = Mathf.Sin(Pitch / 2);
        sy = Mathf.Sin(Yaw / 2);
         cpcy = cp * cy;
        spsy = sp * sy;
        ret.w = cr * cpcy + sr * spsy;
        ret.x = sr * cpcy - cr * spsy;
        ret.y = cr * sp * cy + sr * cp * sy;
        ret.z = cr * cp * sy - sr * sp * cy;
        return ret;
    }

    public override bool Equals(object obj)
    {
        var quat = obj as Quat;
        return quat != null &&
               w == quat.w &&
               x == quat.x &&
               y == quat.y &&
               z == quat.z;
    }

    public override int GetHashCode()
    {
        var hashCode = 1815319614;
        hashCode = hashCode * -1521134295 + w.GetHashCode();
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        hashCode = hashCode * -1521134295 + z.GetHashCode();
        return hashCode;
    }
}